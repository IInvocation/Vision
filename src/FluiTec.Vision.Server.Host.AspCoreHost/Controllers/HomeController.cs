﻿using System;
using FluiTec.Vision.Server.Host.AspCoreHost.Models.HomeMailViewModels;
using FluiTec.AppFx.Mail;
using FluiTec.AppFx.Mail.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using System.Threading.Tasks;
using FluiTec.Vision.Server.Host.AspCoreHost.Configuration;

namespace FluiTec.Vision.Server.Host.AspCoreHost.Controllers
{
    /// <summary>	A controller for handling homes. </summary>
    public class HomeController : Controller
    {
	    /// <summary>	The logger. </summary>
	    private readonly ILogger<HomeController> _logger;

	    /// <summary>	The mail service. </summary>
	    private readonly ITemplatingMailService _mailService;

	    /// <summary>	Options for controlling the error. </summary>
	    private readonly ErrorOptions _errorOptions;

	    /// <summary>	Constructor. </summary>
	    /// <param name="logger">	   	The logger. </param>
	    /// <param name="mailService"> 	The mail service. </param>
	    /// <param name="errorOptions">	Options for controlling the error. </param>
	    public HomeController(ILogger<HomeController> logger, ITemplatingMailService mailService, ErrorOptions errorOptions)
	    {
		    _logger = logger;
		    _mailService = mailService;
		    _errorOptions = errorOptions;
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
        public async Task<IActionResult> Error()
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
					var mailModel = new ErrorModel(exception);
					await _mailService.SendEmailAsync(_errorOptions.ErrorRecipient, mailModel);
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
