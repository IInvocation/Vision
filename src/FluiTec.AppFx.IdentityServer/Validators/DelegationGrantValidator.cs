using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.IdentityServer.Validators
{
	/// <summary>	A delegation grant validator. </summary>
	public class DelegationGrantValidator : IExtensionGrantValidator
	{
		/// <summary>	The validator. </summary>
		private readonly ITokenValidator _validator;

		private readonly ILogger<DelegationGrantValidator> _logger;

		/// <summary>	Constructor. </summary>
		/// <param name="validator">	The validator. </param>
		/// <param name="logger">   	The logger. </param>
		public DelegationGrantValidator(ITokenValidator validator, ILogger<DelegationGrantValidator> logger)
		{
			_validator = validator;
			_logger = logger;
		}

		/// <summary>	Type of the grant. </summary>
		public string GrantType => "delegation";

		/// <summary>	Validates the asynchronous described by context. </summary>
		/// <param name="context">	The context. </param>
		/// <returns>	A Task. </returns>
		public async Task ValidateAsync(ExtensionGrantValidationContext context)
		{
			var userToken = context.Request.Raw.Get(name: "token");

			if (string.IsNullOrEmpty(userToken))
			{
				context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
				return;
			}

			var result = await _validator.ValidateAccessTokenAsync(userToken);
			
			if (result.IsError)
			{
				context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
				return;
			}

			var delegationAllowed = result.Client.Claims.Any(c => c.Type == GrantType && c.Value == context.Request.Client.ClientId);
			if (!delegationAllowed)
			{
				_logger.LogInformation($"{result.Client.ClientName} not allowed to use delegation for {context.Request.Client.ClientName} (fitting Claim is missing).");
				context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient);
				return;
			}

			// get user's identity
			var sub = result.Claims.FirstOrDefault(c => c.Type == "sub").Value;

			_logger.LogInformation($"Issuing DelegationGrant for {result.Claims.FirstOrDefault(c => c.Type == "email")?.Value}.");

			// grant
			context.Result = new GrantValidationResult(sub, authenticationMethod: "delegation");
		}
	}
}