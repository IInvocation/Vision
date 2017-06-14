using System.Collections.Generic;
using FluiTec.Vision.IdentityServer.Data.Entities;

namespace FluiTec.Vision.IdentityServer.Data.Compound
{
	/// <summary>	A compound client entity. </summary>
	public class CompoundClientEntity
	{
		#region Constructors

		/// <summary>	Default constructor. </summary>
		public CompoundClientEntity()
		{
			Scopes = new List<ScopeEntity>();
		}

		#endregion

		#region Properties

		/// <summary>	Gets or sets the client. </summary>
		/// <value>	The client. </value>
		public ClientEntity Client { get; set; }

		/// <summary>	Gets or sets the scopes. </summary>
		/// <value>	The scopes. </value>
		public IList<ScopeEntity> Scopes { get; set; }

		#endregion
	}
}