using System;
using FluiTec.AppFx.Signing.Services;
using FluiTec.AppFx.Signing.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.AuthHost.ConsoleHost.Services
{
	/// <summary>	A configuration signing settings service. </summary>
	public class ConfigSigningSettingsService : ISigningSettingsService
	{
		#region Fields

		/// <summary>	The configuration key. </summary>
		private const string ConfigKey = "IDENTITY_SigningCn";

		/// <summary>	The configuration. </summary>
		private readonly IConfiguration _configuration;

		/// <summary>	The signing settings. </summary>
		private SigningSettings _signingSettings;

		/// <summary>	The logger. </summary>
		protected ILogger Logger;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <exception cref="ArgumentNullException">
		///     Thrown when one or more required arguments are
		///     null.
		/// </exception>
		/// <param name="configuration">	The configuration. </param>
		public ConfigSigningSettingsService(IConfiguration configuration) : this(configuration, null)
		{
		}

		/// <summary>	Constructor. </summary>
		/// <param name="configuration">	The configuration. </param>
		/// <param name="logger">			The logger. </param>
		public ConfigSigningSettingsService(IConfiguration configuration, ILogger<ConfigSigningSettingsService> logger)
		{
			Log($"{GetType().Name} intialized with config-hash: '{configuration?.GetHashCode()}'.");

			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		#endregion

		#region Methods

		/// <summary>	Gets the get. </summary>
		/// <returns>	The ISigningSettings. </returns>
		public ISigningSettings Get()
		{
			// return saved settings
			if (_signingSettings != null) return _signingSettings;

			// get config
			var cn = _configuration[ConfigKey];
			Log(
				$"{GetType().Name} loaded {nameof(SigningSettings.SigningCertificateName)} from config['{ConfigKey}'] as '{cn}'.");
			_signingSettings = new SigningSettings {SigningCertificateName = cn};

			return _signingSettings;
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