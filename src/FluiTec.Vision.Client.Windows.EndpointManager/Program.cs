using System;
using FluiTec.AppFx.InversionOfControl;
using FluiTec.AppFx.InversionOfControl.SimpleIoC;

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

			app.Run();
		}
	}
}