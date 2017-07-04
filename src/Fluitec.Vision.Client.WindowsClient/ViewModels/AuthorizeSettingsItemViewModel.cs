using GalaSoft.MvvmLight.CommandWpf;

namespace Fluitec.Vision.Client.WindowsClient.ViewModels
{
	/// <summary>	A ViewModel for the authorize settings item. </summary>
	public class AuthorizeSettingsItemViewModel : SettingsItemViewModel
	{
		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		public AuthorizeSettingsItemViewModel(ClientConfiguration configuration)
		{
			DisplayName = "Server-Autorisierung";
			StatusOk = true;
			ConfigureCommand = new RelayCommand(() => {});
		}
	}
}