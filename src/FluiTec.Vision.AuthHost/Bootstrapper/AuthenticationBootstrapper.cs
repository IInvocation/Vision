using System;
using FluiTec.AppFx.Authentication.Data;
using FluiTec.Vision.IdentityServer.Data;
using FluiTec.Vision.NancyFx.Authentication.Forms;
using FluiTec.Vision.NancyFx.Authentication.Forms.Services;
using FluiTec.Vision.NancyFx.Authentication.Forms.Settings;
using FluiTec.Vision.NancyFx.Authentication.GoogleOpenId.Handlers;
using FluiTec.Vision.NancyFx.Authentication.GoogleOpenId.Services;
using FluiTec.Vision.NancyFx.Authentication.OpenId.Services;
using FluiTec.Vision.NancyFx.Authentication.Owin.Services;
using FluiTec.Vision.NancyFx.Authentication.Services;
using FluiTec.Vision.NancyFx.Authentication.Settings;
using FluiTec.Vision.NancyFx.IdentityServer.Services;
using FluiTec.Vision.Server.Data;
using FluiTec.Vision.Server.Data.Mssql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Session;
using Nancy.TinyIoc;

namespace FluiTec.Vision.AuthHost.Bootstrapper
{
	/// <summary>	An authentication bootstrapper. </summary>
	public class AuthenticationBootstrapper : ConfigurableBootstrapper
	{
		#region Fields

		/// <summary>	The log. </summary>
		private readonly ILogger _log;

		#endregion

		#region Constructors

		/// <summary>	Constructor. </summary>
		/// <param name="serviceProvider">	The service provider. </param>
		public AuthenticationBootstrapper(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			_log = LoggerFactory.CreateLogger(typeof(AuthenticationBootstrapper));
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

			// enable sessions
			CookieBasedSessions.Enable(pipelines);

			ConfigureDataService(container);
			ConfigureUserService(container);
			ConfigureOwinAuthentication(container);
			ConfigureOpenIdAuthentication(container);
			ConfigureIdentityServer(container);
			ConfigureFormsAuthentication(container, pipelines);
		}

		#endregion

		#region Configuration

		/// <summary>	Configure owin authentication. </summary>
		/// <param name="container">	The container. </param>
		private void ConfigureOwinAuthentication(TinyIoCContainer container)
		{
			_log.LogInformation("Configuring OwinAuthentication...");
			container.Register(ServiceProvider.GetRequiredService<IAuthenticationSettingsService>().Get());
			container.Register<IAuthenticationService, OwinAuthenticationService>();
		}

		/// <summary>	Configure open identifier authentication. </summary>
		/// <param name="container">	The container. </param>
		private void ConfigureOpenIdAuthentication(TinyIoCContainer container)
		{
			_log.LogInformation("Configuring OpenIdAuthentication...");
			container.Register(ServiceProvider.GetRequiredService<IOpenIdAuthenticationSettingsService>());

			// google hookup
			container.Register(ServiceProvider.GetRequiredService<IGoogleOpenIdProviderSettingsService>());

			// register handlers 
			container.RegisterMultiple<IOpenIdAuthenticateHandler>(new[]
			{
				typeof(GoogleOpenIdAuthenticateHandler)
			});
		}

		/// <summary>	Configure identity server. </summary>
		/// <param name="container">	The container. </param>
		private void ConfigureIdentityServer(TinyIoCContainer container)
		{
			_log.LogInformation("Configuring IdentityServer...");
			container.Register<IIdentityServerDataService>(
				(s, p) => new VisionDataService(LoggerFactory, ApplicationSettings.DefaultConnectionString));
			container.Register(ServiceProvider.GetRequiredService<IIdentityServerSettingsService>().Get());
		}

		/// <summary>	Configure data service. </summary>
		/// <param name="container">	The container. </param>
		private void ConfigureDataService(TinyIoCContainer container)
		{
			_log.LogInformation("Configuring DataService...");
			container.Register<IVisionDataService>(
				(s, p) => new VisionDataService(LoggerFactory, ApplicationSettings.DefaultConnectionString));
			container.Register<IAuthenticatingDataService>(
				(s, p) => new VisionDataService(LoggerFactory, ApplicationSettings.DefaultConnectionString));
		}

		/// <summary>	Configure user service. </summary>
		/// <param name="container">	The container. </param>
		private void ConfigureUserService(TinyIoCContainer container)
		{
			_log.LogInformation("Configuring UserService...");
			container.Register(ServiceProvider.GetRequiredService<IAuthenticationSettingsService>());
			container.Register<IUserService, UserService>();
		}

		/// <summary>	Configure forms authentication. </summary>
		/// <param name="container">	The container. </param>
		/// <param name="pipelines">	The pipelines. </param>
		private void ConfigureFormsAuthentication(TinyIoCContainer container, IPipelines pipelines)
		{
			_log.LogInformation("Configuring FormsAuthentication...");
			container.Register(ServiceProvider.GetRequiredService<IFormsAuthenticationSettingsService>().Get());

			// enable forms-authentication
			pipelines.EnableFormsAuthentication(container,
				container.Resolve<IFormsAuthenticationSettings>(),
				container.Resolve<IAuthenticationSettings>());
		}

		#endregion
	}
}