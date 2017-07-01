using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.IdentityServer.Compound;
using FluiTec.AppFx.IdentityServer.Entities;
using FluiTec.AppFx.IdentityServer.Repositories;

namespace FluiTec.AppFx.IdentityServer.Dapper.Repositories
{
	/// <summary>	An API resource repository. </summary>
	public abstract class ApiResourceRepository : DapperRepository<ApiResourceEntity, int>, IApiResourceRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		protected ApiResourceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>	Gets by name. </summary>
		/// <param name="name">	The name. </param>
		/// <returns>	The by name. </returns>
		public abstract ApiResourceEntity GetByName(string name);

		/// <summary>	Gets the identifiers in this collection. </summary>
		/// <param name="ids">	The identifiers. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identifiers in this collection.
		/// </returns>
		public abstract IEnumerable<ApiResourceEntity> GetByIds(int[] ids);

		/// <summary>	Gets all compounds in this collection. </summary>
		/// <returns>
		///     An enumerator that allows foreach to be used to process all compounds in this collection.
		/// </returns>
		public abstract IEnumerable<CompoundApiResource> GetAllCompound();

		/// <summary>	Gets the scope names compounds in this collection. </summary>
		/// <param name="scopeNames">	List of names of the scopes. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the scope names compounds in this
		///     collection.
		/// </returns>
		public abstract IEnumerable<CompoundApiResource> GetByScopeNamesCompound(IEnumerable<string> scopeNames);

		/// <summary>	Gets by name compount. </summary>
		/// <param name="name">	The name. </param>
		/// <returns>	The by name compount. </returns>
		public abstract CompoundApiResource GetByNameCompount(string name);
	}
}