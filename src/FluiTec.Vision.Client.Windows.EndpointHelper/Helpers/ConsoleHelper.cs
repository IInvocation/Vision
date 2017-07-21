using System;

namespace FluiTec.Vision.Client.Windows.EndpointHelper.Helpers
{
	public static class ConsoleHelper
	{
		/// <summary>	Reports the success. </summary>
		/// <param name="message">	The message. </param>
		public static void ReportSuccess(string message)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(message);
			Console.ResetColor();
		}

		public static void ReportStatus(string message)
		{
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine(message);
			Console.ResetColor();
		}

		/// <summary>	Reports a fault. </summary>
		/// <param name="message">	The message. </param>
		public static void ReportFault(string message)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(message);
			Console.ResetColor();
		}
	}
}