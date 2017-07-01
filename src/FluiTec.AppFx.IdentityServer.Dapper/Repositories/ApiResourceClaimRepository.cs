using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.IdentityServer.Entities;
using FluiTec.AppFx.IdentityServer.Repositories;

namespace FluiTec.AppFx.IdentityServer.Dapper.Repositories
{
	/// <summary>	An API resource claim repository. </summary>
	public abstract class ApiResourceClaimRepository : DapperRepository<ApiResourceClaimEntity, int>,
		IApiResourceClaimRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		protected ApiResourceClaimRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>	Gets the API identifiers in this collection. </summary>
		/// <param name="id">	The identifier. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the API identifiers in this
		///     collection.
		/// </returns>
		public abstract IEnumerable<ApiResourceClaimEntity> GetByApiId(int id);
	}
}