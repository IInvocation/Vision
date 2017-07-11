using System;
using System.Globalization;
using FluiTec.AppFx.Options;
using FluiTec.Vision.Server.Host.AspCoreHost.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.Vision.Server.Host.AspCoreHost.StartUpExtensions
{
	public static class StatusCodeExtension
	{
		/// <summary>	Options for controlling the API. </summary>
		private static ApiOptions _apiOptions;

		/// <summary>	Options for controlling the code. </summary>
		private static StatusCodeOptions _codeOptions;

		/// <summary>
		///     An IServiceCollection extension method that handler, called when the configure status
		///     code.
		/// </summary>
		/// <param name="services">			The services to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IServiceCollection. </returns>
		public static IServiceCollection ConfigureStatusCodeHandler(this IServiceCollection services,
			IConfigurationRoot configuration)
		{
			// double parsing to only load options once for the handler (see IdentityExtension)
			_apiOptions = configuration.GetConfiguration<ApiOptions>(); 
			_codeOptions = configuration.GetConfiguration<StatusCodeOptions>();

			services.AddSingleton(_codeOptions);

			return services;
		}

		/// <summary>
		///     An IApplicationBuilder extension method that handler, called when the use status code.
		/// </summary>
		/// <param name="app">				The app to act on. </param>
		/// <param name="configuration">	The configuration. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseStatusCodeHandler(this IApplicationBuilder app,
			IConfigurationRoot configuration)
		{
			const string pathFormat = "/StatusCode/{0}";

			if (app == null)
			{
				throw new ArgumentNullException(nameof(app));
			}

			return app.UseStatusCodePages(async context =>
			{
				// check if we dont have an API-request here and want to handle the statuscode
				if (!context.HttpContext.Request.Path.StartsWithSegments(_apiOptions.ApiOnlyPath) 
					&& _codeOptions.SelfHandledCodes.Contains(context.HttpContext.Response.StatusCode))
					// if so - let the statuscodehandler do its stuff
				{
					var newPath = new PathString(
						string.Format(CultureInfo.InvariantCulture, pathFormat, context.HttpContext.Response.StatusCode));

					var originalPath = context.HttpContext.Request.Path;
					var originalQueryString = context.HttpContext.Request.QueryString;
					// Store the original paths so the app can check it.
					context.HttpContext.Features.Set<IStatusCodeReExecuteFeature>(new StatusCodeReExecuteFeature()
					{
						OriginalPathBase = context.HttpContext.Request.PathBase.Value,
						OriginalPath = originalPath.Value,
						OriginalQueryString = originalQueryString.HasValue ? originalQueryString.Value : null,
					});

					context.HttpContext.Request.Path = newPath;
					context.HttpContext.Request.QueryString = QueryString.Empty;
					try
					{
						await context.Next(context.HttpContext);
					}
					finally
					{
						context.HttpContext.Request.QueryString = originalQueryString;
						context.HttpContext.Request.Path = originalPath;
						context.HttpContext.Features.Set<IStatusCodeReExecuteFeature>(instance: null);
					}
				}
			});
		}
	}
}