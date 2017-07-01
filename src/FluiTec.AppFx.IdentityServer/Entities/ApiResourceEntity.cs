using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.IdentityServer.Entities
{
	/// <summary>	An API resource entity. </summary>
	[EntityName(name: "ApiResource")]
	public class ApiResourceEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the name. </summary>
		/// <value>	The name. </value>
		public string Name { get; set; }

		/// <summary>	Gets or sets the name of the display. </summary>
		/// <value>	The name of the display. </value>
		public string DisplayName { get; set; }

		/// <summary>	Gets or sets a value indicating whether this object is enabled. </summary>
		/// <value>	True if enabled, false if not. </value>
		public bool Enabled { get; set; }

		/// <summary>	Gets or sets the description. </summary>
		/// <value>	The description. </value>
		public string Description { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}