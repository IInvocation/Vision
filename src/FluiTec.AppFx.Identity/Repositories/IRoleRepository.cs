using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Identity.Entities;

namespace FluiTec.AppFx.Identity.Repositories
{
	/// <summary>	Interface for role repository. </summary>
	public interface IRoleRepository : IDataRepository<IdentityRoleEntity, int>
	{
		/// <summary>	Gets an identity role entity using the given identifier. </summary>
		/// <param name="identifier">	The identifier to get. </param>
		/// <returns>	An IdentityRoleEntity. </returns>
		IdentityRoleEntity Get(string identifier);

		/// <summary>	Searches for the first lowered name. </summary>
		/// <param name="loweredName">	Name of the lowered. </param>
		/// <returns>	The found lowered name. </returns>
		IdentityRoleEntity FindByLoweredName(string loweredName);

		/// <summary>	Finds the identifiers in this collection. </summary>
		/// <param name="roleIds">	List of identifiers for the roles. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identifiers in this collection.
		/// </returns>
		IEnumerable<IdentityRoleEntity> FindByIds(IEnumerable<int> roleIds);
	}
}