using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Nancy;
using Newtonsoft.Json;

namespace FluiTec.Vision.NancyFx
{
	/// <summary>	A request extension. </summary>
	public static class RequestExtensions
	{
		/// <summary>	The request identifier key. </summary>
		public const string RequestIdKey = "RequestId";

		/// <summary>	A NancyContext extension method that request identifier. </summary>
		/// <param name="context">	The context to act on. </param>
		/// <returns>	A GUID. </returns>
		public static AutoLoadingLazyGuid RequestId(this NancyContext context)
		{
			return new AutoLoadingLazyGuid(() => context.Trace.Items.ContainsKey(RequestIdKey) ? Guid.Parse(context.Trace.Items[RequestIdKey].ToString()) : Guid.Empty);
		}

		/// <summary>	A NancyContext extension method that handler, called when the log route. </summary>
		/// <param name="logger">	  	The logger to act on. </param>
		/// <param name="context">	  	The context to act on. </param>
		/// <param name="handlerName">	Name of the handler. </param>
		public static void LogRouteHandler(this ILogger logger, NancyContext context, string handlerName)
		{
			logger.LogInformation("Request[{0}]: RouteHandler {1} invoked, User: '{2}'.", context.RequestId(), handlerName, GetUserName(context.CurrentUser));
		}

		/// <summary>	An ILogger extension method that handler, called when the log route. </summary>
		/// <param name="logger">	  	The logger to act on. </param>
		/// <param name="context">	  	The context to act on. </param>
		/// <param name="handlerName">	Name of the handler. </param>
		/// <param name="viewModel">  	The view model. </param>
		public static void LogRouteHandler(this ILogger logger, NancyContext context, string handlerName, object viewModel)
		{
			if (logger.IsEnabled(LogLevel.Information))
			{
				logger.LogInformation("Request[{0}]: RouteHandler {1} invoked, User: '{3}'. ViewModel: {2}.", context.RequestId(), handlerName, JsonConvert.SerializeObject(viewModel, Formatting.None), GetUserName(context.CurrentUser));
			}
		}

		/// <summary>	Gets a name. </summary>
		/// <param name="principal">	The principal. </param>
		/// <returns>	The name. </returns>
		public static string GetUserName(ClaimsPrincipal principal)
		{
			if (principal == null) return string.Empty;
			return !principal.HasClaim(claim => claim.Type == ClaimTypes.Name) ? string.Empty : principal.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
		}
	}
}