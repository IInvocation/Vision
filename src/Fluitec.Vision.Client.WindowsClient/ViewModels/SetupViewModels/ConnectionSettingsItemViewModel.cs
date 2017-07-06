using Fluitec.Vision.Client.WindowsClient.Configuration;
using GalaSoft.MvvmLight.CommandWpf;

namespace Fluitec.Vision.Client.WindowsClient.ViewModels.SetupViewModels
{
	/// <summary>	A ViewModel for the connection settings item. </summary>
	public class ConnectionSettingsItemViewModel : SettingsItemViewModel
	{
		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		public ConnectionSettingsItemViewModel(ClientConfiguration configuration)
		{
			DisplayName = "Server-Verbindung";
			ConfigureCommand = new RelayCommand(() => { });
		}

		/// <summary>	Validates this object. </summary>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		protected override bool Validate()
		{
			return false;
		}
	}
}