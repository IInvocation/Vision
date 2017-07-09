using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using FluiTec.AppFx.Options;
using FluiTec.Vision.Client.AspNetCoreEndpoint.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Internal;
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
			var config = new ConfigurationSettingsService<Configuration.OpenIdConnectOptions>(configuration, configKey: "OpenIdConnect");
			services.AddScoped<ISettingsService<Configuration.OpenIdConnectOptions>>(provider => config);
			return services;
		}

		public static IApplicationBuilder UseOpenIdConnect(this IApplicationBuilder app, IConfigurationRoot configuration)
		{
			var settings = app.ApplicationServices.GetService<ISettingsService<Configuration.OpenIdConnectOptions>>().Get();

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
				SaveTokens = true
			});
			return app;
		}
	}
}