using FluiTec.AppFx.Options;
using FluiTec.Vision.Server.Host.AspCoreHost.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.Vision.Server.Host.AspCoreHost.StartUpExtensions
{
    /// <summary>	An error handling extension. </summary>
    public static class ErrorHandlingExtension
    {
	    /// <summary>	An IServiceCollection extension method that configure error handling. </summary>
	    /// <param name="services">			The services to act on. </param>
	    /// <param name="configuration">	The configuration. </param>
	    /// <returns>	An IServiceCollection. </returns>
	    public static IServiceCollection ConfigureErrorHandling(this IServiceCollection services,
		    IConfigurationRoot configuration)
	    {
		    services.AddSingleton(configuration.GetConfiguration<ErrorOptions>());
			return services;
	    }

	    /// <summary>	An IApplicationBuilder extension method that use hosting services. </summary>
	    /// <param name="app">				The app to act on. </param>
	    /// <param name="environment">  	The environment. </param>
	    /// <returns>	An IApplicationBuilder. </returns>
	    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app, IHostingEnvironment environment)
	    {
		    if (environment.IsDevelopment())
		    {
			    app.UseDeveloperExceptionPage();
		    }
		    else
		    {
			    app.UseExceptionHandler(errorHandlingPath: "/Home/Error");
		    }

		    return app;
	    }
	}
}
