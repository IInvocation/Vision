using System.Windows;
using FluiTec.Vision.Client.Windows.EndpointManager.Views;

namespace FluiTec.Vision.Client.Windows.EndpointManager
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			FindResource(resourceKey: "VisionNotifyIcon");

			var view = new SetupView();
			view.Show();

			// check for valid configuration
			// available: start webserver and keep it running
			// not available: run configuration-ui
		}
	}
}
