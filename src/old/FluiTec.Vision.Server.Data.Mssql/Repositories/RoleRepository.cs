using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Authentication.Data;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Server.Data.Mssql.Repositories
{
	/// <summary>	A role repository. </summary>
	public class RoleRepository : DapperRepository<RoleEntity, int>, IRoleRepository
	{
		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public RoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_logger = unitOfWork.LoggerFactory.CreateLogger(typeof(RoleRepository));
		}

		#endregion

		#region IRoleRepository

		/// <summary>	Gets the many by identifiers in this collection. </summary>
		/// <param name="roleIds">	List of identifiers for the roles. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the many by identifiers in this
		///     collection.
		/// </returns>
		public IEnumerable<RoleEntity> GetManyById(int[] roleIds)
		{
			_logger.LogDebug("Fetching {0} by {1} with {2}='{3}'", TableName, nameof(roleIds), nameof(roleIds), roleIds);
			var command = $"SELECT * FROM {TableName} WHERE {nameof(RoleEntity.Id)} IN @RoIds";
			return UnitOfWork.Connection.Query<RoleEntity>(command, new { RoIds = roleIds },
				UnitOfWork.Transaction);
		}

		#endregion
	}
}