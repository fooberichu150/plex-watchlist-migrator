using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlexWatchlistMigrator.Console.Configuration;
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
	Log.Information("Starting console host");
	IHost host = Host.CreateDefaultBuilder(args)
		.UseConsoleLifetime()
		.ConfigureAppConfiguration(app =>
		{
			app.AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: true, reloadOnChange: true);
			app.AddJsonFile($"appsettings.local.json", optional: true, reloadOnChange: true);
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

	var application = host.Services.GetRequiredService<Application>();
	exitCode = await application.RunAsync(args);

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