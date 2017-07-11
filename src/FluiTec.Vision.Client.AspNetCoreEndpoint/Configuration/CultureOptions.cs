using System.Collections.Generic;
using FluiTec.AppFx.Options;

namespace FluiTec.Vision.Client.AspNetCoreEndpoint.Configuration
{
	/// <summary>	A culture options. </summary>
	[ConfigurationName("Localization")]
	public class CultureOptions
	{
		/// <summary>	Default constructor. </summary>
		public CultureOptions()
		{
			SupportedCultures = new List<string>();
		}

		/// <summary>	Gets or sets the default culture. </summary>
		/// <value>	The default culture. </value>
		public string DefaultCulture { get; set; }

		/// <summary>	Gets or sets the supported cultures. </summary>
		/// <value>	The supported cultures. </value>
		public List<string> SupportedCultures { get; set; }
	}
}