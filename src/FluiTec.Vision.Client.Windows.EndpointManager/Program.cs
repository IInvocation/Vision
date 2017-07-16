extern alias myservicelocation;
using System;
using System.Threading;
using FluiTec.AppFx.InversionOfControl;
using FluiTec.AppFx.InversionOfControl.SimpleIoC;
using FluiTec.Vision.Client.Windows.EndpointManager.Views;
using FluiTec.Vision.Client.Windows.EndpointManager.WebServer;
using myservicelocation::Microsoft.Practices.ServiceLocation;

namespace FluiTec.Vision.Client.Windows.EndpointManager
{
	/// <summary>	A program. </summary>
	internal class Program
	{
		/// <summary>	The event. </summary>
		static EventWaitHandle _sEvent;

		/// <summary>	Main entry-point for this application. </summary>
		[STAThread]
		private static void Main()
		{
			// prevent multiple instances with eventwaithandle
			_sEvent = new EventWaitHandle(initialState: false, mode: EventResetMode.ManualReset, name: "vision_endpoint#startup", createdNew: out bool created);
			if (created) Launch();
			else Exit();
		}

		private static void Launch()
		{
			var app = new App();
			app.InitializeComponent();

			var locatorManager = new SimpleIoCServiceLocatorManager();
			locatorManager.SetLocatorProvider();
			locatorManager.Register<IServiceLocatorManager>(locatorManager);
			locatorManager.Register<IWebServerManager, WebServerManager>();
			locatorManager.Register<IViewService, ViewService>();

			app.Exit += (sender, args) => { StopServer(); };
			app.Run();
		}

		private static void Exit()
		{
			var app = new App();
			app.InitializeComponent();
			var locatorManager = new SimpleIoCServiceLocatorManager();
			locatorManager.SetLocatorProvider();
			locatorManager.Register<IServiceLocatorManager>(locatorManager);
			locatorManager.Register<IWebServerManager, WebServerManager>();
			locatorManager.Register<IViewService, ViewService>();

			app.DoShowExit = true;
			app.Run();
		}

		/// <summary>	Stops a server. </summary>
		private static void StopServer()
		{
			ServiceLocator.Current.GetInstance<IWebServerManager>().Stop();
		}
	}
}