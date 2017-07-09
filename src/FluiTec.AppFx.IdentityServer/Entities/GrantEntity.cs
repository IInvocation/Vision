using System;
using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.IdentityServer.Entities
{
	/// <summary>	A grant entity. </summary>
	[EntityName(name: "IdentityGrant")]
	public class GrantEntity : IEntity<int>
	{
		/// <summary>	Gets or sets the grant key. </summary>
		/// <value>	The grant key. </value>
		public string GrantKey { get; set; }

		/// <summary>	Gets or sets the type. </summary>
		/// <value>	The type. </value>
		public string Type { get; set; }

		/// <summary>	Gets or sets the identifier of the subject. </summary>
		/// <value>	The identifier of the subject. </value>
		public string SubjectId { get; set; }

		/// <summary>	Gets or sets the identifier of the client. </summary>
		/// <value>	The identifier of the client. </value>
		public string ClientId { get; set; }

		/// <summary>	Gets or sets the creation time. </summary>
		/// <value>	The creation time. </value>
		public DateTime CreationTime { get; set; }

		/// <summary>	Gets or sets the Date/Time of the expiration. </summary>
		/// <value>	The expiration. </value>
		public DateTime? Expiration { get; set; }

		/// <summary>	Gets or sets the data. </summary>
		/// <value>	The data. </value>
		public string Data { get; set; }

		/// <summary>	Gets or sets the identifier. </summary>
		/// <value>	The identifier. </value>
		public int Id { get; set; }
	}
}