extern alias myservicelocation;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using FluiTec.Vision.Client.Windows.EndpointHelper.Configuration;
using FluiTec.Vision.Client.Windows.EndpointHelper.Helpers;
using FluiTec.Vision.Client.Windows.EndpointManager.Properties;
using myservicelocation::Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;

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
				HttpName = $"https://+:{_internalSettings.LocalPort}/",
				Validated = true
			};

			var webServerManager = ServiceLocator.Current.GetInstance<IWebServerManager>();

			if (webServerManager.IsRunning)
				Actions.Add(GetStopServerAction(webServerManager));

			if (oldSettings.UpnpPort > 0)
				Actions.Add(GetRemoveUpnpRegistration(oldSettings));

			Actions.Add(GetConfigureHttpAccessAction(newSettings));

			if (newSettings.UseUpnp)
				Actions.Add(AddUpnpRegistration(newSettings));

			Actions.Add(GetSaveServerConfigurationAction(newSettings));

			Actions.Add(GetStartServer(webServerManager));

			Actions.Add(GetCheckConnectivity());
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
					await new UpnpService().RemovePortMapping(oldSettings.UpnpPort);
					return new ValidationResult {Success = true};
				}
			};
		}

		/// <summary>	Gets configure HTTP access action. </summary>
		/// <param name="newSettings">	The new settings. </param>
		/// <returns>	The configure HTTP access action. </returns>
		private static IValidationAction GetConfigureHttpAccessAction(ServerSettings newSettings)
		{
			return new ValidationAction
			{
				DisplayName = ValidateSettings.ConfigureHttpLabel,
				ErrorMessage = ValidateSettings.ConfigureHttpErrorMessage,
				ActionToExecute = () =>
				{
					return Task<ValidationResult>.Factory.StartNew(() =>
					{
						var fileName = CreateConfigurationFile(newSettings);

						// call the helper application and let it process the config-file
						var cmdArgs = $"-a {Settings.Default.ApplicationDir} -f {fileName}";
				
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
							.RunAndWaitForForNamedPipeResult(pipeName: "vision_endpoint_config_pipe");

						return new ValidationResult {Success = ok, ErrorMessage = ok ? string.Empty : ValidateSettings.ConfigureHttpErrorMessage};
					});
				}
			};
		}

		/// <summary>	Creates configuration file. </summary>
		/// <param name="newSettings">	The new settings. </param>
		private static string CreateConfigurationFile(ServerSettings newSettings)
		{
			var appdata = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			var appFolder = Settings.Default.ApplicationDir;
			const string fileName = "config.json";

			var dirPath = Path.Combine(appdata, appFolder);
			var filePath = Path.Combine(appdata, appFolder, fileName);
			if (!Directory.Exists(dirPath))
				Directory.CreateDirectory(dirPath);

			HttpConfiguration config;
			if (File.Exists(filePath))
			{
				using (var sr = new StreamReader(filePath, System.Text.Encoding.Default))
				{
					config = JsonConvert.DeserializeObject<HttpConfiguration>(sr.ReadToEnd());
				}
			}
			else
				config = new HttpConfiguration();

			config.ApplicationName = Settings.Default.ApplicationName;
			config.AddFirewallException = true;
			config.AddFirewallExceptionPort = newSettings.Port;
			config.AddSslCertificate = true;
			config.AddSslCertificatePort = newSettings.Port;
			config.AddSslCertificateApplicationId = Guid.NewGuid();
			config.AddUrlReservation = true;
			config.AddUrlReservationUri = $"https://+:{newSettings.Port}/";


			var json = JsonConvert.SerializeObject(config, Formatting.Indented);
			using (var sw = new StreamWriter(filePath, append: false, encoding: System.Text.Encoding.Default))
			{
				sw.Write(json);
			}

			return fileName;
		}

		/// <summary>	Adds an upnp registration. </summary>
		/// <param name="newSettings">	The new settings. </param>
		/// <returns>	An IValidationAction. </returns>
		private static IValidationAction AddUpnpRegistration(ServerSettings newSettings)
		{
			return new ValidationAction
			{
				DisplayName = ValidateSettings.AddUpnpLabel,
				ActionToExecute = async () =>
				{
					var mapping = await new UpnpService().AddPortMapping(Settings.Default.SafeApplicationName, newSettings.Port);
					newSettings.UpnpPort = mapping.PublicPort;
					return new ValidationResult {Success = true};
				}
			};
		}

		/// <summary>	Gets save server configuration action. </summary>
		/// <param name="newSettings">	The new settings. </param>
		/// <returns>	The save server configuration action. </returns>
		private static IValidationAction GetSaveServerConfigurationAction(ServerSettings newSettings)
		{
			return new ValidationAction
			{
				DisplayName = "SaveSettings",
				ActionToExecute = () =>
				{
					var settingsManager = ServiceLocator.Current.GetInstance<ISettingsManager>();
					settingsManager.Save(newSettings);
					return Task.FromResult(new ValidationResult {Success = true});
				}
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
				ActionToExecute = async () =>
				{
					webServerManager.Start();
					await Task.Delay(TimeSpan.FromSeconds(value: 5));
					return !webServerManager.IsRunning ?
						new ValidationResult
						{
							Success = false,
							ErrorMessage = ValidateSettings.StartServerErrorMessage
						}
						: new ValidationResult { Success = true };
				}
			};
		}

		/// <summary>	Check connectivity. </summary>
		/// <returns>	An IValidationAction. </returns>
		private static IValidationAction GetCheckConnectivity()
		{
			return new ValidationAction
			{
				DisplayName = ValidateSettings.CheckConnectivityLabel,
				ErrorMessage = ValidateSettings.CheckConnectiviyErrorMessage,
				ActionToExecute = () => Task.FromResult(new ValidationResult { Success = false })
			};
		}

		#endregion
	}
}
