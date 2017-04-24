using System;
using System.Net;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
	        Console.WriteLine("Please enter your username");
	        var usr = Console.ReadLine();
			Console.WriteLine("Please enter your password");
	        var pwd = Console.ReadLine();
			var response = TestCredentials(usr, pwd);
			Console.WriteLine(response.Result.HttpStatusCode == HttpStatusCode.OK);
	        Console.ReadLine();
        }

	    static async Task<TokenResponse> TestCredentials(string user, string pwd)
	    {
			var disco = await DiscoveryClient.GetAsync("http://localhost:1234");
		    var tokenClient = new TokenClient(disco.TokenEndpoint, "Jarvis", "Test");
		    return await tokenClient.RequestResourceOwnerPasswordAsync("a.schnell@wtschnell.de", pwd, "api1");
	    }
    }
}