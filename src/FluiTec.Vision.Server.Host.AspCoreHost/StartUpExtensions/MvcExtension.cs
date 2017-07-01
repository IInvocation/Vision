using FluiTec.AppFx.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.Vision.Server.Host.AspCoreHost.StartUpExtensions
{
	public static class MvcExtension
	{
		/// <summary>	An IServiceCollection extension method that configure MVC. </summary>
		/// <param name="services">			The services to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection ConfigureMvc(this IServiceCollection services, IConfigurationRoot configuration)
		{
			services.AddMvc()
				.AddViewLocalization()
				.AddDataAnnotationsLocalization();

			services.AddAuthorization(options =>
			{
				options.AddPolicy(IdentityPolicies.IsAdminPolicy.PolicyName, policy => policy.RequireClaim(IdentityClaims.HasAdministrativeRights));
			});

			return services;
		}

		/// <summary>	An IApplicationBuilder extension method that use MVC. </summary>
		/// <param name="app">				The app to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseMvc(this IApplicationBuilder app, IConfigurationRoot configuration)
		{
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
			return app;
		}
	}
}