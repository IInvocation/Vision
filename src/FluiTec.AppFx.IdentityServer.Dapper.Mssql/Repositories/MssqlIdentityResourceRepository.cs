using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.IdentityServer.Compound;
using FluiTec.AppFx.IdentityServer.Dapper.Repositories;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Dapper.Mssql.Repositories
{
	/// <summary>	A mssql identity resource repository. </summary>
	public class MssqlIdentityResourceRepository : IdentityResourceRepository
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public MssqlIdentityResourceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		#endregion

		#region IdentityResourceRepository

		/// <summary>	Gets all compounds in this collection. </summary>
		/// <returns>
		///     An enumerator that allows foreach to be used to process all compounds in this collection.
		/// </returns>
		public override IEnumerable<CompoundIdentityResource> GetAllCompound()
		{
			var command = $"SELECT * FROM {TableName} AS iRes" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(IdentityResourceClaimEntity))} AS iClaim" +
			              $" ON iRes.{nameof(IdentityResourceEntity.Id)} = iClaim.{nameof(IdentityResourceClaimEntity.IdentityResourceId)}" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(IdentityResourceScopeEntity))} AS iScope" +
			              $" ON iRes.{nameof(IdentityResourceEntity.Id)} = iScope.{nameof(IdentityResourceScopeEntity.IdentityResourceId)}" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(ScopeEntity))} AS scope" +
			              $" ON iScope.{nameof(IdentityResourceScopeEntity.ScopeId)} = scope.{nameof(ScopeEntity.Id)}";
			var lookup = new Dictionary<int, CompoundIdentityResource>();
			UnitOfWork.Connection
				.Query<IdentityResourceEntity, IdentityResourceClaimEntity, IdentityResourceScopeEntity, ScopeEntity,
					CompoundIdentityResource>(command,
					(entity, identityClaim, identityScope, scope) =>
					{
						// make sure the pk exists
						if (entity == null || entity.Id == default(int))
							return null;

						// make sure our list contains the pk
						if (!lookup.ContainsKey(entity.Id))
							lookup.Add(entity.Id, new CompoundIdentityResource
							{
								IdentityResource = entity
							});

						// fetch the real element
						var tempElem = lookup[entity.Id];

						// add identity-scope
						if (identityScope != null)
							tempElem.IdentityResourceScopes.Add(identityScope);

						// add claim
						if (identityClaim != null)
							tempElem.IdentityResourceClaims.Add(identityClaim);

						// add scope
						if (scope != null)
							tempElem.Scopes.Add(scope);

						return tempElem;
					}, param: null, transaction: UnitOfWork.Transaction);
			return lookup.Values;
		}

		/// <summary>	Gets the scope names compounds in this collection. </summary>
		/// <param name="scopeNames">	List of names of the scopes. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the scope names compounds in this
		///     collection.
		/// </returns>
		public override IEnumerable<CompoundIdentityResource> GetByScopeNamesCompound(IEnumerable<string> scopeNames)
		{
			var command = $"SELECT * FROM {TableName} AS iRes" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(IdentityResourceClaimEntity))} AS iClaim" +
			              $" ON iRes.{nameof(IdentityResourceEntity.Id)} = iClaim.{nameof(IdentityResourceClaimEntity.IdentityResourceId)}" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(IdentityResourceScopeEntity))} AS iScope" +
			              $" ON iRes.{nameof(IdentityResourceEntity.Id)} = iScope.{nameof(IdentityResourceScopeEntity.IdentityResourceId)}" +
			              $" LEFT JOIN {UnitOfWork.DapperDataService.NameService.NameByType(typeof(ScopeEntity))} AS scope" +
			              $" ON iScope.{nameof(IdentityResourceScopeEntity.ScopeId)} = scope.{nameof(ScopeEntity.Id)}" +
			              $" WHERE scope.{nameof(ScopeEntity.Name)} IN @ScopeNames";
			var lookup = new Dictionary<int, CompoundIdentityResource>();
			UnitOfWork.Connection
				.Query<IdentityResourceEntity, IdentityResourceClaimEntity, IdentityResourceScopeEntity, ScopeEntity,
					CompoundIdentityResource>(command,
					(entity, identityClaim, identityScope, scope) =>
					{
						// make sure the pk exists
						if (entity == null || entity.Id == default(int))
							return null;

						// make sure our list contains the pk
						if (!lookup.ContainsKey(entity.Id))
							lookup.Add(entity.Id, new CompoundIdentityResource
							{
								IdentityResource = entity
							});

						// fetch the real element
						var tempElem = lookup[entity.Id];

						// add identity-scope
						if (identityScope != null)
							tempElem.IdentityResourceScopes.Add(identityScope);

						// add claim
						if (identityClaim != null)
							tempElem.IdentityResourceClaims.Add(identityClaim);

						// add scope
						if (scope != null)
							tempElem.Scopes.Add(scope);

						return tempElem;
					}, new {ScopeNames = scopeNames}, UnitOfWork.Transaction);
			return lookup.Values;
		}

		#endregion
	}
}