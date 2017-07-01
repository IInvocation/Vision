using FluiTec.AppFx.Identity;
using FluiTec.AppFx.Identity.Entities;
using FluiTec.Vision.Server.Host.AspCoreHost.Localization;
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
			services.AddIdentity<IdentityUserEntity, IdentityRoleEntity>(config =>
				{
					config.SignIn.RequireConfirmedEmail = true;
				})
				.AddErrorDescriber<MultiLanguageIdentityErrorDescriber>()
				.AddDefaultTokenProviders();
			services.AddIdentityStores();

			return services;
		}
	}
}