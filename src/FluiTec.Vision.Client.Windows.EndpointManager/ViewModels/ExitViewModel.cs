using FluiTec.Vision.Client.Windows.EndpointManager.Resources.Localization;
using FluiTec.Vision.Client.Windows.EndpointManager.Resources.Localization.Views.Exit;
using GalaSoft.MvvmLight;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels
{
	/// <summary>	A ViewModel for the exit. </summary>
	public class ExitViewModel : ViewModelBase
	{
		/// <summary>	Default constructor. </summary>
		public ExitViewModel()
		{
			Title = Global.ApplicationName;
			Header = ExitView.Header;
			Message = ExitView.Message;
		}

		/// <summary>	Gets or sets the title. </summary>
		/// <value>	The title. </value>
		public string Title { get; set; }

		/// <summary>	Gets or sets the header. </summary>
		/// <value>	The header. </value>
		public string Header { get; set; }

		/// <summary>	Gets or sets the message. </summary>
		/// <value>	The message. </value>
		public string Message { get; set; }
	}
}