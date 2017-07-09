using System.Collections.Generic;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Compound
{
	/// <summary>	A compound client entity. </summary>
	public class CompoundClientEntity
	{
		#region Constructors

		/// <summary>	Default constructor. </summary>
		public CompoundClientEntity()
		{
			Scopes = new List<ScopeEntity>();
			ClientClaims = new List<ClientClaimEntity>();
		}

		#endregion

		#region Properties

		/// <summary>	Gets or sets the client. </summary>
		/// <value>	The client. </value>
		public ClientEntity Client { get; set; }

		/// <summary>	Gets or sets the client claims. </summary>
		/// <value>	The client claims. </value>
		public IList<ClientClaimEntity> ClientClaims { get; set; }

		/// <summary>	Gets or sets the scopes. </summary>
		/// <value>	The scopes. </value>
		public IList<ScopeEntity> Scopes { get; set; }

		#endregion
	}
}