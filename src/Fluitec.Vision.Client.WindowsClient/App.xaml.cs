using System.Windows;
using Fluitec.Vision.Client.WindowsClient.ViewModels;
using Fluitec.Vision.Client.WindowsClient.Views;

namespace Fluitec.Vision.Client.WindowsClient
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			RunServer();
		}

		internal void RunServer(bool firstRun = true)
		{
			if (!ValidateConfig(out ClientConfiguration config))
			{
				if (firstRun)
					Setup(config);
				else
					Current.Shutdown();
			}
			else
			{
				// start the integrated webserver
			}
		}

		private static bool ValidateConfig(out ClientConfiguration configuration)
		{
			configuration = null;
			return false;
		}

		private void Setup(ClientConfiguration configuration)
		{
			var view = new SetupView {DataContext = new SetupViewModel(configuration)};
			view.Closed += (sender, args) => { RunServer(firstRun: false); };
			view.Show();
		}
	}
}
