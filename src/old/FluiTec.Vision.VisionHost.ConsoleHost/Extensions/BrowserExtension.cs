using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using FluiTec.AppFx.Proxy.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.Vision.VisionHost.ConsoleHost.Extensions
{
	/// <summary>	A browser extension. </summary>
	public static class BrowserExtension
	{
		/// <summary>	An IApplicationBuilder extension method that use browser. </summary>
		/// <param name="application">	The application to act on. </param>
		/// <returns>	An IApplicationBuilder. </returns>
		public static IApplicationBuilder UseBrowser(this IApplicationBuilder application)
		{
			var proxySettings = application.ApplicationServices.GetService<IProxySettingsService>().Get();
			if (proxySettings.Enabled)
			{
				OpenBrowser($"{proxySettings.SchemePrefix}://{proxySettings.HostName}");
			}
			else
			{
				var feature = application.ServerFeatures[typeof(IServerAddressesFeature)] as IServerAddressesFeature;
				var address = feature?.Addresses.First().Replace("*", "localhost");
				OpenBrowser(address);
			}

			return application;
		}

		/// <summary>	Opens a browser. </summary>
		/// <exception cref="NotSupportedException">
		///     Thrown when the requested operation is not
		///     supported.
		/// </exception>
		/// <param name="url">	URL of the document. </param>
		private static void OpenBrowser(string url)
		{
			if (string.IsNullOrWhiteSpace(url)) return;

			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				Process.Start("xdg-open", url);
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
				Process.Start("open", url);
			else
				throw new NotSupportedException("Runtime not supported!");
		}
	}
}