using System;
using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Identity.Entities
{
	/// <summary>	An identity user login entity. </summary>
	[EntityName(name: "IdentityUserLogin")]
	public class IdentityUserLoginEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the name of the provider. </summary>
		/// <value>	The name of the provider. </value>
		public string ProviderName { get; set; }

		/// <summary>	Gets or sets the provider key. </summary>
		/// <value>	The provider key. </value>
		public string ProviderKey { get; set; }

		/// <summary>	Gets or sets the name of the provider display. </summary>
		/// <value>	The name of the provider display. </value>
		public string ProviderDisplayName { get; set; }

		/// <summary>	Gets or sets the identifier of the user. </summary>
		/// <value>	The identifier of the user. </value>
		public Guid UserId { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}