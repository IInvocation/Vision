using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.IdentityServer.Entities
{
	[EntityName(name: "Client")]
	public class ClientEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the identifier of the client. </summary>
		/// <value>	The identifier of the client. </value>
		public string ClientId { get; set; }

		/// <summary>	Gets or sets the secret. </summary>
		/// <value>	The secret. </value>
		public string Secret { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}