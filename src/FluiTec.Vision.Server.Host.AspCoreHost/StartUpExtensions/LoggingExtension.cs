using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace FluiTec.Vision.Server.Host.AspCoreHost.StartUpExtensions
{
	/// <summary>	A logging extension. </summary>
	public static class LoggingExtension
	{
		/// <summary>	An IApplicationBuilder extension method that use logging. </summary>
		/// <param name="app">				The app to act on. </param>
		/// <param name="environment">  	The environment. </param>
		/// <param name="appLifetime">  	The application lifetime. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <param name="loggerFactory">	The logger factory. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseLogging(this IApplicationBuilder app, IHostingEnvironment environment,
			IApplicationLifetime appLifetime, IConfigurationRoot configuration, ILoggerFactory loggerFactory)
		{
			if (environment.IsDevelopment())
			{
				loggerFactory.AddConsole(configuration.GetSection(key: "Logging"));
				loggerFactory.AddDebug();
			}
			else
			{
				// async logger wrapping rolling-file logger
				Log.Logger = new LoggerConfiguration()
					.WriteTo.Async(a =>
						a.Logger(config => config.ReadFrom.Configuration(configuration)))
					.CreateLogger();

				// add static Log to AspNetCoreLogging
				loggerFactory.AddSerilog();

				// close and flush on stopping application
				appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
			}

			return app;
		}
	}
}