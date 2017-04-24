using FluiTec.Vision.NancyFx.Authentication.GoogleOpenId.Services;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.Vision.AuthHost.ConsoleHost.Extensions
{
    public static class GoogleExtension
    {
	    /// <summary>	An IApplicationBuilder extension method that configure nancy. </summary>
	    /// <param name="application">  	The application to act on. </param>
	    /// <returns>	An IApplicationBuilder. </returns>
	    public static IApplicationBuilder ConfigureGoogleAuthentication(this IApplicationBuilder application)
	    {
		    var settings = application.ApplicationServices.GetRequiredService<IGoogleOpenIdProviderSettingsService>().Get();

		    application.UseGoogleAuthentication(new GoogleOptions
		    {
			    AuthenticationScheme = settings.AuthenticationScheme,
			    DisplayName = settings.DisplayName,
			    SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,

			    ClientId = settings.ClientId,
			    ClientSecret = settings.ClientSecret
		    });
			
			return application;
	    }
	}
}
