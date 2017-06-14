using System;
using System.Globalization;
using FluentValidation;
using FluiTec.AppFx.Globalization.Settings;
using FluiTec.Vision.AuthHost.Localization;
using FluiTec.Vision.AuthHost.Services;
using FluiTec.Vision.AuthHost.Settings;
using FluiTec.Vision.NancyFx.Bootstrappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Cryptography;
using Nancy.TinyIoc;

namespace FluiTec.Vision.AuthHost.Bootstrapper
{
	public class ConfigurableBootstrapper : GlobalizingNancyBootstrapper
	{
		#region Fields

		/// <summary>	The log. </summary>
		private readonly ILogger _log;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="serviceProvider">	The service provider. </param>
		public ConfigurableBootstrapper(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			_log = LoggerFactory.CreateLogger(typeof(ConfigurableBootstrapper));
			ApplicationSettings = ServiceProvider.GetRequiredService<IApplicationSettingsService>().Get();
		}

		#endregion

		#region Properties

		/// <summary>	Gets the application settings. </summary>
		/// <value>	The application settings. </value>
		protected IApplicationSettings ApplicationSettings { get; }

		#endregion

		#region Nancy

		/// <summary>	Gets the internal configuration. </summary>
		/// <value>	The internal configuration. </value>
		protected override Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration
		{
			get
			{
				_log.LogDebug($"Configuring nancy to use '{nameof(CustomResourceAssemblyProvider)}' as ResourceAssemblyProvider...");
				return NancyInternalConfiguration.WithOverrides(x => x.ResourceAssemblyProvider =
					typeof(CustomResourceAssemblyProvider));
			}
		}

		/// <summary>	Application startup. </summary>
		/// <param name="container">	The container. </param>
		/// <param name="pipelines">	The pipelines. </param>
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
			base.ApplicationStartup(container, pipelines);
			container.Register(ApplicationSettings);

			// enable csrf if configured
			if (!ApplicationSettings.EnableCsrf) return;
			_log.LogInformation("Enabling csrf...");
			Nancy.Security.Csrf.Enable(pipelines);
		}

		#endregion

		#region Methods

		/// <summary>	Configure cryptography. </summary>
		/// <param name="container">	The container. </param>
		protected virtual void ConfigureCryptography(TinyIoCContainer container)
		{
			_log.LogInformation("Configuring Encryption...");

			var config = new CryptographyConfiguration(
				new AesEncryptionProvider(
					new PassphraseKeyGenerator(
						ApplicationSettings.EncryptionKey,
						new byte[] {5, 2, 7, 5, 9, 1, 2, 1})),
				new DefaultHmacProvider(
					new PassphraseKeyGenerator(
						ApplicationSettings.HmacKey,
						new byte[] {2, 7, 3, 8, 1, 1, 8, 1})));

			container.Register(config);
		}

		#endregion
	}
}