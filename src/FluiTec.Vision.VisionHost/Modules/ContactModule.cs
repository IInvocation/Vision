using FluiTec.Vision.NancyFx;
using Microsoft.Extensions.Logging;
using Nancy;

namespace FluiTec.Vision.VisionHost.Modules
{
	/// <summary>	A contact module. </summary>
	public class ContactModule : NancyModule
	{
		#region Fields

		/// <summary>	The logger. </summary>
		private readonly ILogger _logger;

		#endregion

		#region Constructors

		/// <summary>	Default constructor. </summary>
		/// <param name="loggerFactory">	The logger factory. </param>
		public ContactModule(ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory.CreateLogger(typeof(ContactModule));

			Get("/Contact/Imprint", _ => GET_Imprint());
			Get("/Contact/TermsOfUse", _ => GET_TermsOfUse());
			Get("/Contact/Privacy", _ => GET_Privacy());
		}

		#endregion

		#region RouteHandlers

		/// <summary>	[GET] Imprint. </summary>
		/// <returns>	Imprint. </returns>
		private dynamic GET_Imprint()
		{
			_logger.LogRouteHandler(Context, nameof(GET_Imprint));
			return View["Imprint"];
		}

		/// <summary>	[GET] TermsOfUse. </summary>
		/// <returns>	TermsOfUse. </returns>
		private dynamic GET_TermsOfUse()
		{
			_logger.LogRouteHandler(Context, nameof(GET_TermsOfUse));
			return View["TermsOfUse"];
		}

		/// <summary>	[GET] Privacy. </summary>
		/// <returns>	Privacy. </returns>
		private dynamic GET_Privacy()
		{
			_logger.LogRouteHandler(Context, nameof(GET_Privacy));
			return View["Privacy"];
		}

		#endregion
	}
}