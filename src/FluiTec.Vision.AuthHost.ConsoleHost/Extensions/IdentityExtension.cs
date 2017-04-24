using System.Linq;
using FluiTec.AppFx.Proxy.Services;
using FluiTec.AppFx.Signing.Services;
using FluiTec.Vision.IdentityServer;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluiTec.Vision.AuthHost.ConsoleHost.Extensions
{
	/// <summary>	An identity extension. </summary>
	public static class IdentityExtension
	{
		/// <summary>	An IServiceCollection extension method that configure identity server. </summary>
		/// <param name="services">				 	The services to act on. </param>
		/// <param name="environment">			 	The environment. </param>
		/// <param name="proxySettingsService">  	The proxy settings service. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection ConfigureIdentityServer(this IServiceCollection services,
			IHostingEnvironment environment, IProxySettingsService proxySettingsService)
		{
			var proxySettings = proxySettingsService.Get();

			// setup identityserver-services
			var builder = services.AddIdentityServer(options =>
			{
				// if proxy is configured, use it as issuer
				if (proxySettings.Enabled)
					options.IssuerUri = $"{proxySettings.SchemePrefix}://{proxySettings.HostName}";
			});

			// configure signing credentials

			// configure stores
			builder.AddClientStore<ClientStore>();
			builder.AddResourceStore<ResourceStore>();
			//builder.AddProfileService<ProfileService>();
			builder.AddResourceOwnerValidator<ResourceOwnerValidator>();

			// remove InMemoryPersistedGrantStore (dunno who's adding it in the first place...
			builder.Services.Remove(builder.Services.Single(s => s.ServiceType == typeof(IPersistedGrantStore)));

			// add our own implementation of IPersistedGrantStore
			builder.Services.TryAddSingleton<IPersistedGrantStore, GrantStore>();

			return services;
		}
	}
}