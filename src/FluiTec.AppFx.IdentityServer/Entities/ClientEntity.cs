using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.IdentityServer.Entities
{
	[EntityName(name: "Client")]
	public class ClientEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the identifier of the client. </summary>
		/// <value>	The identifier of the client. </value>
		public string ClientId { get; set; }

		/// <summary>	Gets or sets the name. </summary>
		/// <value>	The name. </value>
		public string Name { get; set; }

		/// <summary>	Gets or sets URI of the redirect. </summary>
		/// <value>	The redirect URI. </value>
		public string RedirectUri { get; set; }

		/// <summary>	Gets or sets URI of the post logout. </summary>
		/// <value>	The post logout URI. </value>
		public string PostLogoutUri { get; set; }

		/// <summary>	Gets or sets a value indicating whether we allow offline access. </summary>
		/// <value>	True if allow offline access, false if not. </value>
		public bool AllowOfflineAccess { get; set; }

		/// <summary>	Gets or sets a list of types of the grants. </summary>
		/// <value>	A list of types of the grants. </value>
		public string GrantTypes { get; set; }

		/// <summary>	Gets or sets the secret. </summary>
		/// <value>	The secret. </value>
		public string Secret { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}