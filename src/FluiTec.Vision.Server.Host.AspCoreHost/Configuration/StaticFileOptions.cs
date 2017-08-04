using FluiTec.AppFx.Options;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Configuration
{
	/// <summary>	Static file options. </summary>
	[ConfigurationName(name: "StaticFiles")]
	public class StaticFileOptions
	{
		/// <summary>	Gets or sets the duration of the cache. </summary>
		/// <value>	The cache duration. </value>
		public int CacheDuration { get; set; }
	}
}