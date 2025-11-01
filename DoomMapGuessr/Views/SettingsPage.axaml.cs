using Avalonia.Controls;

using DoomMapGuessr.ViewModels;


namespace DoomMapGuessr.Views
{

	public partial class SettingsPage : UserControl
	{

		public SettingsPage() { InitializeComponent(); }

		private void SelectingItemsControl_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
		{

			// the MVVM gods are gonna fucking end me for this sin :sob::pray:
			if (DataContext is not SettingsPageViewModel viewModel)
				return; // just discard this bullshit

			viewModel.RunLanguageChangeProtocol(e);

		}

	}

}

