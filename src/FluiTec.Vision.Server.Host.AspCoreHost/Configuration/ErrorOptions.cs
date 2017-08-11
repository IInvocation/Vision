using FluiTec.AppFx.Options;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Configuration
{
	/// <summary>	An error options. </summary>
	[ConfigurationName(name: "Error")]
	public class ErrorOptions
	{
		/// <summary>	Gets or sets the error recipient. </summary>
		/// <value>	The error recipient. </value>
		public string ErrorRecipient { get; set; }
	}
}