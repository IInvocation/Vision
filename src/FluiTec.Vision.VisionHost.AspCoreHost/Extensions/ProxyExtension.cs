using Microsoft.AspNetCore.Builder;

namespace FluiTec.Vision.VisionHost.AspCoreHost.Extensions
{
	/// <summary>	A proxy extension. </summary>
	public static class ProxyExtension
	{
		/// <summary>	An IApplicationBuilder extension method that configure proxy. </summary>
		/// <param name="application">	The application to act on. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder ConfigureProxy(this IApplicationBuilder application)
		{
			return application.UseProxy();
		}
	}
}