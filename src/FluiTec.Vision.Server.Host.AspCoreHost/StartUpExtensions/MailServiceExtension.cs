using FuiTec.AppFx.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorLight.MVC;

namespace FluiTec.Vision.Server.Host.AspCoreHost.StartUpExtensions
{
	/// <summary>	A mail service extension. </summary>
	public static class MailServiceExtension
	{
		/// <summary>	Configure mail service. </summary>
		/// <param name="services">			The services. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection ConfigureMailService(this IServiceCollection services, IConfigurationRoot configuration)
		{
			services.AddRazorLight(root: "/MailViews");
			services.AddScoped<ITemplatingMailService, MailKitTemplatingMailService>();

			return services;
		}
	}
}