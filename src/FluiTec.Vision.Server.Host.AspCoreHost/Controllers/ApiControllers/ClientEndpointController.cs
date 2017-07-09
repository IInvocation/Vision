using System.Linq;
using System.Security.Claims;
using FluiTec.AppFx.Identity;
using FluiTec.Vision.ClientEndpointApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Controllers.ApiControllers
{
	/// <summary>	A controller for handling client endpoints. </summary>
	[Produces(contentType: "application/json")]
	[Route(template: "api/ClientEndpoint")]
	[Authorize(nameof(IdentityPolicies.IsClientEndpointUser))]
	public class ClientEndpointController : Controller
	{
		/// <summary>	The logger. </summary>
		private readonly ILogger<ClientEndpointController> _logger;

		/// <summary>	Constructor. </summary>
		/// <param name="logger">	The logger. </param>
		public ClientEndpointController(ILogger<ClientEndpointController> logger)
		{
			_logger = logger;
		}

		/// <summary>	Post this message. </summary>
		/// <param name="model">	The model. </param>
		/// <returns>	An IActionResult. </returns>
		[HttpPost]
		public IActionResult Post([FromBody] ClientEndpointModel model)
		{
			if (!ModelState.IsValid) return StatusCode(statusCode: 422);

			// since we're using JWT directly - we'll have to use ms-specific claims for now
			_logger.LogInformation($"API posted new ClientEndpoint for {User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value}.");

			// save model to be accepted by user

			return Ok(model);
		}
	}
}