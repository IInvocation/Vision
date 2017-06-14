using System;
using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.Vision.IdentityServer.Data.Entities;
using FluiTec.Vision.IdentityServer.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Server.Data.Mssql.Repositories
{
	/// <summary>	A scope repository. </summary>
	public class ScopeRepository : DapperRepository<ScopeEntity, int>, IScopeRepository
	{
		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public ScopeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_logger = unitOfWork.LoggerFactory.CreateLogger(typeof(ScopeRepository));
		}

		#endregion

		#region IScopeRepository

		/// <summary>	Gets the identifiers in this collection. </summary>
		/// <param name="ids">	The identifiers. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identifiers in this collection.
		/// </returns>
		public IEnumerable<ScopeEntity> GetByIds(int[] ids)
		{
			_logger.LogDebug("Fetching {0} by {1} with {2}='{3}'", TableName, nameof(ids), nameof(ids), ids);
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ScopeEntity.Id)} IN @Ids";
			return UnitOfWork.Connection.Query<ScopeEntity>(command, new {Ids = ids},
				UnitOfWork.Transaction);
		}

		/// <summary>	Gets the names in this collection. </summary>
		/// <param name="names">	The names. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the names in this collection.
		/// </returns>
		public IEnumerable<ScopeEntity> GetByNames(string[] names)
		{
			_logger.LogDebug("Fetching {0} by {1} with {2}='{3}'", TableName, nameof(names), nameof(names), names);
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ScopeEntity.Name)} IN @Names";
			return UnitOfWork.Connection.Query<ScopeEntity>(command, new { Names = names },
				UnitOfWork.Transaction);
		}

		#endregion
	}
}