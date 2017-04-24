using System.Security.Claims;
using FluiTec.Vision.NancyFx.Authentication.Results;
using FluiTec.Vision.NancyFx.Authentication.ViewModels;
using Nancy;

namespace FluiTec.Vision.NancyFx.Authentication.Services
{
	/// <summary>	Interface for authentication service. </summary>
	public interface IAuthenticationService
	{
		/// <summary>	Login. </summary>
		/// <param name="context">						The context. </param>
		/// <param name="validateCredentialsResult">	The validate credentials result. </param>
		/// <param name="loginViewModel">				The login view model. </param>
		/// <returns>	A Response. </returns>
		Response Login(NancyContext context, IValidateCredentialsResult validateCredentialsResult,
			ILoginViewModel loginViewModel);

		/// <summary>	Login. </summary>
		/// <param name="context">  	The context. </param>
		/// <param name="principal">	The principal. </param>
		/// <param name="returnUrl">	URL of the return. </param>
		/// <returns>	A Response. </returns>
		Response Login(NancyContext context, ClaimsPrincipal principal, string returnUrl);

		/// <summary>	Logout. </summary>
		/// <param name="context">	The context. </param>
		/// <returns>	A Response. </returns>
		Response Logout(NancyContext context);

		/// <summary>	Logs in redirect response. </summary>
		/// <param name="context">			  	The context. </param>
		/// <param name="fallbackRedirectUrl">	(Optional) URL of the fallback redirect. </param>
		/// <returns>	A Response. </returns>
		Response LogInRedirectResponse(NancyContext context, string fallbackRedirectUrl = null);

		/// <summary>	Logs out redirect response. </summary>
		/// <param name="context">	  	The context. </param>
		/// <param name="redirectUrl">	(Optional) URL of the redirect. </param>
		/// <returns>	A Response. </returns>
		Response LogOutRedirectResponse(NancyContext context, string redirectUrl = "/");
	}
}