using System;
using System.Collections.Generic;
using System.Linq;
using FluiTec.Vision.NancyFx.Authentication.Forms.Localization;
using FluiTec.Vision.NancyFx.Authentication.Forms.Settings;
using FluiTec.Vision.NancyFx.Authentication.Forms.ViewModels;
using FluiTec.Vision.NancyFx.Authentication.Owin;
using FluiTec.Vision.NancyFx.Authentication.Services;
using FluiTec.Vision.NancyFx.Authentication.Settings;
using FluiTec.Vision.NancyFx.Authentication.ViewModels;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Nancy.Validation;

namespace FluiTec.Vision.NancyFx.Authentication.Forms.Modules
{
	/// <summary>	An account module. </summary>
	public class AccountModule : NancyModule
	{
		#region Constructors

		/// <summary>	Default constructor. </summary>
		/// <exception cref="ArgumentNullException">
		///     Thrown when one or more required arguments are
		///     null.
		/// </exception>
		/// <param name="loggerFactory">			  	The logger factory. </param>
		/// <param name="formsAuthenticationSettings">	The configuration. </param>
		/// <param name="authenticationSettings">	  	The authentication settings. </param>
		/// <param name="userService">				  	The user service. </param>
		/// <param name="authenticationService">	  	The authentication service. </param>
		public AccountModule(ILoggerFactory loggerFactory, IFormsAuthenticationSettings formsAuthenticationSettings,
			IAuthenticationSettings authenticationSettings,
			IUserService userService, IAuthenticationService authenticationService)
		{
			_logger = loggerFactory.CreateLogger(typeof(AccountModule));

			_formsAuthenticationSettings = formsAuthenticationSettings ??
			                               throw new ArgumentNullException(nameof(formsAuthenticationSettings));
			_authenticationSettings = authenticationSettings ?? throw new ArgumentNullException(nameof(authenticationSettings));
			_userService = userService ?? throw new ArgumentNullException(nameof(userService));
			_authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));

			Get(formsAuthenticationSettings.LoginRoute, _ => GET_Login());
			Post(formsAuthenticationSettings.LoginRoute, _ => POST_Login());

			Get(formsAuthenticationSettings.LogoutRoute, _ => GET_Logout());

			Get(formsAuthenticationSettings.RegisterRoute, _ => GET_Register());
			Post(formsAuthenticationSettings.RegisterRoute, _ => POST_Register());

			Get(formsAuthenticationSettings.UnauthorizedRoute, _ => GET_Unauthorized());

