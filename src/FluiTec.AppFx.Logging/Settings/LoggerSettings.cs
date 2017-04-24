using System;
using System.Collections.Generic;
using System.Linq;
using Serilog.Events;

namespace FluiTec.AppFx.Logging.Settings
{
	/// <summary>	A logger settings. </summary>
	public class LoggerSettings : ILoggerSettings
	{
		/// <summary>	Gets or sets the overrides. </summary>
		/// <value>	The overrides. </value>
		public string[] Overrides { get; set; }

		/// <summary>	Gets or sets the log output template. </summary>
		/// <value>	The log output template. </value>
		public string OutputTemplate { get; set; }

		/// <summary>	Gets or sets the log minimum level. </summary>
		/// <value>	The log minimum level. </value>
		public string SerilogMinimumLevel { get; set; }

		/// <summary>	Gets or sets the log file size limit bytes. </summary>
		/// <value>	The log file size limit bytes. </value>
		public long? FileSizeLimitBytes { get; set; }

		/// <summary>	Gets or sets the log path format. </summary>
		/// <value>	The log path format. </value>
		public string PathFormat { get; set; }

		/// <summary>	Gets or sets the log retained file count limit. </summary>
		/// <value>	The log retained file count limit. </value>
		public int? RetainedFileCountLimit { get; set; }

		/// <summary>	Gets or sets a value indicating whether this object use debug. </summary>
		/// <value>	True if use debug, false if not. </value>
		public bool UseDebug { get; set; }

		/// <summary>	Gets or sets the debug minimum level. </summary>
		/// <value>	The debug minimum level. </value>
		public string DebugMinimumLevel { get; set; }

		/// <summary>	Gets or sets a value indicating whether this object use console. </summary>
		/// <value>	True if use console, false if not. </value>
		public bool UseConsole { get; set; }

		/// <summary>	Gets or sets the console minimum level. </summary>
		/// <value>	The console minimum level. </value>
		public string ConsoleMinimumLevel { get; set; }

		/// <summary>	Gets or sets a value indicating whether this object use serilog. </summary>
		/// <value>	True if use serilog, false if not. </value>
		public bool UseSerilog { get; set; }

		/// <summary>	Gets the overrides. </summary>
		/// <returns>	The overrides. </returns>
		public IEnumerable<KeyValuePair<string, LogEventLevel>> GetOverrides()
		{
			if (!Overrides.Any()) return Enumerable.Empty<KeyValuePair<string, LogEventLevel>>();

			try
			{
				return Overrides
					.Select(o => o.Split(new []{'@'}, StringSplitOptions.RemoveEmptyEntries))
					.Select(s => new KeyValuePair<string, LogEventLevel>(s[0], (LogEventLevel)Enum.Parse(typeof(LogEventLevel), s[1])));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return Enumerable.Empty<KeyValuePair<string, LogEventLevel>>();
			}
		}
	}
}