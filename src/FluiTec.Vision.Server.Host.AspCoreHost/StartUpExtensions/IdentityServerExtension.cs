using FluiTec.AppFx.Identity.Entities;
using FluiTec.AppFx.IdentityServer;
using IdentityServer4.Validation;
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
			var idSrv = services.AddIdentityServer(options =>
			{
				options.UserInteraction.ConsentUrl = "/Identity/Consent";
			});

			idSrv.Services.AddScoped<IRedirectUriValidator, LocalhostRedirectUriValidator>();
			idSrv.AddTemporarySigningCredential();
			idSrv.AddAspNetIdentity<IdentityUserEntity>();
			idSrv.AddClientStore<ClientStore>();
			idSrv.AddResourceStore<ResourceStore>();
			idSrv.AddProfileService<ProfileService>();

			return services;
		}
	}
}