using FluiTec.Vision.NancyFx.Authentication.Services;
using FluiTec.Vision.NancyFx.Authentication.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.AuthHost.AspCoreHost.Services
{
	/// <summary>	A configuration authentication settings service. </summary>
	public class ConfigAuthenticationSettingsService :
		ConfigSettingsService<IAuthenticationSettings, AuthenticationSettings>, IAuthenticationSettingsService
	{
		/// <summary>	The section key. </summary>
		private const string SectionKey = "authentication";

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		public ConfigAuthenticationSettingsService(IConfiguration configuration) : base(SectionKey, configuration)
		{
		}

		/// <summary>	Constructor. </summary>
		/// <param name="logger">			The logger. </param>
		/// <param name="configuration">	The configuration. </param>
		// ReSharper disable once SuggestBaseTypeForParameter
		public ConfigAuthenticationSettingsService(ILogger<ConfigAuthenticationSettingsService> logger, IConfiguration configuration) : base(SectionKey, configuration, logger)
		{
		}
	}
}