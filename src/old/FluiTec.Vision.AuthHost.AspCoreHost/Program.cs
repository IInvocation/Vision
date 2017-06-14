using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace FluiTec.Vision.AuthHost.AspCoreHost
{
	/// <summary>	The Vision ConsoleHost entry class. </summary>
	internal class Program
	{
		// ReSharper disable once UnusedMember.Local
		/// <summary>	Main entry-point for this application. </summary>
		private static void Main()
		{
			// enable configuration
			var configuration = new ConfigurationBuilder()
				.AddEnvironmentVariables()
				.Build();

			// configure host
			var builder = new WebHostBuilder()
				.UseConfiguration(configuration)
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>();

			// automatically listen on different uri
			if (configuration["KESTREL_UseUrls"] == "true")
			{
				builder.UseUrls(configuration["KESTREL_ListenUrls"].Split(';'));
			}

			// create host
			var host = builder
				.Build();

			// run host
			host.Run();
		}
	}
}