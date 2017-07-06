using System;
using System.IO;
using FluiTec.AppFx.Cryptography;
using Newtonsoft.Json;

namespace Fluitec.Vision.Client.WindowsClient.Configuration
{
	/// <summary>	A client configuration. </summary>
	public class ClientConfiguration
	{
		/// <summary>	Gets the full pathname of the file. </summary>
		/// <value>	The full pathname of the file. </value>
		private string FilePath { get; set; }

		/// <summary>	Gets or sets the email. </summary>
		/// <value>	The email. </value>
		public string Email { get; set; }

		/// <summary>	Gets or sets the name of the machine. </summary>
		/// <value>	The name of the machine. </value>
		public string MachineName { get; set; }

		/// <summary>	Gets or sets the identifier of the client. </summary>
		/// <value>	The identifier of the client. </value>
		public string ClientId { get; set; }

		/// <summary>	Gets or sets the client secret. </summary>
		/// <value>	The client secret. </value>
		public string ClientSecret { get; set; }

		/// <summary>	Gets or sets the activation code. </summary>
		/// <value>	The activation code. </value>
		public string ActivationCode { get; set; }

		/// <summary>	Saves this object. </summary>
		public void Save()
		{
			ClientConfigurationManager.ToFile(this, FilePath);
		}

		/// <summary>	Initializes this object from the given from file. </summary>
		/// <param name="filePath">	Full pathname of the file. </param>
		/// <returns>	A ClientConfiguration. </returns>
		public static ClientConfiguration FromFile(string filePath)
		{
			if (File.Exists(filePath))
			{
				var content = File.ReadAllText(filePath);
				var configuration = JsonConvert.DeserializeObject<ClientConfiguration>(content);
				configuration.FilePath = filePath;
				return configuration;
			}
			else
			{
				var configuration = new ClientConfiguration
				{
					FilePath = filePath,
					MachineName = Environment.MachineName,
					ClientId = IdGenerator.GetIdString(),
					ClientSecret = IdGenerator.GetIdString()
				};
				configuration.Save();
				return configuration;
			}
		}
	}
}