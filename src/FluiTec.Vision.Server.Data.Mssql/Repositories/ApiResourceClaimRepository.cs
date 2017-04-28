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
	/// <summary>	An API resource claim repository. </summary>
	public class ApiResourceClaimRepository : DapperRepository<ApiResourceClaimEntity, int>, IApiResourceClaimRepository
	{
		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public ApiResourceClaimRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_logger = unitOfWork.LoggerFactory.CreateLogger(typeof(ApiResourceClaimRepository));
		}

		#endregion

		#region IApiResourceClaimRepository

		/// <summary>	Gets the api identifiers in this collection. </summary>
		/// <param name="id">	The identifier. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identity identifiers in this
		///     collection.
		/// </returns>
		public IEnumerable<ApiResourceClaimEntity> GetByApiId(int id)
		{
			_logger.LogDebug("Fetching {0} by {1} with {2}='{3}'", TableName, nameof(id), nameof(id), id);
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ApiResourceClaimEntity.ApiResourceId)} = @CId";
			return UnitOfWork.Connection.Query<ApiResourceClaimEntity>(command, new { CId = id },
				UnitOfWork.Transaction);
		}

		#endregion
	}
}