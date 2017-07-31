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
		/// <summary>	Main entry-point for this application. </summary>
		/// <param name="args">	An array of command-line argument strings. </param>
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

		/// <summary>	Gets server file location. </summary>
		/// <returns>	The server file location. </returns>
		internal static string GetServerFileLocation()
		{
			var appData = GetOsSpecificAppData();

			const string visionDir = "Vision";
			const string endpointDir = "Endpoint";
			const string fileName = "appsettings.Server.json";

			var filePath = Path.Combine(appData, visionDir, endpointDir, fileName);
			return filePath;
		}

		/// <summary>	Gets operating system specific application data. </summary>
		/// <exception cref="Exception">	Thrown when an exception error condition occurs. </exception>
		/// <returns>	The operating system specific application data. </returns>
		internal static string GetOsSpecificAppData()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				return Environment.GetEnvironmentVariable(variable: "LOCALAPPDATA");
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				return Environment.GetEnvironmentVariable(variable: "Home");

			throw new Exception($"Unsupported runtime-os, implement {nameof(GetOsSpecificAppData)} for the given system.");
		}
	}
}