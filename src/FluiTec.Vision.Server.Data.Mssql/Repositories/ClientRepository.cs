using Dapper;
using FluiTec.AppFx.Authentication.Data;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Server.Data.Mssql.Repositories
{
	/// <summary>	A client repository. </summary>
	public class ClientRepository : DapperRepository<ClientEntity, int>, IClientRepository
	{
		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public ClientRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_logger = unitOfWork.LoggerFactory.CreateLogger(typeof(ClientRepository));
		}

		#endregion

		#region IClientRepository

		/// <summary>	Gets by client identifier. </summary>
		/// <param name="clientId">	Identifier for the client. </param>
		/// <returns>	The by client identifier. </returns>
		public ClientEntity GetByClientId(string clientId)
		{
			_logger.LogDebug("Fetching {0} by {1} with {2}='{3}'", TableName, nameof(clientId), nameof(clientId), clientId);
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ClientEntity.ClientId)} = @ClientId";
			return UnitOfWork.Connection.QuerySingleOrDefault<ClientEntity>(command, new { ClientId = clientId },
				UnitOfWork.Transaction);
		}

		#endregion
	}
}