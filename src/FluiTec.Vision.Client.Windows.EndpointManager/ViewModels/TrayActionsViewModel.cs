using System.Collections.ObjectModel;
using System.Windows;
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
			Actions = new ObservableCollection<TrayActionViewModel>(new []
			{
				new TrayActionViewModel(
					text: "Quit", 
					uriImageSource: @"/FluiTec.Vision.Client.Windows.EndpointManager;component/Resources/Images/quit.png", 
					action: () => Application.Current.Shutdown())
			});
		}

		/// <summary>	Gets or sets the header. </summary>
		/// <value>	The header. </value>
		public string Header { get; set; }

		/// <summary>	Gets or sets the actions. </summary>
		/// <value>	The actions. </value>
		public ObservableCollection<TrayActionViewModel> Actions { get; set; }
	}
}