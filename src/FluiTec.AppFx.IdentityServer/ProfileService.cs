using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Entities;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace FluiTec.AppFx.IdentityServer
{
	/// <summary>	A profile service. </summary>
	public class ProfileService : IProfileService
	{
		/// <summary>	Manager for user. </summary>
		private readonly UserManager<IdentityUserEntity> _userManager;

		/// <summary>	Constructor. </summary>
		/// <param name="userManager">	Manager for user. </param>
		public ProfileService(UserManager<IdentityUserEntity> userManager)
		{
			_userManager = userManager;
		}

		/// <summary>	Gets profile data asynchronous. </summary>
		/// <param name="context">	The context. </param>
		/// <returns>	The profile data asynchronous. </returns>
		public async Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			var subject = context.Subject;
			if (subject == null) throw new ArgumentNullException(nameof(context.Subject));

			var subjectId = subject.GetSubjectId();

			var user = await _userManager.FindByIdAsync(subjectId);
			if (user == null)
				throw new ArgumentException(message: "Invalid subject identifier");

			var claims = await GetClaimsFromUser(user);

			var requestedClaimTypes = context.RequestedClaimTypes;
			claims = requestedClaimTypes != null
				? claims.Where(c => requestedClaimTypes.Contains(c.Type))
				: claims.Take(count: 0);

			context.IssuedClaims = claims.ToList();
		}

		/// <summary>	Is active asynchronous. </summary>
		/// <param name="context">	The context. </param>
		/// <returns>	A Task. </returns>
		public async Task IsActiveAsync(IsActiveContext context)
		{
			var subject = context.Subject;
			if (subject == null) throw new ArgumentNullException(nameof(context.Subject));

			var subjectId = subject.GetSubjectId();
			var user = await _userManager.FindByIdAsync(subjectId);

			context.IsActive = false;

			if (user != null)
			{
				if (_userManager.SupportsUserSecurityStamp)
				{
					var securityStamp = subject.Claims.Where(c => c.Type == "security_stamp").Select(c => c.Value).SingleOrDefault();
					if (securityStamp != null)
					{
						var dbSecurityStamp = await _userManager.GetSecurityStampAsync(user);
						if (dbSecurityStamp != securityStamp)
							return;
					}
				}

				context.IsActive =
					!user.LockoutEnabled ||
					!user.LockedOutTill.HasValue ||
					user.LockedOutTill <= DateTime.Now;
			}
		}

		private async Task<IEnumerable<Claim>> GetClaimsFromUser(IdentityUserEntity user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtClaimTypes.Subject, user.Identifier.ToString()),
				new Claim(JwtClaimTypes.PreferredUserName, user.Name)
			};

			if (_userManager.SupportsUserEmail)
				claims.AddRange(new[]
				{
					new Claim(JwtClaimTypes.Email, user.Email),
					new Claim(JwtClaimTypes.EmailVerified, user.EmailConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
				});

			if (_userManager.SupportsUserClaim)
				claims.AddRange(await _userManager.GetClaimsAsync(user));

			if (!_userManager.SupportsUserRole) return claims;

			var roles = await _userManager.GetRolesAsync(user);
			claims.AddRange(roles.Select(role => new Claim(JwtClaimTypes.Role, role)));

			return claims;
		}
	}
}