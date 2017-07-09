using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FluiTec.Vision.Client.AspNetCoreEndpoint.Models.ConfigureViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FluiTec.Vision.Client.AspNetCoreEndpoint.Controllers
{
	/// <summary>	A controller for handling configures. </summary>
	[Authorize]
	public class ConfigureController : Controller
    {
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
			var client = new HttpClient {BaseAddress = new Uri(uriString: "http://localhost:5020/api/")};
			client.DefaultRequestHeaders.Accept
				.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
			var json = JsonConvert.SerializeObject(model);

		    var result = await client.PostAsync(requestUri: "ClientEndpoint", content: new StringContent(json, Encoding.UTF8, mediaType: "application/json"));
		    result.EnsureSuccessStatusCode();

			// redirect user to accept new ClientEndpoint
		    return Redirect(url: "http://localhost:5020/Manage");
	    }
    }
}
