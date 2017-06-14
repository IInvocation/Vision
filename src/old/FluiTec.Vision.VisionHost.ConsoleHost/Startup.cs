using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using FluiTec.AppFx.Globalization.Services;
using FluiTec.AppFx.Logging.Services;
using FluiTec.AppFx.Proxy.Services;
using FluiTec.Vision.VisionHost.ConsoleHost.Extensions;
using FluiTec.Vision.VisionHost.ConsoleHost.Services;
using FluiTec.Vision.VisionHost.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.VisionHost.ConsoleHost
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

				// register settings-services
				services.AddSingleton<ILoggerSettingsService, ConfigLoggerSettingsService>();
				services.AddSingleton<IApplicationSettingsService, ConfigApplicationSettingsService>();
				services.AddSingleton<IProxySettingsService, ConfigProxySettingsService>();
				services.AddSingleton<IGlobalizationSettingsService, ConfigGlobalizationSettingsService>();

				// register services for openid-connect
				services.AddAuthentication();
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

			// configure authentication
			application.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationScheme = "Cookies"
			});

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

			application.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
			{
				AuthenticationScheme = "oidc",
				SignInScheme = "Cookies",

				Authority = "http://localhost:1234",
				RequireHttpsMetadata = false,

				ClientId = "mvc",
				SaveTokens = true,

				AutomaticChallenge = true,
				AutomaticAuthenticate = true
			});

			// nancy
			application.ConfigureNancy(loggerFactory);

			// auto-start browser
			UseBrowser(application, environment);
		}

		/// <summary>	Use browser. </summary>
		///
		/// <param name="application">	The application. </param>
		/// <param name="environment">	The environment. </param>
		/// <remarks>
		/// Automatically starts the browser using any configured proxy in Development-Hosting		 
		/// </remarks>
		protected virtual void UseBrowser(IApplicationBuilder application, IHostingEnvironment environment)
		{
			if (environment.IsDevelopment())
				application.UseBrowser();
		}

		#endregion
	}
}