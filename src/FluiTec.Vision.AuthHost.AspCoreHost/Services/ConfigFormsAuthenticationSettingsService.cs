using FluiTec.Vision.NancyFx.Authentication.Forms.Services;
using FluiTec.Vision.NancyFx.Authentication.Forms.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.AuthHost.AspCoreHost.Services
{
	/// <summary>	A configuration forms authentication settings service. </summary>
	public class ConfigFormsAuthenticationSettingsService :
		ConfigSettingsService<IFormsAuthenticationSettings, FormsAuthenticationSettings>, IFormsAuthenticationSettingsService
	{
		/// <summary>	The section key. </summary>
		private const string SectionKey = "formsAuthentication";

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		public ConfigFormsAuthenticationSettingsService(IConfiguration configuration) : base(SectionKey, configuration)
		{
		}

		/// <summary>	Constructor. </summary>
		/// <param name="logger">			The logger. </param>
		/// <param name="configuration">	The configuration. </param>
		// ReSharper disable once SuggestBaseTypeForParameter
		public ConfigFormsAuthenticationSettingsService(ILogger<ConfigFormsAuthenticationSettingsService> logger,
			IConfiguration configuration) : base(SectionKey, configuration, logger)
		{
		}
	}
}