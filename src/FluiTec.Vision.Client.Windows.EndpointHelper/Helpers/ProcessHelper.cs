using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;

namespace FluiTec.Vision.Client.Windows.EndpointHelper.Helpers
{
	/// <summary>	The process helper. </summary>
	public static class ProcessHelper
	{
		/// <summary>	. </summary>
		private static readonly string _pipeArg = "-pipename:";

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
		///     The Process extension method that executes the and wait for named pipe result operation.
		/// </summary>
		/// <param name="process"> 	The process to act on. </param>
		/// <param name="pipeName">	Name of the pipe. </param>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		public static bool RunAndWaitForForNamedPipeResult(this Process process, string pipeName)
		{
			process.StartInfo.Arguments += $" {_pipeArg}{pipeName}";

			using (var server = new NamedPipeServerStream(pipeName, PipeDirection.InOut))
			{
				process.Start();

				server.WaitForConnection();

				using (var sr = new StreamReader(server))
				{
					var i = 1;

					while (i == 1)
					{
						var line = sr.ReadLine();
						if (!string.IsNullOrWhiteSpace(line))
							i = int.Parse(line);
					}
					return i == 0;
				}
			}
		}

		/// <summary>	Reports status using pipe. </summary>
		/// <param name="ok">  	True if the operation was a success, false if it failed. </param>
		/// <param name="args">	An array of command-line argument strings. </param>
		public static void ReportStatusUsingPipe(bool ok, params string[] args)
		{
			if (args.Length <= 0 || !args.Any(arg => !string.IsNullOrWhiteSpace(arg) && arg.StartsWith(_pipeArg))) return;
			var pipeName = args.First(arg => arg.StartsWith(_pipeArg)).Substring(_pipeArg.Length);

			try
			{
				using (var client = new NamedPipeClientStream(serverName: ".", pipeName: pipeName,
					direction: PipeDirection.InOut))
				{
					client.Connect();
					using (var sw = new StreamWriter(client))
					{
						sw.WriteLine(ok ? 0 : -1);
						sw.Flush();
					}
				}
			}
			catch (Exception)
			{
				// silently ignore, since someone probably started this manually via cli
			}
		}
	}
}