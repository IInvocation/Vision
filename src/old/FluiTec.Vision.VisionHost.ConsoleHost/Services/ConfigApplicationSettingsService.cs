using FluiTec.Vision.VisionHost.Services;
using FluiTec.Vision.VisionHost.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.VisionHost.ConsoleHost.Services
{
	/// <summary>	A configuration application settings service. </summary>
	public class ConfigApplicationSettingsService : ConfigSettingsService<IApplicationSettings, ApplicationSettings>,
		IApplicationSettingsService
	{
		private const string SectionKey = "appsettings";

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		public ConfigApplicationSettingsService(IConfiguration configuration) : base(SectionKey, configuration)
		{
		}

		/// <summary>	Constructor. </summary>
		/// <param name="logger">			The logger. </param>
		/// <param name="configuration">	The configuration. </param>
		// ReSharper disable once SuggestBaseTypeForParameter
		public ConfigApplicationSettingsService(ILogger<ConfigApplicationSettingsService> logger, IConfiguration configuration) : base(SectionKey, configuration, logger)
		{
		}
	}
}