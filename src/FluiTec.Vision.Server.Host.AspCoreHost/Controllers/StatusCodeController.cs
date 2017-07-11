using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Controllers
{
    public class StatusCodeController : Controller
    {
	    /// <summary>	The logger. </summary>
	    private readonly ILogger<StatusCodeController> _logger;

	    /// <summary>	Constructor. </summary>
	    /// <param name="logger">	The logger. </param>
	    public StatusCodeController(ILogger<StatusCodeController> logger)
	    {
		    _logger = logger;
	    }

		/// <summary>	Indexes. </summary>
		/// <param name="statusCode">	The status code. </param>
		/// <returns>	An IActionResult. </returns>
	    [HttpGet(template: "/StatusCode/{statusCode}")]
		public IActionResult Index(int statusCode)
        {
	        var reExecute = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
	        _logger.LogInformation($"Unexpected Status Code: {statusCode}, OriginalPath: {reExecute.OriginalPath}");
	        return View(statusCode);
		}
    }
}