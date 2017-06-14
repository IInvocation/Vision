using System;
using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Identity.Entities
{
	/// <summary>	An identity user entity. </summary>
	[EntityName("IdentityUser")]
	public class IdentityUserEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public Guid Identifier { get; set; }

		/// <summary>	Gets or sets the identifier of the application. </summary>
		/// <value>	The identifier of the application. </value>
		public int ApplicationId { get; set; }

		/// <summary>	Gets or sets the name. </summary>
		/// <value>	The name. </value>
		public string Name { get; set; }

		/// <summary>	Gets or sets the name of the lowered user. </summary>
		/// <value>	The name of the lowered user. </value>
		public string LoweredUserName { get; set; }

		/// <summary>	Gets or sets the mobile alias. </summary>
		/// <value>	The mobile alias. </value>
		public string MobileAlias { get; set; }

		/// <summary>	Gets or sets a value indicating whether this object is anonymous. </summary>
		/// <value>	True if this object is anonymous, false if not. </value>
		public bool IsAnonymous { get; set; }

		/// <summary>	Gets or sets the last activity date. </summary>
		/// <value>	The last activity date. </value>
		public DateTime LastActivityDate { get; set; }

		/// <summary>	Gets or sets the password hash. </summary>
		/// <value>	The password hash. </value>
		public string PasswordHash { get; set; }
	}
}