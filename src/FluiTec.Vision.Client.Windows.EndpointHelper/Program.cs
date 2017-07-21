using System;
using CommandLine;
using FluiTec.AppFx.Cli;
using FluiTec.Vision.Client.Windows.EndpointHelper.Helpers;
using FluiTec.Vision.Client.Windows.EndpointHelper.Options;

namespace FluiTec.Vision.Client.Windows.EndpointHelper
{
	/// <summary>	A program. </summary>
	internal class Program
	{
		/// <summary>	Main entry-point for this application. </summary>
		/// <param name="args">	An array of command-line argument strings. </param>
		private static void Main(string[] args)
		{
			if (CliHelper.IsAdministrator())
			{
				try
				{
					var options = new ConfigFileOptions();
					var parser = new Parser(parserSettings => parserSettings.IgnoreUnknownArguments = true);
					parser.ParseArguments(args, options);

					if (options.IsValid)
					{
						ConsoleHelper.ReportSuccess($"Datei {options.FilePath} wird verarbeitet...");
						options.Execute();
					}
					else
					{
						throw new InvalidOperationException($"Datei {options.FilePath} existiert nicht!");
					}
				}
				catch (Exception e)
				{
					ConsoleHelper.ReportFault(e.Message);
					Console.WriteLine(value: 
						"Ausgabe zur Fehlersuche bitte erst mit <ENTER> schließen, wenn die Ursache des Problems von Ihnen ermittelt wurde!");
					Console.ReadLine();
					throw;
				}
				
			}
			else
			{
				CliHelper.RestartAsAdministrator(shutDownAction: null, args: args);
			}
		}
	}
}
