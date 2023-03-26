using Microsoft.Extensions.Logging;
using PlexWatchlistMigrator.Domain.Responses;
using PlexWatchlistMigrator.Infrastructure;
using PlexWatchlistMigrator.Infrastructure.Repositories;
using Entities = PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;

namespace PlexWatchlistMigrator.Engines.Handlers
{
    public interface IDataLoadHandler
	{
		IDataLoadHandler Initialize(DbContextType dbContextType);
		Task<DataLoadContainer> LoadAsync();
	}

	public class DataLoadHandler : IDataLoadHandler
	{
		public DataLoadHandler(
			ILogger<DataLoadHandler> logger,
			IRepositoryFactory repositoryFactory)
		{
			Logger = logger;
			RepositoryFactory = repositoryFactory;
		}

		private ILogger<DataLoadHandler> Logger { get; }
		private IRepositoryFactory RepositoryFactory { get; }

		private IAccountRepository AccountRepository { get; set; } = default!;
		private ILibrarySectionRepository LibrarySectionRepository { get; set; } = default!;
		private IMetadataItemRepository MetadataItemRepository { get; set; } = default!;
		private IMetadataItemViewRepository MetadataItemViewRepository { get; set; } = default!;
		private IMetadataItemSettingRepository MetadataSettingRepository { get; set; } = default!;

		public IDataLoadHandler Initialize(DbContextType dbContextType)
		{
			AccountRepository = RepositoryFactory.GetRepository<IAccountRepository, Entities.Account>(dbContextType);
			LibrarySectionRepository = RepositoryFactory.GetRepository<ILibrarySectionRepository, Entities.LibrarySection>(dbContextType);
			MetadataItemRepository = RepositoryFactory.GetRepository<IMetadataItemRepository, Entities.MetadataItem>(dbContextType);
			MetadataItemViewRepository = RepositoryFactory.GetRepository<IMetadataItemViewRepository, Entities.MetadataItemView>(dbContextType);
			MetadataSettingRepository = RepositoryFactory.GetRepository<IMetadataItemSettingRepository, Entities.MetadataItemSetting>(dbContextType);

			return this;
		}

		public async Task<DataLoadContainer> LoadAsync()
		{
			// TODO: Tasks
			// 1: get all the views for [all users -or- specific user(s)]; this will be adapted to a special model...
			var userViews = await MetadataItemViewRepository.GetUserViewsAsync();
			var metaSettings = await MetadataSettingRepository.GetAllAsync();

			// 2: get a list of old/source library sections (with types 1,2)
			var librarySections = await LibrarySectionRepository.GetAllAsync();

			// 3: get a list of old/source accounts, watchlists, and settings...
			var users = await AccountRepository.GetAllAsync();

			// 4: get a list of media added dates for preserving ordering in the new system
			var metaDataItems = await MetadataItemRepository.GetAllSimpleAsync();

			var dataContainer = new DataLoadContainer
			{
				Accounts = users.ToArray(),
				LibrarySections = librarySections.ToArray(),
				UserViewData = userViews.ToArray(),
				MetadataItemSettings = metaSettings.ToArray(),
				MetadataItems = metaDataItems.ToArray(),
			};

			return dataContainer;
		}
	}
}
