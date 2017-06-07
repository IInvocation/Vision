using System.Collections.Generic;
using System.Linq;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.Vision.IdentityServer.Data.Compound;
using FluiTec.Vision.IdentityServer.Data.Entities;
using FluiTec.Vision.IdentityServer.Data.Repositories;
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

		/// <summary>	Gets compound by client identifier. </summary>
		/// <param name="clientId">	Identifier for the client. </param>
		/// <returns>	The compound by client identifier. </returns>
		public CompoundClientEntity GetCompoundByClientId(string clientId)
		{
			_logger.LogDebug("Fetching {0} by {1} with {2}='{3}'", TableName, nameof(clientId), nameof(clientId), clientId);
			var command = $"SELECT * FROM {TableName} AS client" +
			              $" LEFT JOIN {DataService.NameByType(typeof(ClientScopeEntity))} AS cScope" +
			              $" ON client.{nameof(ClientEntity.Id)} = cScope.{nameof(ClientScopeEntity.ClientId)}" +
			              $" LEFT JOIN {DataService.NameByType(typeof(ScopeEntity))} AS scope" +
			              $" ON cScope.{nameof(ClientScopeEntity.ScopeId)} = scope.{nameof(ScopeEntity.Id)}" +
			              $" WHERE client.{nameof(ClientEntity.ClientId)} = @ClientId";
			var lookup = new Dictionary<int, CompoundClientEntity>();
			UnitOfWork.Connection.Query<ClientEntity, ClientScopeEntity, ScopeEntity, CompoundClientEntity>(command,
				(entity, clientScope, scope) =>
				{
					// make sure the pk exists
					if (entity == null || entity.Id == default(int))
						return null;

					// make sure our list contains the pk
					if (!lookup.ContainsKey(entity.Id))
						lookup.Add(entity.Id, new CompoundClientEntity
						{
							Client = entity
						});

					// fetch the real element
					var tempElem = lookup[entity.Id];

					// add scope
					if (scope != null)
						tempElem.Scopes.Add(scope);

					return tempElem;
				}, new { ClientId = clientId }, UnitOfWork.Transaction);
			return lookup.Values.Single();
		}

		#endregion
	}
}