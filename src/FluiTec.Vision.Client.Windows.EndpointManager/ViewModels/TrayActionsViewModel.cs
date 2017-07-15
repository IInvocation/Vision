using System.Collections.ObjectModel;
using System.Windows;
using FluiTec.Vision.Client.Windows.EndpointManager.Resources.Localization;
using GalaSoft.MvvmLight;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels
{
	/// <summary>	A ViewModel for the tray actions. </summary>
	public class TrayActionsViewModel : ViewModelBase
	{
		/// <summary>	Default constructor. </summary>
		public TrayActionsViewModel()
		{
			Header = "FluiTech:Vision";
			Actions = new ObservableCollection<ITrayItem>(new ITrayItem[]
			{
				new TraySeparator(),
				new TrayActionViewModel(
					Tray.ServerStartLabel,
					uriImageSource:@"/FluiTec.Vision.Client.Windows.EndpointManager;component/Resources/Images/play.png",
					action: () => {}
				),
				new TrayActionViewModel(
					Tray.ServerStopLabel,
					uriImageSource:@"/FluiTec.Vision.Client.Windows.EndpointManager;component/Resources/Images/pause.png",
					action: () => {}
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
	}
}