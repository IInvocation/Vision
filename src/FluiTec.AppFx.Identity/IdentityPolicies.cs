using System.Collections.Generic;

namespace FluiTec.AppFx.Identity
{
	/// <summary>	An identity policies. </summary>
	public static class IdentityPolicies
	{
		/// <summary>	Gets the is admin policy. </summary>
		/// <value>	The is admin policy. </value>
		public static IdentityPolicy IsAdminPolicy { get; } = new IdentityPolicy
		{
			PolicyName = nameof(IsAdminPolicy),
			Claims = new List<string>(new[]
			{
				IdentityClaims.HasAdministrativeRights
			})
		};

		/// <summary>	Gets the manager for is user. </summary>
		/// <value>	The is user manager. </value>
		public static IdentityPolicy IsUserManager { get; } = new IdentityPolicy
		{
			PolicyName = nameof(IsUserManager),
			Claims = new List<string>(new[]
			{
				IdentityClaims.IsUserManager
			})
		};

		/// <summary>
		///     Gets the policies in this collection.
		/// </summary>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the policies in this collection.
		/// </returns>
		public static IEnumerable<IdentityPolicy> GetPolicies()
		{
			return new[]
			{
				IsAdminPolicy,
				IsUserManager
			};
		}
	}
}