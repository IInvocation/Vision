using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Identity;
using FluiTec.AppFx.Identity.Dapper.Mssql;
using FluiTec.AppFx.IdentityServer;
using FluiTec.AppFx.IdentityServer.Dapper.Mssql;
using FluiTec.Vision.Server.Host.AspCoreHost.Configuration;
using FuiTec.AppFx.Mail.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.Vision.Server.Host.AspCoreHost.StartUpExtensions
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
			services.AddSingleton<IDapperServiceOptions>(new ConfigurationSettingsService<MssqlDapperServiceOptions>(configuration, configKey: "Dapper").Get());
			services.AddSingleton(new ConfigurationSettingsService<MailServiceOptions>(configuration, configKey: "Mail").Get());

			services.AddScoped<IIdentityDataService, MssqlDapperIdentityDataService>();
			services.AddScoped<IIdentityServerDataService, MssqlDapperIdentityServerDataService>();

			return services;
		}
	}
}