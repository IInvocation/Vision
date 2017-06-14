using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace FluiTec.Vision.AuthHost.AspCoreHost
{
	/// <summary>	A startup. </summary>
	public class Startup : ConsoleHost.Startup
	{
		/// <summary>	Constructor. </summary>
		/// <param name="environment">	The environment. </param>
		public Startup(IHostingEnvironment environment) : base(environment)
		{
		}

		/// <summary>	Use browser. </summary>
		/// <param name="application">	The application. </param>
		/// <param name="environment">	The environment. </param>
		/// <remarks>
		/// Just overrides doin nothing to prevent double opening		 
		/// </remarks>
		protected override void UseBrowser(IApplicationBuilder application, IHostingEnvironment environment)
		{
			// ignore, since ASP.NET automatically does it.
			// base.UseBrowser(application, environment);
		}
	}
}