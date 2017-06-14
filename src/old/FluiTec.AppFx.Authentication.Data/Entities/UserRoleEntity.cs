using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Authentication.Data
{
	/// <summary>	A user role entity. </summary>
	[EntityName("UserRole")]
	public class UserRoleEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the identifier of the user. </summary>
		/// <value>	The identifier of the user. </value>
		public int UserId { get; set; }

		/// <summary>	Gets or sets the identifier of the role. </summary>
		/// <value>	The identifier of the role. </value>
		public int RoleId { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}