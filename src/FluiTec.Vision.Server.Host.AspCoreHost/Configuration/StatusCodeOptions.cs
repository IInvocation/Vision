using System.Collections.Generic;
using FluiTec.AppFx.Options;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Configuration
{
	/// <summary>	The status code options. </summary>
	[ConfigurationName(name: "StatusCode")]
	public class StatusCodeOptions
	{
		/// <summary>	Gets or sets the self handled codes. </summary>
		/// <value>	The self handled codes. </value>
		public List<int> SelfHandledCodes { get; set; }
	}
}