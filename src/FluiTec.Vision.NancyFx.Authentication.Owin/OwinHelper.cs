using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Nancy;
using Nancy.Owin;

namespace FluiTec.Vision.NancyFx.Authentication.Owin
{
	public static class OwinHelper
	{
		/// <summary>	Gets HTTP context. </summary>
		/// <param name="context">	The context. </param>
		/// <returns>	The HTTP context. </returns>
		private static HttpContext GetHttpContext(NancyContext context)
		{
			var owin = context.GetOwinEnvironment();
			return (HttpContext) owin[typeof(HttpContext).FullName];
		}

		/// <summary>	Sign in. </summary>
		/// <param name="context">			   	The context. </param>
		/// <param name="authenticationScheme">	The authentication scheme. </param>
		/// <param name="principal">		   	The principal. </param>
		public static void SignIn(this NancyContext context, string authenticationScheme, ClaimsPrincipal principal)
		{
			var httpContext = GetHttpContext(context);

			var authenticationManager = httpContext.Authentication;

			authenticationManager.SignInAsync(authenticationScheme, principal).Wait();
		}

		/// <summary>	A NancyContext extension method that sign out. </summary>
		/// <param name="context">			   	The context. </param>
		/// <param name="authenticationScheme">	The authentication scheme. </param>
		public static void SignOut(this NancyContext context, string authenticationScheme)
		{
			var httpContext = GetHttpContext(context);

			var authenticationManager = httpContext.Authentication;

			authenticationManager.SignOutAsync(authenticationScheme);
		}

		/// <summary>	Gets the external authentication schemes in this collection. </summary>
		/// <param name="context">	The context. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the external authentication schemes
		///     in this collection.
		/// </returns>
		public static IEnumerable<AuthenticationDescription> GetExternalAuthenticationSchemes(this NancyContext context)
		{
			var httpContext = GetHttpContext(context);

			var authenticationManager = httpContext.Authentication;

			var authenticationSchemes = authenticationManager.GetAuthenticationSchemes();

			var allowedSchemes = authenticationSchemes.Where(s => !string.IsNullOrWhiteSpace(s.DisplayName));

			return allowedSchemes;
		}
	}
}