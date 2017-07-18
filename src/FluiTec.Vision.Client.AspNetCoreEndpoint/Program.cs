using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Server;

namespace FluiTec.Vision.Client.AspNetCoreEndpoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
	        try
	        {
		        var config = new ConfigurationBuilder()
			        .AddCommandLine(args)
			        .AddJsonFile(path: "appsettings.Server.json")
			        .Build();

		        var builder = new WebHostBuilder()
			        .UseContentRoot(Directory.GetCurrentDirectory())
			        .UseStartup<Startup>()
			        .UseConfiguration(config)
			        .UseWebListener(options =>
			        {
				        options.ListenerSettings.Authentication.Schemes = AuthenticationSchemes.None;
				        options.ListenerSettings.Authentication.AllowAnonymous = true;
			        })
			        .UseUrls(config.GetValue<string>(key: "ASPNETCORE_URLS"));

		        var host = builder.Build();
		        host.Run();
			}
	        catch (Exception e)
	        {
		        Console.WriteLine(e);
		        throw;
	        }
        }
    }
}
