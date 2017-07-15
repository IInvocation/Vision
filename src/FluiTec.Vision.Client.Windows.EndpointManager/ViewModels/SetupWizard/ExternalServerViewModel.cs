using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Navigation;
using FluiTec.Vision.Client.Windows.EndpointManager.Resources.Localization.Views.Setup.Wizard;
using FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.Wizard;
using FluiTec.Vision.Client.Windows.EndpointManager.Views.SetupWizard;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.SetupWizard
{
	/// <summary>	A ViewModel for the external server. </summary>
	public class ExternalServerViewModel : WizardPageViewModel
	{
		/// <summary>	The server modes. </summary>
		private ObservableCollection<string> _serverModes;

		/// <summary>	The selected server mode. </summary>
		private string _selectedServerMode;

		/// <summary>	Name of the manual host. </summary>
		private string _manualHostName;

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
			var result = new[]
			{
				ServerModes.Contains(SelectedServerMode),
				IsManualContentValid() || IsUpnpContentValid()
			}.All(b => b);

			return result;
		}

		/// <summary>	Query if this object is manual content valid. </summary>
		/// <returns>	True if the manual content is valid, false if not. </returns>
		private bool IsManualContentValid()
		{
			var result = ManualContentVisible
			             && !string.IsNullOrWhiteSpace(ManualHostName)
			             && Uri.TryCreate(ManualHostName, UriKind.Absolute, out Uri uriResult)
			             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
			return result;
		}

		/// <summary>	Query if this object is upnp content valid. </summary>
		/// <returns>	True if the upnp content is valid, false if not. </returns>
		private bool IsUpnpContentValid()
		{
			return UpnpContentVisible && false;
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
				Validate();
				OnPropertyChanged();
				OnPropertyChanged(nameof(UpnpContentVisible));
				OnPropertyChanged(nameof(ManualContentVisible));
			}
		}

		/// <summary>	Gets or sets the name of the manual host. </summary>
		/// <value>	The name of the manual host. </value>
		public string ManualHostName
		{
			get => _manualHostName;
			set
			{
				_manualHostName = value;
				Validate();
				OnPropertyChanged();
			}
		}
		#endregion
	}
}
