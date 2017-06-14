using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.Vision.IdentityServer.Data.Entities;
using FluiTec.Vision.IdentityServer.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Server.Data.Mssql.Repositories
{
	/// <summary>	An identity resource claim repository. </summary>
	public class IdentityResourceClaimRepository : DapperRepository<IdentityResourceClaimEntity, int>, IIdentityResourceClaimRepository
	{
		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public IdentityResourceClaimRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_logger = unitOfWork.LoggerFactory.CreateLogger(typeof(IdentityResourceClaimRepository));
		}

		#endregion

		#region IApiResourceClaimRepository

		/// <summary>	Gets the identity identifiers in this collection. </summary>
		/// <param name="id">	The identifier. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identity identifiers in this
		///     collection.
		/// </returns>
		public IEnumerable<IdentityResourceClaimEntity> GetByIdentityId(int id)
		{
			_logger.LogDebug("Fetching {0} by {1} with {2}='{3}'", TableName, nameof(id), nameof(id), id);
			var command = $"SELECT * FROM {TableName} WHERE {nameof(IdentityResourceClaimEntity.IdentityResourceId)} = @CId";
			return UnitOfWork.Connection.Query<IdentityResourceClaimEntity>(command, new { CId = id },
				UnitOfWork.Transaction);
		}

		#endregion
	}
}