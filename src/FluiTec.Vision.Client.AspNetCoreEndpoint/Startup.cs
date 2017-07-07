﻿using FluiTec.Vision.Client.AspNetCoreEndpoint.StartUpExtensions;
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
		/// <summary>	A startup. </summary>
		/// <remarks>
		/// 1: Startup - used to initialize configuration
		/// 2: ConfigureServices - loads all required services
		/// 3: Configure - actual pipeline using the services
		/// </remarks>
		public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
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
		        .ConfigureDataServices(Configuration)
		        .AddMvc();
        }

        /// <summary>	Configures. </summary>
        /// <param name="app">				The application. </param>
        /// <param name="env">				The environment. </param>
        /// <param name="loggerFactory">	The logger factory. </param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app
	            .UseLogging(Configuration, loggerFactory)
				.UseMvc();
        }
    }
}
