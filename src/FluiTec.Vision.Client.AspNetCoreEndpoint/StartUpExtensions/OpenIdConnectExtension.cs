using System.IdentityModel.Tokens.Jwt;
using FluiTec.AppFx.Options;
using FluiTec.Vision.ClientEndpointApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluiTec.Vision.Client.AspNetCoreEndpoint.Configuration;

namespace FluiTec.Vision.Client.AspNetCoreEndpoint.StartUpExtensions
{
	public static class OpenIdConnectExtension
	{
		public static IServiceCollection ConfigureOpenIdConnect(this IServiceCollection services,
			IConfigurationRoot configuration)
		{
			// fetch settings
			var settings = configuration.GetConfiguration<OpenIdConnectOptions>();

			// configure asp.net
			services.AddAuthentication(settings.AuthenticationScheme)
				.AddCookie()
				.AddOpenIdConnect(settings.AuthenticationScheme, options =>
				{
					options.SignInScheme = settings.SignInScheme;
					options.Authority = settings.Authority;
					options.RequireHttpsMetadata = settings.RequireHttpsMetadata;

					options.ClientId = settings.ClientId;
					options.ClientSecret = settings.ClientSecret;
					options.ResponseType = settings.ResponseType;

					options.Scope.Add("friday");
					options.Scope.Add("offline_access");
					options.Scope.Add("openid");
					options.Scope.Add("profile");

					options.UseTokenLifetime = true;
					options.GetClaimsFromUserInfoEndpoint = true;
					options.SaveTokens = true;
				});

			// configure openid
			services.AddSingleton(settings);
			services.AddSingleton(configuration.GetConfiguration<DelegationApiOptions>());
			services.AddSingleton<ClientEndpointService>(); // to keep tokens in store
			return services;
		}

		/// <summary>
		/// An IApplicationBuilder extension method that use open identifier connect.
		/// </summary>
		/// <param name="app">				The app to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseOpenIdConnect(this IApplicationBuilder app, IConfigurationRoot configuration)
		{
			var settings = app.ApplicationServices.GetService<OpenIdConnectOptions>();

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			app.UseAuthentication();	

			return app;
		}
	}
}