using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.Vision.IdentityServer.Data.Entities;

namespace FluiTec.Vision.IdentityServer.Data.Repositories
{
	/// <summary>	Interface for client scope repository. </summary>
	public interface IClientScopeRepository : IDataRepository<ClientScopeEntity, int>
	{
		/// <summary>	Gets the client identifiers in this collection. </summary>
		/// <param name="id">	The identifier. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the client identifiers in this
		///     collection.
		/// </returns>
		IEnumerable<ClientScopeEntity> GetByClientId(int id);
	}
}