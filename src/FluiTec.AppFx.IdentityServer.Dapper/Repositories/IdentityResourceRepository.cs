using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.IdentityServer.Compound;
using FluiTec.AppFx.IdentityServer.Entities;
using FluiTec.AppFx.IdentityServer.Repositories;

namespace FluiTec.AppFx.IdentityServer.Dapper.Repositories
{
	/// <summary>	An identity resource repository. </summary>
	public abstract class IdentityResourceRepository : DapperRepository<IdentityResourceEntity, int>,
		IIdentityResourceRepository
	{
		/// <summary>	Specialised constructor for use only by derived class. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		protected IdentityResourceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>	Gets all compounds in this collection. </summary>
		/// <returns>
		///     An enumerator that allows foreach to be used to process all compounds in this collection.
		/// </returns>
		public abstract IEnumerable<CompoundIdentityResource> GetAllCompound();

		/// <summary>	Gets the scope names compounds in this collection. </summary>
		/// <param name="scopeNames">	List of names of the scopes. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the scope names compounds in this
		///     collection.
		/// </returns>
		public abstract IEnumerable<CompoundIdentityResource> GetByScopeNamesCompound(IEnumerable<string> scopeNames);
	}
}