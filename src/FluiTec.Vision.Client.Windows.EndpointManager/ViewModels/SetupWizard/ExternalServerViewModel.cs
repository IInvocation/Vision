using System.Collections.ObjectModel;
using System.Windows.Navigation;
using FluiTec.Vision.Client.Windows.EndpointManager.Resources.Localization.Views.Setup.Wizard;
using FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.Wizard;
using FluiTec.Vision.Client.Windows.EndpointManager.Views.SetupWizard;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.SetupWizard
{
	/// <summary>	A ViewModel for the external server. </summary>
	public class ExternalServerViewModel : WizardPageViewModel
	{
		private ObservableCollection<string> _serverModes;
		private string _selectedServerMode;

		#region Fields

		#endregion

		#region Constructors

		/// <summary>	Default constructor. </summary>
		public ExternalServerViewModel()
		{
			Title = ExternalServer.Header;
			Description = ExternalServer.Description;
			Content = new ExternalServerPage();

			ServerModes = new ObservableCollection<string>(new []
			{
				ExternalServer.ModeLabelUpnp,
				ExternalServer.ModeLabelManual
			});
			SelectedServerMode = ServerModes[index: 0];
		}

		#endregion

		#region Methods

		/// <summary>	Validates the model. </summary>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		protected override bool ValidateModel()
		{
			return base.ValidateModel();
		}

		#endregion

		#region Properties

		/// <summary>	Gets a value indicating whether the upnp content is visible. </summary>
		/// <value>	True if upnp content visible, false if not. </value>
		public bool UpnpContentVisible => SelectedServerMode == ExternalServer.ModeLabelUpnp;

		/// <summary>	Gets a value indicating whether the manual content is visible. </summary>
		/// <value>	True if manual content visible, false if not. </value>
		public bool ManualContentVisible => SelectedServerMode == ExternalServer.ModeLabelManual;

		/// <summary>	Gets or sets the server modes. </summary>
		/// <value>	The server modes. </value>
		public ObservableCollection<string> ServerModes
		{
			get => _serverModes;
			set { _serverModes = value; OnPropertyChanged();}
		}

		/// <summary>	Gets or sets the selected server mode. </summary>
		/// <value>	The selected server mode. </value>
		public string SelectedServerMode
		{
			get => _selectedServerMode;
			set
			{
				_selectedServerMode = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(UpnpContentVisible));
				OnPropertyChanged(nameof(ManualContentVisible));
			}
		}

		#endregion
	}
}
