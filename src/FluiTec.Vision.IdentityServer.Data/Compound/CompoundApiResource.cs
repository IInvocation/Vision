using System.Collections.Generic;
using FluiTec.Vision.IdentityServer.Data.Entities;

namespace FluiTec.Vision.IdentityServer.Data.Compound
{
	/// <summary>	A compound API resource. </summary>
	public class CompoundApiResource
	{
		/// <summary>	Gets or sets the API resource. </summary>
		/// <value>	The API resource. </value>
		public ApiResourceEntity ApiResource { get; set; }

		/// <summary>	Gets or sets the API resource claims. </summary>
		/// <value>	The API resource claims. </value>
		public IEnumerable<ApiResourceClaimEntity> ApiResourceClaims { get; set; }

		/// <summary>	Gets or sets the API resource scopes. </summary>
		/// <value>	The API resource scopes. </value>
		public IEnumerable<ApiResourceScopeEntity> ApiResourceScopes { get; set; }
	}
}