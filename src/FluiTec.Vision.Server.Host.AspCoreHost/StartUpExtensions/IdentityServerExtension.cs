using FluiTec.AppFx.Identity.Entities;
using FluiTec.AppFx.IdentityServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.Vision.Server.Host.AspCoreHost.StartUpExtensions
{
	public static class IdentityServerExtension
	{
		/// <summary>	Configure identity server. </summary>
		/// <param name="services">			The services. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection ConfigureIdentityServer(this IServiceCollection services,
			IConfigurationRoot configuration)
		{
			var idSrv = services.AddIdentityServer()
				.AddTemporarySigningCredential()
				.AddAspNetIdentity<IdentityUserEntity>();
			idSrv.AddClientStore<ClientStore>();
			idSrv.AddResourceStore<ResourceStore>();
			idSrv.AddProfileService<ProfileService>();

			return services;
		}
	}
}