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
		using (var scope = ServiceProvider.CreateAsyncScope())
		{
			var migrationHandler = scope.ServiceProvider.GetRequiredService<IMigrateViewDataActionHandler>();
			var response = await migrationHandler.ProcessAsync();
		}

		return exitCode;
    }
}
