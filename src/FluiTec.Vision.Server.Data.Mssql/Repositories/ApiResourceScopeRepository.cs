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
	/// <summary>	An API resource scope repository. </summary>
	public class ApiResourceScopeRepository : DapperRepository<ApiResourceScopeEntity, int>, IApiResourceScopeRepository
	{
		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public ApiResourceScopeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_logger = unitOfWork.LoggerFactory.CreateLogger(typeof(ApiResourceScopeRepository));
		}

		#endregion

		#region IApiResourceScopeRepository

		/// <summary>	Gets the identifiers in this collection. </summary>
		/// <param name="ids">	The identifiers. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identifiers in this collection.
		/// </returns>
		public IEnumerable<ApiResourceScopeEntity> GetByScopeIds(int[] ids)
		{
			_logger.LogDebug("Fetching {0} by {1} with {2}='{3}'", TableName, nameof(ids), nameof(ids), ids);
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ApiResourceScopeEntity.ScopeId)} IN @Ids";
			return UnitOfWork.Connection.Query<ApiResourceScopeEntity>(command, new {Ids = ids},
				UnitOfWork.Transaction);
		}

		/// <summary>	Gets the API identifiers in this collection. </summary>
		/// <param name="ids">	The identifiers. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the API identifiers in this
		///     collection.
		/// </returns>
		public IEnumerable<ApiResourceScopeEntity> GetByApiIds(int[] ids)
		{
			_logger.LogDebug("Fetching {0} by {1} with {2}='{3}'", TableName, nameof(ids), nameof(ids), ids);
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ApiResourceScopeEntity.ApiResourceId)} IN @Ids";
			return UnitOfWork.Connection.Query<ApiResourceScopeEntity>(command, new { Ids = ids },
				UnitOfWork.Transaction);
		}

		#endregion
	}
}