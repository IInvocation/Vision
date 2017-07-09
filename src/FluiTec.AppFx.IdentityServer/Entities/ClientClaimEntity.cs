using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.IdentityServer.Entities
{
	/// <summary>	A client claim entity. </summary>
	[EntityName(name: "ClientClaim")]
	public class ClientClaimEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the identifier of the client. </summary>
		/// <value>	The identifier of the client. </value>
		public int ClientId { get; set; }

		/// <summary>	Gets or sets the type of the claim. </summary>
		/// <value>	The type of the claim. </value>
		public string ClaimType { get; set; }

		/// <summary>	Gets or sets the claim value. </summary>
		/// <value>	The claim value. </value>
		public string ClaimValue { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}