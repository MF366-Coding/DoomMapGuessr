using System;
using System.Globalization;
using System.Linq;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

using DoomMapGuessr.Services;
using DoomMapGuessr.ViewModels;
using DoomMapGuessr.Views;


namespace DoomMapGuessr
{

	public class App : Application
	{

		public static readonly string[] allowedCultures = [ "en-US", "pt-br", "pt-PT", "sk-sk" ];
		public static readonly string systemCulture = CultureInfo.CurrentCulture.Name;

		private static void DisableAvaloniaDataAnnotationValidation()
		{

			// Get an array of plugins to remove
			var dataValidationPluginsToRemove = BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

			// remove each entry found
			foreach (var plugin in dataValidationPluginsToRemove)
				BindingPlugins.DataValidators.Remove(plugin);

		}

		private static void PrepareAppSettings()
		{

			// IMPORTANT: APPLICATION SETTINGS SETUP
			if (!ApplicationSettings.Shared.Settings.Sections.ContainsSection("Language"))
				ApplicationSettings.Shared.Settings.Sections.Add(new("Language"));

			if (!ApplicationSettings.Shared.Settings.Sections.ContainsSection("GUI"))
				ApplicationSettings.Shared.Settings.Sections.Add(new("GUI"));

			if (!ApplicationSettings.Shared.Settings["Language"].ContainsKey("Culture"))
			{

				ApplicationSettings.Shared.Settings["Language"]["Culture"] = allowedCultures.Contains(systemCulture, StringComparer.OrdinalIgnoreCase)
																				 ? systemCulture
																				 : allowedCultures[0];

			}

			if (!ApplicationSettings.Shared.Settings["GUI"].ContainsKey("FollowSystem"))
				ApplicationSettings.Shared.Settings["GUI"]["FollowSystem"] = "1";

			if (!ApplicationSettings.Shared.Settings["GUI"].ContainsKey("DarkMode"))
				ApplicationSettings.Shared.Settings["GUI"]["DarkMode"] = "1";

			ApplicationSettings.Shared.Save("config");

		}

		public override void Initialize() => AvaloniaXamlLoader.Load(this);

		public override void OnFrameworkInitializationCompleted()
		{

			PrepareAppSettings();

			RequestedThemeVariant = ApplicationSettings.Shared.Settings["GUI"]["FollowSystem"] == "1"
										? ThemeVariant.Default
										: (ApplicationSettings.Shared.Settings["GUI"]["DarkMode"] == "1"
											   ? ThemeVariant.Dark
											   : ThemeVariant.Light);
			Strings.Resources.Culture = new(ApplicationSettings.Shared.Settings["Language"]["Culture"]);

			if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
			{

				// ReSharper disable CommentTypo
				// Avoid duplicate validations from both Avalonia and the CommunityToolkit.
				// More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
				// ReSharper restore CommentTypo
				DisableAvaloniaDataAnnotationValidation();

				desktop.MainWindow = new MainWindow
				{
					DataContext = new MainWindowViewModel()
				};

			}

			base.OnFrameworkInitializationCompleted();

		}

	}

}
