using FluiTec.AppFx.Authentication.Amazon;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace FluiTec.Vision.Server.Host.AspCoreHost.StartUpExtensions
{
	/// <summary>	An amazon extension. </summary>
	public static class AmazonExtension
	{
		/// <summary>
		///     An IApplicationBuilder extension method that use amazon authentication.
		/// </summary>
		/// <param name="app">				The app to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseAmazonAuthentication(this IApplicationBuilder app,
			IConfigurationRoot configuration)
		{
			app.UseAmazonAuthentication(new AmazonOptions
			{
				ClientId = configuration[key: "Authentication:Amazon:ClientId"],
				ClientSecret = configuration[key: "Authentication:Amazon:ClientSecret"]
			});

			return app;
		}
	}
}