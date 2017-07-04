using System;

namespace Fluitec.Vision.Client.WindowsClient
{
	internal class Program
	{
		[STAThread]
		private static void Main()
		{
			var app = new App();
			app.InitializeComponent();
			app.Run();
		}
	}
}
