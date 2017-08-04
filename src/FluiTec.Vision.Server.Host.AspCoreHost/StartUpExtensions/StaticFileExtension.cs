using FluiTec.AppFx.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

namespace FluiTec.Vision.Server.Host.AspCoreHost.StartUpExtensions
{
	/// <summary>	A static file extension. </summary>
	public static class StaticFileExtension
	{

		/// <summary>	An IApplicationBuilder extension method that use static files. </summary>
		/// <param name="app">				The app to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseStaticFiles(this IApplicationBuilder app, IConfigurationRoot configuration)
		{
			var cacheDuration = configuration.GetConfiguration<Configuration.StaticFileOptions>().CacheDuration;
			app.UseStaticFiles(new StaticFileOptions
			{
				OnPrepareResponse = ctx =>
				{
					ctx.Context.Response.Headers[HeaderNames.CacheControl] =
						"public,max-age=" + cacheDuration;
				}
			});

			return app;
		}
	}
}