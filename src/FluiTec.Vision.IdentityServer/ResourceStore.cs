using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluiTec.Vision.IdentityServer.Data;
using FluiTec.Vision.IdentityServer.Data.Entities;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace FluiTec.Vision.IdentityServer
{
	/// <summary>	A resource store. </summary>
	public class ResourceStore : IResourceStore
	{
		#region Fields

		/// <summary>	The data service. </summary>
		private readonly IIdentityServerDataService _dataService;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="dataService">	The data service. </param>
		public ResourceStore(IIdentityServerDataService dataService)
		{
			_dataService = dataService;
		}

		#endregion

		#region IResourceStore

		/// <summary>	Searches for the first identity resources by scope asynchronous. </summary>
		/// <param name="scopeNames">	List of names of the scopes. </param>
		/// <returns>	The found identity resources by scope asynchronous. </returns>
		public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
		{
			return Task.FromResult(GetAllIdentityResources().Where(r => scopeNames.ToList().Contains(r.Name)));
		}

		/// <summary>	Searches for the first API resources by scope asynchronous. </summary>
		/// <param name="scopeNames">	List of names of the scopes. </param>
		/// <returns>	The found API resources by scope asynchronous. </returns>
		public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
		{
			return Task<IEnumerable<ApiResource>>.Factory.StartNew(() =>
			{
				using (var uow = _dataService.StartUnitOfWork())
				{
					var matchedScopes = uow.ScopeRepository.GetByNames(scopeNames.ToArray());
					var matchedScopesArray = matchedScopes as ScopeEntity[] ?? matchedScopes.ToArray();
					if (matchedScopesArray != null && !matchedScopesArray.Any())
						return Enumerable.Empty<ApiResource>();

					var apiScopes = uow.ApiResourceScopeRepository.GetByScopeIds(matchedScopesArray.Select(s => s.Id).ToArray());
					var apiScopesArray = apiScopes as ApiResourceScopeEntity[] ?? apiScopes.ToArray();
					if (apiScopes == null || !apiScopesArray.Any())
						return Enumerable.Empty<ApiResource>();

					var apiResources = uow.ApiResourceRepository.GetByIds(apiScopesArray.Select(s => s.ApiResourceId).ToArray());
					var apiResourcesArray = apiResources as ApiResourceEntity[] ?? apiResources.ToArray();
					if (apiResources == null || !apiResourcesArray.Any())
						return Enumerable.Empty<ApiResource>();

					var scopeEntites = uow.ScopeRepository.GetByIds(apiScopesArray.Select(s => s.ScopeId).ToArray());

					var apiClaims = uow.ApiResourceClaimRepository.GetAll();

					return apiResourcesArray.Select(r => new ApiResource
					{
						Name = r.Name,
						DisplayName = r.DisplayName,
						Description = r.Description,
						Enabled = r.Enabled,
						UserClaims = new List<string>(apiClaims.Where(c => c.ApiResourceId == r.Id).Select(c => c.ClaimType).ToList()),
						Scopes = new List<Scope>(scopeEntites.Select(s => new Scope
						{
							Name = s.Name,
							DisplayName = s.DisplayName,
							Description = s.Description,
							Required = s.Required,
							Emphasize = s.Emphasize,
							ShowInDiscoveryDocument = s.ShowInDiscoveryDocument
						}))
					});
				}
			});
		}

		/// <summary>	Searches for the first API resource asynchronous. </summary>
		/// <param name="name">	The name. </param>
		/// <returns>	The found API resource asynchronous. </returns>
		public Task<ApiResource> FindApiResourceAsync(string name)
		{
			return Task<ApiResource>.Factory.StartNew(() =>
			{
				using (var uow = _dataService.StartUnitOfWork())
				{
					var entity = uow.ApiResourceRepository.GetByName(name);
					if (entity == null)
						return null;
					var apiScopes = uow.ApiResourceScopeRepository.GetByApiIds(new[] {entity.Id});
					var apiScopesArray = apiScopes as ApiResourceScopeEntity[] ?? apiScopes.ToArray();

					IEnumerable<ScopeEntity> scopes = null;
					if (apiScopesArray != null && apiScopesArray.Any())
					{
						scopes = uow.ScopeRepository.GetByIds(apiScopesArray.Select(s => s.ScopeId).ToArray());
					}
					return new ApiResource
						{
							Name = entity.Name,
							DisplayName = entity.DisplayName,
							Description = entity.Description,
							Enabled = entity.Enabled,
							UserClaims = new List<string>(uow.ApiResourceClaimRepository.GetByApiId(entity.Id).Select(c => c.ClaimType)),
							Scopes = scopes == null ? null : new List<Scope>(scopes.Select(s => new Scope
							{
								Name = s.Name,
								DisplayName = s.DisplayName,
								Description = s.Description,
								Required = s.Required,
								Emphasize = s.Emphasize,
								ShowInDiscoveryDocument = s.ShowInDiscoveryDocument
							}))
						};
				}
			});
		}

		/// <summary>	Gets all resources. </summary>
		/// <returns>	all resources. </returns>
		public Task<Resources> GetAllResources()
		{
			return Task<Resources>.Factory.StartNew(
				() => new Resources {ApiResources = GetAllApiResources(), IdentityResources = GetAllIdentityResources()});
		}

		#endregion

		#region Methods

		/// <summary>	Gets all API resources. </summary>
		/// <returns>	all API resources. </returns>
		private ICollection<ApiResource> GetAllApiResources()
		{
			using (var uow = _dataService.StartUnitOfWork())
			{
				var entities = uow.ApiResourceRepository.GetAllCompound();

				return entities.Select(e => new ApiResource
				{
					Name = e.ApiResource.Name,
					DisplayName = e.ApiResource.DisplayName,
					Description = e.ApiResource.Description,
					Enabled = e.ApiResource.Enabled,
					Scopes = new List<Scope>
					(
						e.Scopes.Select(s => new Scope
						{
							Name = s.Name,
							DisplayName = s.DisplayName,
							Description = s.Description,
							Required = s.Required,
							Emphasize = s.Emphasize,
							ShowInDiscoveryDocument = s.ShowInDiscoveryDocument
						})
					),
					UserClaims = new List<string>(e.ApiResourceClaims.Select(c => c.ClaimType))
				}).ToList();
			}
		}

		/// <summary>	Gets all identity resources. </summary>
		/// <returns>	all identity resources. </returns>
		private ICollection<IdentityResource> GetAllIdentityResources()
		{
			using (var uow = _dataService.StartUnitOfWork())
			{
				var resources = uow.IdentityResourceRepository.GetAll();
				var resx = resources.Select(r => new IdentityResource
				{
					Name = r.Name,
					DisplayName = r.DisplayName,
					Description = r.Description,
					Enabled = r.Enabled,
					Required = r.Required,
					Emphasize = r.Emphasize,
					ShowInDiscoveryDocument = r.ShowInDiscoveryDocument,
					UserClaims = new List<string>(uow.IdentityResourceClaimRepository.GetByIdentityId(r.Id).Select(c => c.ClaimType))
				}).ToList();
			}
				var res = new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile()
			};

			return res;
		}

		#endregion
	}
}