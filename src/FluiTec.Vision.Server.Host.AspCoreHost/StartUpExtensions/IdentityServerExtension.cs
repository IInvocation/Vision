using System;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity.Entities;
using FluiTec.AppFx.IdentityServer;
using FluiTec.AppFx.IdentityServer.Configuration;
using FluiTec.AppFx.IdentityServer.Validators;
using FluiTec.Vision.Server.Host.AspCoreHost.Configuration;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
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
			services.AddSingleton(new ConfigurationSettingsService<ClientEndpointApiOptions>(configuration, configKey: "ClientEndpointApi").Get());

			var idSrv = services.AddIdentityServer(options =>
			{
				options.UserInteraction.ConsentUrl = "/Identity/Consent";
			});

			idSrv.Services.AddScoped<IRedirectUriValidator, LocalhostRedirectUriValidator>();
			idSrv.Services.AddScoped<IExtensionGrantValidator, DelegationGrantValidator>();
			idSrv.Services.AddScoped<IValidationKeysStore, SigningCredentialStore>();
			idSrv.Services.AddScoped<ISigningCredentialStore, SigningCredentialStore>();
			idSrv.Services.AddScoped<IPersistedGrantStore, GrantStore>();

			idSrv.AddAspNetIdentity<IdentityUserEntity>();
			idSrv.AddClientStore<ClientStore>();
			idSrv.AddResourceStore<ResourceStore>();
			idSrv.AddProfileService<ProfileService>();

			return services;
		}

		/// <summary>	An IApplicationBuilder extension method that use identity server. </summary>
		/// <param name="app">				The app to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseIdentityServer(this IApplicationBuilder app, IConfigurationRoot configuration)
		{
			// enable identityserver
			app.UseIdentityServer();

			// enable jwt-authentication for api-calls of the ClientEndpoint-API
			var options = app.ApplicationServices.GetService<ClientEndpointApiOptions>();
			app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
			{
				Authority = options.Authority,
				RequireHttpsMetadata = false,
				AutomaticAuthenticate = options.AutomaticAuthenticate,
				AutomaticChallenge = options.AutomaticChallenge,
				LegacyAudienceValidation = false,
				JwtBearerEvents = new JwtBearerEvents
				{
					OnAuthenticationFailed = OnAuthenticationFailed,
					OnChallenge = OnChallenge
				}
			});
			return app;
		}

		private static Task OnChallenge(JwtBearerChallengeContext jwtBearerChallengeContext)
		{
			throw new NotImplementedException();
		}

		private static Task OnAuthenticationFailed(AuthenticationFailedContext authenticationFailedContext)
		{
			throw new NotImplementedException();
		}
	}
}