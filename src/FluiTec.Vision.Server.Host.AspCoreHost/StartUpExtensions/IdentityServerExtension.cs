using System;
using System.Collections.Generic;
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
using Microsoft.IdentityModel.Tokens;

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
			var credentialStore = app.ApplicationServices.GetService<ISigningCredentialStore>();
			var securityKey = credentialStore.GetSigningCredentialsAsync().Result.Key;

			// using jwt-bearer manually duo to identityserver taking wrong signing-material
			app.UseJwtBearerAuthentication(new JwtBearerOptions
			{
				Authority = options.Authority,
				RequireHttpsMetadata = false,
				AutomaticAuthenticate = options.AutomaticAuthenticate,
				AutomaticChallenge = options.AutomaticChallenge,
				TokenValidationParameters = new TokenValidationParameters
				{
					ValidAudience = $"{options.Authority}/resources",
					ValidIssuer = options.Authority,
					IssuerSigningKey = securityKey,
					IssuerSigningKeyResolver = (string token, SecurityToken securityToken, string kid, TokenValidationParameters validationParameters) => 
					new List<SecurityKey> { securityKey }
				}
			});
			return app;
		}
	}
}