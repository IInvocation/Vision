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
			Claims = new List<string>(new []
			{
				IdentityClaims.HasAdministrativeRights
			})
		};
	}
}