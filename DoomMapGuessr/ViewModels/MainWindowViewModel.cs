using System.Globalization;

using Avalonia.Controls;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DoomMapGuessr.Strings;
using DoomMapGuessr.Views;


namespace DoomMapGuessr.ViewModels
{

	public partial class MainWindowViewModel : ViewModelBase
	{

		[ObservableProperty]
		private UserControl currentPage;

		[ObservableProperty]
		private bool isSidebarOpen = true;

		public MainWindowViewModel()
		{

			var dataCtx = new HomePageViewModel();
			ViewModelArchive.Set(dataCtx);

			CurrentPage = new HomePage
			{
				DataContext = dataCtx
			};

		}

		[RelayCommand]
		private void ChangeCulture(string culture) =>
			Resources.Culture = culture switch
			{

				"null" or "default" => CultureInfo.CurrentCulture,
				_                   => new(culture)

			};

		[RelayCommand]
		private void Navigate(string page)
		{

			switch (page)
			{

				case "Home":
					if (!ViewModelArchive.TryGet<HomePageViewModel>(out var homePageViewModel))
					{
						homePageViewModel = new();
						ViewModelArchive.Set(homePageViewModel);
					}

					CurrentPage = new HomePage
					{
						DataContext = homePageViewModel
					};

					break;

				case "Settings":
					if (!ViewModelArchive.TryGet<SettingsPageViewModel>(out var settingsPageViewModel))
					{
						settingsPageViewModel = new();
						ViewModelArchive.Set(settingsPageViewModel);
					}

					CurrentPage = new SettingsPage
					{
						DataContext = settingsPageViewModel
					};

					break;

				case "CloseSidebarPane":
					IsSidebarOpen = false;

					break;

				case "OpenSidebarPane":
					IsSidebarOpen = true;

					break;

				case "ToggleSidebarPane":
					IsSidebarOpen = !IsSidebarOpen;

					break;

			}

		}

	}

}
