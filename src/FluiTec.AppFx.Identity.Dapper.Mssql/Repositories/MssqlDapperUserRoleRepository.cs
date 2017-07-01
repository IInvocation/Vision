using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Identity.Dapper.Repositories;
using FluiTec.AppFx.Identity.Entities;

namespace FluiTec.AppFx.Identity.Dapper.Mssql.Repositories
{
	/// <summary>	A mssql dapper user role repository. </summary>
	public class MssqlDapperUserRoleRepository : DapperUserRoleRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public MssqlDapperUserRoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>	Searches for the first user identifier and role identifier. </summary>
		/// <param name="userId">	Identifier for the user. </param>
		/// <param name="roleId">	Identifier for the role. </param>
		/// <returns>	The found user identifier and role identifier. </returns>
		public override IdentityUserRoleEntity FindByUserIdAndRoleId(int userId, int roleId)
		{
			var command =
				$"SELECT * FROM {TableName} WHERE {nameof(IdentityUserRoleEntity.UserId)} = @UserId AND {nameof(IdentityUserRoleEntity.RoleId)} = @RoleId";
			return UnitOfWork.Connection.QuerySingleOrDefault<IdentityUserRoleEntity>(command,
				new {UserId = userId, RoleId = roleId},
				UnitOfWork.Transaction);
		}

		/// <summary>	Finds the users in this collection. </summary>
		/// <param name="user">	The user. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the users in this collection.
		/// </returns>
		public override IEnumerable<int> FindByUser(IdentityUserEntity user)
		{
			var command =
				$"SELECT {nameof(IdentityUserRoleEntity.RoleId)} FROM {TableName} WHERE {nameof(IdentityUserRoleEntity.UserId)} = @UserId";
			return UnitOfWork.Connection.Query<int>(command, new {UserId = user.Id},
				UnitOfWork.Transaction);
		}

		/// <summary>	Finds the roles in this collection. </summary>
		/// <param name="role">	The role. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the roles in this collection.
		/// </returns>
		public override IEnumerable<int> FindByRole(IdentityRoleEntity role)
		{
			var command =
				$"SELECT {nameof(IdentityUserRoleEntity.Id)} FROM {TableName} WHERE {nameof(IdentityUserRoleEntity.RoleId)} = @RoleId";
			return UnitOfWork.Connection.Query<int>(command, new {RoleId = role.Id},
				UnitOfWork.Transaction);
		}
	}
}