using FluiTec.Vision.NancyFx.Authentication;
using FluiTec.Vision.NancyFx.Authentication.Settings;
using Microsoft.AspNetCore.Builder;

namespace FluiTec.Vision.AuthHost.AspCoreHost.Extensions
{
	/// <summary>	A cookie extension. </summary>
	public static class CookieExtension
	{
		/// <summary>
		/// An IApplicationBuilder extension method that configure cookie authentication.
		/// </summary>
		///
		/// <param name="application">	The application to act on. </param>
		/// <param name="settings">   	Options for controlling the operation. </param>
		///
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder ConfigureCookieAuthentication(this IApplicationBuilder application, IAuthenticationSettings settings)
		{
			application.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationScheme = AuthenticationTypes.OwinCookie,
				AutomaticAuthenticate = true,
				AutomaticChallenge = false,
				ClaimsIssuer = settings.ClaimsIssuer
			});

			return application;
		}
	}
}