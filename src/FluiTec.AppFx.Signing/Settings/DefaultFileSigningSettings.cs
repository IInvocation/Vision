using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluiTec.AppFx.Signing.Settings
{
	/// <summary>	A default file signing settings. </summary>
	public class DefaultFileSigningSettings : IFileSigningSettings
	{
		#region Constructors

		/// <summary>	Default constructor. </summary>
		public DefaultFileSigningSettings()
		{
			var currentDirectory = Directory.GetCurrentDirectory();
			var targetDirectory = Path.Combine(currentDirectory, "KeyVault");

			TargetDirectory = targetDirectory;
		}

		#endregion

		#region Methods

		/// <summary>	Generates a file names. </summary>
		public virtual void GenerateFileNames()
		{
			CurrentKeyFileName = Path.Combine(TargetDirectory, "current.secret.key");
			ExpiredKeyFileNames = Enumerable.Empty<string>();
		}

		#endregion

		#region Properties

		/// <summary>	Gets the pathname of the target directory. </summary>
		/// <value>	The pathname of the target directory. </value>
		protected string TargetDirectory { get; }

		/// <summary>	Gets or sets the filename of the current key file. </summary>
		/// <value>	The filename of the current key file. </value>
		public string CurrentKeyFileName { get; protected set; }

		/// <summary>	Gets or sets a list of names of the expired key files. </summary>
		/// <value>	A list of names of the expired key files. </value>
		public IEnumerable<string> ExpiredKeyFileNames { get; protected set; }

		#endregion
	}
}