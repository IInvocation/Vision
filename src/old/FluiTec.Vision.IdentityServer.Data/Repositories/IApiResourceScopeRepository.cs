using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.Vision.IdentityServer.Data.Entities;

namespace FluiTec.Vision.IdentityServer.Data.Repositories
{
	/// <summary>	Interface for API resource scope repository. </summary>
	public interface IApiResourceScopeRepository : IDataRepository<ApiResourceScopeEntity, int>
	{
		/// <summary>	Gets the identifiers in this collection. </summary>
		/// <param name="ids">	The identifiers. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identifiers in this collection.
		/// </returns>
		IEnumerable<ApiResourceScopeEntity> GetByScopeIds(int[] ids);

		/// <summary>	Gets the API identifiers in this collection. </summary>
		/// <param name="ids">	The identifiers. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the API identifiers in this
		///     collection.
		/// </returns>
		IEnumerable<ApiResourceScopeEntity> GetByApiIds(int[] ids);
	}
}