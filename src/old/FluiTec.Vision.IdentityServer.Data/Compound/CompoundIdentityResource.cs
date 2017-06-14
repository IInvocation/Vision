using System.Collections.Generic;
using FluiTec.Vision.IdentityServer.Data.Entities;

namespace FluiTec.Vision.IdentityServer.Data.Compound
{
	/// <summary>	A compound identity resource. </summary>
	public class CompoundIdentityResource
	{
		#region Constructors

		/// <summary>	Default constructor. </summary>
		public CompoundIdentityResource()
		{
			IdentityResourceClaims = new List<IdentityResourceClaimEntity>();
			IdentityResourceScopes = new List<IdentityResourceScopeEntity>();
			Scopes = new List<ScopeEntity>();
		}

		#endregion

		#region Properties

		/// <summary>	Gets or sets the identity resource. </summary>
		/// <value>	The identity resource. </value>
		public IdentityResourceEntity IdentityResource { get; set; }

		/// <summary>	Gets or sets the identity resource claims. </summary>
		/// <value>	The identity resource claims. </value>
		public IList<IdentityResourceClaimEntity> IdentityResourceClaims { get; set; }

		/// <summary>	Gets or sets the identity resource scopes. </summary>
		/// <value>	The identity resource scopes. </value>
		public IList<IdentityResourceScopeEntity> IdentityResourceScopes { get; set; }

		/// <summary>	Gets or sets the scopes. </summary>
		/// <value>	The scopes. </value>
		public IList<ScopeEntity> Scopes { get; set; }

		#endregion
	}
}