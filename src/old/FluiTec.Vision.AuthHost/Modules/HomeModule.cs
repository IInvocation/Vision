using FluiTec.Vision.NancyFx;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Responses;

namespace FluiTec.Vision.AuthHost.Modules
{
	/// <summary>	A home module. </summary>
	public class HomeModule : NancyModule
	{
		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region Constructors

		/// <summary>	Default constructor. </summary>
		/// <param name="loggerFactory">	The logger factory. </param>
		public HomeModule(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger(typeof(HomeModule));

			Get("/", _ => GET_Home());
			Get("/Home", _ => Response.AsRedirect("/", RedirectResponse.RedirectType.Permanent));
			Get("/Home/Index", _ => Response.AsRedirect("/", RedirectResponse.RedirectType.Permanent));
		}

		#endregion

		#region RouteHandlers

		/// <summary>	[GET] Home. </summary>
		/// <returns>	Home. </returns>
		private dynamic GET_Home()
		{
			_logger.LogRouteHandler(Context, nameof(GET_Home));
			return View["Index"];
		}

		#endregion
	}
}