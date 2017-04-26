using FluiTec.Vision.NancyFx.IdentityServer.Services;
using FluiTec.Vision.NancyFx.IdentityServer.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.AuthHost.AspCoreHost.Services
{
	/// <summary>	A configuration identity server settings service. </summary>
	public class ConfigIdentityServerSettingsService :
		ConfigSettingsService<IIdentityServerSettings, IdentityServerSettings>, IIdentityServerSettingsService
	{
		/// <summary>	The section key. </summary>
		private const string SectionKey = "identity";

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		public ConfigIdentityServerSettingsService(IConfiguration configuration) : base(SectionKey, configuration)
		{
		}

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		/// <param name="logger">			The logger. </param>
		public ConfigIdentityServerSettingsService(IConfiguration configuration, ILogger logger) : base(SectionKey,
			configuration, logger)
		{
		}
	}
}