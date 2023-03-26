using Microsoft.Extensions.Logging;
using PlexWatchlistMigrator.Collections;
using PlexWatchlistMigrator.Domain.Responses;
using PlexWatchlistMigrator.Engines.Comparers;
using PlexWatchlistMigrator.Infrastructure;
using PlexWatchlistMigrator.Infrastructure.Repositories;
using Entities = PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;

namespace PlexWatchlistMigrator.Engines.Handlers
{
	public interface IUpdateDataHandler
	{
		Task<MigrateDataResponse> UpdateDataAsync(DataLoadContainer sourceData, DataLoadContainer destinationData);
	}

	public class UpdateDataHandler : IUpdateDataHandler
	{
		public UpdateDataHandler(
			ILogger<UpdateDataHandler> logger,
			IRepositoryFactory repositoryFactory)
		{
			Logger = logger;
			RepositoryFactory = repositoryFactory;

			var dbContextType = DbContextType.Destination;
			AccountRepository = RepositoryFactory.GetRepository<IAccountRepository, Entities.Account>(dbContextType);
			LibrarySectionRepository = RepositoryFactory.GetRepository<ILibrarySectionRepository, Entities.LibrarySection>(dbContextType);
			MetadataItemRepository = RepositoryFactory.GetRepository<IMetadataItemRepository, Entities.MetadataItem>(dbContextType);
			MetadataItemViewRepository = RepositoryFactory.GetRepository<IMetadataItemViewRepository, Entities.MetadataItemView>(dbContextType);
			MetadataSettingRepository = RepositoryFactory.GetRepository<IMetadataItemSettingRepository, Entities.MetadataItemSetting>(dbContextType);
		}

		private ILogger<UpdateDataHandler> Logger { get; }
		private IRepositoryFactory RepositoryFactory { get; }

		private IAccountRepository AccountRepository { get; }
		private ILibrarySectionRepository LibrarySectionRepository { get; }
		private IMetadataItemRepository MetadataItemRepository { get; }
		private IMetadataItemViewRepository MetadataItemViewRepository { get; }
		private IMetadataItemSettingRepository MetadataSettingRepository { get; }

		private DataLoadContainer SourceData { get; set; } = default!;
		private DataLoadContainer DestinationData { get; set; } = default!;

		public async Task<MigrateDataResponse> UpdateDataAsync(DataLoadContainer sourceData, DataLoadContainer destinationData)
		{
			SourceData = sourceData;
			DestinationData = destinationData;

			var response = new MigrateDataResponse();
			var addedAccounts = await UpdateUsersAsync();
			var addedSettings = await UpdateMetadataSettingsAsync();
			var addedViews = await UpdateUserViewsAsync();

			response.AddedAccounts = addedAccounts;
			response.AddedSettings = addedSettings;
			response.AddedViews = addedViews;

			return response;
		}

		private async Task<int> UpdateUsersAsync()
		{
			var updateUsers = SourceData.Accounts.Except(DestinationData.Accounts, AccountEqualityComparer.Instance).ToArray();

			Logger.LogInformation("Users not found: {accounts}", updateUsers.Length);
			var addedAccounts = await AccountRepository.AddAccountsAsync(updateUsers);
			Logger.LogInformation("Added {accounts} accounts", addedAccounts);
			return addedAccounts;
		}

		private async Task<int> UpdateMetadataSettingsAsync()
		{
			var updateSettings = SourceData.MetadataItemSettings
				.Except(DestinationData.MetadataItemSettings, MetadataItemSettingComparer.Instance)
				.ToArray();

			// match by guid
			var settingsWithMatches = updateSettings
				.Join(DestinationData.MetadataItems, setting => setting.Guid, meta => meta.Guid, (setting, meta) => setting)
				.ToArray();

			Logger.LogInformation("Metadata Item Settings not found: {settings}", updateSettings.Length);
			Logger.LogInformation("Metadata Item Settings with matches: {settings}", settingsWithMatches.Length);

			var addedSettings = await MetadataSettingRepository.AddMetadataItemSettingsAsync(settingsWithMatches);
			return addedSettings;
		}

		private async Task<int> UpdateUserViewsAsync()
		{
			var updateViews = SourceData.UserViewData
				.Except(DestinationData.UserViewData, MetadataItemViewComparer.Instance)
				.ToArray();

			Logger.LogInformation("User views not found: {views}", updateViews.Length);

			var addedViews = await MetadataItemViewRepository.AddUserViewsAsync(updateViews);
			return addedViews;
		}
	}
}
