using CommandLine;
using CommandLine.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlexWatchlistMigrator.ConsoleApp.Configuration;
using Serilog;

var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
var builder = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
	.AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: true, reloadOnChange: true)
	.AddJsonFile($"appsettings.local.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables();

IConfigurationRoot configuration = builder.Build();

Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(configuration)
	.CreateBootstrapLogger();

var exitCode = 0;
try
{
	var argOverrideSettings = new Dictionary<string, string?>();
	var parser = new Parser(with =>
	{
		with.CaseInsensitiveEnumValues = true;
		with.HelpWriter = Console.Error;
	});

	var parsed = parser.ParseArguments<PlexWatchlistMigrator.ConsoleApp.Models.Arguments>(args);
	parsed.WithParsed(args =>
	{
		if (!string.IsNullOrWhiteSpace(args.InputFile) && !File.Exists(args.InputFile))
		{
			throw new ArgumentException(nameof(args.InputFile), "Input file does not exist");
		}

		if (!string.IsNullOrWhiteSpace(args.OutputFile) && !File.Exists(args.OutputFile))
		{
			throw new ArgumentException(nameof(args.OutputFile), "Destination file does not exist");
		}

		if (!string.IsNullOrWhiteSpace(args.InputFile))
			argOverrideSettings["ConnectionStrings:Source"] = $"Data Source={args.InputFile}";
		if (!string.IsNullOrWhiteSpace(args.OutputFile))
			argOverrideSettings["ConnectionStrings:Destination"] = $"Data Source={args.OutputFile}";
	});
	parsed.WithNotParsed(errs =>
	{
		var helpText = HelpText.AutoBuild(parsed, h => HelpText.DefaultParsingErrorsHandler(parsed, h), e => e);
		Console.Error.Write(helpText);

		exitCode = -2;
	});

	Log.Information("Starting console host");
	IHost host = Host.CreateDefaultBuilder(args)
		.UseConsoleLifetime()
		.ConfigureAppConfiguration(app =>
		{
			app.AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: true, reloadOnChange: true);
			app.AddJsonFile($"appsettings.local.json", optional: true, reloadOnChange: true);
			app.AddInMemoryCollection(argOverrideSettings);
		})
		.UseSerilog((context, services, configuration) =>
		{
			configuration
				.ReadFrom.Configuration(context.Configuration)
				.ReadFrom.Services(services);
		})
		.ConfigureServices((hostContext, services) =>
		{
			services.ConfigureApplicationServices(hostContext);
		})
		.Build();

	if (exitCode == 0)
	{
		var application = host.Services.GetRequiredService<Application>();
		exitCode = await application.RunAsync(args);
	}

	await Task.Delay(250);
	Console.WriteLine("Press any key to end...");
	Console.ReadKey();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
	Log.CloseAndFlush();
	Environment.Exit(exitCode);
}