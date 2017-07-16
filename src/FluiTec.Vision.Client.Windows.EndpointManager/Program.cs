extern alias myservicelocation;
using System;
using FluiTec.AppFx.InversionOfControl;
using FluiTec.AppFx.InversionOfControl.SimpleIoC;
using FluiTec.Vision.Client.Windows.EndpointManager.WebServer;
using myservicelocation::Microsoft.Practices.ServiceLocation;

namespace FluiTec.Vision.Client.Windows.EndpointManager
{
	/// <summary>	A program. </summary>
	internal class Program
	{
		/// <summary>	Main entry-point for this application. </summary>
		[STAThread]
		private static void Main()
		{
			var app = new App();
			app.InitializeComponent();

			var locatorManager = new SimpleIoCServiceLocatorManager();
			locatorManager.SetLocatorProvider();
			locatorManager.Register<IServiceLocatorManager>(locatorManager);
			locatorManager.Register<IWebServerManager, WebServerManager>();
			
			app.Exit += (sender, args) => { StopServer(); };
			app.Run();
		}

		/// <summary>	Stops a server. </summary>
		private static void StopServer()
		{
			ServiceLocator.Current.GetInstance<IWebServerManager>().Stop();
		}
	}
}