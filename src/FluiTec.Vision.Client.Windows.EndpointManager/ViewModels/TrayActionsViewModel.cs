extern alias myservicelocation;
using System.Collections.ObjectModel;
using System.Windows;
using FluiTec.Vision.Client.Windows.EndpointManager.Resources.Localization;
using FluiTec.Vision.Client.Windows.EndpointManager.WebServer;
using GalaSoft.MvvmLight;
using myservicelocation::Microsoft.Practices.ServiceLocation;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels
{
	/// <summary>	A ViewModel for the tray actions. </summary>
	public class TrayActionsViewModel : ViewModelBase
	{
		/// <summary>	Manager for web server. </summary>
		private readonly IWebServerManager _webServerManager;

		/// <summary>	Default constructor. </summary>
		public TrayActionsViewModel()
		{
			_webServerManager = ServiceLocator.Current.GetInstance<IWebServerManager>();

			Header = "FluiTech:Vision";
			Actions = new ObservableCollection<ITrayItem>(new ITrayItem[]
			{
				new TraySeparator(),
				new WebserverTrayActionViewModel(
					Tray.ServerStartLabel,
					uriImageSource:@"/FluiTec.Vision.Client.Windows.EndpointManager;component/Resources/Images/play.png",
					action: StartServer,
					enabledFunction: manager => !manager.IsRunning
				), 
				new WebserverTrayActionViewModel(
					Tray.ServerStopLabel,
					uriImageSource:@"/FluiTec.Vision.Client.Windows.EndpointManager;component/Resources/Images/pause.png",
					action: StopServer,
					enabledFunction: manager => manager.IsRunning
				),		
				new TraySeparator(),
				new TrayActionViewModel(
					Tray.QuitLabel, 
					uriImageSource: @"/FluiTec.Vision.Client.Windows.EndpointManager;component/Resources/Images/quit.png", 
					action: () => Application.Current.Shutdown())
			});
		}

		/// <summary>	Gets or sets the header. </summary>
		/// <value>	The header. </value>
		public string Header { get; set; }

		/// <summary>	Gets or sets the actions. </summary>
		/// <value>	The actions. </value>
		public ObservableCollection<ITrayItem> Actions { get; set; }

		private void StartServer()
		{
			_webServerManager.Start();
		}

		private void StopServer()
		{
			_webServerManager.Stop();
		}
	}
}