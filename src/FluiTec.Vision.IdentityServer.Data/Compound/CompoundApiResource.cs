using System.Collections.Generic;
using FluiTec.Vision.IdentityServer.Data.Entities;

namespace FluiTec.Vision.IdentityServer.Data.Compound
{
	/// <summary>	A compound API resource. </summary>
	public class CompoundApiResource
	{
		/// <summary>	Default constructor. </summary>
		public CompoundApiResource()
		{
			ApiResourceClaims = new List<ApiResourceClaimEntity>();
			ApiResourceScopes = new List<ApiResourceScopeEntity>();
			Scopes = new List<ScopeEntity>();
		}

		/// <summary>	Gets or sets the API resource. </summary>
		/// <value>	The API resource. </value>
		public ApiResourceEntity ApiResource { get; set; }

		/// <summary>	Gets or sets the API resource claims. </summary>
		/// <value>	The API resource claims. </value>
		public IList<ApiResourceClaimEntity> ApiResourceClaims { get; set; }

		/// <summary>	Gets or sets the API resource scopes. </summary>
		/// <value>	The API resource scopes. </value>
		public IList<ApiResourceScopeEntity> ApiResourceScopes { get; set; }

		/// <summary>	Gets or sets the scopes. </summary>
		/// <value>	The scopes. </value>
		public IList<ScopeEntity> Scopes { get; set; }
	}
}