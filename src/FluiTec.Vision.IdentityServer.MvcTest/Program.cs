using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace FluiTec.Vision.IdentityServer.MvcTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
				.UseUrls("http://*:5030")
                .Build();

            host.Run();
        }
    }
}
