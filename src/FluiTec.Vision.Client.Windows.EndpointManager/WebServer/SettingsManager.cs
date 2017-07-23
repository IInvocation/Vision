using System;
using System.IO;
using System.Text;
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
			else
				CurrentSettings = new ServerSettings();
		}

		/// <summary>	Saves the given settings. </summary>
		/// <param name="settings">	The settings to save. </param>
		public void Save(ServerSettings settings)
		{
			var dirName = GetConfigDirectoryName();
			if (!Directory.Exists(dirName))
				Directory.CreateDirectory(dirName);

			using (var sw = new StreamWriter(GetConfigFileName(), append: false, encoding: Encoding.Default))
			{
				sw.Write(JsonConvert.SerializeObject(settings));
			}

			CurrentSettings = settings;
		}

		/// <summary>	Gets configuration directory name. </summary>
		/// <returns>	The configuration directory name. </returns>
		private static string GetConfigDirectoryName()
		{
			var appdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			const string visionDir = "Vision";
			const string endpointDir = "Endpoint";

			return Path.Combine(appdata, visionDir, endpointDir);
		}

		/// <summary>	Gets configuration file name. </summary>
		/// <returns>	The configuration file name. </returns>
		private static string GetConfigFileName()
		{
			const string fileName = "appsettings.Server.json";

			var filePath = Path.Combine(GetConfigDirectoryName(), fileName);

			return filePath;
		}
	}
}