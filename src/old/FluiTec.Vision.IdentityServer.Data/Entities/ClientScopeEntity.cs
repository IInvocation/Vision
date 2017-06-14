using FluiTec.AppFx.Data;

namespace FluiTec.Vision.IdentityServer.Data.Entities
{
	/// <summary>	A client scope entity. </summary>
	[EntityName("ClientScope")]
	public class ClientScopeEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the identifier of the client. </summary>
		/// <value>	The identifier of the client. </value>
		public int ClientId { get; set; }

		/// <summary>	Gets or sets the identifier of the scope. </summary>
		/// <value>	The identifier of the scope. </value>
		public int ScopeId { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}