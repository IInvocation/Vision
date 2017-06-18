using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LineCounter
{
	/// <summary>	A program counting files and lines. </summary>
	internal class Program
	{
		/// <summary>	Main entry-point for this application. </summary>
		// ReSharper disable once UnusedMember.Local
		private static void Main()
		{
			var baseDir = new DirectoryInfo("..\\").FullName;
			Console.WriteLine($"Inspecting '{baseDir}' ...");

			foreach (var fileType in GetFileTypes())
			{
				var files = FindFiles(fileType.Value, baseDir).ToList();
				var fileCounter = files.Count;
				var lineCounter =
					files.Sum(file => File.ReadLines(file).Count(line => !string.IsNullOrWhiteSpace(line) && line.Length > 1));
				Console.WriteLine($"- found {lineCounter} lines of {fileType.Key} ({fileType.Value}) in {fileCounter} files.");
			}
			Console.ReadLine();
		}

		/// <summary>	Gets file types. </summary>
		/// <returns>	The file types. </returns>
		private static Dictionary<string, string> GetFileTypes()
		{
			return new Dictionary<string, string>
			{
				{"C#-Code", "*.cs"},
				{"XAML", "*.xaml"},
				{"XML", "*.config|*.manifest|*.xml"},
				{"ProjectMarkup", "*.csproj"},
				{"JSON", "*.json" }
			};
		}

		/// <summary>	Finds the files in this collection. </summary>
		/// <param name="searchPattern">	A pattern specifying the search. </param>
		/// <param name="baseDir">			The base dir. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process the files in this collection.
		/// </returns>
		private static IEnumerable<string> FindFiles(string searchPattern, string baseDir)
		{
			// ArrayList will hold all file names
			var allFiles = new List<string>();

			// Create an array of filter string
			var multipleFilters = searchPattern.Split('|');

			// for each filter find mathing file names
			foreach (var fileFilter in multipleFilters)
				allFiles.AddRange(Directory.GetFiles(baseDir, fileFilter, SearchOption.AllDirectories));

			return allFiles;
		}
	}
}