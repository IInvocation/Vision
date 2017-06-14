using System.Collections.Generic;
using FluiTec.AppFx.Data;

namespace FluiTec.AppFx.Authentication.Data
{
	/// <summary>	Interface for user role repository. </summary>
	public interface IUserRoleRepository : IDataRepository<UserRoleEntity, int>
	{
		/// <summary>	Gets the user identifiers in this collection. </summary>
		/// <param name="userId">	Identifier for the user. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the user identifiers in this
		///     collection.
		/// </returns>
		IEnumerable<UserRoleEntity> GetByUserId(int userId);

		/// <summary>	Gets the users in this collection. </summary>
		/// <param name="user">	The user. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the users in this collection.
		/// </returns>
		IEnumerable<UserRoleEntity> GetByUser(UserEntity user);
	}
}