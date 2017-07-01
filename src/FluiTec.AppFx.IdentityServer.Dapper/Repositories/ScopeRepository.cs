using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.IdentityServer.Entities;
using FluiTec.AppFx.IdentityServer.Repositories;

namespace FluiTec.AppFx.IdentityServer.Dapper.Repositories
{
	/// <summary>	A scope repository. </summary>
	public abstract class ScopeRepository : DapperRepository<ScopeEntity, int>, IScopeRepository
	{
		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		protected ScopeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>	Gets the identifiers in this collection. </summary>
		/// <param name="ids">	The identifiers. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identifiers in this collection.
		/// </returns>
		public abstract IEnumerable<ScopeEntity> GetByIds(int[] ids);

		/// <summary>	Gets the names in this collection. </summary>
		/// <param name="names">	The names. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the names in this collection.
		/// </returns>
		public abstract IEnumerable<ScopeEntity> GetByNames(string[] names);
	}
}