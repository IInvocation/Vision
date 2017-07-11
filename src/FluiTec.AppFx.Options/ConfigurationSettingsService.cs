using System;
using Microsoft.Extensions.Configuration;

namespace FluiTec.AppFx.Options
{
	/// <summary>	A configuration settings service. </summary>
	/// <typeparam name="TSettings">	Type of the settings. </typeparam>
	public class ConfigurationSettingsService<TSettings> : ISettingsService<TSettings>
		where TSettings : new()
	{
		/// <summary>	The configuration. </summary>
		protected readonly IConfiguration Configuration;

		/// <summary>	The configuration key. </summary>
		protected readonly string ConfigurationKey;

		/// <summary>	Options for controlling the operation. </summary>
		protected TSettings Settings;

		/// <summary>	Constructor. </summary>
		/// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
		/// <param name="configuration">	The configuration. </param>
		/// <param name="configKey">		The configuration key. </param>
		public ConfigurationSettingsService(IConfiguration configuration, string configKey)
		{
			if (string.IsNullOrWhiteSpace(configKey)) throw new ArgumentNullException(nameof(configKey));
			ConfigurationKey = configKey;
			Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		/// <summary>	Gets the get. </summary>
		/// <returns>	The TSettings. </returns>
		public TSettings Get()
		{
			if (Settings != null)
				return Settings;

			// load config-section
			var section = Configuration.GetSection(ConfigurationKey);

			// parse config-section
			Settings = section.Get<TSettings>();

			return Settings;
		}
	}
}