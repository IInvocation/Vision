using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.IdentityServer.Entities
{
	/// <summary>	An API resource claim entity. </summary>
	[EntityName(name: "ApiResourceClaim")]
	public class ApiResourceClaimEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the identifier of the API resource. </summary>
		/// <value>	The identifier of the API resource. </value>
		public int ApiResourceId { get; set; }

		/// <summary>	Gets or sets the type of the claim. </summary>
		/// <value>	The type of the claim. </value>
		public string ClaimType { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}