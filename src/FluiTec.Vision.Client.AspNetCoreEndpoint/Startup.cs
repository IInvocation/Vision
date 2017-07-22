using FluiTec.Vision.Client.AspNetCoreEndpoint.StartUpExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Client.AspNetCoreEndpoint
{
	/// <summary>	A startup. </summary>
	public class Startup
	{
		/// <summary>	Constructor. </summary>
		/// <param name="env">	The environment. </param>
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile(path: "appsettings.secret.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddJsonFile(Program.GetServerFileLocation(), optional: false)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		/// <summary>	Gets the configuration. </summary>
		/// <value>	The configuration. </value>
		public IConfigurationRoot Configuration { get; }

		/// <summary>	Configure services. </summary>
		/// <param name="services">	The services. </param>
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.ConfigureLocalization(Configuration)
				.ConfigureDataServices(Configuration)
				.ConfigureOpenIdConnect(Configuration)
				.ConfigureMvc(Configuration);
		}

		/// <summary>	Configures. </summary>
		/// <param name="app">				The application. </param>
		/// <param name="env">				The environment. </param>
		/// <param name="loggerFactory">	The logger factory. </param>
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			app
				.UseLogging(Configuration, loggerFactory)
				.UseHostingServices(env)
				.UseStaticFiles()
				.UseLocalization(Configuration)
				.UseOpenIdConnect(Configuration)
				.UseMvc(Configuration);
		}
	}
}