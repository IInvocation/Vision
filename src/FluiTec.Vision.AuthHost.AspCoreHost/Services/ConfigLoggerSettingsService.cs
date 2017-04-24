using FluiTec.AppFx.Logging.Services;
using FluiTec.AppFx.Logging.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.AuthHost.AspCoreHost.Services
{
	/// <summary>	A configuration logger settings service. </summary>
	public class ConfigLoggerSettingsService : ConfigSettingsService<ILoggerSettings, LoggerSettings>,
		ILoggerSettingsService
	{
		/// <summary>	The section key. </summary>
		private const string SectionKey = "logging";

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		public ConfigLoggerSettingsService(IConfiguration configuration) : base(SectionKey, configuration)
		{
		}

		/// <summary>	Constructor. </summary>
		/// <param name="logger">			The logger. </param>
		/// <param name="configuration">	The configuration. </param>
		// ReSharper disable once SuggestBaseTypeForParameter
		public ConfigLoggerSettingsService(ILogger<ConfigLoggerSettingsService> logger,
			IConfiguration configuration) : base(SectionKey, configuration, logger)
		{
		}
	}
}