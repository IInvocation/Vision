using FluiTec.Vision.NancyFx.Authentication.GoogleOpenId.Services;
using FluiTec.Vision.NancyFx.Authentication.OpenId.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.AuthHost.AspCoreHost.Services
{
	/// <summary>	A configuration google open identifier provider settings service. </summary>
	public class ConfigGoogleOpenIdProviderSettingsService : ConfigSettingsService<IOpenIdProviderSetting, OpenIdProviderSetting>, IGoogleOpenIdProviderSettingsService
	{
		private const string SectionKey = "googleAuth";

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		public ConfigGoogleOpenIdProviderSettingsService(IConfiguration configuration) : base(SectionKey,
			configuration)
		{
		}

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		/// <param name="logger">			The logger. </param>
		public ConfigGoogleOpenIdProviderSettingsService(IConfiguration configuration,
			ILogger logger) : base(SectionKey, configuration, logger)
		{
		}
	}
}