using System.Collections.Generic;
using System.Linq;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Compound;
using FluiTec.AppFx.IdentityServer.Dapper.Repositories;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Dapper.Mssql.Repositories
{
	/// <summary>	A mssql API resource repository. </summary>
	public class MssqlApiResourceRepository : ApiResourceRepository
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public MssqlApiResourceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#endregion

		#region ApiResourceRepository

		/// <summary>	Gets by name. </summary>
		/// <param name="name">	The name. </param>
		/// <returns>	The by name. </returns>
		public override ApiResourceEntity GetByName(string name)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ClientEntity.ClientId)} = @Name";
			return UnitOfWork.Connection.QuerySingleOrDefault<ApiResourceEntity>(command, new {Name = name},
				UnitOfWork.Transaction);
		}

		/// <summary>	Gets the identifiers in this collection. </summary>
		/// <param name="ids">	The identifiers. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the identifiers in this collection.
		/// </returns>
		public override IEnumerable<ApiResourceEntity> GetByIds(int[] ids)
		{
			var command = $"SELECT * FROM {TableName} WHERE {nameof(ApiResourceEntity.Id)} IN @Ids";
			return UnitOfWork.Connection.Query<ApiResourceEntity>(command, new {Ids = ids},
				UnitOfWork.Transaction);
		}

		/// <summary>	Gets all compounds in this collection. </summary>
		/// <returns>
		///     An enumerator that allows foreach to be used to process all compounds in this collection.
		/// </returns>
		public override IEnumerable<CompoundApiResource> GetAllCompound()
		{
			var command = $"SELECT * FROM {TableName} AS aRes" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(ApiResourceScopeEntity))} AS aScope" +
			              $" ON aRes.{nameof(ApiResourceEntity.Id)} = aScope.{nameof(ApiResourceScopeEntity.ApiResourceId)}" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(ApiResourceClaimEntity))} AS aClaim" +
			              $" ON aRes.{nameof(ApiResourceEntity.Id)} = aClaim.{nameof(ApiResourceClaimEntity.ApiResourceId)}" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(ScopeEntity))} AS scope" +
			              $" ON aScope.{nameof(ApiResourceScopeEntity.ScopeId)} = scope.{nameof(ScopeEntity.Id)}";
			var lookup = new Dictionary<int, CompoundApiResource>();
			UnitOfWork.Connection
				.Query<ApiResourceEntity, ApiResourceScopeEntity, ApiResourceClaimEntity, ScopeEntity, CompoundApiResource>(command,
					(entity, apiScope, apiClaim, scope) =>
					{
						// make sure the pk exists
						if (entity == null || entity.Id == default(int))
							return null;

						// make sure our list contains the pk
						if (!lookup.ContainsKey(entity.Id))
							lookup.Add(entity.Id, new CompoundApiResource {ApiResource = entity});

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
					}, param: null, transaction: UnitOfWork.Transaction);
			return lookup.Values;
		}

		/// <summary>	Gets the scope names in this collection. </summary>
		/// <param name="scopeNames">	List of names of the scopes. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the scope names in this collection.
		/// </returns>
		public override IEnumerable<CompoundApiResource> GetByScopeNamesCompound(IEnumerable<string> scopeNames)
		{
			var command = $"SELECT * FROM {TableName} AS aRes" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(ApiResourceScopeEntity))} AS aScope" +
			              $" ON aRes.{nameof(ApiResourceEntity.Id)} = aScope.{nameof(ApiResourceScopeEntity.ApiResourceId)}" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(ApiResourceClaimEntity))} AS aClaim" +
			              $" ON aRes.{nameof(ApiResourceEntity.Id)} = aClaim.{nameof(ApiResourceClaimEntity.ApiResourceId)}" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(ScopeEntity))} AS scope" +
			              $" ON aScope.{nameof(ApiResourceScopeEntity.ScopeId)} = scope.{nameof(ScopeEntity.Id)}" +
			              $" WHERE scope.{nameof(ScopeEntity.Name)} IN @ScopeNames";
			var lookup = new Dictionary<int, CompoundApiResource>();
			UnitOfWork.Connection
				.Query<ApiResourceEntity, ApiResourceScopeEntity, ApiResourceClaimEntity, ScopeEntity, CompoundApiResource>(command,
					(entity, apiScope, apiClaim, scope) =>
					{
						// make sure the pk exists
						if (entity == null || entity.Id == default(int))
							return null;

						// make sure our list contains the pk
						if (!lookup.ContainsKey(entity.Id))
							lookup.Add(entity.Id, new CompoundApiResource
							{
								ApiResource = entity
							});

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
					}, new {ScopeNames = scopeNames}, UnitOfWork.Transaction);
			return lookup.Values;
		}

		/// <summary>	Gets the name compounts in this collection. </summary>
		/// <param name="name">	The name. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the name compounts in this collection.
		/// </returns>
		public override CompoundApiResource GetByNameCompount(string name)
		{
			var command = $"SELECT * FROM {TableName} AS aRes" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(ApiResourceScopeEntity))} AS aScope" +
			              $" ON aRes.{nameof(ApiResourceEntity.Id)} = aScope.{nameof(ApiResourceScopeEntity.ApiResourceId)}" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(ApiResourceClaimEntity))} AS aClaim" +
			              $" ON aRes.{nameof(ApiResourceEntity.Id)} = aClaim.{nameof(ApiResourceClaimEntity.ApiResourceId)}" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(ScopeEntity))} AS scope" +
			              $" ON aScope.{nameof(ApiResourceScopeEntity.ScopeId)} = scope.{nameof(ScopeEntity.Id)}" +
			              $" WHERE aRes.{nameof(ApiResourceEntity.Name)} = @ResName";
			var lookup = new Dictionary<int, CompoundApiResource>();
			UnitOfWork.Connection
				.Query<ApiResourceEntity, ApiResourceScopeEntity, ApiResourceClaimEntity, ScopeEntity, CompoundApiResource>(command,
					(entity, apiScope, apiClaim, scope) =>
					{
						// make sure the pk exists
						if (entity == null || entity.Id == default(int))
							return null;

						// make sure our list contains the pk
						if (!lookup.ContainsKey(entity.Id))
							lookup.Add(entity.Id, new CompoundApiResource
							{
								ApiResource = entity
							});

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
					}, new {ResName = name}, UnitOfWork.Transaction);
			return lookup.Values.SingleOrDefault();
		}

		#endregion
	}
}