using FluiTec.Vision.Server.Host.AspCoreHost.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Localization
{
	/// <summary>	A multilanguage identity error describer. </summary>
	public class MultiLanguageIdentityErrorDescriber : IdentityErrorDescriber
	{
		/// <summary>	The localizer. </summary>
		private readonly IStringLocalizer<SharedResource> _localizer;

		/// <summary>	Constructor. </summary>
		/// <param name="localizer">	The localizer. </param>
		public MultiLanguageIdentityErrorDescriber(IStringLocalizer<SharedResource> localizer)
		{
			_localizer = localizer;
		}

		/// <summary>	From resource. </summary>
		/// <param name="errorName">	Name of the error. </param>
		/// <param name="args">			A variable-length parameters list containing arguments. </param>
		/// <returns>	An IdentityError. </returns>
		private IdentityError FromResource(string errorName, params object[] args)
		{
			var resString = SharedResource.ResourceManager.GetString(errorName);
			return new IdentityError
			{
				Code = errorName,
				Description = string.Format(resString, args)
			};
		}

		/// <summary>	Duplicate email. </summary>
		/// <param name="email">	The email. </param>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError DuplicateEmail(string email)
		{
			return FromResource(nameof(DuplicateEmail), email);
		}

		/// <summary>	Duplicate user name. </summary>
		/// <param name="userName">	Name of the user. </param>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError DuplicateUserName(string userName)
		{
			return FromResource(nameof(DuplicateUserName), userName);
		}

		/// <summary>	Invalid email. </summary>
		/// <param name="email">	The email. </param>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError InvalidEmail(string email)
		{
			return FromResource(nameof(InvalidEmail), email);
		}

		/// <summary>	Default error. </summary>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError DefaultError()
		{
			return FromResource(nameof(DefaultError));
		}

		/// <summary>	Duplicate role name. </summary>
		/// <param name="role">	The role. </param>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError DuplicateRoleName(string role)
		{
			return FromResource(nameof(DuplicateRoleName), role);
		}

		/// <summary>	Invalid role name. </summary>
		/// <param name="role">	The role. </param>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError InvalidRoleName(string role)
		{
			return FromResource(nameof(InvalidRoleName), role);
		}

		/// <summary>	Invalid token. </summary>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError InvalidToken()
		{
			return FromResource(nameof(InvalidToken));
		}

		/// <summary>	Invalid user name. </summary>
		/// <param name="userName">	Name of the user. </param>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError InvalidUserName(string userName)
		{
			return FromResource(nameof(InvalidUserName), userName);
		}

		/// <summary>	Concurrency failure. </summary>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError ConcurrencyFailure()
		{
			return FromResource(nameof(ConcurrencyFailure));
		}

		/// <summary>	Login already associated. </summary>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError LoginAlreadyAssociated()
		{
			return FromResource(nameof(LoginAlreadyAssociated));
		}

		/// <summary>	Password mismatch. </summary>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError PasswordMismatch()
		{
			return FromResource(nameof(PasswordMismatch));
		}

		/// <summary>	Password requires digit. </summary>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError PasswordRequiresDigit()
		{
			return FromResource(nameof(PasswordRequiresDigit));
		}

		/// <summary>	Password requires lower. </summary>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError PasswordRequiresLower()
		{
			return FromResource(nameof(PasswordRequiresLower));
		}

		/// <summary>	Password requires non alphanumeric. </summary>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError PasswordRequiresNonAlphanumeric()
		{
			return FromResource(nameof(PasswordRequiresNonAlphanumeric));
		}

		/// <summary>	Password requires upper. </summary>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError PasswordRequiresUpper()
		{
			return FromResource(nameof(PasswordRequiresUpper));
		}

		/// <summary>	Password too short. </summary>
		/// <param name="length">	The length. </param>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError PasswordTooShort(int length)
		{
			return FromResource(nameof(PasswordTooShort), length);
		}

		/// <summary>	User already has password. </summary>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError UserAlreadyHasPassword()
		{
			return FromResource(nameof(UserAlreadyHasPassword));
		}

		/// <summary>	User already in role. </summary>
		/// <param name="role">	The role. </param>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError UserAlreadyInRole(string role)
		{
			return FromResource(nameof(UserAlreadyInRole), role);
		}

		/// <summary>	User lockout not enabled. </summary>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError UserLockoutNotEnabled()
		{
			return FromResource(nameof(UserLockoutNotEnabled));
		}

		/// <summary>	User not in role. </summary>
		/// <param name="role">	The role. </param>
		/// <returns>	An IdentityError. </returns>
		public override IdentityError UserNotInRole(string role)
		{
			return FromResource(nameof(UserNotInRole), role);
		}
	}
}