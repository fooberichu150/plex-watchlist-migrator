using CommandLine;

namespace PlexWatchlistMigrator.ConsoleApp.Models
{
	public class Arguments
	{
		[Option('i', "input", Required = false, HelpText = "Full path to input file")]
		public string InputFile { get; set; } = string.Empty;

		[Option('o', "output", Required = false, HelpText = "Full path to destination file")]
		public string OutputFile { get; set; } = string.Empty;

	}
}
