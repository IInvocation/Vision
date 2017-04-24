using System;
using FluiTec.AppFx.Globalization.Services;
using FluiTec.AppFx.Logging.Services;
using FluiTec.AppFx.Proxy.Services;
using FluiTec.Vision.VisionHost.AspCoreHost.Extensions;
using FluiTec.Vision.VisionHost.AspCoreHost.Services;
using FluiTec.Vision.VisionHost.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.VisionHost.AspCoreHost
{
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

		    // nancy
		    application.ConfigureNancy(loggerFactory);
	    }

	    #endregion
	}
}
