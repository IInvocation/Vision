using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace FluiTec.Vision.NancyFx.Bootstrappers
{
    public class LoggingNancyBootstrapper : DefaultNancyBootstrapper
    {
		#region Fields

		/// <summary>	The logger. </summary>
		private ILogger _logger;

		#endregion

		#region Configuration

		/// <summary>	Application startup. </summary>
		/// <param name="container">	The container. </param>
		/// <param name="pipelines">	The pipelines. </param>
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
	    {
			base.ApplicationStartup(container, pipelines);

		    _logger = container.Resolve<ILoggerFactory>().CreateLogger(typeof(LoggingNancyBootstrapper));
		    _logger.LogDebug("Registering Log-RequestHooks...");

		    // do basic hooks
		    pipelines.BeforeRequest += OnPipelinesBeforeRequest;
		    pipelines.AfterRequest += OnPipelinesAfterRequest;
		    pipelines.OnError += OnError;
	    }

		#endregion

		#region PipelineHooks

		/// <summary>	Executes the pipelines before request action. </summary>
		///
		/// <param name="ctx">  	The context. </param>
		/// <param name="token">	The token. </param>
		///
		/// <returns>	Task from null. </returns>
		/// <remarks>
		/// Logs Url, method and user of every request to process
		/// </remarks>
		private Task<Response> OnPipelinesBeforeRequest(NancyContext ctx, CancellationToken token)
	    {
			return Task<Response>.Factory.StartNew(() =>
		    {
			    var forwaredFor = ctx.Request.Headers["X-Forwarded-For"].FirstOrDefault();

				_logger.LogInformation("Request[{0}]: {1} Url: '{2}', User: '{3}', ForUser: '{4}'", ctx.RequestId(), ctx.Request.Method, ctx.Request.Url, ctx.Request.UserHostAddress, forwaredFor);
			    return null; // always return null to not interrupt normal control flow
		    }, token);
	    }

	    /// <summary>	Executes the pipelines after request action. </summary>
	    ///
	    /// <param name="ctx">  	The context. </param>
	    /// <param name="token">	The token. </param>
	    ///
	    /// <returns>	A Task. </returns>
	    /// <remarks>
	    /// Logs Url, StatusCode and ContentType of every processed request
	    /// </remarks>
	    private Task OnPipelinesAfterRequest(NancyContext ctx, CancellationToken token)
	    {
		    return Task.Factory.StartNew(() =>
		    {
			    _logger.LogInformation("Request[{0}]: Url: '{0}', StatusCode: {1}, ContentType: {2}", ctx.RequestId(), ctx.Request.Url, ctx.Response.StatusCode, ctx.Response.ContentType);
		    }, token);
	    }

	    /// <summary>	Executes the error action. </summary>
	    ///
	    /// <param name="ctx">	The context. </param>
	    /// <param name="e">  	The Exception to process. </param>
	    ///
	    /// <returns>	NULL. </returns>
	    /// <remarks>
	    /// Logs every request-associated exception as an error		 
	    /// </remarks>
	    private object OnError(NancyContext ctx, Exception e)
	    {
			_logger.LogError("Request[{0}]: Url: '{0}', StatusCode: {1}, ContentType: {2}", ctx.RequestId(), ctx.Request.Url, ctx.Response.StatusCode, ctx.Response.ContentType);
			_logger.LogError("Unhandled Exception", e);
		    return null;
	    }

	    #endregion
	}
}
