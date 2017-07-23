using System;
using System.Threading.Tasks;
using FluiTec.Vision.Client.AspNetCoreEndpoint.Configuration;
using FluiTec.Vision.Client.AspNetCoreEndpoint.Models.ConfigureViewModels;
using FluiTec.Vision.Client.AspNetCoreEndpoint.Services;
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
		#region Fields

		/// <summary>	The endpoint service. </summary>
		private readonly ClientEndpointService _endpointService;

		/// <summary>	The server settings. </summary>
		private readonly ServerSettings _serverSettings;

		/// <summary>	Manager for service. </summary>
		private readonly EndpointManagerService _managerService;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		///
		/// <param name="endpointService">	The endpoint service. </param>
		/// <param name="serverSettings"> 	The server settings. </param>
		/// <param name="managerService"> 	Manager for service. </param>
		public ConfigureController(ClientEndpointService endpointService, ServerSettings serverSettings, EndpointManagerService managerService)
		{
			_endpointService = endpointService;
			_serverSettings = serverSettings;
			_managerService = managerService;
		}

		#endregion

		#region Routes

		/// <summary>	Gets the index. </summary>
		/// <returns>	An IActionResult. </returns>
		public IActionResult Index()
		{
			var model = new RegistrationStatusModel {Registered = !IsRegistrationNecessary()};
			return View(model);
		}

		/// <summary>	Configure client registration. </summary>
		/// <returns>	An IActionResult. </returns>
		public IActionResult ConfigureClientRegistration()
		{
			// make machinename start UpperCase and continue LowerCase
			var machineName = Environment.MachineName;
			machineName = machineName.Length >= 2
				? string.Concat(machineName.Substring(startIndex: 0, length: 1).ToUpper(),
					machineName.Substring(startIndex: 1).ToLower())
				: machineName;

			var vm = new ClientRegistrationViewModel
			{
				MachineName = machineName,
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
			await DoRegistration(model);

			return Redirect(url: "http://localhost:5020/Manage");
		}

		#endregion

		#region Helpers

		/// <summary>	Is registration necessary. </summary>
		/// <returns>	A Task&lt;bool&gt; </returns>
		private bool IsRegistrationNecessary()
		{
			return _managerService.CurrentSettings == null;
		}

		/// <summary>	Executes the registration operation. </summary>
		/// <param name="model">	The model. </param>
		/// <returns>	A Task. </returns>
		private async Task DoRegistration(ClientRegistrationViewModel model)
		{
			// initialize service, exchanging user tokens for delegation tokens
			_endpointService.Init(await HttpContext.Authentication.GetTokenAsync(tokenName: "access_token"),
				await HttpContext.Authentication.GetTokenAsync(tokenName: "refresh_token"));

			// do registration
			var host = _serverSettings.UseUpnp ? $"{_serverSettings.UpnpPort}" : _serverSettings.ExternalHostname;
			var registration = await _endpointService.RegisterClientEndpoint(new ClientEndpointModel
			{
				UseUpnp = _serverSettings.UseUpnp,
				MachineName = model.MachineName,
				EndpointHost = host,
				ForwardFriday = model.ForwardFridayCalls,
				ForwardJarvis = model.ForwardJarvisCalls
			});

			// save credentials
			_managerService.Save(registration);
		}

		#endregion
	}
}