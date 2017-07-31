using System;
using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Identity.Entities
{
	/// <summary>	An identity role entity. </summary>
	[EntityName(name: "IdentityRole")]
	public class IdentityRoleEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the identifier of the application. </summary>
		/// <value>	The identifier of the application. </value>
		public int ApplicationId { get; set; }

		/// <summary>	Gets or sets the name. </summary>
		/// <value>	The name. </value>
		public string Name { get; set; }

		/// <summary>	Gets or sets the name of the lowered. </summary>
		/// <value>	The name of the lowered. </value>
		public string LoweredName { get; set; }

		/// <summary>	Gets or sets the description. </summary>
		/// <value>	The description. </value>
		public string Description { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public Guid Identifier { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}