extern alias myservicelocation;
using System.Collections.ObjectModel;
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
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="externalSettings">	The external settings. </param>
		/// <param name="internalSettings">	The internal settings. </param>
		public ValidateSettingsViewModel(
			ExternalServerViewModel externalSettings, 
			InternalServerViewModel internalSettings)
		{
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
			var webServerManager = ServiceLocator.Current.GetInstance<IWebServerManager>();
			Actions.Add(GetStopServerAction(webServerManager));
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

		/// <summary>	Executes the validation actions operation. </summary>
		private void RunValidationActions()
		{
			
		}

		#endregion
	}
}
