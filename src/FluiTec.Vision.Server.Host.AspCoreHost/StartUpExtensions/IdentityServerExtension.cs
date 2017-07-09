using FluiTec.AppFx.Identity.Entities;
using FluiTec.AppFx.IdentityServer;
using FluiTec.AppFx.IdentityServer.Configuration;
using FluiTec.AppFx.IdentityServer.Validators;
using FluiTec.AppFx.Options;
using FluiTec.Vision.Server.Host.AspCoreHost.Configuration;
using IdentityServer4.Stores;
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
			services.AddSingleton(new ConfigurationSettingsService<SigningOptions>(configuration, configKey: "Signing").Get());
			var idSrv = services.AddIdentityServer(options =>
			{
				options.UserInteraction.ConsentUrl = "/Identity/Consent";
			});

			idSrv.Services.AddScoped<IRedirectUriValidator, LocalhostRedirectUriValidator>();
			idSrv.Services.AddScoped<IExtensionGrantValidator, DelegationGrantValidator>();
			idSrv.Services.AddScoped<IValidationKeysStore, SigningCredentialStore>();
			idSrv.Services.AddScoped<ISigningCredentialStore, SigningCredentialStore>();
			idSrv.AddAspNetIdentity<IdentityUserEntity>();
			idSrv.AddClientStore<ClientStore>();
			idSrv.AddResourceStore<ResourceStore>();
			idSrv.AddProfileService<ProfileService>();

			return services;
		}
	}
}