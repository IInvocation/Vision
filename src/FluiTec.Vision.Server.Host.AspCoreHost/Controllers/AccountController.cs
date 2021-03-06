﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FluiTec.Vision.Server.Host.AspCoreHost.Models.AccountViewModels;
using FluiTec.Vision.Server.Host.AspCoreHost.Services;
using FluiTec.AppFx.Identity.Entities;
using FluiTec.AppFx.Mail;
using FluiTec.Vision.Server.Host.AspCoreHost.Models.AccountMailViewModels;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
		private readonly UserManager<IdentityUserEntity> _userManager;
        private readonly SignInManager<IdentityUserEntity> _signInManager;
        private readonly ITemplatingMailService _emailSender;
	    private readonly ILogger _logger;
        private readonly string _externalCookieScheme;

	    public AccountController(
            UserManager<IdentityUserEntity> userManager,
            SignInManager<IdentityUserEntity> signInManager,
            IOptions<IdentityCookieOptions> identityCookieOptions,
            ITemplatingMailService emailSender,
            ILoggerFactory loggerFactory,
			IIdentityDataService dataService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            _emailSender = emailSender;
	        _logger = loggerFactory.CreateLogger<AccountController>();
        }

        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.Authentication.SignOutAsync(_externalCookieScheme);

            ViewData[index: "ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData[index: "ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation(eventId: 1, message: "User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(eventId: 2, message: "User account locked out.");
                    return View(viewName: "Lockout");
                }

		        var user = await _userManager.FindByEmailAsync(model.Email);
		        if (user != null && !user.EmailConfirmed)
		        {
					ModelState.AddModelError(string.Empty, Resources.Controllers.AccountController.EmailNotConfirmed);
		        }
		        else
		        {
					ModelState.AddModelError(string.Empty, Resources.Controllers.AccountController.InvalidLoginAttempt);
				}
					
	            return View(model);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData[index: "ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new IdentityUserEntity { Name = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
					// Send an email with this link
					var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					var callbackUrl = Url.Action(nameof(ConfirmEmail), controller: "Account", values: new { userId = user.Identifier, code }, protocol: HttpContext.Request.Scheme);
					var mailModel = new ConfirmMailModel(callbackUrl);
					await _emailSender.SendEmailAsync(model.Email, mailModel);

					// sign the user in
					// disabled to force the the user to confirm his mail address
					//await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(eventId: 3, message: "User created a new account with password.");
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(eventId: 4, message: "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), controllerName: "Home");
        }

        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), controller: "Account", values: new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        // GET: /Account/ExternalLoginCallback
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"{Resources.Controllers.AccountController.ExternalProviderError} {remoteError}");
                return View(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl });
            }
            if (result.IsLockedOut)
            {
                return View(viewName: "Lockout");
            }

	        // If the user does not have an account, then ask the user to create an account.
	        ViewData[index: "ReturnUrl"] = returnUrl;
	        ViewData[index: "LoginProvider"] = info.LoginProvider;
	        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
	        return View(viewName: "ExternalLoginConfirmation", model: new ExternalLoginConfirmationViewModel { Email = email });
        }

        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View(viewName: "ExternalLoginFailure");
                }

				var user = new IdentityUserEntity { Name = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
	                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
	                    var callbackUrl = Url.Action(nameof(ConfirmEmail), controller: "Account", values: new { userId = user.Identifier, code }, protocol: HttpContext.Request.Scheme);
	                    var mailModel = new ConfirmMailModel(callbackUrl);
	                    await _emailSender.SendEmailAsync(model.Email, mailModel);

	                    // disabled to force the the user to confirm his mail address
						// await _signInManager.SignInAsync(user, isPersistent: false);
						_logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewData[index: "ReturnUrl"] = returnUrl;
            return View(model);
        }

        // GET: /Account/ConfirmEmail
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View(viewName: "Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View(viewName: "Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

		// GET: /Account/ConfirmEmailAgain
	    [HttpGet]
	    [AllowAnonymous]
	    public IActionResult ConfirmEmailAgain()
	    {
		    return View();
	    }

	    // POST: /Account/ConfirmEmailAgain
	    [HttpPost]
	    [AllowAnonymous]
		public async Task<IActionResult> ConfirmEmailAgain(ConfirmEmailAgainViewModel model)
	    {
		    if (!ModelState.IsValid) return View(model);

		    var user = await _userManager.FindByEmailAsync(model.Email);
		    if (user == null || await _userManager.IsEmailConfirmedAsync(user))
		    {
			    // Don't reveal that the user does not exist or is confirmed
			    return View(viewName: "ConfirmEmailAgainConfirmation");
		    }

		    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
		    var callbackUrl = Url.Action(nameof(ConfirmEmail), controller: "Account", values: new { userId = user.Identifier, code }, protocol: HttpContext.Request.Scheme);
		    var mailModel = new ConfirmMailModel(callbackUrl);
		    await _emailSender.SendEmailAsync(model.Email, mailModel);
		    return View(viewName: "ConfirmEmailAgainConfirmation");
	    }

        // GET: /Account/ForgotPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
	        if (!ModelState.IsValid) return View(model);

	        var user = await _userManager.FindByEmailAsync(model.Email);
	        if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
	        {
		        // Don't reveal that the user does not exist or is not confirmed
		        return View(viewName: "ForgotPasswordConfirmation");
	        }

	        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
	        var callbackUrl = Url.Action(nameof(ResetPassword), controller: "Account", values: new { userId = user.Identifier, code }, protocol: HttpContext.Request.Scheme);
	        var mailModel = new RecoverPasswordModel(callbackUrl);
	        await _emailSender.SendEmailAsync(model.Email, mailModel);
	        return View(viewName: "ForgotPasswordConfirmation");

	        // If we got this far, something failed, redisplay form
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View(viewName: "Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation), controllerName: "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation), controllerName: "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/SendCode
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View(viewName: "Error");
            }
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View(viewName: "Error");
            }

            // Generate the token and send it
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
            if (string.IsNullOrWhiteSpace(code))
            {
                return View(viewName: "Error");
            }

	        switch (model.SelectedProvider)
	        {
		        case "Email":
			        //await _emailSender.SendEmailAsync(await _userManager.GetEmailAsync(user), "Security Code", message);
			        break;
		        case "Phone":
			        //await _smsSender.SendSmsAsync(await _userManager.GetPhoneNumberAsync(user), message);
			        break;
				default:
					throw new NotImplementedException();
	        }

	        return RedirectToAction(nameof(VerifyCode), new { Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe });
        }

        //
        // GET: /Account/VerifyCode
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
        {
            // Require that the user has already logged in via username/password or external login
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View(viewName: "Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
            if (result.Succeeded)
            {
                return RedirectToLocal(model.ReturnUrl);
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning(eventId: 7, message: "User account locked out.");
                return View(viewName: "Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, Resources.Controllers.AccountController.InvalidCode);
                return View(model);
            }
        }

        //
        // GET /Account/AccessDenied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
	        if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
	        return RedirectToAction(nameof(HomeController.Index), controllerName: "Home");
        }

        #endregion
    }
}
