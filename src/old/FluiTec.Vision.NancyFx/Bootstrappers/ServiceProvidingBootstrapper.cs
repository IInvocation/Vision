using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace FluiTec.Vision.NancyFx.Bootstrappers
{
	/// <summary>	A service providing bootstrapper. </summary>
	public class ServiceProvidingBootstrapper : LoggingNancyBootstrapper
	{
		#region Fields

		/// <summary>	The log. </summary>
		private readonly ILogger _log;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
		/// <param name="serviceProvider">	The service provider. </param>
		public ServiceProvidingBootstrapper(IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
			LoggerFactory = ServiceProvider.GetRequiredService<ILoggerFactory>();
			_log = LoggerFactory.CreateLogger(typeof(ServiceProvidingBootstrapper));
		}

		#endregion

		#region Nancy

		/// <summary>	Application startup. </summary>
		/// <param name="container">	The container. </param>
		/// <param name="pipelines">	The pipelines. </param>
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
			_log.LogDebug("Registering LoggerFactory...");
			container.Register(LoggerFactory);

			base.ApplicationStartup(container, pipelines);
		}

		#endregion

		#region Properties

		/// <summary>	Gets the service provider. </summary>
		/// <value>	The service provider. </value>
		protected IServiceProvider ServiceProvider { get; }

		/// <summary>	Gets the logger factory. </summary>
		/// <value>	The logger factory. </value>
		protected ILoggerFactory LoggerFactory { get; }

		#endregion
	}
}