using System.Windows.Input;
using GalaSoft.MvvmLight;

namespace Fluitec.Vision.Client.WindowsClient.ViewModels.SetupViewModels
{
	/// <summary>	A ViewModel for the settings item. </summary>
	public abstract class SettingsItemViewModel : ViewModelBase
	{
		private string _displayName;
		private ICommand _configureCommand;

		/// <summary>	Gets or sets the name of the display. </summary>
		/// <value>	The name of the display. </value>
		public string DisplayName
		{
			get => _displayName;
			set => Set(ref _displayName, value);
		}

		/// <summary>	Gets or sets a value indicating whether the status ok. </summary>
		/// <value>	True if status ok, false if not. </value>
		public bool StatusOk => Validate();

		/// <summary>	Gets or sets the configure command. </summary>
		/// <value>	The configure command. </value>
		public ICommand ConfigureCommand
		{
			get => _configureCommand;
			set => Set(ref _configureCommand, value);
		}

		/// <summary>	Gets a value indicating whether this object is validated. </summary>
		/// <value>	True if this object is validated, false if not. </value>
		protected abstract bool Validate();
	}
}