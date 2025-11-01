using System;
using System.Globalization;
using System.Linq;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;

using DoomMapGuessr.Services;
using DoomMapGuessr.ViewModels;
using DoomMapGuessr.Views;


namespace DoomMapGuessr
{

	public class App : Application
	{

		public static readonly string[] allowedCultures = [ "en-US", "pt-br", "pt-PT" ];
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

			if (!ApplicationSettings.Shared.Settings["Language"].ContainsKey("Culture"))
			{

				ApplicationSettings.Shared.Settings["Language"]["Culture"] = allowedCultures.Contains(systemCulture, StringComparer.OrdinalIgnoreCase)
																				 ? systemCulture
																				 : allowedCultures[0];

			}

			ApplicationSettings.Shared.Save("config");

		}

		public override void Initialize() => AvaloniaXamlLoader.Load(this);

		public override void OnFrameworkInitializationCompleted()
		{

			PrepareAppSettings();
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
