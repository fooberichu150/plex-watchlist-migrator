using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlexWatchlistMigrator.Engines.Handlers;

public class Application
{
    public Application(
		ILogger<Application> logger,
		IServiceProvider serviceProvider)
    {
		Logger = logger;
		ServiceProvider = serviceProvider;
	}

	private ILogger<Application> Logger { get; }
	private IServiceProvider ServiceProvider { get; }

	public async Task<int> RunAsync(string[] args)
    {
		var exitCode = 0;

		var parser = new Parser(with =>
		{
			with.CaseInsensitiveEnumValues = true;
			with.HelpWriter = Console.Error;
		});

		using (var scope = ServiceProvider.CreateAsyncScope())
		{
			var migrationHandler = scope.ServiceProvider.GetRequiredService<IMigrateViewDataActionHandler>();
			await migrationHandler.ProcessAsync();
		}

		//var parsed = parser.ParseArguments<CryptoComTax.Console.Models.Arguments>(args);

		//parsed.WithParsed(args =>
		//{
		//	if (!File.Exists(args.InputFile))
		//	{
		//		throw new ArgumentException(nameof(args.InputFile), "File does not exist");
		//	}

		//	if (!Directory.Exists(args.OutputFolder))
		//	{
		//		throw new ArgumentException(nameof(args.OutputFolder), "Output folder does not exist");
		//	}

		//	var importer = _transactionImporterFactory.GetTransactionImporter(args.Exchange);
		//	var records = importer
		//		.ConvertFile(args.InputFile)
		//		.Where(record => record.IsValid)
		//		.ToList();

		//	var outputFileName = !string.IsNullOrWhiteSpace(args.OutputFile)
		//		? args.OutputFile
		//		: $"converted-crypto-com-tax-{Path.GetFileName(args.InputFile)}";

		//	var outputPath = Path.Combine(args.OutputFolder, outputFileName);
		//	_transactionExporter.WriteFile(outputPath, records);
		//});

		//parsed.WithNotParsed(errs =>
		//{
		//	var helpText = HelpText.AutoBuild(parsed, h => HelpText.DefaultParsingErrorsHandler(parsed, h), e => e);
		//	Console.Error.Write(helpText);

		//	exitCode = -2;
		//});

		return await Task.FromResult(exitCode);
    }
}
