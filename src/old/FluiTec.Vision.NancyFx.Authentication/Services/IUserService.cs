using System;
using System.Security.Claims;
using FluiTec.Vision.NancyFx.Authentication.Results;
using FluiTec.Vision.NancyFx.Authentication.ViewModels;

namespace FluiTec.Vision.NancyFx.Authentication.Services
{
	/// <summary>	Interface for user service. </summary>
	public interface IUserService
	{
		/// <summary>	Gets user from identifier. </summary>
		/// <param name="identifier">	The identifier. </param>
		/// <returns>	The user from identifier. </returns>
		ClaimsPrincipal GetUserFromIdentifier(Guid identifier);

		/// <summary>	Initializes this object from the given from user name and password. </summary>
		/// <param name="model">		The model. </param>
		/// <returns>	An IValidateCredentialsResult. </returns>
		IValidateCredentialsResult ValidateCredentials(ILoginViewModel model);

		/// <summary>	Validates the credentials. </summary>
		/// <param name="username">	The username. </param>
		/// <param name="password">	The password. </param>
		/// <returns>	An IValidateCredentialsResult. </returns>
		IValidateCredentialsResult ValidateCredentials(string username, string password);

		/// <summary>	Creates this object. </summary>
		/// <param name="model">	The model. </param>
		/// <returns>	An ICreateUserResult. </returns>
		ICreateUserResult Create(IRegisterViewModel model);
	}
}