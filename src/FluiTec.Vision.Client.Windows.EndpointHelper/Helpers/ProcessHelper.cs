using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;

namespace FluiTec.Vision.Client.Windows.EndpointHelper.Helpers
{
	/// <summary>	The process helper. </summary>
	public static class ProcessHelper
	{
		/// <summary>	The Process extension method that redirect output to console. </summary>
		/// <param name="process">		 	The process to act on. </param>
		/// <param name="createNoWindow">	(Optional) True to create no window. </param>
		/// <returns>	The Process. </returns>
		public static Process RedirectOutputToConsole(this Process process, bool createNoWindow = true)
		{
			// set up output redirection
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			process.EnableRaisingEvents = true;
			process.StartInfo.CreateNoWindow = createNoWindow;

			// handle output
			process.ErrorDataReceived += (sender, args) => 
			{
				if (!string.IsNullOrWhiteSpace(args.Data))
					Console.WriteLine(args.Data);
			};
			process.OutputDataReceived += (sender, args) => 
			{
				if (!string.IsNullOrWhiteSpace(args.Data))
					Console.WriteLine(args.Data);
			};

			return process;
		}

		/// <summary>	The Process extension method that executes the and wait operation. </summary>
		/// <param name="process">	The process to act on. </param>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		public static bool RunAndWait(this Process process)
		{
			process.Start();

			process.BeginErrorReadLine();
			process.BeginOutputReadLine();

			process.WaitForExit();

			return process.ExitCode == 0;
		}

		/// <summary>
		/// The Process extension method that executes the and wait for named pipe result operation.
		/// </summary>
		/// <param name="process"> 	The process to act on. </param>
		/// <param name="pipeName">	Name of the pipe. </param>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		public static bool RunAndWaitForForNamedPipeResult(this Process process, string pipeName)
		{
			process.Start();
			process.BeginErrorReadLine();
			process.BeginOutputReadLine();

			using (var server = new NamedPipeServerStream(pipeName, PipeDirection.InOut))
			{
				server.WaitForConnection();
				using (var sr = new StreamReader(server))
				{
					var i = 1;
					while (i == 1)
					{
						var line = sr.ReadLine();
						if (!string.IsNullOrWhiteSpace(line))
						{
							i = int.Parse(line);
						}
					}
					return i == 0;
				}
			}
		}
	}
}
