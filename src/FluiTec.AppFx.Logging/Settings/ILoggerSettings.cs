using System.Collections.Generic;
using Serilog.Events;

namespace FluiTec.AppFx.Logging.Settings
{
	/// <summary>	Interface for logger settings. </summary>
	public interface ILoggerSettings
	{
		/// <summary>	Gets or sets a value indicating whether this object use serilog. </summary>
		/// <value>	True if use serilog, false if not. </value>
		bool UseSerilog { get; set; }

		/// <summary>	Gets or sets the overrides. </summary>
		/// <value>	The overrides. </value>
		string[] Overrides { get; set; }

		/// <summary>	Gets or sets the log output template. </summary>
		/// <value>	The log output template. </value>
		string OutputTemplate { get; set; }

		/// <summary>	Gets or sets the log minimum level. </summary>
		/// <value>	The log minimum level. </value>
		string SerilogMinimumLevel { get; set; }

		/// <summary>	Gets or sets the log file size limit bytes. </summary>
		/// <value>	The log file size limit bytes. </value>
		long? FileSizeLimitBytes { get; set; }

		/// <summary>	Gets or sets the log path format. </summary>
		/// <value>	The log path format. </value>
		string PathFormat { get; set; }

		/// <summary>	Gets or sets the log retained file count limit. </summary>
		/// <value>	The log retained file count limit. </value>
		int? RetainedFileCountLimit { get; set; }

		/// <summary>	Gets or sets a value indicating whether this object use debug. </summary>
		/// <value>	True if use debug, false if not. </value>
		bool UseDebug { get; set; }

		/// <summary>	Gets or sets the debug minimum level. </summary>
		/// <value>	The debug minimum level. </value>
		string DebugMinimumLevel { get; set; }

		/// <summary>	Gets or sets a value indicating whether this object use console. </summary>
		/// <value>	True if use console, false if not. </value>
		bool UseConsole { get; set; }

		/// <summary>	Gets or sets the console minimum level. </summary>
		/// <value>	The console minimum level. </value>
		string ConsoleMinimumLevel { get; set; }

		/// <summary>	Gets the overrides. </summary>
		/// <returns>	The overrides. </returns>
		IEnumerable<KeyValuePair<string, LogEventLevel>> GetOverrides();
	}
}