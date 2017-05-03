using System.Collections.Generic;
using FluiTec.AppFx.Data;
using FluiTec.Vision.IdentityServer.Data.Compound;
using FluiTec.Vision.IdentityServer.Data.Entities;
using FluiTec.Vision.IdentityServer.Data.Repositories;
using Microsoft.Extensions.Logging;
using FluiTec.AppFx.Data.Dapper;
using Dapper;

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
			return UnitOfWork.Connection.Query<ApiResourceEntity>(command, new {Ids = ids},
				UnitOfWork.Transaction);
		}

		/// <summary>	Gets all compounds in this collection. </summary>
		/// <returns>
		///     An enumerator that allows foreach to be used to process all compounds in this collection.
		/// </returns>
		public IEnumerable<CompoundApiResource> GetAllCompound()
		{
			_logger.LogDebug("Fetching {0} compound.", TableName);
			var command =   $"SELECT * FROM {TableName} AS aRes" + 
							$" LEFT JOIN {DataService.NameByType(typeof(ApiResourceScopeEntity))} AS aScope"+
							$" ON aRes.{nameof(ApiResourceEntity.Id)} = aScope.{nameof(ApiResourceScopeEntity.ApiResourceId)}" +
							$" LEFT JOIN {DataService.NameByType(typeof(ApiResourceClaimEntity))} AS aClaim" + 
							$" ON aRes.{nameof(ApiResourceEntity.Id)} = aClaim.{nameof(ApiResourceClaimEntity.ApiResourceId)}" +
			                $" LEFT JOIN {DataService.NameByType(typeof(ScopeEntity))} AS scope" +
			                $" ON aScope.{nameof(ApiResourceScopeEntity.ScopeId)} = scope.{nameof(ScopeEntity.Id)}";
			var lookup = new Dictionary<int, CompoundApiResource>();
			UnitOfWork.Connection.Query<ApiResourceEntity, ApiResourceScopeEntity, ApiResourceClaimEntity, ScopeEntity, CompoundApiResource>(command,
				(entity, apiScope, apiClaim, scope) =>
				{
					// make sure the pk exists
					if (entity == null || entity.Id == default(int))
						return null;

					// make sure our list contains the pk
					if (!lookup.ContainsKey(entity.Id))
						lookup.Add(entity.Id, new CompoundApiResource { ApiResource = entity });

					// fetch the real element
					var tempElem = lookup[entity.Id];

					// add api-scope
					if (apiScope != null)
						tempElem.ApiResourceScopes.Add(apiScope);

					// add claim
					if (apiClaim != null)
						tempElem.ApiResourceClaims.Add(apiClaim);

					// add scope
					if (scope != null)
						tempElem.Scopes.Add(scope);

					return tempElem;
				}, null, UnitOfWork.Transaction);
			return lookup.Values;
		}

		#endregion
	}
}