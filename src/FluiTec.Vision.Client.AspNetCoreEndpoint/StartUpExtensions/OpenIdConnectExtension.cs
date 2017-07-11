using System.IdentityModel.Tokens.Jwt;
using FluiTec.AppFx.Options;
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
			services.AddSingleton(configuration.GetConfiguration<Configuration.OpenIdConnectOptions>());
			services.AddSingleton(configuration.GetConfiguration<DelegationApiOptions>());
			services.AddSingleton<ClientEndpointService>(); // to keep tokens in store
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