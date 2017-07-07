using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Client.AspNetCoreEndpoint.StartUpExtensions
{
	/// <summary>	A logging extension. </summary>
	public static class LoggingExtension
	{
		/// <summary>	An IApplicationBuilder extension method that use logging. </summary>
		/// <param name="app">				The app to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <param name="loggerFactory">	The logger factory. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseLogging(this IApplicationBuilder app, IConfigurationRoot configuration,
			ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(configuration.GetSection(key: "Logging"));
			loggerFactory.AddDebug();

			return app;
		}
	}
}