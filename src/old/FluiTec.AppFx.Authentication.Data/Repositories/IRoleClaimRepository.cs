using System.Collections.Generic;
using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Authentication.Data
{
	/// <summary>	Interface for role claim repository. </summary>
	public interface IRoleClaimRepository : IDataRepository<RoleClaimEntity, int>
	{
		/// <summary>	Gets the roles in this collection. </summary>
		/// <param name="role">	The role. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the roles in this collection.
		/// </returns>
		IEnumerable<RoleClaimEntity> GetByRole(RoleEntity role);

		/// <summary>	Gets the role identifiers in this collection. </summary>
		/// <param name="roleId">	Identifier for the role. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the role identifiers in this
		///     collection.
		/// </returns>
		IEnumerable<RoleClaimEntity> GetByRoleId(int roleId);

		/// <summary>	Gets the roles in this collection. </summary>
		/// <param name="roles">	The roles. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the roles in this collection.
		/// </returns>
		IEnumerable<RoleClaimEntity> GetByRoles(RoleEntity[] roles);

		/// <summary>	Gets the role identifiers in this collection. </summary>
		/// <param name="roleIds">	List of identifiers for the roles. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the role identifiers in this
		///     collection.
		/// </returns>
		IEnumerable<RoleClaimEntity> GetByRoleIds(int[] roleIds);
	}
}