using FluiTec.Vision.AuthHost.Bootstrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Nancy.Owin;

namespace FluiTec.Vision.AuthHost.ConsoleHost.Extensions
{
	/// <summary>	A nancy extension. </summary>
	public static class NancyExtension
	{
		/// <summary>	An IApplicationBuilder extension method that configure nancy. </summary>
		/// <param name="application">  	The application to act on. </param>
		/// <param name="loggerFactory">	The logger factory. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder ConfigureNancy(this IApplicationBuilder application, ILoggerFactory loggerFactory)
		{
			// enable nancy using owin
			application.UseOwin(x => x.UseNancy(new NancyOptions
			{
				Bootstrapper = new AuthenticationBootstrapper(application.ApplicationServices)
			}));

			return application;
		}
	}
}