using System.Collections.Generic;
using System.Threading.Tasks;
using FluiTec.Vision.NancyFx.Authentication.Services;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace FluiTec.Vision.IdentityServer
{
	public class ResourceOwnerValidator : IResourceOwnerPasswordValidator, ISecretValidator
	{
		private readonly IUserService _userService;

		/// <summary>	Constructor. </summary>
		/// <param name="userService">	The user service. </param>
		public ResourceOwnerValidator(IUserService userService)
		{
			_userService = userService;
		}

		/// <summary>	Validates the asynchronous described by context. </summary>
		/// <param name="context">	The context. </param>
		/// <returns>	A Task. </returns>
		public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
		{
			var result = _userService.ValidateCredentials(context.UserName, context.Password);

			if (result.Succeeded)
				context.Result = new GrantValidationResult(result.UniqueId.ToString(), OidcConstants.AuthenticationMethods.Password, result.Principal.Claims);

			return Task.FromResult(0);
		}

		public Task<SecretValidationResult> ValidateAsync(IEnumerable<Secret> secrets, ParsedSecret parsedSecret)
		{
			throw new System.NotImplementedException();
		}
	}
}