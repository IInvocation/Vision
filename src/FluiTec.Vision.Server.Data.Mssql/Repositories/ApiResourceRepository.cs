using System;
using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Authentication.Data;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.Vision.IdentityServer.Data.Entities;
using FluiTec.Vision.IdentityServer.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Server.Data.Mssql.Repositories
{
	/// <summary>	An API resource repository. </summary>
	public class ApiResourceRepository : DapperRepository<ApiResourceEntity, int>, IApiResourceRepository
	{
		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public ApiResourceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_logger = unitOfWork.LoggerFactory.CreateLogger(typeof(ApiResourceRepository));
		}

		#endregion

		#region IApiResourceRepository

		/// <summary>	Gets by name. </summary>
		/// <param name="name">	The name. </param>
		/// <returns>	The by name. </returns>
		public ApiResourceEntity GetByName(string name)
		{
			_logger.LogDebug("Fetching {0} by {1} with {2}='{3}'", TableName, nameof(name), nameof(name), name);
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ClientEntity.ClientId)} = @Name";
			return UnitOfWork.Connection.QuerySingleOrDefault<ApiResourceEntity>(command, new {Name = name},
				UnitOfWork.Transaction);
		}

		/// <summary>	Gets the identifiers in this collection. </summary>
		/// <param name="ids">	The identifiers. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identifiers in this collection.
		/// </returns>
		public IEnumerable<ApiResourceEntity> GetByIds(int[] ids)
		{
			_logger.LogDebug("Fetching {0} by {1} with {2}='{3}'", TableName, nameof(ids), nameof(ids), ids);
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ApiResourceEntity.Id)} IN @Ids";
			return UnitOfWork.Connection.Query<ApiResourceEntity>(command, new { Ids = ids },
				UnitOfWork.Transaction);
		}

		#endregion
	}
}