using Microsoft.Extensions.Logging;
using PlexWatchlistMigrator.Domain.Responses;
using PlexWatchlistMigrator.Infrastructure;

namespace PlexWatchlistMigrator.Engines.Handlers
{
	public interface IMigrateViewDataActionHandler
	{
		Task<MigrateDataResponse> ProcessAsync();
	}

	public class MigrateViewDataActionHandler : IMigrateViewDataActionHandler
	{
		public MigrateViewDataActionHandler(
			ILogger<MigrateViewDataActionHandler> logger,
			IDataLoadHandlerFactory dataLoadHandlerFactory)
		{
			Logger = logger;
			DataLoadHandlerFactory = dataLoadHandlerFactory;
		}

		private ILogger<MigrateViewDataActionHandler> Logger { get; }
		private IDataLoadHandlerFactory DataLoadHandlerFactory { get; }

		public async Task<MigrateDataResponse> ProcessAsync()
		{
			var sourceLoader = DataLoadHandlerFactory.GetHandler(DbContextType.Source);
			var sourceData = await sourceLoader.LoadAsync();

			Logger.LogInformation("Source Account count: {accounts}", sourceData.Accounts.Length);
			Logger.LogInformation("Source Library Section count: {sections}", sourceData.LibrarySections.Length);
			Logger.LogInformation("Source Metadata Items count: {items}", sourceData.MetadataItems.Length);
			Logger.LogInformation("Source Metadata Item Settings count: {settings}", sourceData.MetadataItemSettings.Length);
			Logger.LogInformation("Source User Views count: {views}", sourceData.UserViewData.Length);

			var destinationLoader = DataLoadHandlerFactory.GetHandler(DbContextType.Destination);
			var destinationData = await destinationLoader.LoadAsync();

			Logger.LogInformation("Destination Account count: {accounts}", destinationData.Accounts.Length);
			Logger.LogInformation("Destination Library Section count: {sections}", destinationData.LibrarySections.Length);
			Logger.LogInformation("Destination Metadata Items count: {items}", destinationData.MetadataItems.Length);
			Logger.LogInformation("Destination Metadata Item Settings count: {settings}", destinationData.MetadataItemSettings.Length);
			Logger.LogInformation("Destination User Views count: {views}", destinationData.UserViewData.Length);

			var destinationUpdater = DataLoadHandlerFactory.GetUpdateHandler();
			var response = await destinationUpdater.UpdateDataAsync(sourceData, destinationData);

			return response;
		}
	}
}
