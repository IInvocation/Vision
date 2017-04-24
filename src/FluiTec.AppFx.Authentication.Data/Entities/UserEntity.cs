using System;
using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Authentication.Data
{
	/// <summary>	A user entity. </summary>
	[EntityName("VisionUser")]
	public class UserEntity : IEntity<int>
	{
		/// <summary>	Gets or sets a unique identifier. </summary>
		/// <value>	The identifier of the unique. </value>
		public Guid UniqueId { get; set; }

		/// <summary>	Gets or sets the name of the user. </summary>
		/// <value>	The name of the user. </value>
		public string UserName { get; set; }

		/// <summary>	Gets or sets the email. </summary>
		/// <value>	The email. </value>
		public string Email { get; set; }

		/// <summary>	Gets or sets a value indicating whether the email confirmed. </summary>
		/// <value>	True if email confirmed, false if not. </value>
		public bool EmailConfirmed { get; set; }

		/// <summary>	Gets or sets the password hash. </summary>
		/// <value>	The password hash. </value>
		public string PasswordHash { get; set; }

		/// <summary>	Gets or sets the phone number. </summary>
		/// <value>	The phone number. </value>
		public string PhoneNumber { get; set; }

		/// <summary>	Gets or sets a value indicating whether the phone number confirmed. </summary>
		/// <value>	True if phone number confirmed, false if not. </value>
		public bool PhoneNumberConfirmed { get; set; }

		/// <summary>	Gets or sets the number of access failed. </summary>
		/// <value>	The number of access failed. </value>
		public int AccessFailedCount { get; set; }

		/// <summary>	Gets or sets the Date/Time of the locked out till. </summary>
		/// <value>	The locked out till. </value>
		public DateTime? LockedOutTill { get; set; }

		/// <summary>	Gets or sets a value indicating whether this object is disabled. </summary>
		/// <value>	True if disabled, false if not. </value>
		public bool Disabled { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}