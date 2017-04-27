using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Authentication.Data
{
	/// <summary>	A role claim entity. </summary>
	[EntityName("RoleClaim")]
	public class RoleClaimEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the identifier of the role. </summary>
		/// <value>	The identifier of the role. </value>
		public int RoleId { get; set; }

		/// <summary>	Gets or sets the type. </summary>
		/// <value>	The type. </value>
		public string Type { get; set; }

		/// <summary>	Gets or sets the value. </summary>
		/// <value>	The value. </value>
		public string Value { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}