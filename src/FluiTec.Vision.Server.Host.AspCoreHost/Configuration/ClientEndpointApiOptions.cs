using FluiTec.AppFx.Options;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Configuration
{
	/// <summary>	A client endpoint API options. </summary>
	[ConfigurationName("ClientEndpointApi")]
	public class ClientEndpointApiOptions
	{
		/// <summary>	Gets or sets the authority. </summary>
		/// <value>	The authority. </value>
		public string Authority { get; set; }

		/// <summary>	Gets or sets a value indicating whether the automatic authenticate. </summary>
		/// <value>	True if automatic authenticate, false if not. </value>
		public bool AutomaticAuthenticate { get; set; }

		/// <summary>	Gets or sets a value indicating whether the automatic challenge. </summary>
		/// <value>	True if automatic challenge, false if not. </value>
		public bool AutomaticChallenge { get; set; }
	}
}