using System;
using System.Security.Claims;
using FluiTec.Vision.NancyFx.Authentication.Results;
using FluiTec.Vision.NancyFx.Authentication.Services;
using FluiTec.Vision.NancyFx.Authentication.Settings;
using FluiTec.Vision.NancyFx.Authentication.ViewModels;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Extensions;
using Nancy.Helpers;
using Nancy.Security;

namespace FluiTec.Vision.NancyFx.Authentication.Owin.Services
{
	/// <summary>	An owin authentication service. </summary>
	public class OwinAuthenticationService : IAuthenticationService
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="loggerFactory">		 	The logger factory. </param>
		/// <param name="authenticationSettings">	The authentication settings. </param>
		public OwinAuthenticationService(ILoggerFactory loggerFactory, IAuthenticationSettings authenticationSettings)
		{
			_logger = loggerFactory.CreateLogger(typeof(OwinAuthenticationService));
			_authenticationSettings = authenticationSettings;
		}

		#endregion

		#region Fields

		/// <summary>	The authentication settings. </summary>
		private readonly IAuthenticationSettings _authenticationSettings;

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region IAuthenticationService

		/// <summary>	Login. </summary>
		/// <exception cref="ArgumentNullException">
		///     Thrown when one or more required arguments are
		///     null.
		/// </exception>
		/// <exception cref="ArgumentException">
		///     Thrown when one or more arguments have
		///     unsupported or illegal values.
		/// </exception>
		/// <param name="context">						The context. </param>
		/// <param name="validateCredentialsResult">	The validate credentials result. </param>
		/// <param name="loginViewModel">				The login view model. </param>
		/// <returns>	A Response. </returns>
		public Response Login(NancyContext context, IValidateCredentialsResult validateCredentialsResult,
			ILoginViewModel loginViewModel)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));
			if (validateCredentialsResult == null) throw new ArgumentNullException(nameof(validateCredentialsResult));
			if (loginViewModel == null) throw new ArgumentNullException(nameof(loginViewModel));
			if (validateCredentialsResult.Principal?.Identity == null ||
			    validateCredentialsResult.Principal.Identity.IsAuthenticated == false)
				throw new ArgumentException("Principal must be authenticated to log in.");

			return Login(context, validateCredentialsResult.Principal, loginViewModel.ReturnUrl);
		}

		/// <summary>	Login. </summary>
		/// <exception cref="InvalidOperationException">
		///     Thrown when the requested operation is
		///     invalid.
		/// </exception>
		/// <param name="context">  	The context. </param>
		/// <param name="principal">	The principal. </param>
		/// <param name="returnUrl">	URL of the return. </param>
		/// <returns>	A Response. </returns>
		public Response Login(NancyContext context, ClaimsPrincipal principal, string returnUrl)
		{
			if (principal == null || !principal.IsAuthenticated())
				throw new InvalidOperationException("Tried to sign in NULL or an unauthenticated user");

			_logger.LogInformation("Request[{0}]: Signing in user '{1}'.", context.RequestId(),
				RequestExtensions.GetUserName(principal));

			context.SignIn(AuthenticationTypes.OwinCookie, principal);
			return LogInRedirectResponse(context, returnUrl);
		}

		/// <summary>	Logout. </summary>
		/// <param name="context">  	The context. </param>
		/// <returns>	A Response. </returns>
		public Response Logout(NancyContext context)
		{
			_logger.LogInformation("Request[{0}]: Signing out user '{1}'.", context.RequestId(),
				RequestExtensions.GetUserName(context.CurrentUser));

			context.SignOut(AuthenticationTypes.OwinCookie);
			return LogOutRedirectResponse(context, _authenticationSettings.LogoutRedirectUrl);
		}

		#endregion

		#region Responses

		/// <summary>	Logs in redirect response. </summary>
		/// <param name="context">			  	The context. </param>
		/// <param name="fallbackRedirectUrl">	(Optional) URL of the fallback redirect. </param>
		/// <returns>	A Response. </returns>
		public Response LogInRedirectResponse(NancyContext context, string fallbackRedirectUrl = null)
		{
			var redirectUrl = fallbackRedirectUrl;

			// if no value was given, try redirecting to the base-path
			if (string.IsNullOrEmpty(redirectUrl))
				redirectUrl = context.Request.Url.BasePath;

			// if base-path didnt work as well - redirect to the absolute root
			if (string.IsNullOrEmpty(redirectUrl))
				redirectUrl = "/";

			var redirectQuerystringKey = _authenticationSettings.RedirectQuerystringKey;

			// try extracting the returnUrl off the querystring (e.g. redirect to the route the user originally wanted)
			if (context.Request.Headers.Referrer != null)
			{
				var query = new Uri(context.Request.Headers.Referrer).Query;
				var queryUrl = HttpUtility.ParseQueryString(query).Get(redirectQuerystringKey);

				if (context.IsLocalUrl(queryUrl))
					redirectUrl = queryUrl;
			}

			// create redirect response
			return context.GetRedirect(redirectUrl);
		}

		/// <summary>	Logs out redirect response. </summary>
		/// <param name="context">	  	The context. </param>
		/// <param name="redirectUrl">	(Optional) URL of the redirect. </param>
		/// <returns>	A Response. </returns>
		public Response LogOutRedirectResponse(NancyContext context, string redirectUrl = "/")
		{
			return context.GetRedirect(string.IsNullOrWhiteSpace(redirectUrl) ? "/" : redirectUrl);
		}

		#endregion
	}
}