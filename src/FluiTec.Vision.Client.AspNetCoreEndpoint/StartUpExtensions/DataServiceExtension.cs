using FluiTec.AppFx.Data.LiteDb;
using FluiTec.Vision.Client.AspNetCoreEndpoint.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.Vision.Client.AspNetCoreEndpoint.StartUpExtensions
{
	/// <summary>	A data service extension. </summary>
	public static class DataServiceExtension
	{
		/// <summary>	An IServiceCollection extension method that configure data services. </summary>
		/// <param name="services">			The services to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection ConfigureDataServices(this IServiceCollection services,
			IConfigurationRoot configuration)
		{
			//services.AddSingleton<ILiteDbServiceOptions>(new ConfigurationSettingsService<LiteDbServiceOptions>(configuration, configKey: "LiteDb").Get());

			//	services.AddScoped<IIdentityDataService, MssqlDapperIdentityDataService>();
			//	services.AddScoped<IIdentityServerDataService, MssqlDapperIdentityServerDataService>();

			return services;
		}
	}
}
