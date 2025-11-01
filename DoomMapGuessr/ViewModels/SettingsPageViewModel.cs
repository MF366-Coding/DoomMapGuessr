using System;
using System.Linq;

using Avalonia.Controls;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DoomMapGuessr.Services;


namespace DoomMapGuessr.ViewModels
{

	public partial class SettingsPageViewModel : ViewModelBase
	{

		[ObservableProperty]
		private int currentIndex = Array.IndexOf(App.allowedCultures, ApplicationSettings.Shared.Settings["Language"]["Culture"]) +
								   1;

		[ObservableProperty]
		private string[] languageComboBoxItems =
		[
			Strings.Resources.SameAsSystemOption, "English (USA)", "Português (Brasil)", "Português (Portugal)"
		];

		[RelayCommand]
		private void SaveSettings() => ApplicationSettings.Shared.Save("config");

		public void RunLanguageChangeProtocol(SelectionChangedEventArgs args) =>
			ApplicationSettings.Shared.Settings["Language"]["Culture"] = CurrentIndex == 0 // same as system
																			 ? (App.allowedCultures.Contains(
																					App.systemCulture,
																					StringComparer.OrdinalIgnoreCase // same as system is allowed
																				)
																					? App.systemCulture       // same as system
																					: App.allowedCultures[0]) // en-US
																			 : App.allowedCultures
																				 [CurrentIndex - 1];

	}

}
