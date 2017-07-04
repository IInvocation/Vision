using System;
using System.IO;

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
			if (!File.Exists(filePath))
			{
				var path = Path.GetDirectoryName(filePath);
				if (!Directory.Exists(path))
				{
					if (path == null) throw new ArgumentException();
					Directory.CreateDirectory(path);
				}
			}

			var fileContent = File.ReadAllText(filePath);
			return new ClientConfiguration {FilePath = filePath};
		}

		/// <summary>	Converts this object to a file. </summary>
		/// <param name="configuration">	The configuration. </param>
		/// <param name="filePath">			Full pathname of the file. </param>
		public static void ToFile(ClientConfiguration configuration, string filePath)
		{
			// save
		}
	}
}