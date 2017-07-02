using System;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;

namespace FluiTec.Vision.IdentityServer.ConsoleTest
{
	internal class Program
	{
		private static void Main()
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile(path: "appsettings.json", optional: false)
				.AddJsonFile(path: "appsettings.secret.json", optional: false)
				.Build();

			var credentials = configuration.Get<IdentiyCredentials>();
			var server = configuration.Get<IdentityServer>();

			var response = TestCredentials(server, credentials);
			TestUserInfo(server, response.Result);
			Console.WriteLine($"Result: {response.Result.HttpStatusCode}.");
			Console.ReadLine();
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
			return await tokenClient.RequestResourceOwnerPasswordAsync(credentials.UserName, credentials.Password,
				$"{server.Api} openid profile");
		}

		/// <summary>	Tests user information. </summary>
		/// <param name="server">			The user. </param>
		/// <param name="tokenResponse">	The token response. </param>
		private static void TestUserInfo(IdentityServer server, TokenResponse tokenResponse)
		{
			try
			{
				var disco = DiscoveryClient.GetAsync(server.TargetServer).Result;
				
				var userInfoClient = new UserInfoClient(disco.UserInfoEndpoint);

				var response = userInfoClient.GetAsync(tokenResponse.AccessToken).Result;

				Console.WriteLine(value: "Claims:");
				foreach(var claim in response.Claims)
					Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}