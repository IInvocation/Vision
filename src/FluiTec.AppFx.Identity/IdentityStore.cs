using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.Identity
{
	public class IdentityStore : IUserPasswordStore<IdentityUserEntity>, IUserClaimStore<IdentityUserEntity>,
		IRoleStore<IdentityRoleEntity>, IUserSecurityStampStore<IdentityUserEntity>, IUserRoleStore<IdentityUserEntity>,
		IUserLoginStore<IdentityUserEntity>, IUserEmailStore<IdentityUserEntity>
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <exception cref="ArgumentNullException">
		///     Thrown when one or more required arguments are
		///     null.
		/// </exception>
		/// <param name="dataService">	The data service. </param>
		public IdentityStore(IIdentityDataService dataService)
		{
			UnitOfWork = dataService?.StartUnitOfWork() ?? throw new ArgumentNullException(nameof(dataService));
		}

		#endregion

		#region Properties

		/// <summary>	Gets or sets the unit of work. </summary>
		/// <value>	The unit of work. </value>
		protected IIdentityUnitOfWork UnitOfWork { get; set; }

		#endregion

		/// <summary>	Sets email asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="email">				The email. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task. </returns>
		public Task SetEmailAsync(IdentityUserEntity user, string email, CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() =>
			{
				user.Email = email;
				UnitOfWork.UserRepository.Update(user);
			}, cancellationToken);
		}

		/// <summary>	Gets email asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The email asynchronous. </returns>
		public Task<string> GetEmailAsync(IdentityUserEntity user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.Email);
		}

		/// <summary>	Gets email confirmed asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The email confirmed asynchronous. </returns>
		public Task<bool> GetEmailConfirmedAsync(IdentityUserEntity user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.EmailConfirmed);
		}

		public Task SetEmailConfirmedAsync(IdentityUserEntity user, bool confirmed, CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() =>
			{
				user.EmailConfirmed = confirmed;
				UnitOfWork.UserRepository.Update(user);
			}, cancellationToken);
		}

		/// <summary>	Searches for the first email asynchronous. </summary>
		/// <param name="normalizedEmail">  	The normalized email. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The found email asynchronous. </returns>
		public Task<IdentityUserEntity> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
		{
			return Task<IdentityUserEntity>.Factory.StartNew(
				() => UnitOfWork.UserRepository.FindByNormalizedEmail(normalizedEmail), cancellationToken);
		}

		public Task<string> GetNormalizedEmailAsync(IdentityUserEntity user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.NormalizedEmail);
		}

		/// <summary>	Sets normalized email asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="normalizedEmail">  	The normalized email. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task. </returns>
		public Task SetNormalizedEmailAsync(IdentityUserEntity user, string normalizedEmail,
			CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() =>
			{
				user.NormalizedEmail = normalizedEmail;
				UnitOfWork.UserRepository.Update(user);
			}, cancellationToken);
		}

		#region IDisposable

		private bool _disposed;

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
		///     resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(disposing: true);
		}

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
		///     resources.
		/// </summary>
		/// <param name="disposing">
		///     true to release both managed and unmanaged resources; false to
		///     release only unmanaged resources.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
				UnitOfWork.Commit();
			_disposed = true;
		}

		#endregion

		#region IUserClaimStore

		/// <summary>	Gets claims asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The claims asynchronous. </returns>
		public Task<IList<Claim>> GetClaimsAsync(IdentityUserEntity user, CancellationToken cancellationToken)
		{
			return Task<IList<Claim>>.Factory.StartNew(
				() => { return UnitOfWork.ClaimRepository.GetByUser(user).Select(c => new Claim(c.Type, c.Value)).ToList(); },
				cancellationToken);
		}

		/// <summary>	Adds the claims asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="claims">				The claims. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task. </returns>
		public Task AddClaimsAsync(IdentityUserEntity user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() =>
			{
				var identityClaims = claims.Select(c => new IdentityClaimEntity {UserId = user.Id, Type = c.Type, Value = c.Value});
			}, cancellationToken);
		}

		/// <summary>	Replace claim asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="claim">				The claim. </param>
		/// <param name="newClaim">				The new claim. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task. </returns>
		public Task ReplaceClaimAsync(IdentityUserEntity user, Claim claim, Claim newClaim,
			CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() =>
			{
				var entity = UnitOfWork.ClaimRepository.GetByUserAndType(user, claim.Type);
				entity.Type = newClaim.Type;
				entity.Value = newClaim.Value;
				UnitOfWork.ClaimRepository.Update(entity);
			}, cancellationToken);
		}

		/// <summary>	Removes the claims asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="claims">				The claims. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task. </returns>
		public Task RemoveClaimsAsync(IdentityUserEntity user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() =>
			{
				var claimTypes = claims.Select(c => c.Type).ToList();
				var entities = UnitOfWork.ClaimRepository.GetByUser(user).Where(c => claimTypes.Contains(c.Type));
				foreach (var entity in entities)
					UnitOfWork.ClaimRepository.Delete(entity);
			}, cancellationToken);
		}

		/// <summary>	Gets users for claim asynchronous. </summary>
		/// <param name="claim">				The claim. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The users for claim asynchronous. </returns>
		public Task<IList<IdentityUserEntity>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
		{
			return Task<IList<IdentityUserEntity>>.Factory.StartNew(() =>
			{
				var userIds = UnitOfWork.ClaimRepository.GetUserIdsForClaimType(claim.Type);
				var users = UnitOfWork.UserRepository.FindByIds(userIds);
				return users.ToList();
			}, cancellationToken);
		}

		#endregion

		#region IUserStore

		/// <summary>	Gets user identifier asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The user identifier asynchronous. </returns>
		public Task<string> GetUserIdAsync(IdentityUserEntity user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.Identifier.ToString());
		}

		/// <summary>	Gets user name asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The user name asynchronous. </returns>
		public Task<string> GetUserNameAsync(IdentityUserEntity user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.Name);
		}

		/// <summary>	Sets user name asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="userName">				Name of the user. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task. </returns>
		public Task SetUserNameAsync(IdentityUserEntity user, string userName, CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() =>
			{
				user.Name = userName;
				UnitOfWork.UserRepository.Update(user);
			}, cancellationToken);
		}

		/// <summary>	Gets normalized user name asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The normalized user name asynchronous. </returns>
		public Task<string> GetNormalizedUserNameAsync(IdentityUserEntity user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.LoweredUserName);
		}

		/// <summary>	Sets normalized user name asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="normalizedName">   	Name of the normalized. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task. </returns>
		public Task SetNormalizedUserNameAsync(IdentityUserEntity user, string normalizedName,
			CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() =>
			{
				user.LoweredUserName = normalizedName;
				UnitOfWork.UserRepository.Update(user);
			}, cancellationToken);
		}

		/// <summary>	Creates the asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The new asynchronous. </returns>
		public Task<IdentityResult> CreateAsync(IdentityUserEntity user, CancellationToken cancellationToken)
		{
			return Task<IdentityResult>.Factory.StartNew(() =>
			{
				try
				{
					var entity = UnitOfWork.UserRepository.Add(user);
					return IdentityResult.Success;
				}
				catch (Exception)
				{
					return IdentityResult.Failed();
				}
			}, cancellationToken);
		}

		/// <summary>	Updates the asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task&lt;IdentityResult&gt; </returns>
		public Task<IdentityResult> UpdateAsync(IdentityUserEntity user, CancellationToken cancellationToken)
		{
			return Task<IdentityResult>.Factory.StartNew(() =>
			{
				try
				{
					var entity = UnitOfWork.UserRepository.Update(user);
					return IdentityResult.Success;
				}
				catch (Exception)
				{
					return IdentityResult.Failed();
				}
			}, cancellationToken);
		}

		/// <summary>	Deletes the asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task&lt;IdentityResult&gt; </returns>
		public Task<IdentityResult> DeleteAsync(IdentityUserEntity user, CancellationToken cancellationToken)
		{
			return Task<IdentityResult>.Factory.StartNew(() =>
			{
				try
				{
					UnitOfWork.UserRepository.Delete(user);
					return IdentityResult.Success;
				}
				catch (Exception)
				{
					return IdentityResult.Failed();
				}
			}, cancellationToken);
		}

		/// <summary>	Searches for the first identifier asynchronous. </summary>
		/// <param name="userId">				Identifier for the user. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The found identifier asynchronous. </returns>
		public Task<IdentityUserEntity> FindByIdAsync(string userId, CancellationToken cancellationToken)
		{
			return Task<IdentityUserEntity>.Factory.StartNew(() => UnitOfWork.UserRepository.Get(userId), cancellationToken);
		}

		/// <summary>	Searches for the first name asynchronous. </summary>
		/// <param name="normalizedUserName">	Name of the normalized user. </param>
		/// <param name="cancellationToken"> 	The cancellation token. </param>
		/// <returns>	The found name asynchronous. </returns>
		public Task<IdentityUserEntity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			return Task<IdentityUserEntity>.Factory.StartNew(
				() => UnitOfWork.UserRepository.FindByLoweredName(normalizedUserName), cancellationToken);
		}

		#endregion

		#region IUserPasswordStore

		/// <summary>	Sets password hash asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="passwordHash">			The password hash. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task. </returns>
		public Task SetPasswordHashAsync(IdentityUserEntity user, string passwordHash, CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() =>
			{
				user.PasswordHash = passwordHash;
				UnitOfWork.UserRepository.Update(user);
			}, cancellationToken);
		}

		/// <summary>	Gets password hash asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The password hash asynchronous. </returns>
		public Task<string> GetPasswordHashAsync(IdentityUserEntity user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.PasswordHash);
		}

		/// <summary>	Has password asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task&lt;bool&gt; </returns>
		public Task<bool> HasPasswordAsync(IdentityUserEntity user, CancellationToken cancellationToken)
		{
			return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
		}

		#endregion

		#region IRoleStore

		/// <summary>	Creates the asynchronous. </summary>
		/// <param name="role">					The role. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The new asynchronous. </returns>
		public Task<IdentityResult> CreateAsync(IdentityRoleEntity role, CancellationToken cancellationToken)
		{
			return Task<IdentityResult>.Factory.StartNew(() =>
			{
				try
				{
					var entity = UnitOfWork.RoleRepository.Add(role);
					return IdentityResult.Success;
				}
				catch (Exception)
				{
					return IdentityResult.Failed();
				}
			}, cancellationToken);
		}

		/// <summary>	Updates the asynchronous. </summary>
		/// <param name="role">					The role. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task&lt;IdentityResult&gt; </returns>
		public Task<IdentityResult> UpdateAsync(IdentityRoleEntity role, CancellationToken cancellationToken)
		{
			return Task<IdentityResult>.Factory.StartNew(() =>
			{
				try
				{
					var entity = UnitOfWork.RoleRepository.Update(role);
					return IdentityResult.Success;
				}
				catch (Exception)
				{
					return IdentityResult.Failed();
				}
			}, cancellationToken);
		}

		/// <summary>	Deletes the asynchronous. </summary>
		/// <param name="role">					The role. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task&lt;IdentityResult&gt; </returns>
		public Task<IdentityResult> DeleteAsync(IdentityRoleEntity role, CancellationToken cancellationToken)
		{
			return Task<IdentityResult>.Factory.StartNew(() =>
			{
				try
				{
					UnitOfWork.RoleRepository.Delete(role);
					return IdentityResult.Success;
				}
				catch (Exception)
				{
					return IdentityResult.Failed();
				}
			}, cancellationToken);
		}

		/// <summary>	Gets role identifier asynchronous. </summary>
		/// <param name="role">					The role. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The role identifier asynchronous. </returns>
		public Task<string> GetRoleIdAsync(IdentityRoleEntity role, CancellationToken cancellationToken)
		{
			return Task.FromResult(role.Identifier.ToString());
		}

		/// <summary>	Gets role name asynchronous. </summary>
		/// <param name="role">					The role. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The role name asynchronous. </returns>
		public Task<string> GetRoleNameAsync(IdentityRoleEntity role, CancellationToken cancellationToken)
		{
			return Task.FromResult(role.Name);
		}

		/// <summary>	Sets role name asynchronous. </summary>
		/// <param name="role">					The role. </param>
		/// <param name="roleName">				Name of the role. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task. </returns>
		public Task SetRoleNameAsync(IdentityRoleEntity role, string roleName, CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() =>
			{
				role.Name = roleName;
				UnitOfWork.RoleRepository.Update(role);
			}, cancellationToken);
		}

		/// <summary>	Gets normalized role name asynchronous. </summary>
		/// <param name="role">					The role. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The normalized role name asynchronous. </returns>
		public Task<string> GetNormalizedRoleNameAsync(IdentityRoleEntity role, CancellationToken cancellationToken)
		{
			return Task.FromResult(role.LoweredName);
		}

		/// <summary>	Sets normalized role name asynchronous. </summary>
		/// <param name="role">					The role. </param>
		/// <param name="normalizedName">   	Name of the normalized. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task. </returns>
		public Task SetNormalizedRoleNameAsync(IdentityRoleEntity role, string normalizedName,
			CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() =>
			{
				role.LoweredName = normalizedName;
				UnitOfWork.RoleRepository.Update(role);
			}, cancellationToken);
		}

		/// <summary>	Searches for the first identifier asynchronous. </summary>
		/// <typeparam name="IdentityRoleEntity">	Type of the identity role entity. </typeparam>
		/// <param name="roleId">				Identifier for the role. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The found identifier asynchronous. </returns>
		Task<IdentityRoleEntity> IRoleStore<IdentityRoleEntity>.FindByIdAsync(string roleId,
			CancellationToken cancellationToken)
		{
			return Task<IdentityRoleEntity>.Factory.StartNew(() => UnitOfWork.RoleRepository.Get(roleId), cancellationToken);
		}

		/// <summary>	Searches for the first name asynchronous. </summary>
		/// <typeparam name="IdentityRoleEntity">	Type of the identity role entity. </typeparam>
		/// <param name="normalizedRoleName">	Name of the normalized role. </param>
		/// <param name="cancellationToken"> 	The cancellation token. </param>
		/// <returns>	The found name asynchronous. </returns>
		Task<IdentityRoleEntity> IRoleStore<IdentityRoleEntity>.FindByNameAsync(string normalizedRoleName,
			CancellationToken cancellationToken)
		{
			return Task<IdentityRoleEntity>.Factory.StartNew(
				() => UnitOfWork.RoleRepository.FindByLoweredName(normalizedRoleName), cancellationToken);
		}

		#endregion

		#region IUserSecurityStampStore

		/// <summary>	Sets security stamp asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="stamp">				The stamp. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task. </returns>
		public Task SetSecurityStampAsync(IdentityUserEntity user, string stamp, CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() =>
			{
				user.SecurityStamp = stamp;
				UnitOfWork.UserRepository.Update(user);
			}, cancellationToken);
		}

		/// <summary>	Gets security stamp asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The security stamp asynchronous. </returns>
		public Task<string> GetSecurityStampAsync(IdentityUserEntity user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.SecurityStamp);
		}

		#endregion

		#region IUserRoleStore

		/// <summary>	Adds to the role asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="roleName">				Name of the role. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task. </returns>
		public Task AddToRoleAsync(IdentityUserEntity user, string roleName, CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() =>
			{
				var role = UnitOfWork.RoleRepository.FindByLoweredName(roleName.ToUpper());
				UnitOfWork.UserRoleRepository.Add(new IdentityUserRoleEntity {RoleId = role.Id, UserId = user.Id});
			}, cancellationToken);
		}

		/// <summary>	Removes from role asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="roleName">				Name of the role. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task. </returns>
		public Task RemoveFromRoleAsync(IdentityUserEntity user, string roleName, CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() =>
			{
				var role = UnitOfWork.RoleRepository.FindByLoweredName(roleName.ToUpper());
				var userRole = UnitOfWork.UserRoleRepository.FindByUserIdAndRoleId(user.Id, role.Id);
				UnitOfWork.UserRoleRepository.Delete(userRole);
			}, cancellationToken);
		}

		/// <summary>	Gets roles asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The roles asynchronous. </returns>
		public Task<IList<string>> GetRolesAsync(IdentityUserEntity user, CancellationToken cancellationToken)
		{
			return Task<IList<string>>.Factory.StartNew(() =>
			{
				var roleIds = UnitOfWork.UserRoleRepository.FindByUser(user);
				return UnitOfWork.RoleRepository.FindByIds(roleIds).Select(r => r.Name).ToList();
			}, cancellationToken);
		}

		/// <summary>	Is in role asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="roleName">				Name of the role. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task&lt;bool&gt; </returns>
		public async Task<bool> IsInRoleAsync(IdentityUserEntity user, string roleName, CancellationToken cancellationToken)
		{
			var roles = await GetRolesAsync(user, cancellationToken);
			return roles.Contains(roleName);
		}

		/// <summary>	Gets users in role asynchronous. </summary>
		/// <param name="roleName">				Name of the role. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The users in role asynchronous. </returns>
		public Task<IList<IdentityUserEntity>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
		{
			return Task<IList<IdentityUserEntity>>.Factory.StartNew(() =>
			{
				var role = UnitOfWork.RoleRepository.FindByLoweredName(roleName.ToUpper());
				var userIds = UnitOfWork.UserRoleRepository.FindByRole(role);
				return UnitOfWork.UserRepository.FindByIds(userIds).ToList();
			}, cancellationToken);
		}

		#endregion

		#region IUserLoginStore

		/// <summary>	Adds a login asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="login">				The login. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task. </returns>
		public Task AddLoginAsync(IdentityUserEntity user, UserLoginInfo login, CancellationToken cancellationToken)
		{
			return Task<IdentityUserEntity>.Factory.StartNew(() =>
			{
				UnitOfWork.LoginRepository.Add(new IdentityUserLoginEntity
				{
					ProviderName = login.LoginProvider,
					ProviderKey = login.ProviderKey,
					ProviderDisplayName = login.ProviderDisplayName,
					UserId = user.Identifier
				});
				return user;
			}, cancellationToken);
		}

		/// <summary>	Removes the login asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="loginProvider">		The login provider. </param>
		/// <param name="providerKey">			The provider key. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	A Task. </returns>
		public Task RemoveLoginAsync(IdentityUserEntity user, string loginProvider, string providerKey,
			CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() => { UnitOfWork.LoginRepository.RemoveByNameAndKey(loginProvider, providerKey); },
				cancellationToken);
		}

		/// <summary>	Gets logins asynchronous. </summary>
		/// <param name="user">					The user. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The logins asynchronous. </returns>
		public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUserEntity user, CancellationToken cancellationToken)
		{
			return Task<IList<UserLoginInfo>>.Factory.StartNew(() =>
			{
				var entities = UnitOfWork.LoginRepository.FindByUserId(user.Identifier);
				return entities.Select(e => new UserLoginInfo(e.ProviderName, e.ProviderKey, e.ProviderDisplayName)).ToList();
			}, cancellationToken);
		}

		/// <summary>	Searches for the first login asynchronous. </summary>
		/// <param name="loginProvider">		The login provider. </param>
		/// <param name="providerKey">			The provider key. </param>
		/// <param name="cancellationToken">	The cancellation token. </param>
		/// <returns>	The found login asynchronous. </returns>
		public Task<IdentityUserEntity> FindByLoginAsync(string loginProvider, string providerKey,
			CancellationToken cancellationToken)
		{
			return Task<IdentityUserEntity>.Factory.StartNew(
				() => UnitOfWork.UserRepository.FindByLogin(loginProvider, providerKey), cancellationToken);
		}

		#endregion
	}
}