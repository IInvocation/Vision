using System.Security.Claims;
using System.Threading.Tasks;
using FluiTec.Vision.Client.AspNetCoreEndpoint.Models.ConfigureViewModels;
using FluiTec.Vision.ClientEndpointApi;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluiTec.Vision.Client.AspNetCoreEndpoint.Controllers
{
	/// <summary>	A controller for handling configures. </summary>
	[Authorize]
	public class ConfigureController : Controller
	{
		/// <summary>	The endpoint service. </summary>
		private readonly ClientEndpointService _endpointService;

		/// <summary>	Options for controlling the operation. </summary>
		private readonly DelegationApiOptions _options;

		/// <summary>	Constructor. </summary>
		/// <param name="options">		  	Options for controlling the operation. </param>
		/// <param name="endpointService">	The endpoint service. </param>
		public ConfigureController(DelegationApiOptions options, ClientEndpointService endpointService)
		{
			_options = options;
			_endpointService = endpointService;
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
			var vm = new ClientRegistrationViewModel
			{
				Email = User.FindFirstValue(claimType: "email"),
				MachineName = "Friday",
				ForwardFridayCalls = true
			};
			return View(vm);
		}

		/// <summary>
		///     (An Action that handles HTTP POST requests) configure client registration.
		/// </summary>
		/// <param name="model">	The model. </param>
		/// <returns>	An IActionResult. </returns>
		[HttpPost]
		public async Task<IActionResult> ConfigureClientRegistration(ClientRegistrationViewModel model)
		{
			_endpointService.Init(await HttpContext.Authentication.GetTokenAsync(tokenName: "access_token"),
				await HttpContext.Authentication.GetTokenAsync(tokenName: "refresh_token"));
			await _endpointService.RegisterClientEndpoint(new ClientEndpointModel {MachineName = model.MachineName, EndpointHost = _options.EndpointHost});

			//   var accessToken = await HttpContext.Authentication.GetTokenAsync(tokenName: "access_token");
			//   var refreshToken = await HttpContext.Authentication.GetTokenAsync(tokenName: "refresh_token");

			//   var payload = new
			//   {
			//    token = accessToken
			//   };

			//var disco = await DiscoveryClient.GetAsync(_openIdOptions.Authority);
			//   var tokenClient = new TokenClient(disco.TokenEndpoint, _openIdOptions.DelegationClientId, _openIdOptions.DelegationClientSecret);

			//   var response = await tokenClient.RequestCustomGrantAsync(grantType: "delegation", scope: "clientendpoint", extra: payload);
			//   var bearerAccessToken = response.AccessToken;

			//var client = new HttpClient {BaseAddress = new Uri($"{_openIdOptions.Authority}/api/")};
			//client.DefaultRequestHeaders.Accept
			//	.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
			//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer",parameter: bearerAccessToken);
			//var json = JsonConvert.SerializeObject(model);

			//   var result = await client.PostAsync(requestUri: "ClientEndpoint", content: new StringContent(json, Encoding.UTF8, mediaType: "application/json"));
			//   result.EnsureSuccessStatusCode();

			// redirect user to accept new ClientEndpoint
			return Redirect(url: "http://localhost:5020/Manage");
		}
	}
}