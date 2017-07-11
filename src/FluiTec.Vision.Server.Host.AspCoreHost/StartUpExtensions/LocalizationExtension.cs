using System.Globalization;
using System.Linq;
using FluiTec.AppFx.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using FluiTec.Vision.Server.Host.AspCoreHost.Configuration;

namespace FluiTec.Vision.Server.Host.AspCoreHost.StartUpExtensions
{
	/// <summary>	A localization extension. </summary>
	public static class LocalizationExtension
	{
		/// <summary>	An IServiceCollection extension method that configure localization. </summary>
		/// <param name="services">			The services to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection ConfigureLocalization(this IServiceCollection services,
			IConfigurationRoot configuration)
		{
			var locConfig = configuration.GetConfiguration<CultureOptions>();

			services.Configure<RequestLocalizationOptions>(options =>
			{
				var supportedCultures = locConfig.SupportedCultures.Select(e => new CultureInfo(e)).ToList();
				options.DefaultRequestCulture = new RequestCulture(locConfig.DefaultCulture);
				options.SupportedCultures = supportedCultures;
				options.SupportedUICultures = supportedCultures;
			});

			services.AddLocalization(options => options.ResourcesPath = "Resources");

			return services;
		}

		/// <summary>	An IApplicationBuilder extension method that use localization. </summary>
		/// <param name="app">				The app to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseLocalization(this IApplicationBuilder app, IConfigurationRoot configuration)
		{
			var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
			app.UseRequestLocalization(options.Value);

			return app;
		}
	}
}