using System;
using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Authentication.Data;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Server.Data.Mssql.Repositories
{
	/// <summary>	A claim repository. </summary>
	public class ClaimRepository : DapperRepository<UserClaimEntity, int>, IClaimRepository
	{
		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public ClaimRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_logger = unitOfWork.LoggerFactory.CreateLogger(typeof(ClaimRepository));
		}

		#endregion

		#region IClaimRepository

		/// <summary>	Gets the user identifiers in this collection. </summary>
		/// <param name="userId">	Identifier for the user. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the user identifiers in this
		///     collection.
		/// </returns>
		public IEnumerable<UserClaimEntity> GetByUserId(int userId)
		{
			_logger.LogDebug("Fetching {0} by {1} with {2}='{3}'", TableName, nameof(userId), nameof(userId), userId);
			var command = $"SELECT * FROM {TableName} WHERE {nameof(UserClaimEntity.UserId)} = @UId";
			return UnitOfWork.Connection.Query<UserClaimEntity>(command, new { UId = userId },
				UnitOfWork.Transaction);
		}

		#endregion
	}
}