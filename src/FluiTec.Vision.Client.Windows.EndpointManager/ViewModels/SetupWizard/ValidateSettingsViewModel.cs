extern alias myservicelocation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FluiTec.Vision.Client.Windows.EndpointManager.Resources.Localization.Views.Setup.Wizard;
using FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.SetupWizard.Actions;
using FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.Wizard;
using FluiTec.Vision.Client.Windows.EndpointManager.Views.SetupWizard;
using FluiTec.Vision.Client.Windows.EndpointManager.WebServer;
using GalaSoft.MvvmLight.CommandWpf;
using myservicelocation::Microsoft.Practices.ServiceLocation;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.SetupWizard
{
    /// <summary>	A ViewModel for validating previous settings. </summary>
    public class ValidateSettingsViewModel : WizardPageViewModel
	{
		#region Fields

		/// <summary>	The external settings. </summary>
		private readonly ExternalServerViewModel _externalSettings;

		/// <summary>	The internal settings. </summary>
		private readonly InternalServerViewModel _internalSettings;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="externalSettings">	The external settings. </param>
		/// <param name="internalSettings">	The internal settings. </param>
		public ValidateSettingsViewModel(
			ExternalServerViewModel externalSettings, 
			InternalServerViewModel internalSettings)
		{
			_externalSettings = externalSettings;
			_internalSettings = internalSettings;

			Title = ValidateSettings.Title;
			Description = ValidateSettings.Description;
			Content = new ValidateSettingsPage();

			ExecuteValidationCommand = new RelayCommand(RunValidationActions);
			Actions = new ObservableCollection<IValidationAction>();
			BuildActions();
		}

		#endregion

		#region Properties

		/// <summary>	Gets or sets the actions. </summary>
		/// <value>	The actions. </value>
		public ObservableCollection<IValidationAction> Actions { get; set; }

		#endregion

		#region Commands

		/// <summary>	Gets or sets the execute validation command. </summary>
		/// <value>	The execute validation command. </value>
		public ICommand ExecuteValidationCommand { get; set; }

		#endregion

		#region Methods

		/// <summary>	Builds the actions. </summary>
		private void BuildActions()
		{
			var manager = ServiceLocator.Current.GetInstance<ISettingsManager>();
			var oldSettings = manager.CurrentSettings;
			var newSettings = new ServerSettings
			{
				ExternalHostname = _externalSettings.ManualHostName,
				Port = _internalSettings.LocalPort,
				UseUpnp = _externalSettings.UpnpContentVisible,
				UpnpPort = 0,
				HttpName = $"http://+:{_internalSettings.LocalPort}"
			};

			var webServerManager = ServiceLocator.Current.GetInstance<IWebServerManager>();
			Actions.Add(GetStopServerAction(webServerManager));
			if (oldSettings.Port > 0)
				Actions.Add(GetRemoveHttpRegistration(oldSettings));
			if (oldSettings.UpnpPort > 0)
				Actions.Add(GetRemoveUpnpRegistration(oldSettings));
			Actions.Add(AddHttpRegistration(newSettings));
			if (newSettings.UseUpnp)
				Actions.Add(AddUpnpRegistration(newSettings));
			Actions.Add(GetCheckConnectivity());
			Actions.Add(GetStartServer());
		}

		/// <summary>	Gets stop server action. </summary>
		/// <param name="webServerManager">	Manager for web server. </param>
		/// <returns>	The stop server action. </returns>
		private static IValidationAction GetStopServerAction(IWebServerManager webServerManager)
		{
			return new ValidationAction
			{
				DisplayName = ValidateSettings.StopServerLabel,
				ActionToExecute = settings =>
				{
					webServerManager.Stop();
					return webServerManager.IsRunning ?
						new ValidationResult
						{
							Success = false,
							ErrorMessage = ValidateSettings.StopServerErrorMessage
						} : new ValidationResult { Success = true };
				}
			};
		}

		/// <summary>	Gets remove HTTP registration. </summary>
		/// <param name="oldSettings">	The old settings. </param>
		/// <returns>	The remove HTTP registration. </returns>
		private IValidationAction GetRemoveHttpRegistration(ServerSettings oldSettings)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>	Gets remove upnp registration. </summary>
		/// <param name="oldSettings">	The old settings. </param>
		/// <returns>	The remove upnp registration. </returns>
		private IValidationAction GetRemoveUpnpRegistration(ServerSettings oldSettings)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>	Adds a HTTP registration. </summary>
		/// <param name="newSettings">	The new settings. </param>
		/// <returns>	An IValidationAction. </returns>
		private IValidationAction AddHttpRegistration(ServerSettings newSettings)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>	Adds an upnp registration. </summary>
		/// <param name="newSettings">	The new settings. </param>
		/// <returns>	An IValidationAction. </returns>
		private IValidationAction AddUpnpRegistration(ServerSettings newSettings)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>	Check connectivity. </summary>
		/// <returns>	An IValidationAction. </returns>
		private IValidationAction GetCheckConnectivity()
		{
			throw new System.NotImplementedException();
		}

		/// <summary>	Gets start server. </summary>
		/// <returns>	The start server. </returns>
		private IValidationAction GetStartServer()
		{
			throw new System.NotImplementedException();
		}

		/// <summary>	Executes the validation actions operation. </summary>
		private void RunValidationActions()
		{
			var endResult = Actions.Select(action => action.Run()).Any(result => !result.Success);
		}

		#endregion
	}
}
