using System;
using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.IdentityServer.Entities
{
	/// <summary>	A signing credential entity. </summary>
	[EntityName(name: "SigningCredential")]
	public class SigningCredentialEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the Date/Time of the issued. </summary>
		/// <value>	The issued. </value>
		public DateTime Issued { get; set; }

		/// <summary>	Gets or sets the content. </summary>
		/// <value>	The content. </value>
		public string Contents { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}