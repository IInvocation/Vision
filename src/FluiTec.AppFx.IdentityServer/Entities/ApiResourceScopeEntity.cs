using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.IdentityServer.Entities
{
	/// <summary>	An API resource scope entity. </summary>
	[EntityName(name: "ApiResourceScope")]
	public class ApiResourceScopeEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the identifier of the API resource. </summary>
		/// <value>	The identifier of the API resource. </value>
		public int ApiResourceId { get; set; }

		/// <summary>	Gets or sets the identifier of the scope. </summary>
		/// <value>	The identifier of the scope. </value>
		public int ScopeId { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}