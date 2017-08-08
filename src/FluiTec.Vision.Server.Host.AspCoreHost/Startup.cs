using FluiTec.Vision.Server.Host.AspCoreHost.Services;
using FluiTec.Vision.Server.Host.AspCoreHost.StartUpExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Server.Host.AspCoreHost
{
	/// <summary>	A startup. </summary>
	/// <remarks>
	///     1: Startup - used to initialize configuration
	///     2: ConfigureServices - loads all required services
	///     3: Configure - actual pipeline using the services
	/// </remarks>
	public class Startup
	{
		#region Properties

		/// <summary>	Gets the configuration. </summary>
		/// <value>	The configuration. </value>
		public IConfigurationRoot Configuration { get; }

		#endregion

		#region AspNetCore

		/// <summary>	Constructor. </summary>
		/// <param name="env">	The environment. </param>
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile(path: "appsettings.secret.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			builder.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		/// <summary>	Configure services. </summary>
		/// <param name="services">	The services. </param>
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.ConfigureMailService(Configuration)
				.ConfigureDataServices(Configuration)
				.ConfigureIdentity(Configuration)
				.ConfigureLocalization(Configuration)
				.ConfigureIdentityServer(Configuration)
				.ConfigureStatusCodeHandler(Configuration)
				.ConfigureMvc(Configuration);

			// Add application services.
			services.AddTransient<ISmsSender, AuthMessageSender>();
		}

		/// <summary>	Configures. </summary>
		/// <param name="app">				The application. </param>
		/// <param name="env">				The environment. </param>
		/// <param name="loggerFactory">	The logger factory. </param>
		/// <param name="appLifetime">  	The application lifetime. </param>
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
			IApplicationLifetime appLifetime)
		{
			app
				.UseLogging(env, appLifetime, Configuration, loggerFactory)
				.UseHostingServices(env)
				.UseStaticFiles(Configuration)
				.UseIdentity()
				.UseIdentityServer(Configuration)
				.UseGoogleAuthentication(Configuration)
				.UseAmazonAuthentication(Configuration)
				.UseLocalization(Configuration)
				.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod())
				.UseStatusCodeHandler(Configuration)
				.UseMvc(Configuration);
		}

		#endregion
	}
}