using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.IdentityServer.Entities;
using FluiTec.AppFx.IdentityServer.Repositories;

namespace FluiTec.AppFx.IdentityServer.Dapper.Repositories
{
	/// <summary>	An API resource scope repository. </summary>
	public abstract class ApiResourceScopeRepository : DapperRepository<ApiResourceScopeEntity, int>,
		IApiResourceScopeRepository
	{
		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		protected ApiResourceScopeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>	Gets the scope identifiers in this collection. </summary>
		/// <param name="ids">	The identifiers. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the scope identifiers in this
		///     collection.
		/// </returns>
		public abstract IEnumerable<ApiResourceScopeEntity> GetByScopeIds(int[] ids);

		/// <summary>	Gets the API identifiers in this collection. </summary>
		/// <param name="ids">	The identifiers. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the API identifiers in this
		///     collection.
		/// </returns>
		public abstract IEnumerable<ApiResourceScopeEntity> GetByApiIds(int[] ids);
	}
}