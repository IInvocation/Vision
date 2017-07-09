using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FluiTec.AppFx.Options;
using FluiTec.Vision.Client.AspNetCoreEndpoint.Configuration;
using FluiTec.Vision.Client.AspNetCoreEndpoint.Models.ConfigureViewModels;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FluiTec.Vision.Client.AspNetCoreEndpoint.Controllers
{
	/// <summary>	A controller for handling configures. </summary>
	[Authorize]
	public class ConfigureController : Controller
	{
		/// <summary>	Options for controlling the open identifier. </summary>
		private readonly OpenIdConnectOptions _openIdOptions;

	    /// <summary>	Constructor. </summary>
	    /// <param name="settingsService">	The settings service. </param>
	    public ConfigureController(ISettingsService<OpenIdConnectOptions> settingsService)
	    {
		    _openIdOptions = settingsService.Get();
	    }

		/// <summary>	Gets the index. </summary>
		/// <returns>	An IActionResult. </returns>
		public IActionResult Index()
	    {
            return View();
        }

	    /// <summary>	Configure client registration. </summary>
	    /// <returns>	An IActionResult. </returns>
	    public IActionResult ConfigureClientRegistration()
	    {
		    var vm = new ClientRegistrationViewModel {Email = User.FindFirstValue(claimType: "email"), MachineName = "Friday", ForwardFridayCalls = true};
		    return View(vm);
	    }

	    /// <summary>
	    /// (An Action that handles HTTP POST requests) configure client registration.
	    /// </summary>
	    /// <param name="model">	The model. </param>
	    /// <returns>	An IActionResult. </returns>
		[HttpPost]
	    public async Task<IActionResult> ConfigureClientRegistration(ClientRegistrationViewModel model)
	    {
		    var accessToken = await HttpContext.Authentication.GetTokenAsync(tokenName: "access_token");
		    var refreshToken = await HttpContext.Authentication.GetTokenAsync(tokenName: "refresh_token");

		    var payload = new
		    {
			    token = accessToken
		    };

			var disco = await DiscoveryClient.GetAsync(_openIdOptions.Authority);
		    var tokenClient = new TokenClient(disco.TokenEndpoint, _openIdOptions.DelegationClientId, _openIdOptions.DelegationClientSecret);

		    var response = await tokenClient.RequestCustomGrantAsync(grantType: "delegation", scope: "clientendpoint", extra: payload);
		    var bearerAccessToken = response.AccessToken;

			var client = new HttpClient {BaseAddress = new Uri($"{_openIdOptions.Authority}/api/")};
			client.DefaultRequestHeaders.Accept
				.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer",parameter: bearerAccessToken);
			var json = JsonConvert.SerializeObject(model);

		    var result = await client.PostAsync(requestUri: "ClientEndpoint", content: new StringContent(json, Encoding.UTF8, mediaType: "application/json"));
		    result.EnsureSuccessStatusCode();

			// redirect user to accept new ClientEndpoint
		    return Redirect(url: "http://localhost:5020/Manage");
	    }
    }
}