			Get(formsAuthenticationSettings.ManageRoute, _ => GET_Manage());
		}

		#endregion

		#region Fields

		/// <summary>	The configuration. </summary>
		private readonly IFormsAuthenticationSettings _formsAuthenticationSettings;

		/// <summary>	The authentication settings. </summary>
		private readonly IAuthenticationSettings _authenticationSettings;

		/// <summary>	The user service. </summary>
		private readonly IUserService _userService;

		/// <summary>	The authentication service. </summary>
		private readonly IAuthenticationService _authenticationService;

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region RouteHandlers

		/// <summary>	[GET] Login. </summary>
		/// <returns>	Login. </returns>
		private dynamic GET_Login()
		{
			_logger.LogRouteHandler(Context, nameof(GET_Login));
			var vm = new LoginViewModel
			{
				ReturnUrl = Request.Query[_authenticationSettings.RedirectQuerystringKey],
				ExternalAuthenticationProviders = Context.GetExternalAuthenticationSchemes(),
				RememberLogin = true,
				RegisterUrl = !string.IsNullOrWhiteSpace(Request.Query[_authenticationSettings.RedirectQuerystringKey])
					? $"{_formsAuthenticationSettings.RegisterRoute}?{_authenticationSettings.RedirectQuerystringKey}={Request.Query[_authenticationSettings.RedirectQuerystringKey]}"
					: $"{_formsAuthenticationSettings.RegisterRoute}"
			};
			_logger.LogRouteHandler(Context, nameof(GET_Login), vm);
			return View[_formsAuthenticationSettings.LoginViewName, vm];
		}

		/// <summary>	[POST] Login. </summary>
		/// <returns>	Login. </returns>
		private dynamic POST_Login()
		{
			_logger.LogRouteHandler(Context, nameof(POST_Login));
			ILoginViewModel model = this.BindAndValidate<LoginViewModel>();
			_logger.LogRouteHandler(Context, nameof(POST_Login), model);

			// return password-stripped view for faults
			if (!Context.ModelValidationResult.IsValid)
			{
				model.Password = null;
				return View[_formsAuthenticationSettings.LoginViewName, model];
			}

			// validate using database
			var result = _userService.ValidateCredentials(model);

			// log the user in and redirect
			if (result.Succeeded)
				return _authenticationService.Login(Context, result, model);

			// return password-stripped view for faults
			model.Password = null;
			Context.ModelValidationResult.Errors.Add(new KeyValuePair<string, IList<ModelValidationError>>(string.Empty,
				new List<ModelValidationError>(
					result.Faults.Select(f => new ModelValidationError(string.Empty, ValidationResources.Localize(f))))));
			return View[_formsAuthenticationSettings.LoginViewName, model];
		}

		/// <summary>	[GET] Logout. </summary>
		/// <returns>	Logout. </returns>
		private dynamic GET_Logout()
		{
			_logger.LogRouteHandler(Context, nameof(GET_Logout));
			return _authenticationService.Logout(Context);
		}

		/// <summary>	[GET] Register. </summary>
		/// <returns>	Register. </returns>
		private dynamic GET_Register()
		{
			_logger.LogRouteHandler(Context, nameof(GET_Register));
			var vm = new LoginViewModel
			{
				ReturnUrl = Request.Query[_authenticationSettings.RedirectQuerystringKey],
				RememberLogin = true
			};
			_logger.LogRouteHandler(Context, nameof(GET_Register), vm);
			return View[_formsAuthenticationSettings.RegisterViewName, vm];
		}

		/// <summary>	[POST] Register. </summary>
		/// <returns>	Register. </returns>
		private dynamic POST_Register()
		{
			_logger.LogRouteHandler(Context, nameof(POST_Register));
			IRegisterViewModel model = this.BindAndValidate<RegisterViewModel>();
			_logger.LogRouteHandler(Context, nameof(POST_Register), model);

			// return password-stripped view for faults
			if (!Context.ModelValidationResult.IsValid)
			{
				model.Password = null;
				model.ConfirmationPassword = null;
				return View[_formsAuthenticationSettings.RegisterViewName, model];
			}

			// create the user
			var result = _userService.Create(model);

			// log the user in
			if (result.Succeeded)
				return _authenticationService.Login(Context, result.Principal, model.ReturnUrl);

			// return password-stripped view for faults
			model.Password = null;
			model.ConfirmationPassword = null;
			Context.ModelValidationResult.Errors.Add(new KeyValuePair<string, IList<ModelValidationError>>(string.Empty,
				new List<ModelValidationError>(
					result.Faults.Select(f => new ModelValidationError(string.Empty, ValidationResources.Localize(f))))));

			return View[_formsAuthenticationSettings.RegisterViewName, model];
		}

		/// <summary>	[GET] Unauthorized. </summary>
		/// <returns>	Unauthorized. </returns>
		private dynamic GET_Unauthorized()
		{
			_logger.LogRouteHandler(Context, nameof(GET_Unauthorized));
			return View[_formsAuthenticationSettings.UnauthorizedViewName];
		}

		/// <summary>	[GET] Manage. </summary>
		/// <returns>	Manage. </returns>
		private dynamic GET_Manage()
		{
			_logger.LogRouteHandler(Context, nameof(GET_Manage));
			this.RequiresAuthentication();
			return View[_formsAuthenticationSettings.ManageViewName];
		}

		#endregion
	}
}