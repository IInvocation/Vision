using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluiTec.Vision.IdentityServer.Data;
using FluiTec.Vision.IdentityServer.Data.Compound;
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
			return Task<IEnumerable<IdentityResource>>.Factory.StartNew(() =>
			{
				using (var uow = _dataService.StartUnitOfWork())
				{
					var entities = uow.IdentityResourceRepository.GetByScopeNamesCompound(scopeNames);
					return FromCompoundEntities(entities);
				}
			});
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
					var entities = uow.ApiResourceRepository.GetByScopeNamesCompound(scopeNames);
					return FromCompoundEntities(entities);
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
					var entity = uow.ApiResourceRepository.GetByNameCompount(name);
					return FromCompoundEntities(new[] {entity}).SingleOrDefault();
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
				return FromCompoundEntities(entities);
			}
		}

		/// <summary>	Initializes this object from the given from compound entities. </summary>
		/// <param name="entities">	The entities. </param>
		/// <returns>	A list of. </returns>
		private static IList<ApiResource> FromCompoundEntities(IEnumerable<CompoundApiResource> entities)
		{
			var compoundApiResources = entities as CompoundApiResource[] ?? entities.ToArray();
			if (entities == null || !compoundApiResources.Any())
				return new List<ApiResource>();

			return compoundApiResources.Select(e => new ApiResource
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
			})
			.ToList();
		}

		/// <summary>	Gets all identity resources. </summary>
		/// <returns>	all identity resources. </returns>
		private ICollection<IdentityResource> GetAllIdentityResources()
		{
			using (var uow = _dataService.StartUnitOfWork())
			{
				var entities = uow.IdentityResourceRepository.GetAllCompound();
				return FromCompoundEntities(entities);
			}
		}

		/// <summary>	Initializes this object from the given from compound entities. </summary>
		/// <param name="entities">	The entities. </param>
		/// <returns>	A list of. </returns>
		private static IList<IdentityResource> FromCompoundEntities(IEnumerable<CompoundIdentityResource> entities)
		{
			var compoundIdentityResources = entities as CompoundIdentityResource[] ?? entities.ToArray();
			if (entities == null || !compoundIdentityResources.Any())
				return new List<IdentityResource>();

			return compoundIdentityResources.Select(e => new IdentityResource
			{
				Name = e.IdentityResource.Name,
				DisplayName = e.IdentityResource.DisplayName,
				Description = e.IdentityResource.Description,
				Enabled = e.IdentityResource.Enabled,
				Required = e.IdentityResource.Required,
				Emphasize = e.IdentityResource.Emphasize,
				ShowInDiscoveryDocument = e.IdentityResource.ShowInDiscoveryDocument,
				UserClaims = new List<string>(e.IdentityResourceClaims.Select(c => c.ClaimType))
			})
			.ToList();
		}

		#endregion
	}
}