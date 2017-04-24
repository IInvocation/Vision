using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace FluiTec.Vision.IdentityServer
{
	/// <summary>	A resource store. </summary>
	public class ResourceStore : IResourceStore
	{
		/// <summary>	Searches for the first identity resources by scope asynchronous. </summary>
		/// <param name="scopeNames">	List of names of the scopes. </param>
		/// <returns>	The found identity resources by scope asynchronous. </returns>
		public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
		{
			IEnumerable<IdentityResource> resources = new List<IdentityResource>(new[] {new IdentityResource("a.schnell@wtschnell.de", "a.schnell@wtschnell.de", new List<string>(new []{"test"}))});
			return Task.FromResult(resources);
		}

		/// <summary>	Searches for the first API resources by scope asynchronous. </summary>
		/// <param name="scopeNames">	List of names of the scopes. </param>
		/// <returns>	The found API resources by scope asynchronous. </returns>
		public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
		{
			IEnumerable<ApiResource> resources = new List<ApiResource>(new[] { new ApiResource("api1", "API 1") });
			return Task.FromResult(resources);
		}

		/// <summary>	Searches for the first API resource asynchronous. </summary>
		/// <param name="name">	The name. </param>
		/// <returns>	The found API resource asynchronous. </returns>
		public Task<ApiResource> FindApiResourceAsync(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>	Gets all resources. </summary>
		/// <returns>	all resources. </returns>
		public Task<Resources> GetAllResources()
		{
			return Task<Resources>.Factory.StartNew(() => new Resources());
		}
	}
}