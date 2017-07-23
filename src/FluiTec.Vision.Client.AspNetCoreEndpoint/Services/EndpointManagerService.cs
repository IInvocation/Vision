using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using FluiTec.Vision.ClientEndpointApi;
using Newtonsoft.Json;

namespace FluiTec.Vision.Client.AspNetCoreEndpoint.Services
{
	/// <summary>	An endpoint manager service. </summary>
	public class EndpointManagerService
	{
		/// <summary>	Default constructor. </summary>
		public EndpointManagerService()
		{
			Load();
		}

		/// <summary>	Gets or sets the current settings. </summary>
		/// <value>	The current settings. </value>
		public ClientEndpointRegistrationModel CurrentSettings { get; private set; }

		/// <summary>	Loads this object. </summary>
		/// <summary>	Loads this object. </summary>
		public void Load()
		{
			var filePath = GetConfigFileName();
			if (File.Exists(filePath))
				using (var file = File.OpenRead(filePath))
				using (var sr = new StreamReader(file, Encoding.UTF8))
				{
					CurrentSettings = JsonConvert.DeserializeObject<ClientEndpointRegistrationModel>(sr.ReadToEnd());
				}
			else
				CurrentSettings = null;
		}

		/// <summary>	Saves the given settings. </summary>
		/// <param name="settings">	The settings to save. </param>
		public void Save(ClientEndpointRegistrationModel settings)
		{
			var dirName = GetConfigDirectoryName();
			if (!Directory.Exists(dirName))
				Directory.CreateDirectory(dirName);

			using (var file = File.Create(GetConfigFileName()))
			using (var sw = new StreamWriter(file, Encoding.UTF8))
			{
				sw.Write(JsonConvert.SerializeObject(settings));
			}

			CurrentSettings = settings;
		}

		/// <summary>	Gets configuration directory name. </summary>
		/// <returns>	The configuration directory name. </returns>
		private static string GetConfigDirectoryName()
		{
			var appdata = GetOsSpecificAppData();
			const string visionDir = "Vision";
			const string endpointDir = "Endpoint";

			return Path.Combine(appdata, visionDir, endpointDir);
		}

		/// <summary>	Gets configuration file name. </summary>
		/// <returns>	The configuration file name. </returns>
		private static string GetConfigFileName()
		{
			const string fileName = "appsettings.Registration.json";

			var filePath = Path.Combine(GetConfigDirectoryName(), fileName);

			return filePath;
		}

		/// <summary>	Gets operating system specific application data. </summary>
		/// <exception cref="Exception">	Thrown when an exception error condition occurs. </exception>
		/// <returns>	The operating system specific application data. </returns>
		private static string GetOsSpecificAppData()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				return Environment.GetEnvironmentVariable(variable: "LOCALAPPDATA");
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				return Environment.GetEnvironmentVariable(variable: "Home");

			throw new Exception($"Unsupported runtime-os, implement {nameof(GetOsSpecificAppData)} for the given system.");
		}
	}
}