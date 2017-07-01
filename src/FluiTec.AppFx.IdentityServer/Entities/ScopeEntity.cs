using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.IdentityServer.Entities
{
	/// <summary>	A scope entity. </summary>
	[EntityName(name: "Scope")]
	public class ScopeEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the name. </summary>
		/// <value>	The name. </value>
		public string Name { get; set; }

		/// <summary>	Gets or sets the name of the display. </summary>
		/// <value>	The name of the display. </value>
		public string DisplayName { get; set; }

		/// <summary>	Gets or sets the description. </summary>
		/// <value>	The description. </value>
		public string Description { get; set; }

		/// <summary>	Gets or sets a value indicating whether the required. </summary>
		/// <value>	True if required, false if not. </value>
		public bool Required { get; set; }

		/// <summary>	Gets or sets a value indicating whether the emphasize. </summary>
		/// <value>	True if emphasize, false if not. </value>
		public bool Emphasize { get; set; }

		/// <summary>
		///     Gets or sets a value indicating whether the in discovery document is shown.
		/// </summary>
		/// <value>	True if show in discovery document, false if not. </value>
		public bool ShowInDiscoveryDocument { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}