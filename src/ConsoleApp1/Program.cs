using System;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp1
{
	/// <summary>	A program. </summary>
	class Program
    {
        /// <summary>	Main entry-point for this application. </summary>
        /// <param name="args">	An array of command-line argument strings. </param>
        // ReSharper disable once UnusedMember.Local
        private static void Main(string[] args)
        {
	        var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", false)
		        .AddJsonFile("appsettings.secret.json", false)
		        .Build();

	        var credentials = configuration.Get<IdentiyCredentials>();
	        var server = configuration.Get<IdentityServer>();

			var response = TestCredentials(server, credentials);
			TestUserInfo(server, response.Result);
			Console.WriteLine($"Result: {response.Result.HttpStatusCode}.");
        }

	    /// <summary>	Tests credentials. </summary>
	    /// <param name="server">	  	The user. </param>
	    /// <param name="credentials">	The password. </param>
	    /// <returns>	A Task&lt;TokenResponse&gt; </returns>
	    private static async Task<TokenResponse> TestCredentials(IdentityServer server, IdentiyCredentials credentials)
	    {
			Console.WriteLine($"Testing server '{server.TargetServer}'...");
			Console.WriteLine($"ClientId: '{credentials.ClientId}'.");
			Console.WriteLine($"UserName: '{credentials.UserName}'.");

			var disco = await DiscoveryClient.GetAsync(server.TargetServer);
		    var tokenClient = new TokenClient(disco.TokenEndpoint, credentials.ClientId, credentials.ClientSecret);
		    return await tokenClient.RequestResourceOwnerPasswordAsync(credentials.UserName, credentials.Password, $"{server.Api} openid");
	    }

	    static void TestUserInfo(IdentityServer server, TokenResponse tokenResponse)
	    {
		    var disco = DiscoveryClient.GetAsync(server.TargetServer).Result;

			var userInfoClient = new UserInfoClient(disco.UserInfoEndpoint);

		    var response = userInfoClient.GetAsync(tokenResponse.AccessToken).Result;
		    var claims = response.Claims;
		}
    }
}