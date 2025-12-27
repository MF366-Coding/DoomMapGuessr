using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;


namespace DoomMapGuessr
{

	public partial class NoInternetDialog : Window
	{

		public NoInternetDialog()
		{

			InitializeComponent();

		}

		private void OnRetryButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs? e)
		{

			if (App.HasInternetConnection())
				Close(true);

		}

	}

}