using System;
using System.IO;
using System.Text;
using CommandLine;
using FluiTec.Vision.Client.Windows.EndpointHelper.Configuration;
using Newtonsoft.Json;

namespace FluiTec.Vision.Client.Windows.EndpointHelper.Options
{
	/// <summary>	A configuration file options. </summary>
	public class ConfigFileOptions
	{
		/// <summary>	Gets or sets the name of the application. </summary>
		/// <value>	The name of the application. </value>
		[Option(shortName: 'a', longName: "applicationname", Required = true, HelpText = "Name of the application")]
		public string ApplicationName { get; set; }

		/// <summary>	Gets or sets the filename of the file. </summary>
		/// <value>	The name of the file. </value>
		[Option(shortName: 'f', longName: "filename", Required = true, HelpText = "Name of the file")]
		public string FileName { get; set; }

		/// <summary>	Gets the full pathname of the file. </summary>
		/// <value>	The full pathname of the file. </value>
		public string FilePath
		{
			get
			{
				var appdata = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
				return Path.Combine(appdata, ApplicationName, FileName);
			}
		}

		/// <summary>	True if this object is valid. </summary>
		public bool IsValid => File.Exists(FilePath);

		/// <summary>	Executes this object. </summary>
		public void Execute()
		{
			string fileContent;
			using (var sr = new StreamReader(FilePath, Encoding.Default))
			{
				fileContent = sr.ReadToEnd();
			}

			var configuration = JsonConvert.DeserializeObject<HttpConfiguration>(fileContent);
			if (configuration == null || !configuration.IsValid)
				throw new InvalidOperationException(message: "Konfigurationsdatei ist ungültig!");

			try
			{
				configuration.Run();
				using (var sw = new StreamWriter(FilePath, append: false, encoding: Encoding.Default))
				{
					sw.Write(JsonConvert.SerializeObject(configuration, Formatting.Indented));
				}
			}
			catch (Exception) // catch errors and autosave
			{
				using (var sw = new StreamWriter(FilePath, append: false, encoding: Encoding.Default))
				{
					sw.Write(JsonConvert.SerializeObject(configuration, Formatting.Indented));
				}
				throw;
			}
		}
	}
}