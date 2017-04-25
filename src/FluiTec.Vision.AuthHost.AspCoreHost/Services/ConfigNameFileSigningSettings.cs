using FluiTec.AppFx.Signing.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.AuthHost.AspCoreHost.Services
{
	/// <summary>	A configuration name file signing settings. </summary>
	public class ConfigNameFileSigningSettings : ConfigSettingsService<IFileSigningSettings, NameFileSigningSettings>
	{
		/// <summary>	The section key. </summary>
		private const string SectionKey = "signing";

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		public ConfigNameFileSigningSettings(IConfiguration configuration) : base(SectionKey, configuration)
		{
		}

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		/// <param name="logger">			The logger. </param>
		public ConfigNameFileSigningSettings(IConfiguration configuration, ILogger logger) : base(SectionKey, configuration,
			logger)
		{
		}

		/// <summary>	Gets the get. </summary>
		/// <returns>	The IFileSigningSettings. </returns>
		public override IFileSigningSettings Get()
		{
			var settings = base.Get() as NameFileSigningSettings;
			settings?.GenerateFileNames();
			return settings;
		}
	}
}