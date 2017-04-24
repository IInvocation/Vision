using System;
using Microsoft.Extensions.Logging;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;

namespace FluiTec.Vision.VisionHost.Bootstrapper
{
	/// <summary>	A vision bootstrapper. </summary>
	public class VisionBootstrapper : ConfigurableBootstrapper
	{
		#region Fields

		/// <summary>	The log. </summary>
		private readonly ILogger _log;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="serviceProvider">	The service provider. </param>
		public VisionBootstrapper(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			_log = LoggerFactory.CreateLogger(typeof(VisionBootstrapper));
		}

		#endregion

		#region Nancy

		/// <summary>	Configure conventions. </summary>
		/// <param name="nancyConventions">	The nancy conventions. </param>
		/// <remarks>
		///     Basically maps incoming routes to content-paths
		/// </remarks>
		protected override void ConfigureConventions(NancyConventions nancyConventions)
		{
			base.ConfigureConventions(nancyConventions);

			_log.LogInformation("Configuring FileConventions...");
			nancyConventions.StaticContentsConventions.AddDirectory("js", "/Content/js", "js");
			nancyConventions.StaticContentsConventions.AddDirectory("images", "/Content/images", "jpg", "png");
			nancyConventions.StaticContentsConventions.AddDirectory("css", "/Content/css", "css", "htc", "js", "png");
			nancyConventions.StaticContentsConventions.AddDirectory("fonts", "/Content/fonts", "eot", "svg", "ttf", "woff",
				"otf");
		}

		/// <summary>	Application startup. </summary>
		/// <param name="container">	The container. </param>
		/// <param name="pipelines">	The pipelines. </param>
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
			// let the base class do it's job
			base.ApplicationStartup(container, pipelines);
		}

		#endregion

		#region Configuration

		#endregion
	}
}