using System;
using System.IO;
using Newtonsoft.Json;

namespace Fluitec.Vision.Client.WindowsClient.Configuration
{
	/// <summary>	Manager for client configurations. </summary>
	public static class ClientConfigurationManager
	{
		/// <summary>	Initializes this object from the given from file. </summary>
		/// <exception cref="ArgumentException">
		///     Thrown when one or more arguments have unsupported or
		///     illegal values.
		/// </exception>
		/// <param name="filePath">	Full pathname of the file. </param>
		/// <returns>	A ClientConfiguration. </returns>
		public static ClientConfiguration FromFile(string filePath)
		{
			var path = Path.GetDirectoryName(filePath);
			if (Directory.Exists(path))
			Directory.CreateDirectory(path);

			return ClientConfiguration.FromFile(filePath);
		}

		/// <summary>	Converts this object to a file. </summary>
		/// <param name="configuration">	The configuration. </param>
		/// <param name="filePath">			Full pathname of the file. </param>
		public static void ToFile(ClientConfiguration configuration, string filePath)
		{
			using (var file = File.Create(filePath))
			{
				using (var sw = new StreamWriter(file, System.Text.Encoding.Default))
				{
					var content = JsonConvert.SerializeObject(configuration);
					sw.Write(content);
				}
			}
		}
	}
}