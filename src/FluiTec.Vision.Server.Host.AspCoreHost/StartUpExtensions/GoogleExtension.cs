using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace FluiTec.Vision.Server.Host.AspCoreHost.StartUpExtensions
{
	public static class GoogleExtension
	{
		/// <summary>
		///     An IApplicationBuilder extension method that use google authentication.
		/// </summary>
		/// <param name="app">				The app to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseGoogleAuthentication(this IApplicationBuilder app,
			IConfigurationRoot configuration)
		{
			app.UseGoogleAuthentication(new GoogleOptions
			{
				ClientId = configuration[key: "Authentication:Google:ClientId"],
				ClientSecret = configuration[key: "Authentication:Google:ClientSecret"]
			});

			return app;
		}
	}
}