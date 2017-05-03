using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.Vision.IdentityServer.Data.Compound;
using FluiTec.Vision.IdentityServer.Data.Entities;
using FluiTec.Vision.IdentityServer.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Server.Data.Mssql.Repositories
{
	/// <summary>	An identity resource repository. </summary>
	public class IdentityResourceRepository : DapperRepository<IdentityResourceEntity, int>, IIdentityResourceRepository
	{
		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="unitOfWork">	The unit of work. </param>
		public IdentityResourceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
			_logger = UnitOfWork.LoggerFactory.CreateLogger(typeof(IdentityResourceRepository));
		}

		#endregion

		#region IIdentityResourceRepository

		/// <summary>	Gets all compounds in this collection. </summary>
		/// <returns>
		/// An enumerator that allows foreach to be used to process all compounds in this collection.
		/// </returns>
		public IEnumerable<CompoundIdentityResource> GetAllCompound()
		{
			_logger.LogDebug("Fetching {0} compound.", TableName);
			var command = $"SELECT * FROM {TableName} AS iRes" +
			              $" LEFT JOIN {DataService.NameByType(typeof(IdentityResourceClaimEntity))} AS iClaim" +
			              $" ON iRes.{nameof(IdentityResourceEntity.Id)} = iClaim.{nameof(IdentityResourceClaimEntity.IdentityResourceId)}";
			var lookup = new Dictionary<int, CompoundIdentityResource>();
			UnitOfWork.Connection.Query<IdentityResourceEntity, IdentityResourceClaimEntity, CompoundIdentityResource>(command,
				(entity, claimEntity) =>
				{
					// make sure the pk exists
					if (entity == null || entity.Id == default(int))
						return null;

					// make sure our list contains the pk
					if (!lookup.ContainsKey(entity.Id))
						lookup.Add(entity.Id, new CompoundIdentityResource() { IdentityResource = entity });

					// fetch the real element
					var tempElem = lookup[entity.Id];

					// add claim
					if (claimEntity != null)
						tempElem.IdentityResourceClaims.Add(claimEntity);

					return tempElem;
				}, null, UnitOfWork.Transaction);
			return lookup.Values;
		}

		/// <summary>	Gets the names compounds in this collection. </summary>
		/// <param name="scopeNames">	The names. </param>
		/// <returns>
		/// An enumerator that allows foreach to be used to process the names compounds in this
		/// collection.
		/// </returns>
		public IEnumerable<CompoundIdentityResource> GetByScopeNamesCompound(IEnumerable<string> scopeNames)
		{
			_logger.LogDebug("Fetching {0} compound by scopeNames.", TableName);
			var command = $"SELECT * FROM {TableName} AS iRes" +
						 $" LEFT JOIN {DataService.NameByType(typeof(IdentityResourceClaimEntity))} AS iClaim" +
			              $" ON iRes.{nameof(IdentityResourceEntity.Id)} = iClaim.{nameof(IdentityResourceClaimEntity.IdentityResourceId)}" +
			              $" LEFT JOIN {DataService.NameByType(typeof(IdentityResourceScopeEntity))} AS iScope" +
			              $" ON iRes.{nameof(IdentityResourceEntity.Id)} = iScope.{nameof(IdentityResourceScopeEntity.IdentityResourceId)}" +
			              $" LEFT JOIN {DataService.NameByType(typeof(ScopeEntity))} AS scope" +
			              $" ON iScope.{nameof(IdentityResourceScopeEntity.ScopeId)} = scope.{nameof(ScopeEntity.Id)}" +
			              $" WHERE scope.{nameof(ScopeEntity.Name)} IN @ScopeNames";
			var lookup = new Dictionary<int, CompoundIdentityResource>();
			UnitOfWork.Connection.Query<IdentityResourceEntity, IdentityResourceClaimEntity, IdentityResourceScopeEntity, ScopeEntity, CompoundIdentityResource>(command,
				(entity, identityClaim, identityScope, scope) =>
				{
					// make sure the pk exists
					if (entity == null || entity.Id == default(int))
						return null;

					// make sure our list contains the pk
					if (!lookup.ContainsKey(entity.Id))
						lookup.Add(entity.Id, new CompoundIdentityResource()
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
				}, new { ScopeNames = scopeNames }, UnitOfWork.Transaction);
			return lookup.Values;
		}

		#endregion
	}
}