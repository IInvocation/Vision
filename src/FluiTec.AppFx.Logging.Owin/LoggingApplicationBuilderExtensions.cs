using System;
using FluiTec.AppFx.Logging.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
	/// <summary>	A logging application builder extensions. </summary>
	public static class LoggingApplicationBuilderExtensions
	{
		/// <summary>	An IApplicationBuilder extension method that use logging. </summary>
		/// <param name="app">	The app to act on. </param>
		/// <param name="loggerFactory"></param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseSerilogLogging(this IApplicationBuilder app, ILoggerFactory loggerFactory)
		{
			// get logsettings
			var logsettings = app.ApplicationServices.GetRequiredService<ILoggerSettingsService>().Get();

			// configure logging
			var config = new LoggerConfiguration()
				.MinimumLevel.ControlledBy(new LoggingLevelSwitch(
					(LogEventLevel) Enum.Parse(typeof(LogEventLevel), logsettings.SerilogMinimumLevel)))
				.Enrich.FromLogContext()
				.WriteTo.Async(a => a.RollingFile(logsettings.PathFormat, fileSizeLimitBytes: logsettings.FileSizeLimitBytes,
					retainedFileCountLimit: logsettings.RetainedFileCountLimit, outputTemplate: logsettings.OutputTemplate));

			// configure overrides
			foreach (var entry in logsettings.GetOverrides())
				config.MinimumLevel.Override(entry.Key, entry.Value);

			// create the logger
			Log.Logger = config.CreateLogger();

			return app;
		}
	}
}