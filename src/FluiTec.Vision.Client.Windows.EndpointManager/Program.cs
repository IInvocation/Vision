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
		#region Fields

		/// <summary>	The event. </summary>
		// ReSharper disable once NotAccessedField.Local
		private static EventWaitHandle _eventWaitHandle;

		#endregion

		/// <summary>	Main entry-point for this application. </summary>
		[STAThread]
		private static void Main()
		{
			if (IsFirstInstance())
				Launch();
			else
				Exit();
		}

		#region Helpers

		/// <summary>	Query if this object is first instance. </summary>
		/// <returns>	True if first instance, false if not. </returns>
		/// <remarks>
		///     Uses an EventWaitHandle created by windows to determine other instances of this application.
		/// </remarks>
		private static bool IsFirstInstance()
		{
			_eventWaitHandle = new EventWaitHandle(initialState: false, mode: EventResetMode.ManualReset,
				name: "vision_endpoint#startup", createdNew: out bool created);
			return created;
		}

		#endregion

		#region LaunchModes

		/// <summary>	Launches the application. </summary>
		/// <remarks>
		///     Registers some Services via IoC, automatically shut's down the webserver when exiting
		/// </remarks>
		private static void Launch()
		{
			var app = new App();
			app.InitializeComponent();

			var locatorManager = new SimpleIoCServiceLocatorManager();
			locatorManager.SetLocatorProvider();
			locatorManager.Register<IServiceLocatorManager>(locatorManager);
			locatorManager.Register<IWebServerManager, WebServerManager>();
			locatorManager.Register<IViewService, ViewService>();
			locatorManager.Register<ISettingsManager, SettingsManager>();

			app.Exit += (sender, args) => { ServiceLocator.Current.GetInstance<IWebServerManager>().Stop(); };
			app.Run();
		}

		/// <summary>	Exits the application. </summary>
		/// <remarks>
		///     Shows a message before exiting the application.
		/// </remarks>
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

		#endregion
	}
}