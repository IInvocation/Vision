using System;
using Dapper;
using FluiTec.AppFx.Authentication.Data;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Server.Data.Mssql.Repositories
{
	/// <summary>	A user repository. </summary>
	public class UserRepository : DapperRepository<UserEntity, int>, IUserRepository
	{
		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_logger = unitOfWork.LoggerFactory.CreateLogger(typeof(UserRepository));
		}

		#endregion

		#region IUserRepository

		/// <summary>	Gets by user name. </summary>
		/// <param name="userName">	Name of the user. </param>
		/// <returns>	The by user name. </returns>
		public UserEntity GetByUserName(string userName)
		{
			_logger.LogDebug("Fetching {0} by {1} with {2}='{3}'", TableName, nameof(userName), nameof(userName), userName);
			var command = $"SELECT * FROM {TableName} WHERE {nameof(UserEntity.UserName)} = @UserName";
			return UnitOfWork.Connection.QuerySingleOrDefault<UserEntity>(command, new {UserName = userName},
				UnitOfWork.Transaction);
		}

		/// <summary>	Gets by user ídentifier. </summary>
		/// <param name="identifier">	The identifier. </param>
		/// <returns>	The by user ídentifier. </returns>
		public UserEntity GetByUserIdentifier(Guid identifier)
		{
			_logger.LogDebug("Fetching {0} by {1} with {1}='{2}'", TableName, nameof(identifier), identifier);
			var command = $"SELECT * FROM {TableName} WHERE {nameof(UserEntity.UniqueId)} = @UniqueId";
			return UnitOfWork.Connection.QuerySingleOrDefault<UserEntity>(command, new {UniqueId = identifier},
				UnitOfWork.Transaction);
		}

		/// <summary>	Queries if a given already exists. </summary>
		/// <param name="userName">	Name of the user. </param>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		public bool AlreadyExists(string userName)
		{
			_logger.LogDebug("Validate {0} already exists by {1} with {2}='{3}'", TableName, nameof(userName), nameof(userName), userName);
			var command =
				$"SELECT COUNT({nameof(UserEntity.Id)}) FROM {TableName} WHERE {nameof(UserEntity.UserName)} = @UserName OR {nameof(UserEntity.Email)} = @Email";
			return UnitOfWork.Connection.ExecuteScalar<int>(command, new {UserName = userName, Email = userName},
				       UnitOfWork.Transaction) > 0;
		}

		/// <summary>	Increase access failed count. </summary>
		/// <param name="entity">	The entity. </param>
		public void IncreaseAccessFailedCount(UserEntity entity)
		{
			_logger.LogDebug("Increase AccessFailedCount in {0} using entity with key '{1}'", TableName, entity.Id);
			var command = $"UPDATE {TableName} SET " +
			              $"{nameof(UserEntity.AccessFailedCount)} = @FailCount, " + 
						  $"{nameof(UserEntity.LockedOutTill)} = @LockedTill " +
						  $"WHERE {nameof(UserEntity.Id)} = @Id";

			UnitOfWork.Connection.ExecuteScalar(command, new{ FailCount = entity.AccessFailedCount, LockedTill = entity.LockedOutTill, entity.Id}, UnitOfWork.Transaction);
		}

		#endregion
	}
}