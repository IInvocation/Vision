using System;
using System.Collections.Generic;
using System.Linq;
using FluiTec.Vision.NancyFx.Authentication.OpenId.Services;
using FluiTec.Vision.NancyFx.Authentication.OpenId.ViewModels;
using FluiTec.Vision.NancyFx.Authentication.Owin;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;

namespace FluiTec.Vision.NancyFx.Authentication.OpenId.Modules
{
	public class OpenIdAccountModule : NancyModule
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="loggerFactory">						The logger factory. </param>
		/// <param name="owinAuthenticationSettingsService">	The owin authentication settings service. </param>
		/// <param name="authenticateHandlers">					The authenticate handlers. </param>
		public OpenIdAccountModule(ILoggerFactory loggerFactory,
			IOpenIdAuthenticationSettingsService owinAuthenticationSettingsService,
			IEnumerable<IOpenIdAuthenticateHandler> authenticateHandlers)
		{
			_logger = loggerFactory.CreateLogger(typeof(OpenIdAccountModule));
			_handlers = authenticateHandlers;
			var owinAuthenticationSettings = owinAuthenticationSettingsService.Get();

			Post(owinAuthenticationSettings.ExternalLoginRoute, parameters => POST_ExternalLogin(parameters));

			foreach (var h in _handlers)
			{
				var redirectUri = new Uri(h.Settings.RedirectUri);
				Get(redirectUri.AbsolutePath, _ => GET_ExternalSignIn(h));
			}
		}

		#endregion

		#region RouteHandlers

		/// <summary>	Posts an external login. </summary>
		/// <param name="parameters">	Options for controlling the operation. </param>
		/// <returns>	A dynamic. </returns>
		private dynamic POST_ExternalLogin(dynamic parameters)
		{
			_logger.LogRouteHandler(Context, nameof(POST_ExternalLogin));

			// validate and return to home if invalid
			var vm = this.BindAndValidate<LoginProviderViewModel>();
			if (!Context.ModelValidationResult.IsValid)
				return Context.GetRedirect("/");

			// get the associated provider
			var provider = Context.GetExternalAuthenticationSchemes()
				.SingleOrDefault(s => s.AuthenticationScheme == vm.ProviderName);

			// return BadRequest because of missing AuthenticationScheme
			if (provider == null)
				return Negotiate.WithModel("Invalid AuthenticationScheme").WithStatusCode(HttpStatusCode.BadRequest);

			// check if anybody registered a fitting handler
			if (_handlers != null && _handlers.All(h => h.Name != vm.ProviderName))
				return Negotiate.WithModel("Missing AuthenticationHandler").WithStatusCode(HttpStatusCode.InternalServerError);

			// get the handler
			var handler = _handlers.First(h => h.Name == vm.ProviderName);

			// challenge the user
			return handler.Challenge(Context, "/");
		}

		/// <summary>	Gets external sign in. </summary>
		/// <param name="openIdAuthenticateHandler">	The open identifier authenticate handler. </param>
		/// <returns>	The external sign in. </returns>
		private object GET_ExternalSignIn(IOpenIdAuthenticateHandler openIdAuthenticateHandler)
		{
			return openIdAuthenticateHandler.ValidateChallenge(Context);
		}

		#endregion

		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		/// <summary>	The handlers. </summary>
		private readonly IEnumerable<IOpenIdAuthenticateHandler> _handlers;

		#endregion
	}
}