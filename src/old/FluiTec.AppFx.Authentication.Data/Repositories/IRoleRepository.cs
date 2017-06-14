using System.Collections.Generic;
using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Authentication.Data
{
	/// <summary>	Interface for role repository. </summary>
	public interface IRoleRepository : IDataRepository<RoleEntity, int>
	{
		/// <summary>	Gets the many by identifiers in this collection. </summary>
		/// <param name="roleIds">	List of identifiers for the roles. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the many by identifiers in this
		///     collection.
		/// </returns>
		IEnumerable<RoleEntity> GetManyById(int[] roleIds);
	}
}