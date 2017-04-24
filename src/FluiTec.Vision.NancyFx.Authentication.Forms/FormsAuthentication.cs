using System;
using System.Linq;
using FluentValidation;
using FluiTec.Vision.NancyFx.Authentication.Forms.Settings;
using FluiTec.Vision.NancyFx.Authentication.Forms.Validators;
using FluiTec.Vision.NancyFx.Authentication.Services;
using FluiTec.Vision.NancyFx.Authentication.Settings;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Cookies;
using Nancy.Cryptography;
using Nancy.Extensions;
using Nancy.Helpers;
using Nancy.TinyIoc;

namespace FluiTec.Vision.NancyFx.Authentication.Forms
{
	/// <summary>	The forms authentication helper. </summary>
	public static class FormsAuthentication
	{
		#region Login

		/// <summary>	User logged in redirect response. </summary>
		/// <param name="context">			  	The context. </param>
		/// <param name="userIdentifier">	  	Identifier for the user. </param>
		/// <param name="cookieExpiry">		  	(Optional) The cookie expiry. </param>
		/// <param name="fallbackRedirectUrl">	(Optional) URL of the fallback redirect. </param>
		/// <returns>	A Response. </returns>
		public static Response LogInAndRedirectResponse(NancyContext context, Guid userIdentifier,
			DateTime? cookieExpiry = null, string fallbackRedirectUrl = null)
		{
			// create redirect response
			var response = LogInRedirectResponse(context, fallbackRedirectUrl);

			// build a cookie and attach it to the response
			var authenticationCookie = BuildCookie(userIdentifier, cookieExpiry);
			response.WithCookie(authenticationCookie);

			return response;
		}

		/// <summary>	Logs in redirect response. </summary>
		/// <param name="context">			  	The context. </param>
		/// <param name="fallbackRedirectUrl">	(Optional) URL of the fallback redirect. </param>
		/// <returns>	A Response. </returns>
		public static Response LogInRedirectResponse(NancyContext context, string fallbackRedirectUrl = null)
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

		/// <summary>	Logs out and redirect response. </summary>
		/// <param name="context">	  	The context. </param>
		/// <param name="redirectUrl">	URL of the redirect. </param>
		/// <returns>	A Response. </returns>
		public static Response LogOutAndRedirectResponse(NancyContext context, string redirectUrl = "/")
		{
			var response = LogOutRedirectResponse(context, redirectUrl);
			var authenticationCookie = BuildLogoutCookie();
			response.WithCookie(authenticationCookie);

			return response;
		}

		/// <summary>	Logs out redirect response. </summary>
		/// <param name="context">	  	The context. </param>
		/// <param name="redirectUrl">	URL of the redirect. </param>
		/// <returns>	A Response. </returns>
		public static Response LogOutRedirectResponse(NancyContext context, string redirectUrl = "/")
		{
			return context.GetRedirect(redirectUrl);
		}

		#endregion

		#region Cookies

		/// <summary>
		///     Build the forms authentication cookie
		/// </summary>
		/// <param name="userIdentifier">Authenticated user identifier</param>
		/// <param name="cookieExpiry">Optional expiry date for the cookie (for 'Remember me')</param>
		/// <returns>Nancy cookie instance</returns>
		private static INancyCookie BuildCookie(Guid userIdentifier, DateTime? cookieExpiry)
		{
			var cookieContents = EncryptAndSignCookie(userIdentifier.ToString());

			var cookie = new NancyCookie(CookieName, cookieContents, true, false, cookieExpiry);

			return cookie;
		}

		/// <summary>	Builds logout cookie. </summary>
		/// <returns>	An INancyCookie. </returns>
		private static INancyCookie BuildLogoutCookie()
		{
			var cookie = new NancyCookie(CookieName, string.Empty, true, false, DateTime.Now.AddDays(-1));

			return cookie;
		}

		#endregion

		#region Cryptography

		/// <summary>
		///     Encrypt and sign the cookie contents
		/// </summary>
		/// <param name="cookieValue">Plain text cookie value</param>
		/// <returns>Encrypted and signed string</returns>
		private static string EncryptAndSignCookie(string cookieValue)
		{
			var encryptedCookie = _cryptographyConfiguration.EncryptionProvider.Encrypt(cookieValue);
			var hmacBytes = GenerateHmac(encryptedCookie);
			var hmacString = Convert.ToBase64String(hmacBytes);

			return $"{hmacString}{encryptedCookie}";
		}

		/// <summary>
		///     Generate a hmac for the encrypted cookie string
		/// </summary>
		/// <param name="encryptedCookie">Encrypted cookie string</param>
		/// <returns>Hmac byte array</returns>
		private static byte[] GenerateHmac(string encryptedCookie)
		{
			return _cryptographyConfiguration.HmacProvider.GenerateHmac(encryptedCookie);
		}

		#endregion

		#region Fields

		/// <summary>	The forms authentication settings. </summary>
		private static IFormsAuthenticationSettings _formsAuthenticationSettings;

		/// <summary>	The authentication settings. </summary>
		private static IAuthenticationSettings _authenticationSettings;

		/// <summary>	The cryptography configuration. </summary>
		private static CryptographyConfiguration _cryptographyConfiguration;

		/// <summary>	The user service. </summary>
		private static IUserService _userService;

		/// <summary>	Name of the cookie. </summary>
		public static string CookieName => "_nfac";

		#endregion

		#region NancyExtensions

