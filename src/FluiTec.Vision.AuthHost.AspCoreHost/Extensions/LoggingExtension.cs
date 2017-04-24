using System;
using FluiTec.AppFx.Logging.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace FluiTec.Vision.AuthHost.AspCoreHost.Extensions
{
	/// <summary>	A logging extension. </summary>
	public static class LoggingExtension
	{
		/// <summary>	Configure logging. </summary>
		/// <param name="application">  	The application. </param>
		/// <param name="loggerFactory">	The logger factory. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder ConfigureLogging(this IApplicationBuilder application, ILoggerFactory loggerFactory)
		{
			// get logsettings
			var logsettings = application.ApplicationServices.GetRequiredService<ILoggerSettingsService>().Get();

			// configure logging
			application.UseSerilogLogging(loggerFactory);

			// enable logging
			if (logsettings.UseDebug)
				loggerFactory.AddDebug((LogLevel)Enum.Parse(typeof(LogLevel), logsettings.DebugMinimumLevel));

			if (logsettings.UseConsole)
				loggerFactory.AddConsole((LogLevel)Enum.Parse(typeof(LogLevel), logsettings.ConsoleMinimumLevel));

			if (logsettings.UseSerilog)
				loggerFactory.AddSerilog();

			return application;
		}
	}
}