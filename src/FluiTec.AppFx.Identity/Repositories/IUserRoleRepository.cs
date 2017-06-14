using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Identity.Entities;

namespace FluiTec.AppFx.Identity.Repositories
{
	/// <summary>	Interface for user role repository. </summary>
	public interface IUserRoleRepository : IDataRepository<IdentityUserRoleEntity, int>
	{
		/// <summary>	Searches for the first user identifier and role identifier. </summary>
		/// <param name="userId">	Identifier for the user. </param>
		/// <param name="roleId">	Identifier for the role. </param>
		/// <returns>	The found user identifier and role identifier. </returns>
		IdentityUserRoleEntity FindByUserIdAndRoleId(int userId, int roleId);

		/// <summary>	Searches for the first user. </summary>
		/// <param name="user">	The user. </param>
		/// <returns>	The found user. </returns>
		IEnumerable<int> FindByUser(IdentityUserEntity user);

		/// <summary>	Finds the roles in this collection. </summary>
		/// <param name="role">	The role. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the roles in this collection.
		/// </returns>
		IEnumerable<int> FindByRole(IdentityRoleEntity role);
	}
}