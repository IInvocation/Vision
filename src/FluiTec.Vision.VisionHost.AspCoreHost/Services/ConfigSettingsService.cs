using System;
using FluiTec.AppFx.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.VisionHost.AspCoreHost.Services
{
	/// <summary>	A configuration settings service. </summary>
	/// <typeparam name="TSettings">			  	Type of the settings. </typeparam>
	/// <typeparam name="TSettingsImplementation">	Type of the settings implementation. </typeparam>
	public abstract class ConfigSettingsService<TSettings, TSettingsImplementation> : ISettingsService<TSettings>
		where TSettingsImplementation : TSettings
	{
		#region Fields

		/// <summary>	The configuration. </summary>
		protected readonly IConfiguration Configuration;

		/// <summary>	Name of the section. </summary>
		protected readonly string SectionName;

		/// <summary>	The settings. </summary>
		protected TSettingsImplementation Settings;

		/// <summary>	The logger. </summary>
		protected ILogger Logger;

		#endregion

		#region Constructors

		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <param name="sectionName">  	Name of the section. </param>
		/// <param name="configuration">	The configuration. </param>
		protected ConfigSettingsService(string sectionName, IConfiguration configuration) : this(sectionName, configuration, null)
		{
		}

		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <param name="sectionName">  	Name of the section. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <param name="logger">			The logger. </param>
		protected ConfigSettingsService(string sectionName, IConfiguration configuration, ILogger logger)
		{
			Logger = logger;

			Log($"{GetType().Name}: Intialized with config-section '{sectionName}', config-hash: '{configuration?.GetHashCode()}'.");

			Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			if (string.IsNullOrWhiteSpace(sectionName)) throw new ArgumentNullException(nameof(sectionName));
			SectionName = sectionName;
		}

		#endregion

		#region Methods

		/// <summary>	Gets the get. </summary>
		/// <returns>	The TSettings. </returns>
		public virtual TSettings Get()
		{
			// return saved settings
			if (Settings != null) return Settings;

			Log($"{GetType().Name}: Loading config.");

			// load config section
			var section = Configuration.GetSection(SectionName);

			// parse settings
			Settings = section.Get<TSettingsImplementation>();

			// return loaded and parsed settings
			return Settings;
		}

		/// <summary>	Logs. </summary>
		/// <param name="message">	The message. </param>
		private void Log(string message)
		{
			if (Logger != null)
				Logger.LogInformation(message);
			else
				Console.WriteLine(message);
		}

		#endregion
	}
}