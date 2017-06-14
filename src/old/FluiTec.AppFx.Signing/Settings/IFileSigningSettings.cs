using System.Collections.Generic;

namespace FluiTec.AppFx.Signing.Settings
{
	/// <summary>	Interface for file signing settings. </summary>
	public interface IFileSigningSettings
	{
		/// <summary>	Gets or sets the filename of the current key file. </summary>
		/// <value>	The filename of the current key file. </value>
		string CurrentKeyFileName { get; }

		/// <summary>	Gets or sets a list of names of the expired key files. </summary> 
		/// <value>	A list of names of the expired key files. </value>
		IEnumerable<string> ExpiredKeyFileNames { get; }
	}
}