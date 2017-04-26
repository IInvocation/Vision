using System.IO;
using System.Linq;

namespace FluiTec.AppFx.Signing.Settings
{
	/// <summary>	A name file signing settings. </summary>
	public class NameFileSigningSettings : DefaultFileSigningSettings
	{
		/// <summary>	Gets or sets the current key name. </summary>
		/// <value>	The name of the current key. </value>
		public string CurrentKeyName { get; set; }

		/// <summary>	Gets or sets a list of names of the expired keys. </summary>
		/// <value>	A list of names of the expired keys. </value>
		public string[] ExpiredKeyNames { get; set; }

		/// <summary>	Generates a file names. </summary>
		public override void GenerateFileNames()
		{
			CurrentKeyFileName = Path.Combine(TargetDirectory, CurrentKeyName);

			if (ExpiredKeyNames == null || ExpiredKeyNames.Length < 1)
				ExpiredKeyFileNames = Enumerable.Empty<string>();
			else
				ExpiredKeyFileNames = ExpiredKeyNames.Select(k => Path.Combine(TargetDirectory, k));
		}
	}
}