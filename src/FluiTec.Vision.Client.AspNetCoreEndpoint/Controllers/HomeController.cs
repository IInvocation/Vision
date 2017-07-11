using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace FluiTec.Vision.Client.AspNetCoreEndpoint.Controllers
{
	/// <summary>	A controller for handling homes. </summary>
	public class HomeController : Controller
	{
		/// <summary>	The logger. </summary>
		private readonly ILogger<HomeController> _logger;

		/// <summary>	Constructor. </summary>
		/// <param name="logger">	The logger. </param>
		public HomeController(ILogger<HomeController> logger)
		{
			
		}

		/// <summary>	Gets the index. </summary>
		/// <returns>	An IActionResult. </returns>
		public IActionResult Index()
		{
			return View();
		}

		/// <summary>	Gets the about. </summary>
		/// <returns>	An IActionResult. </returns>
		public IActionResult About()
		{
			return View();
		}

		/// <summary>	Gets the error. </summary>
		/// <returns>	An IActionResult. </returns>
		public IActionResult Error()
		{
			// get exception-data
			var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
			var exception = exceptionFeature?.Error;
			var route = exceptionFeature?.Path;

			// only execute if there was a real exception
			if (exception != null)
			{
				_logger.LogError(0, exception, "Unhandled exception");

				// collect additional exception-info from log?
				
				try
				{
					// TODO: Call not yet existent server-api to alert developer about exception
				}
				catch (Exception e)
				{
					_logger.LogError(0, e, "Unhandled exception in error-handling api");
				}
				
			}

			return View();
		}
	}
}