using System;
using System.Linq;
using FluiTec.AppFx.Authentication.Data;
using FluiTec.AppFx.Globalization.Services;
using FluiTec.AppFx.Logging.Services;
using FluiTec.AppFx.Proxy.Services;
using FluiTec.AppFx.Signing.Services;
using FluiTec.Vision.AuthHost.AspCoreHost.Extensions;
using FluiTec.Vision.AuthHost.AspCoreHost.Services;
using FluiTec.Vision.AuthHost.Services;
using FluiTec.Vision.NancyFx.Authentication.Forms.Services;
using FluiTec.Vision.NancyFx.Authentication.GoogleOpenId.Services;
using FluiTec.Vision.NancyFx.Authentication.OpenId.Services;
using FluiTec.Vision.NancyFx.Authentication.Services;
using FluiTec.Vision.Server.Data;
using FluiTec.Vision.Server.Data.Mssql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.AuthHost.AspCoreHost
{
	/// <summary>	A startup. </summary>
	public class Startup
	{
		#region Fields

		/// <summary>	The configuration. </summary>
		private readonly IConfiguration _configuration;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="environment">	The environment. </param>
		public Startup(IHostingEnvironment environment)
		{
			// load configuration
			_configuration = new ConfigurationBuilder()
				.SetBasePath(environment.ContentRootPath)
				.AddJsonFile("appsettings.json", true, true)
				.AddJsonFile("Secrets/appsettings.secret.json", true, true)
				.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true)
				.AddEnvironmentVariables()
				.Build();
		}

		#endregion

		#region HostingStartup

		/// <summary>	Configure services. </summary>
		/// <param name="services">	The services. </param>
		public void ConfigureServices(IServiceCollection services)
		{
			try
			{
				// register configuration
				services.AddSingleton(_configuration);

				// precreate some settings-services
				var applicationSettingsService = new ConfigApplicationSettingsService(_configuration);
				var applicationSettings = applicationSettingsService.Get();
				var loggingSettingsService = new ConfigLoggerSettingsService(_configuration);
				var proxySettingsService = new ConfigProxySettingsService(_configuration);
				var signingService = new FileSigningService(new ConfigNameFileSigningSettings(_configuration).Get());

				// register settings-services
				services.AddSingleton<ILoggerSettingsService, ConfigLoggerSettingsService>(provider => loggingSettingsService);
				services.AddSingleton<IApplicationSettingsService>(provider => applicationSettingsService);
				services.AddSingleton<IDataServiceSettings>(provider => applicationSettings);
				services.AddSingleton<IProxySettingsService, ConfigProxySettingsService>(provider => proxySettingsService);
				services.AddSingleton<IFormsAuthenticationSettingsService, ConfigFormsAuthenticationSettingsService>();
				services.AddSingleton<IGlobalizationSettingsService, ConfigGlobalizationSettingsService>();
				services.AddSingleton<IAuthenticationSettingsService, ConfigAuthenticationSettingsService>();
				services.AddSingleton<IGoogleOpenIdProviderSettingsService, ConfigGoogleOpenIdProviderSettingsService>();
				services.AddSingleton<IOpenIdAuthenticationSettingsService, ConfigOpenIdAuthenticationSettingsService>();
				services.AddTransient<IAuthenticatingDataService, VisionDataService>();
				services.AddTransient<IUserService, UserService>();
				services.AddSingleton<ISigningService, FileSigningService>(provider => signingService);

				// configure identityserver
				var environment = (IHostingEnvironment)services.FirstOrDefault(t => t.ServiceType == typeof(IHostingEnvironment)).ImplementationInstance;
				services.ConfigureIdentityServer(environment, signingService, proxySettingsService);
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error in {nameof(ConfigureServices)}:");
				Console.WriteLine(e);
				throw;
			}
		}

		/// <summary>	Configures. </summary>
		/// <param name="application">  	The application. </param>
		/// <param name="environment">  	The environment. </param>
		/// <param name="loggerFactory">	The logger factory. </param>
		public void Configure(IApplicationBuilder application, IHostingEnvironment environment, ILoggerFactory loggerFactory)
		{
			// basic config
			application.ConfigureLogging(loggerFactory);
			application.ConfigureProxy();

			// authentication config
			application.ConfigureCookieAuthentication(application.ApplicationServices.GetRequiredService<IAuthenticationSettingsService>().Get());
			application.UseIdentityServer();
			application.ConfigureGoogleAuthentication();

			// nancy
			application.ConfigureNancy(loggerFactory);
		}

		#endregion
	}
}