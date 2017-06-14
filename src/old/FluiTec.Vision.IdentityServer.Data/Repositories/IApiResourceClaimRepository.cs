using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.Vision.IdentityServer.Data.Entities;

namespace FluiTec.Vision.IdentityServer.Data.Repositories
{
	/// <summary>	Interface for API resource claim repository. </summary>
	public interface IApiResourceClaimRepository : IDataRepository<ApiResourceClaimEntity, int>
	{
		/// <summary>	Gets the api identifiers in this collection. </summary>
		/// <param name="id">	The identifier. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identity identifiers in this
		///     collection.
		/// </returns>
		IEnumerable<ApiResourceClaimEntity> GetByApiId(int id);
	}
}