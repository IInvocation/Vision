using FluiTec.AppFx.Proxy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
	/// <summary>	A logging application builder extensions. </summary>
	public static class ProxyApplicationBuilderExtensions
	{
		/// <summary>	An IApplicationBuilder extension method that use logging. </summary>
		/// <param name="app">	The app to act on. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseProxy(this IApplicationBuilder app)
		{
			// get proxy-settings
			var proxySettings = app.ApplicationServices.GetRequiredService<IProxySettingsService>().Get();

			//rewrite host-url for identityserver when sitting behind a proxy
			if (proxySettings.Enabled)
				app.Use(async (context, next) =>
				{
					context.Request.Scheme = proxySettings.SchemePrefix;
					var httpsHostString = new HostString(proxySettings.HostName, proxySettings.HostPort);
					context.Request.Host = httpsHostString;
					await next.Invoke();
				});

			return app;
		}
	}
}