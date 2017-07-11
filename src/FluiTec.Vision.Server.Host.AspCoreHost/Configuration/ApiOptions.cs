using FluiTec.AppFx.Options;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Configuration
{
	/// <summary>	An API options. </summary>
	[ConfigurationName(name: "Api")]
	public class ApiOptions
	{
		/// <summary>	Gets or sets the full pathname of the API only file. </summary>
		/// <value>	The full pathname of the API only file. </value>
		public string ApiOnlyPath { get; set; }
	}
}