using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.IdentityServer.Entities
{
	/// <summary>	An identity resource claim entity. </summary>
	[EntityName(name: "IdentityResourceClaim")]
	public class IdentityResourceClaimEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the identifier of the identity resource. </summary>
		/// <value>	The identifier of the identity resource. </value>
		public int IdentityResourceId { get; set; }

		/// <summary>	Gets or sets the type of the claim. </summary>
		/// <value>	The type of the claim. </value>
		public string ClaimType { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}