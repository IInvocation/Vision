using System;
using System.Diagnostics;
using System.Security.Principal;

namespace FluiTec.AppFx.Cli
{
	public static class CliHelper
	{
		/// <summary>	Restart as administrator. </summary>
		/// <param name="args">			 	A variable-length parameters list containing arguments. </param>
		/// <param name="shutDownAction">	The shut down action. </param>
		public static void RestartAsAdministrator(Action shutDownAction = null, params string[] args)
		{
			var exeName = Process.GetCurrentProcess().MainModule.FileName;
			var startInfo = new ProcessStartInfo(exeName)
			{
				Verb = "runas",
				Arguments = string.Join(separator: " ", value: args)
			};
			Process.Start(startInfo);
			shutDownAction?.Invoke();
		}

		/// <summary>	Query if this object is administrator. </summary>
		/// <returns>	True if administrator, false if not. </returns>
		public static bool IsAdministrator()
		{
			var identity = WindowsIdentity.GetCurrent();
			var principal = new WindowsPrincipal(identity);
			return principal.IsInRole(WindowsBuiltInRole.Administrator);
		}
	}
}