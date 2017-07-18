using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using FluiTec.Vision.Client.Windows.EndpointManager.Properties;
using FluiTec.Vision.Client.Windows.EndpointManager.Resources.Localization;
using FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.SetupWizard;
using FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.Wizard;
using FluiTec.Vision.Client.Windows.EndpointManager.WebServer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels
{
	/// <summary>	A ViewModel for the setup. </summary>
	public class SetupViewModel : ViewModelBase
	{
		/// <summary>	Default constructor. </summary>
		public SetupViewModel()
		{
			// init view content
			Title = Global.ApplicationName;

			// init commands
			FinishCommand = new RelayCommand(() => { });

			var internalModel = new InternalServerViewModel();
			var externalModel = new ExternalServerViewModel();

			LoadSettings();

			// init wizard
			Wizard = new WizardModel
			{
				Pages = new List<WizardPageViewModel>
				{
					new WelcomeViewModel(),
					internalModel,
					externalModel,
					new ValidateSettingsViewModel(externalModel, internalModel)
				}.AsReadOnly()
			};
		}

		/// <summary>	Gets or sets the title. </summary>
		/// <value>	The title. </value>
		public string Title { get; set; }

		/// <summary>	Gets or sets the current server settings. </summary>
		/// <value>	The current server settings. </value>
		public ServerSettings CurrentServerSettings { get; private set; }

		/// <summary>	Gets or sets the wizard. </summary>
		/// <value>	The wizard. </value>
		public WizardModel Wizard { get; set; }

		/// <summary>	Gets or sets the finish command. </summary>
		/// <value>	The finish command. </value>
		public ICommand FinishCommand { get; set; }

		/// <summary>	Loads the settings. </summary>
		public void LoadSettings()
		{
			var filePath = GetConfigFileName();
			if (File.Exists(filePath))
				using (var sr = new StreamReader(filePath, Encoding.Default))
				{
					CurrentServerSettings = JsonConvert.DeserializeObject<ServerSettings>(sr.ReadToEnd());
				}
			CurrentServerSettings = new ServerSettings();
		}

		/// <summary>	Saves the settings. </summary>
		/// <param name="settings">	Options for controlling the operation. </param>
		public void SaveSettings(ServerSettings settings)
		{
			using (var sw = new StreamWriter(GetConfigFileName(), append: false, encoding: Encoding.Default))
			{
				sw.Write(JsonConvert.SerializeObject(CurrentServerSettings));
			}

			CurrentServerSettings = settings;
		}

		/// <summary>	Gets configuration file name. </summary>
		/// <returns>	The configuration file name. </returns>
		private static string GetConfigFileName()
		{
			var appdata = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			var appFolder = Settings.Default.ApplicationDir;
			var fileName = Settings.Default.ServerConfigFileName;

			var filePath = Path.Combine(appdata, appFolder, fileName);

			return filePath;
		}
	}
}