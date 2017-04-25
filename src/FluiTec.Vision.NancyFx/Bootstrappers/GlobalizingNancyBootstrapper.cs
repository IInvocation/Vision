using System;
using System.Globalization;
using FluiTec.AppFx.Globalization.Services;
using FluiTec.AppFx.Globalization.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.Culture;
using Nancy.TinyIoc;

namespace FluiTec.Vision.NancyFx.Bootstrappers
{
	/// <summary>	A globalizing nancy bootstrapper. </summary>
	public class GlobalizingNancyBootstrapper : RequestTracingBootstrapper
	{
		#region Fields

		/// <summary>	The log. </summary>
		private readonly ILogger _log;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="serviceProvider">	The service provider. </param>
		public GlobalizingNancyBootstrapper(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			_log = LoggerFactory.CreateLogger(typeof(GlobalizingNancyBootstrapper));
		}

		#endregion

		#region Nancy

		/// <summary>	Configures the given environment. </summary>
		/// <param name="environment">	The environment. </param>
		public override void Configure(INancyEnvironment environment)
		{
			_log.LogInformation("Configuring nancy for globalization...");

			var settings = ServiceProvider.GetRequiredService<IGlobalizationSettingsService>().Get();
			_log.LogInformation("GlobalizationSettings loaded: {0}.", settings);

			environment.Globalization(settings.SupportedCultures, settings.DefaultCulture);

			ConfigureGlobalization(settings);

			base.Configure(environment);
		}

		protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
		{
			base.RequestStartup(container, pipelines, context);

			_log.LogDebug("Request[{0}]: Configuring RequestCulture...", context.RequestId());

			var cultureService = container.Resolve<ICultureService>();
			var culture = cultureService.DetermineCurrentCulture(context);
			_log.LogDebug("Request[{0}]: RequestCulture is {1}.", context.RequestId(), culture);

			ConfigureCulture(culture);
		}

		#endregion

		#region Methods

		/// <summary>	Configure globalization. </summary>
		/// <param name="globalizationSettings">	The globalization settings. </param>
		protected virtual void ConfigureGlobalization(IGlobalizationSettings globalizationSettings)
		{
		}

		/// <summary>	Configure culture. </summary>
		/// <param name="requestCulture">	The request culture. </param>
		protected virtual void ConfigureCulture(CultureInfo requestCulture)
		{
			CultureInfo.CurrentUICulture = requestCulture;
		}

		#endregion
	}
}