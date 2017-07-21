extern alias myservicelocation;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FluiTec.Vision.Client.Windows.EndpointManager.Resources.Localization.Views.Setup.Wizard;
using FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.SetupWizard.Actions;
using FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.Wizard;
using FluiTec.Vision.Client.Windows.EndpointManager.Views.SetupWizard;
using FluiTec.Vision.Client.Windows.EndpointManager.WebServer;
using GalaSoft.MvvmLight.CommandWpf;
using FluiTec.AppFx.Upnp;
using FluiTec.Vision.Client.Windows.EndpointHelper.Helpers;
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
			Actions = new ObservableCollection<IValidationAction>();

			_externalSettings.PropertyChanged += (sender, args) => BuildActions();
			_internalSettings.PropertyChanged += (sender, args) => BuildActions();

			Title = ValidateSettings.Title;
			Description = ValidateSettings.Description;
			Content = new ValidateSettingsPage();

			ExecuteValidationCommand = new RelayCommand(RunValidationActions);
			
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
			Actions.Clear();

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

			if (webServerManager.IsRunning)
				Actions.Add(GetStopServerAction(webServerManager));
			if (oldSettings.UpnpPort > 0)
				Actions.Add(GetRemoveUpnpRegistration(oldSettings));
			Actions.Add(GetConfigureHttpAccessAction(oldSettings, newSettings));
			if (newSettings.UseUpnp)
				Actions.Add(AddUpnpRegistration(newSettings));
			Actions.Add(GetCheckConnectivity());
			Actions.Add(GetStartServer(webServerManager));
		}

		/// <summary>	Executes the validation actions operation. </summary>
		private async void RunValidationActions()
		{
			foreach (var action in Actions)
			{
				var result = await action.Run();
				if (!result.Success)
					return;
			}
			Application.Current.MainWindow.Close();
		}

		#endregion

		#region Actions

		/// <summary>	Gets stop server action. </summary>
		/// <param name="webServerManager">	Manager for web server. </param>
		/// <returns>	The stop server action. </returns>
		private static IValidationAction GetStopServerAction(IWebServerManager webServerManager)
		{
			return new ValidationAction
			{
				DisplayName = ValidateSettings.StopServerLabel,
				ActionToExecute = () =>
				{
					webServerManager.Stop();
					return webServerManager.IsRunning ?
						Task.FromResult(new ValidationResult
						{
							Success = false,
							ErrorMessage = ValidateSettings.StopServerErrorMessage
						}) 
						: Task.FromResult(new ValidationResult { Success = true });
				}
			};
		}

		/// <summary>	Gets remove upnp registration. </summary>
		/// <param name="oldSettings">	The old settings. </param>
		/// <returns>	The remove upnp registration. </returns>
		private static IValidationAction GetRemoveUpnpRegistration(ServerSettings oldSettings)
		{
			return new ValidationAction
			{
				DisplayName = ValidateSettings.RemoveUpnpLabel,
				ActionToExecute = async () =>
				{
					await new UpnpService().RemovePortMapping(Properties.Settings.Default.ApplicationName, oldSettings.UpnpPort);
					return new ValidationResult {Success = true};
				}
			};
		}

		/// <summary>	Gets configure HTTP access action. </summary>
		/// <param name="oldSettings">	The old settings. </param>
		/// <param name="newSettings">	The new settings. </param>
		/// <returns>	The configure HTTP access action. </returns>
		private IValidationAction GetConfigureHttpAccessAction(ServerSettings oldSettings, ServerSettings newSettings)
		{
			return new ValidationAction
			{
				DisplayName = ValidateSettings.ConfigureHttpLabel,
				ErrorMessage = ValidateSettings.ConfigureHttpErrorMessage,
				ActionToExecute = () =>
				{
					return Task<ValidationResult>.Factory.StartNew(() =>
					{
						var cmdArgs = $"-a {Properties.Settings.Default.ApplicationDir} -f config.json";

						var ok = new Process
							{
								StartInfo = new ProcessStartInfo(fileName: "Helper\\FluiTec.Vision.Client.Windows.EndpointHelper.exe")
								{
									Arguments = cmdArgs,
									UseShellExecute = false,
									Verb = "runas"
								}
							}
							.RedirectOutputToConsole(createNoWindow: false)
							.RunAndWait();

						return new ValidationResult {Success = ok, ErrorMessage = ok ? string.Empty : ValidateSettings.ConfigureHttpErrorMessage};
					});
				}
			};
		}

		/// <summary>	Adds an upnp registration. </summary>
		/// <param name="newSettings">	The new settings. </param>
		/// <returns>	An IValidationAction. </returns>
		private static IValidationAction AddUpnpRegistration(ServerSettings newSettings)
		{
			return new ValidationAction
			{
				DisplayName = ValidateSettings.AddUpnpLabel,
				ErrorMessage = "Bla",
				ActionToExecute = async () =>
				{
					var mapping = await new UpnpService().AddPortMapping(Properties.Settings.Default.ApplicationName, newSettings.Port);
					newSettings.UpnpPort = mapping.PublicPort;
					return new ValidationResult {Success = true};
				}
			};
		}

		/// <summary>	Check connectivity. </summary>
		/// <returns>	An IValidationAction. </returns>
		private IValidationAction GetCheckConnectivity()
		{
			return new ValidationAction
			{
				DisplayName = ValidateSettings.CheckConnectivityLabel,
				ErrorMessage = ValidateSettings.CheckConnectiviyErrorMessage,
				ActionToExecute = () => Task.FromResult(new ValidationResult { Success = false })
			};
		}

		/// <summary>	Gets start server. </summary>
		/// <param name="webServerManager">	Manager for web server. </param>
		/// <returns>	The start server. </returns>
		private static IValidationAction GetStartServer(IWebServerManager webServerManager)
		{
			return new ValidationAction
			{
				DisplayName = ValidateSettings.StartServerLabel,
				ActionToExecute = () =>
				{
					webServerManager.Start();
					return !webServerManager.IsRunning ?
						Task.FromResult(new ValidationResult
						{
							Success = false,
							ErrorMessage = ValidateSettings.StartServerErrorMessage
						}) 
						: Task.FromResult(new ValidationResult { Success = true });
				}
			};
		}

		#endregion
	}
}
