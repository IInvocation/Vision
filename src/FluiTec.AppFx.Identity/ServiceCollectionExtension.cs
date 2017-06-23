using FluiTec.AppFx.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.AppFx.Identity
{
	/// <summary>	A service collection extension. </summary>
	public static class ServiceCollectionExtension
	{
		/// <summary>	An IServiceCollection extension method that adds an identity stores. </summary>
		/// <param name="services">	The services to act on. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection AddIdentityStores(this IServiceCollection services)
		{
			services.AddScoped<IdentityStore>();
			services.AddScoped<IUserStore<IdentityUserEntity>>(provider => provider.GetService<IdentityStore>());
			services.AddScoped<IUserPasswordStore<IdentityUserEntity>>(provider => provider.GetService<IdentityStore>());
			services.AddScoped<IUserSecurityStampStore<IdentityUserEntity>>(provider => provider.GetService<IdentityStore>());
			services.AddScoped<IRoleStore<IdentityRoleEntity>>(provider => provider.GetService<IdentityStore>());
			services.AddScoped<IUserRoleStore<IdentityUserEntity>>(provider => provider.GetService<IdentityStore>());
			services.AddScoped<IUserLoginStore<IdentityUserEntity>>(provider => provider.GetService<IdentityStore>());
			services.AddScoped<IUserEmailStore<IdentityUserEntity>>(provider => provider.GetService<IdentityStore>());
			services.AddScoped<IUserPhoneNumberStore<IdentityUserEntity>>(provider => provider.GetService<IdentityStore>());
			services.AddScoped<IUserTwoFactorStore<IdentityUserEntity>>(provider => provider.GetService<IdentityStore>());
			return services;
		}
	}
}