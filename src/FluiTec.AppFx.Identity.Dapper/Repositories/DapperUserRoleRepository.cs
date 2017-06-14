using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Identity.Entities;
using FluiTec.AppFx.Identity.Repositories;

namespace FluiTec.AppFx.Identity.Dapper.Repositories
{
	/// <summary>	A dapper user role repository. </summary>
	public abstract class DapperUserRoleRepository : DapperRepository<IdentityUserRoleEntity, int>, IUserRoleRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		protected DapperUserRoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>	Searches for the first user identifier and role identifier. </summary>
		/// <param name="userId">	Identifier for the user. </param>
		/// <param name="roleId">	Identifier for the role. </param>
		/// <returns>	The found user identifier and role identifier. </returns>
		public abstract IdentityUserRoleEntity FindByUserIdAndRoleId(int userId, int roleId);

		/// <summary>	Finds the users in this collection. </summary>
		/// <param name="user">	The user. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the users in this collection.
		/// </returns>
		public abstract IEnumerable<int> FindByUser(IdentityUserEntity user);

		/// <summary>	Finds the roles in this collection. </summary>
		/// <param name="role">	The role. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the roles in this collection.
		/// </returns>
		public abstract IEnumerable<int> FindByRole(IdentityRoleEntity role);
	}
}