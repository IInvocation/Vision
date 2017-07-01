using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace FluiTec.Vision.Server.Host.AspCoreHost.StartUpExtensions
{
	/// <summary>	A hosting extension. </summary>
	public static class HostingExtension
	{
		/// <summary>	An IApplicationBuilder extension method that use hosting services. </summary>
		/// <param name="app">		  	The app to act on. </param>
		/// <param name="environment">	The environment. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseHostingServices(this IApplicationBuilder app, IHostingEnvironment environment)
		{
			if (environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler(errorHandlingPath: "/Home/Error");
			}

			return app;
		}
	}
}