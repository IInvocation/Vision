using FluiTec.Vision.Client.AspNetCoreEndpoint.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.Vision.Client.AspNetCoreEndpoint.StartUpExtensions
{
	/// <summary>	A server settings extension. </summary>
	public static class ServerSettingsExtension
	{
		/// <summary>	An IServiceCollection extension method that configure server settings. </summary>
		/// <param name="services">			The services to act on. </param>
		/// <param name="configuration">	The configuration to act on. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection ConfigureServerSettings(this IServiceCollection services, IConfigurationRoot configuration)
		{
			var settings = new ServerSettings
			{
				ExternalHostname = configuration[key: "ExternalHostname"],
				HttpName = configuration[key: "ASPNETCORE_URLS"],
				Port = int.Parse(configuration[key: "Port"]),
				SslPort = int.Parse(configuration[key: "SslPort"]),
				UpnpPort = int.Parse(configuration[key: "UpnpPort"]),
				UseUpnp = bool.Parse(configuration[key: "UseUpnp"]),
				Validated = bool.Parse(configuration[key: "Validated"])
			};

			services.AddSingleton(settings);

			return services;
		}
	}
}