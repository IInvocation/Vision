using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Authentication.Amazon
{
	/// <summary>	An amazon application builder extension. </summary>
	public static class AmazonAppBuilderExtension
	{
		/// <summary>
		///     An IApplicationBuilder extension method that use amazon authentication.
		/// </summary>
		/// <param name="app">	The app to act on. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseAmazonAuthentication(this IApplicationBuilder app)
		{
			if (app == null)
				throw new ArgumentNullException(nameof(app));

			return app.UseMiddleware<AmazonMiddleware>();
		}

		/// <summary>
		///     An IApplicationBuilder extension method that use google authentication.
		/// </summary>
		/// <exception cref="ArgumentNullException">
		///     Thrown when one or more required arguments are
		///     null.
		/// </exception>
		/// <param name="app">	  	The app to act on. </param>
		/// <param name="options">	Options for controlling the operation. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseAmazonAuthentication(this IApplicationBuilder app, AmazonOptions options)
		{
			if (app == null)
				throw new ArgumentNullException(nameof(app));
			if (options == null)
				throw new ArgumentNullException(nameof(options));

			return app.UseMiddleware<AmazonMiddleware>(Options.Create(options));
		}
	}
}