using System;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace FluiTec.Vision.NancyFx.Bootstrappers
{
	/// <summary>	A request tracing bootstrapper. </summary>
	public class RequestTracingBootstrapper : ServiceProvidingBootstrapper
	{
		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="serviceProvider">	The service provider. </param>
		public RequestTracingBootstrapper(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		#endregion

		/// <summary>	Request startup. </summary>
		/// <param name="container">	The container. </param>
		/// <param name="pipelines">	The pipelines. </param>
		/// <param name="context">  	The context. </param>
		protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
		{
			context.Trace.Items.Add(RequestExtensions.RequestIdKey, Guid.NewGuid());
			base.RequestStartup(container, pipelines, context);
		}
	}
}