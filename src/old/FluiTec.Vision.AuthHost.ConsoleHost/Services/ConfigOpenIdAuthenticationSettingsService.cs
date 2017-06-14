using FluiTec.Vision.NancyFx.Authentication.OpenId.Services;
using FluiTec.Vision.NancyFx.Authentication.OpenId.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.AuthHost.ConsoleHost.Services
{
	/// <summary>	A configuration owin authentication settings service. </summary>
	public class ConfigOpenIdAuthenticationSettingsService :
		ConfigSettingsService<IOpenIdAuthenticationSettings, OpenIdAuthenticationSettings>, IOpenIdAuthenticationSettingsService
	{
		/// <summary>	The section key. </summary>
		private const string SectionKey = "openIdAuthentication";

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		public ConfigOpenIdAuthenticationSettingsService(IConfiguration configuration) : base(SectionKey, configuration)
		{
		}

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		/// <param name="logger">			The logger. </param>
		public ConfigOpenIdAuthenticationSettingsService(IConfiguration configuration, ILogger logger) : base(SectionKey,
			configuration, logger)
		{
		}
	}
}