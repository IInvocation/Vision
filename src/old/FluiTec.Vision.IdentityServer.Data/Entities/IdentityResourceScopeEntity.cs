using FluiTec.AppFx.Data;

namespace FluiTec.Vision.IdentityServer.Data.Entities
{
	/// <summary>	An identity resource scope entity. </summary>
	[EntityName("IdentityResourceScope")]
	public class IdentityResourceScopeEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the identifier of the identity resource. </summary>
		/// <value>	The identifier of the identity resource. </value>
		public int IdentityResourceId { get; set; }

		/// <summary>	Gets or sets the identifier of the scope. </summary>
		/// <value>	The identifier of the scope. </value>
		public int ScopeId { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}