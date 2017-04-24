using FluiTec.AppFx.Proxy.Services;
using FluiTec.AppFx.Proxy.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.AuthHost.ConsoleHost.Services
{
	/// <summary>	A configuration proxy settings service. </summary>
	public class ConfigProxySettingsService : ConfigSettingsService<IProxySettings, ProxySettings>, IProxySettingsService
	{
		/// <summary>	The section key. </summary>
		private const string SectionKey = "proxy";

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		public ConfigProxySettingsService(IConfiguration configuration) : base(SectionKey, configuration)
		{
		}

		/// <summary>	Constructor. </summary>
		/// <param name="logger">			The logger. </param>
		/// <param name="configuration">	The configuration. </param>
		// ReSharper disable once SuggestBaseTypeForParameter
		public ConfigProxySettingsService(ILogger<ConfigProxySettingsService> logger,
			IConfiguration configuration) : base(SectionKey, configuration, logger)
		{
		}
	}
}