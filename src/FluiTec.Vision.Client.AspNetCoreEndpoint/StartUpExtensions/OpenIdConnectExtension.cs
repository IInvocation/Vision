using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using FluiTec.Vision.Client.AspNetCoreEndpoint.Configuration;
using FluiTec.Vision.ClientEndpointApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIdConnectOptions = Microsoft.AspNetCore.Builder.OpenIdConnectOptions;

namespace FluiTec.Vision.Client.AspNetCoreEndpoint.StartUpExtensions
{
	public static class OpenIdConnectExtension
	{
		public static IServiceCollection ConfigureOpenIdConnect(this IServiceCollection services,
			IConfigurationRoot configuration)
		{
			services.AddScoped(provider => new ConfigurationSettingsService<Configuration.OpenIdConnectOptions>(configuration, configKey: "OpenIdConnect").Get());
			services.AddScoped(provider => new ConfigurationSettingsService<DelegationApiOptions>(configuration, configKey: "ClientEndpoint").Get());
			services.AddScoped<ClientEndpointService>();
			return services;
		}

		public static IApplicationBuilder UseOpenIdConnect(this IApplicationBuilder app, IConfigurationRoot configuration)
		{
			var settings = app.ApplicationServices.GetService<Configuration.OpenIdConnectOptions>();

			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationScheme = settings.SignInScheme
			});

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

			app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
			{
				AuthenticationScheme = settings.AuthenticationScheme,
				SignInScheme = settings.SignInScheme,

				Authority = settings.Authority,
				RequireHttpsMetadata = settings.RequireHttpsMetadata,

				ClientId = settings.ClientId,
				ClientSecret = settings.ClientSecret,

				ResponseType = settings.ResponseType,
				Scope = {"friday", "offline_access", "openid", "profile"},
				
				UseTokenLifetime = true,
				GetClaimsFromUserInfoEndpoint = true,
				SaveTokens = true,
			});
			return app;
		}
	}
}