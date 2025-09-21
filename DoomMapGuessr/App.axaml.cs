using System.Globalization;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;

using System.Linq;

using Avalonia.Markup.Xaml;

using DoomMapGuessr.Helpers;
using DoomMapGuessr.Services;
using DoomMapGuessr.ViewModels;
using DoomMapGuessr.Views;

using IniParser.Model;


namespace DoomMapGuessr
{

	public partial class App : Application
	{

		public IniData Settings { get; private set; } = null!;

		public override void Initialize() => AvaloniaXamlLoader.Load(this);

		public override void OnFrameworkInitializationCompleted()
		{

			Settings = ApplicationSettings.Shared.GetResourceFile("{18D527F8-9848-4301-8049-DE4C679B7AEB}");

			if (!Settings.Sections.ContainsSection("Language"))
				Settings.Sections.Add(new("Language"));

			if (!Settings["Language"].ContainsKey("Culture"))
				Settings["Language"]["Culture"] = CultureInfo.CurrentCulture.Name;

			Settings.Save("{18D527F8-9848-4301-8049-DE4C679B7AEB}");

			Strings.Resources.Culture = new(Settings["Language"]["Culture"]);

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

		private static void DisableAvaloniaDataAnnotationValidation()
		{

			// Get an array of plugins to remove
			var dataValidationPluginsToRemove = BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

			// remove each entry found
			foreach (var plugin in dataValidationPluginsToRemove)
				BindingPlugins.DataValidators.Remove(plugin);

		}

	}

}
