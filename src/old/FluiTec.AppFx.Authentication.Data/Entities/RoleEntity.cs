using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Authentication.Data
{
	/// <summary>	A role entity. </summary>
	[EntityName("VisionRole")]
	public class RoleEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the name. </summary>
		/// <value>	The name. </value>
		public string Name { get; set; }

		/// <summary>	Gets or sets the name of the display. </summary>
		/// <value>	The name of the display. </value>
		public string DisplayName { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}