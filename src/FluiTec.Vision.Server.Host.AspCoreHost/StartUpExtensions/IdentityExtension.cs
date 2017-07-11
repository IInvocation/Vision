using System;
using System.Net;
using System.Threading.Tasks;
using FluiTec.AppFx.Identity;
using FluiTec.AppFx.Identity.Entities;
using FluiTec.AppFx.Options;
using FluiTec.Vision.Server.Host.AspCoreHost.Configuration;
using FluiTec.Vision.Server.Host.AspCoreHost.Localization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.Vision.Server.Host.AspCoreHost.StartUpExtensions
{
	/// <summary>	An identity server extension. </summary>
	public static class IdentityExtension
	{
		/// <summary>	An IServiceCollection extension method that configure identity server. </summary>
		/// <param name="services">			The services to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection ConfigureIdentity(this IServiceCollection services,
			IConfigurationRoot configuration)
		{
			var options = configuration.GetConfiguration<ApiOptions>();
			services.AddSingleton(options);
			services.AddIdentity<IdentityUserEntity, IdentityRoleEntity>(config =>
				{
					config.SignIn.RequireConfirmedEmail = true;
					config.Lockout.AllowedForNewUsers = true;
					config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(value: 5);
					config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents
					{
						// disable redirect to login for api-users
						OnRedirectToLogin = context =>
						{
							if (context.Request.Path.StartsWithSegments(options.ApiOnlyPath) &&
							    context.Response.StatusCode == (int) HttpStatusCode.OK)
								context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
							else
								context.Response.Redirect(context.RedirectUri);

							return Task.CompletedTask;
						}
					};
				})
				.AddErrorDescriber<MultiLanguageIdentityErrorDescriber>()
				.AddDefaultTokenProviders();
			services.AddIdentityStores();

			return services;
		}
	}
}