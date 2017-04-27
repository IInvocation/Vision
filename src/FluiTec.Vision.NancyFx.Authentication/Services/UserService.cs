using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using FluiTec.AppFx.Authentication.Data;
using FluiTec.AppFx.Cryptography;
using FluiTec.Vision.NancyFx.Authentication.FaultReasons;
using FluiTec.Vision.NancyFx.Authentication.Results;
using FluiTec.Vision.NancyFx.Authentication.Settings;
using FluiTec.Vision.NancyFx.Authentication.ViewModels;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.NancyFx.Authentication.Services
{
	/// <summary>	A user service. </summary>
	public class UserService : IUserService
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="dataService">						The data service. </param>
		/// <param name="authenticationSettingsService">	The authentication settings service. </param>
		/// <param name="loggerFactory">					The logger factory. </param>
		public UserService(IAuthenticatingDataService dataService,
			IAuthenticationSettingsService authenticationSettingsService, ILoggerFactory loggerFactory)
		{
			_dataService = dataService;
			_authenticationSettings = authenticationSettingsService.Get();
			_logger = loggerFactory.CreateLogger(typeof(UserService));
		}

		#endregion

		#region Methods

		/// <summary>	Constructor. </summary>
		/// <param name="uow">				 	The uow. </param>
		/// <param name="entity">			 	The entity. </param>
		/// <param name="authenticationType">	Type of the authentication. </param>
		/// <returns>	A ClaimsPrincipal. </returns>
		private static ClaimsPrincipal FromEntity(IAuthenticatingUnitOfWork uow, UserEntity entity, string authenticationType)
		{
			if (entity == null)
				return null;

			// add default claims
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, entity.UniqueId.ToString()),
				new Claim(ClaimTypes.Name, entity.UserName),
			};

			// add mail claim if it was confirmed
			if (entity.EmailConfirmed)
				claims.Add(new Claim(ClaimTypes.Email, entity.Email));

			AddUserClaims(uow, entity, claims);
			AddRolesAndRoleClaims(uow, entity, claims);
			
			var identity = new ClaimsIdentity(claims, authenticationType, ClaimTypes.Name, "User");

			return new ClaimsPrincipal(identity);
		}

		/// <summary>	Adds a user claims. </summary>
		/// <param name="uow">   	The uow. </param>
		/// <param name="entity">	The entity. </param>
		/// <param name="claims">	The claims. </param>
		private static void AddUserClaims(IAuthenticatingUnitOfWork uow, UserEntity entity, List<Claim> claims)
		{
			var userClaims = uow.UserClaimRepository.GetByUserId(entity.Id);
			var userClaimArray = userClaims as UserClaimEntity[] ?? userClaims.ToArray();
			if (userClaims != null && userClaimArray.Any())
			{
				claims.AddRange(userClaimArray.Select(c => new Claim(c.Type, c.Value)));
			}
		}

		/// <summary>	Adds the roles and role claims. </summary>
		/// <param name="uow">   	The uow. </param>
		/// <param name="entity">	The entity. </param>
		/// <param name="claims">	The claims. </param>
		private static void AddRolesAndRoleClaims(IAuthenticatingUnitOfWork uow, UserEntity entity, List<Claim> claims)
		{
			var userRoles = uow.UserRoleRepository.GetByUserId(entity.Id);
			var userRolesArray = userRoles as UserRoleEntity[] ?? userRoles.ToArray();

			if (userRolesArray == null || userRolesArray.Length <= 0) return;
			var roles = uow.RoleRepository.GetManyById(userRolesArray.Select(ur => ur.RoleId).ToArray());
			var rolesArray = roles as RoleEntity[] ?? roles.ToArray();

			if (rolesArray == null || rolesArray.Length <= 0) return;

			// add roles as claims of type role
			claims.AddRange(rolesArray.Select(r => new Claim(ClaimTypes.Role, r.Name)));

			var roleClaims = uow.RoleClaimRepository.GetByRoles(rolesArray);
			var roleClaimsArray = roleClaims as RoleClaimEntity[] ?? roleClaims.ToArray();

			if (roleClaimsArray != null && roleClaimsArray.Length > 0)
			{
				// add role-specific claims
				claims.AddRange(roleClaimsArray.Select(rc => new Claim(rc.Type, rc.Value)));
			}
		}

		#endregion

		#region Fields

		/// <summary>	The data service. </summary>
		private readonly IAuthenticatingDataService _dataService;

		/// <summary>	The authentication settings. </summary>
		private readonly IAuthenticationSettings _authenticationSettings;

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region IUserService

		/// <summary>	Gets user from identifier. </summary>
		/// <param name="identifier">	The identifier. </param>
		/// <returns>	The user from identifier. </returns>
		public ClaimsPrincipal GetUserFromIdentifier(Guid identifier)
		{
			using (var uow = _dataService.StartUnitOfWork())
			{
				var entity = uow.UserRepository.GetByUserIdentifier(identifier);
				return FromEntity(uow, entity, AuthenticationTypes.IdentifierCookie);
			}
		}

		/// <summary>	Initializes this object from the given from user name and password. </summary>
		/// <param name="model">		The model. </param>
		/// <returns>	A ILoginViewModel. </returns>
		public IValidateCredentialsResult ValidateCredentials(ILoginViewModel model)
		{
			return ValidateCredentials(model.UserName, model.Password);
		}

		/// <summary>	Validates the credentials. </summary>
		/// <param name="username">	The username. </param>
		/// <param name="password">	The password. </param>
		/// <returns>	An IValidateCredentialsResult. </returns>
		public IValidateCredentialsResult ValidateCredentials(string username, string password)
		{
			try
			{
				ClaimsPrincipal principal;
				UserEntity entity;

				using (var uow = _dataService.StartUnitOfWork())
				{
					entity = uow.UserRepository.GetByUserName(username);

					// show invalid credentials if we cant find a matching user
					if (entity == null)
						return ValidateCredentialsResult.FromFault(ValidateCredentialsFaultReason.InvalidCredentials);

					// validate credentials
					if (!SecurePasswordHasher.Verify(password, entity.PasswordHash))
					{
						if (!_authenticationSettings.AutoLockout)
							return ValidateCredentialsResult.FromFault(ValidateCredentialsFaultReason.InvalidCredentials);

						// increase accessfailedcount and eventually lock out user
						entity.AccessFailedCount++;
						entity.LockedOutTill = entity.AccessFailedCount >= _authenticationSettings.AutoLockoutMaxRetryCount
							? DateTime.UtcNow.Add(_authenticationSettings.AutoLockoutTimeSpan) as DateTime?
							: null;
						uow.UserRepository.IncreaseAccessFailedCount(entity);
						uow.Commit();

						return ValidateCredentialsResult.FromFault(ValidateCredentialsFaultReason.InvalidCredentials);
					}

					// make sure user is not locked out
					if (entity.LockedOutTill > DateTime.UtcNow)
						return ValidateCredentialsResult.FromFault(ValidateCredentialsFaultReason.LockedOut);

					// make sure user is not disabled
					if (entity.Disabled)
						return ValidateCredentialsResult.FromFault(ValidateCredentialsFaultReason.Disabled);

					// revoke lock out
					if (entity.LockedOutTill.HasValue || entity.AccessFailedCount > 0)
					{
						entity.AccessFailedCount = 0;
						entity.LockedOutTill = null;
						uow.UserRepository.Update(entity);
					}

					// load the principal
					principal = FromEntity(uow, entity, AuthenticationTypes.FormCredentials);
					uow.Commit();
				}

				return ValidateCredentialsResult.FromSuccess(entity.UniqueId, principal);
			}
			catch (Exception e)
			{
				_logger.LogCritical(new EventId(), e, "Error validating credentials");
				return ValidateCredentialsResult.FromFault(ValidateCredentialsFaultReason.Unknown);
			}
		}

		/// <summary>	Creates this object. </summary>
		/// <param name="model">	The model. </param>
		public ICreateUserResult Create(IRegisterViewModel model)
		{
			try
			{
				UserEntity entity;
				ClaimsPrincipal principal;

				using (var uow = _dataService.StartUnitOfWork())
				{
					// check if the user already exists
					if (uow.UserRepository.AlreadyExists(model.UserName))
						return CreateUserResult.FromFault(CreateUserFaultReason.ExistingUser);

					// create the user
					entity = new UserEntity
					{
						UserName = model.UserName,
						Email = model.UserName,
						AccessFailedCount = 0,
						EmailConfirmed = false,
						UniqueId = Guid.NewGuid(),
						PasswordHash = SecurePasswordHasher.Hash(model.Password)
					};
					uow.UserRepository.Add(entity);

					// load the principal
					principal = FromEntity(uow, entity, AuthenticationTypes.FormCredentials);

					// commit db operations
					uow.Commit();
				}

				return CreateUserResult.FromSuccess(entity.UniqueId, principal);
			}
			catch (Exception e)
			{
				_logger.LogCritical("Error creating user", e);
				return CreateUserResult.FromFault(CreateUserFaultReason.Unknown);
			}
		}

		#endregion
	}
}