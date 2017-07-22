using System;
using System.IO;
using System.Runtime.InteropServices;
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
			        .AddJsonFile(GetServerFileLocation(), optional: false)
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

	    internal static string GetServerFileLocation()
	    {
		    var appData = GetOsSpecificAppData();

		    const string visionDir = "Vision";
		    const string endpointDir = "Endpoint";
		    const string fileName = "appsettings.Server.json";

		    var filePath = Path.Combine(appData, visionDir, endpointDir, fileName);
		    return filePath;
	    }

	    internal static string GetOsSpecificAppData()
	    {
		    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		    {
			    return Environment.GetEnvironmentVariable(variable: "LOCALAPPDATA");
		    }
		    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
		    {
			    return Environment.GetEnvironmentVariable(variable: "Home");
		    }

			throw new Exception($"Unsupported runtime-os, implement {nameof(GetOsSpecificAppData)} for the given system.");
	    }
    }
}
