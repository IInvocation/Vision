using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Compound;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Repositories
{
	/// <summary>	Interface for identity resource repository. </summary>
	public interface IIdentityResourceRepository : IDataRepository<IdentityResourceEntity, int>
	{
		/// <summary>	Gets all compounds in this collection. </summary>
		/// <returns>
		/// An enumerator that allows foreach to be used to process all compounds in this collection.
		/// </returns>
		IEnumerable<CompoundIdentityResource> GetAllCompound();

		/// <summary>	Gets the names compounds in this collection. </summary>
		/// <param name="scopeNames">	The names.</param>
		/// <returns>
		/// An enumerator that allows foreach to be used to process the names compounds in this
		/// collection.
		/// </returns>
		IEnumerable<CompoundIdentityResource> GetByScopeNamesCompound(IEnumerable<string> scopeNames);
	}
}