using System;
using System.IO;
using System.Text;
using FluiTec.Vision.Client.Windows.EndpointManager.Properties;
using Newtonsoft.Json;

namespace FluiTec.Vision.Client.Windows.EndpointManager.WebServer
{
	/// <summary>	Manager for settings. </summary>
	public class SettingsManager : ISettingsManager
	{
		/// <summary>	Default constructor. </summary>
		public SettingsManager()
		{
			Load();
		}

		/// <summary>	Gets or sets the current settings. </summary>
		/// <value>	The current settings. </value>

		public ServerSettings CurrentSettings { get; private set; }

		/// <summary>	Loads this object. </summary>
		public void Load()
		{
			var filePath = GetConfigFileName();
			if (File.Exists(filePath))
				using (var sr = new StreamReader(filePath, Encoding.Default))
				{
					CurrentSettings = JsonConvert.DeserializeObject<ServerSettings>(sr.ReadToEnd());
				}
			CurrentSettings = new ServerSettings();
		}

		/// <summary>	Saves the given settings. </summary>
		/// <param name="settings">	The settings to save. </param>
		public void Save(ServerSettings settings)
		{
			using (var sw = new StreamWriter(GetConfigFileName(), append: false, encoding: Encoding.Default))
			{
				sw.Write(JsonConvert.SerializeObject(settings));
			}

			CurrentSettings = settings;
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