		/// <summary>	The IPipelines extension method that enables the forms authentication. </summary>
		/// <param name="pipelines">	The pipelines to act on. </param>
		/// <param name="container">	The container. </param>
		/// <remarks>
		///     Relies on the IoCContainer returning a valid <see cref="IFormsAuthenticationSettings" />.
		/// </remarks>
		public static void EnableFormsAuthentication(this IPipelines pipelines, TinyIoCContainer container)
		{
			pipelines.EnableFormsAuthentication(container, container.Resolve<IFormsAuthenticationSettings>(),
				container.Resolve<IAuthenticationSettings>());
		}

		/// <summary>	The IPipelines extension method that enables the forms authentication. </summary>
		/// <param name="pipelines">			 	The pipelines to act on. </param>
		/// <param name="container">			 	The container. </param>
		/// <param name="formsAuthenticationSettings">		 	The configuration. </param>
		/// <param name="authenticationSettings">	The authentication settings. </param>
		public static void EnableFormsAuthentication(this IPipelines pipelines, TinyIoCContainer container,
			IFormsAuthenticationSettings formsAuthenticationSettings, IAuthenticationSettings authenticationSettings)
		{
			ValidateConfiguration(formsAuthenticationSettings);
			_authenticationSettings = authenticationSettings ?? throw new ArgumentNullException(nameof(authenticationSettings));

			_formsAuthenticationSettings = formsAuthenticationSettings;
			_cryptographyConfiguration = container.Resolve<CryptographyConfiguration>();
			_userService = container.Resolve<IUserService>();

			// only try the authenticate ourselves if UseOwinAuthentication is disabled
			if (!_formsAuthenticationSettings.UseOwinAuthentication)
				pipelines.BeforeRequest.AddItemToStartOfPipeline(AuthenticateUsingCookieHook());

			pipelines.AfterRequest.AddItemToEndOfPipeline(
				GetRedirectToLoginHook(formsAuthenticationSettings, authenticationSettings));
		}

		#endregion

		#region HelperMethods

		/// <summary>	Gets redirect to login hook. </summary>
		/// <param name="formsAuthenticationSettings">	The configuration. </param>
		/// <param name="authenticationSettings">	  	The authentication settings. </param>
		/// <returns>	The redirect to login hook. </returns>
		private static Action<NancyContext> GetRedirectToLoginHook(IFormsAuthenticationSettings formsAuthenticationSettings,
			IAuthenticationSettings authenticationSettings)
		{
			return context =>
			{
				// only redirect for unauthorized users
				if (context.Response.StatusCode != HttpStatusCode.Unauthorized) return;

				// only redirect browsers that request html
				if (!context.Request.Headers.Accept.Any(acc => acc.Item1.ToLower().Contains("html"))) return;

				// redirect to RedirectUrl preserving originally requested url
				context.Response = context.GetRedirect(
					$"{formsAuthenticationSettings.RedirectUrl}?{authenticationSettings.RedirectQuerystringKey}={context.ToFullPath("~" + context.Request.Path + HttpUtility.UrlEncode(context.Request.Url.Query))}");
			};
		}

		/// <summary>	Authenticate using cookie hook. </summary>
		/// <returns>	A Func&lt;NancyContext,Response&gt; </returns>
		private static Func<NancyContext, Response> AuthenticateUsingCookieHook()
		{
			return context =>
			{
				if (!context.Request.Cookies.ContainsKey(CookieName))
					return null;

				var encryptedCookieValue = context.Request.Cookies[CookieName];
				if (string.IsNullOrWhiteSpace(encryptedCookieValue))
					return null;

				var cookieValue = DecryptAndValidateAuthenticationCookie(encryptedCookieValue);
				if (string.IsNullOrEmpty(cookieValue) || !Guid.TryParse(cookieValue, out Guid userIdentifier))
					return null;

				context.CurrentUser = _userService.GetUserFromIdentifier(userIdentifier);

				return null;
			};
		}

		/// <summary>
		///     Decrypt and validate an encrypted and signed cookie value
		/// </summary>
		/// <param name="cookieValue">Encrypted and signed cookie value</param>
		/// <returns>Decrypted value, or empty on error or if failed validation</returns>
		private static string DecryptAndValidateAuthenticationCookie(string cookieValue)
		{
			var hmacStringLength = Base64Helpers.GetBase64Length(_cryptographyConfiguration.HmacProvider.HmacLength);

			var encryptedCookie = cookieValue.Substring(hmacStringLength);
			var hmacString = cookieValue.Substring(0, hmacStringLength);

			var encryptionProvider = _cryptographyConfiguration.EncryptionProvider;

			// Check the hmacs, but don't early exit if they don't match
			var hmacBytes = Convert.FromBase64String(hmacString);
			var newHmac = GenerateHmac(encryptedCookie);
			var hmacValid = HmacComparer.Compare(newHmac, hmacBytes, _cryptographyConfiguration.HmacProvider.HmacLength);

			var decrypted = encryptionProvider.Decrypt(encryptedCookie);

			// Only return the decrypted result if the hmac was ok
			return hmacValid ? decrypted : string.Empty;
		}

		/// <summary>	Validates the configuration described by configuration. </summary>
		/// <exception cref="ArgumentNullException">
		///     Thrown when one or more required arguments are
		///     null.
		/// </exception>
		/// <exception cref="ValidationException">  	Thrown when a Validation error condition occurs. </exception>
		/// <param name="configuration">	The configuration. </param>
		private static void ValidateConfiguration(IFormsAuthenticationSettings configuration)
		{
			// check for null
			if (configuration == null)
				throw new ArgumentNullException(nameof(configuration));

			// validate using FluentValidation
			var validator = new FormsAuthenticationConfigurationValidator();
			var result = validator.Validate(configuration);

			// throw if necessary
			if (!result.IsValid)
				throw new ValidationException("Invalid FormsAuthenticationConfiguration", result.Errors);
		}

		#endregion
	}
}