using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using FluiTec.AppFx.Authentication.Data;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Server.Data.Mssql.Repositories
{
	/// <summary>	A role claim repository. </summary>
	public class RoleClaimRepository : DapperRepository<RoleClaimEntity, int>, IRoleClaimRepository
	{
		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public RoleClaimRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_logger = unitOfWork.LoggerFactory.CreateLogger(typeof(RoleClaimRepository));
		}

		#endregion

		#region IRoleClaimRepository

		/// <summary>	Gets the roles in this collection. </summary>
		/// <param name="role">	The role. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the roles in this collection.
		/// </returns>
		public IEnumerable<RoleClaimEntity> GetByRole(RoleEntity role)
		{
			return GetByRoleId(role.Id);
		}

		/// <summary>	Gets the role identifiers in this collection. </summary>
		/// <param name="roleId">	Identifier for the role. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the role identifiers in this
		///     collection.
		/// </returns>
		public IEnumerable<RoleClaimEntity> GetByRoleId(int roleId)
		{
			_logger.LogDebug("Fetching {0} by {1} with {2}='{3}'", TableName, nameof(roleId), nameof(roleId), roleId);
			var command = $"SELECT * FROM {TableName} WHERE {nameof(UserClaimEntity.UserId)} = @RId";
			return UnitOfWork.Connection.Query<RoleClaimEntity>(command, new { RId = roleId },
				UnitOfWork.Transaction);
		}

		/// <summary>	Gets the roles in this collection. </summary>
		/// <param name="roles">	The roles. </param>
		/// <returns>
		/// An enumerator that allows foreach to be used to process the roles in this collection.
		/// </returns>
		public IEnumerable<RoleClaimEntity> GetByRoles(RoleEntity[] roles)
		{
			return GetByRoleIds(roles.Select(r => r.Id).ToArray());
		}

		/// <summary>	Gets the role identifiers in this collection. </summary>
		/// <param name="roleIds">	List of identifiers for the roles. </param>
		/// <returns>
		/// An enumerator that allows foreach to be used to process the role identifiers in this
		/// collection.
		/// </returns>
		public IEnumerable<RoleClaimEntity> GetByRoleIds(int[] roleIds)
		{
			_logger.LogDebug("Fetching {0} by {1} with {2}='{3}'", TableName, nameof(roleIds), nameof(roleIds), roleIds);
			var command = $"SELECT * FROM {TableName} WHERE {nameof(RoleClaimEntity.RoleId)} IN @RoIds";
			return UnitOfWork.Connection.Query<RoleClaimEntity>(command, new { RoIds = roleIds },
				UnitOfWork.Transaction);
		}

		#endregion
	}
}