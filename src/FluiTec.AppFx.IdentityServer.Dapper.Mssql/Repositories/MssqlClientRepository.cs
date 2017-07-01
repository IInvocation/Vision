using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Compound;
using FluiTec.AppFx.IdentityServer.Dapper.Repositories;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Dapper.Mssql.Repositories
{
	/// <summary>	A mssql client repository. </summary>
	public class MssqlClientRepository : ClientRepository
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public MssqlClientRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#endregion

		#region ClientRepository

		/// <summary>	Gets by client identifier. </summary>
		/// <param name="clientId">	Identifier for the client. </param>
		/// <returns>	The by client identifier. </returns>
		public override ClientEntity GetByClientId(string clientId)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ClientEntity.ClientId)} = @ClientId";
			return UnitOfWork.Connection.QuerySingleOrDefault<ClientEntity>(command, new { ClientId = clientId },
				UnitOfWork.Transaction);
		}

		/// <summary>	Gets compound by client identifier. </summary>
		/// <param name="clientId">	Identifier for the client. </param>
		/// <returns>	The compound by client identifier. </returns>
		public override CompoundClientEntity GetCompoundByClientId(string clientId)
		{
			var command = $"SELECT * FROM {TableName} AS client" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(ClientScopeEntity))} AS cScope" +
			              $" ON client.{nameof(ClientEntity.Id)} = cScope.{nameof(ClientScopeEntity.ClientId)}" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(ScopeEntity))} AS scope" +
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