using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Identity.Dapper.Repositories;
using FluiTec.AppFx.Identity.Entities;

namespace FluiTec.AppFx.Identity.Dapper.Mssql.Repositories
{
	/// <summary>	A mssql dapper role repository. </summary>
	public class MssqlDapperRoleRepository : DapperRoleRepository
	{
		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public MssqlDapperRoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		/// <summary>	Gets an identity role entity using the given identifier. </summary>
		/// <param name="identifier">	The identifier to get. </param>
		/// <returns>	An IdentityRoleEntity. </returns>
		public override IdentityRoleEntity Get(string identifier)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(IdentityRoleEntity.Identifier)} = @Identifier";
			return UnitOfWork.Connection.QuerySingleOrDefault<IdentityRoleEntity>(command, new {Identifier = identifier},
				UnitOfWork.Transaction);
		}

		/// <summary>	Searches for the first lowered name. </summary>
		/// <param name="loweredName">	Name of the lowered. </param>
		/// <returns>	The found lowered name. </returns>
		public override IdentityRoleEntity FindByLoweredName(string loweredName)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(IdentityRoleEntity.LoweredName)} = @LoweredRoleName";
			return UnitOfWork.Connection.QuerySingleOrDefault<IdentityRoleEntity>(command, new {LoweredRoleName = loweredName},
				UnitOfWork.Transaction);
		}

		/// <summary>	Finds the identifiers in this collection. </summary>
		/// <param name="roleIds">	List of identifiers for the roles. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identifiers in this collection.
		/// </returns>
		public override IEnumerable<IdentityRoleEntity> FindByIds(IEnumerable<int> roleIds)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(IdentityUserEntity.Id)} IN @Ids";
			return UnitOfWork.Connection.Query<IdentityRoleEntity>(command, new {Ids = roleIds},
				UnitOfWork.Transaction);
		}
	}
}