using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace Fluitec.Vision.Client.WindowsClient.ViewModels
{
	/// <summary>	A ViewModel for the setup. </summary>
	public class SetupViewModel : ViewModelBase
	{
		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		public SetupViewModel(ClientConfiguration configuration)
		{
			Settings = new ObservableCollection<SettingsItemViewModel>(new SettingsItemViewModel[]
			{
				new AuthorizeSettingsItemViewModel(configuration),
				new ConnectionSettingsItemViewModel(configuration)
			});
		}

		/// <summary>	Gets or sets options for controlling the operation. </summary>
		/// <value>	The settings. </value>
		public ObservableCollection<SettingsItemViewModel> Settings { get; set; }
	}
}