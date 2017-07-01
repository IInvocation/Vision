using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Repositories
{
	/// <summary>	Interface for scope repository. </summary>
	public interface IScopeRepository : IDataRepository<ScopeEntity, int>
	{
		/// <summary>	Gets the identifiers in this collection. </summary>
		/// <param name="ids">	The identifiers. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identifiers in this collection.
		/// </returns>
		IEnumerable<ScopeEntity> GetByIds(int[] ids);

		/// <summary>	Gets the names in this collection. </summary>
		/// <param name="names">	The names. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the names in this collection.
		/// </returns>
		IEnumerable<ScopeEntity> GetByNames(string[] names);
	}
}