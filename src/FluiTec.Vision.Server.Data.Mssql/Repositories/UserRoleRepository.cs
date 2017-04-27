using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Authentication.Data;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Server.Data.Mssql.Repositories
{
    /// <summary>	A user role repository. </summary>
    public class UserRoleRepository : DapperRepository<UserRoleEntity, int>, IUserRoleRepository
    {
		#region Fields

	    /// <summary>	The logger. </summary>
	    private readonly ILogger _logger;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public UserRoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_logger = unitOfWork.LoggerFactory.CreateLogger(typeof(UserRoleRepository));
		}

		#endregion

		#region IUserRoleRepository

		/// <summary>	Gets the user identifiers in this collection. </summary>
		///
		/// <param name="userId">	Identifier for the user. </param>
		///
		/// <returns>
		/// An enumerator that allows foreach to be used to process the user identifiers in this
		/// collection.
		/// </returns>
		public IEnumerable<UserRoleEntity> GetByUserId(int userId)
	    {
			_logger.LogDebug("Fetching {0} by {1} with {1}='{2}'", TableName, nameof(userId), userId);
		    var command = $"SELECT * FROM {TableName} WHERE {nameof(UserRoleEntity.UserId)} = @UsrId";
		    return UnitOfWork.Connection.Query<UserRoleEntity>(command, new { usrId = userId },
			    UnitOfWork.Transaction);
		}

	    /// <summary>	Gets the users in this collection. </summary>
	    ///
	    /// <param name="user">	The user. </param>
	    ///
	    /// <returns>
	    /// An enumerator that allows foreach to be used to process the users in this collection.
	    /// </returns>
	    public IEnumerable<UserRoleEntity> GetByUser(UserEntity user)
	    {
		    return GetByUserId(user.Id);
	    }

		#endregion
	}
}
