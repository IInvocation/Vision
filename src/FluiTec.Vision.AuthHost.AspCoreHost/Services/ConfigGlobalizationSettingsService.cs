using FluiTec.AppFx.Globalization.Services;
using FluiTec.AppFx.Globalization.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.AuthHost.AspCoreHost.Services
{
	/// <summary>	A configuration globalization settings service. </summary>
	public class ConfigGlobalizationSettingsService : ConfigSettingsService<IGlobalizationSettings, GlobalizationSettings>,
		IGlobalizationSettingsService
	{
		/// <summary>	The section key. </summary>
		private const string SectionKey = "globalization";

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		public ConfigGlobalizationSettingsService(IConfiguration configuration) : base(SectionKey, configuration)
		{
		}

		/// <summary>	Constructor. </summary>
		/// <param name="logger">			The logger. </param>
		/// <param name="configuration">	The configuration. </param>
		// ReSharper disable once SuggestBaseTypeForParameter
		public ConfigGlobalizationSettingsService(ILogger<ConfigGlobalizationSettingsService> logger,
			IConfiguration configuration) : base(SectionKey, configuration, logger)
		{
		}
	}
}