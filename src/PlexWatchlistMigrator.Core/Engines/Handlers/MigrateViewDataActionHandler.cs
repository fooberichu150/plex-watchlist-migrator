using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlexWatchlistMigrator.Infrastructure;
using PlexWatchlistMigrator.Infrastructure.ContextSqlite;
using PlexWatchlistMigrator.Infrastructure.Repositories;

namespace PlexWatchlistMigrator.Engines.Handlers
{
	public interface IMigrateViewDataActionHandler
	{
		Task<object> ProcessAsync();
	}

	public class MigrateViewDataActionHandler : IMigrateViewDataActionHandler
	{
		public MigrateViewDataActionHandler(
			ILogger<MigrateViewDataActionHandler> logger,
			ILocalDbContextFactory dbContextFactory)
		{
			Logger = logger;
			DbContextFactory = dbContextFactory;
		}

		private ILogger<MigrateViewDataActionHandler> Logger { get; }
		private ILocalDbContextFactory DbContextFactory { get; }

		public async Task<object> ProcessAsync()
		{
			// TODO: load all the view data from soure
			var sourceDb = DbContextFactory.GetContext(DbContextType.Source);
			// TODO: save all the view data to destination
			var destinationDb = DbContextFactory.GetContext(DbContextType.Destination);

			// TODO: Tasks
			// 1: get all the views for [all users -or- specific user(s)]; this will be adapted to a special model...
			var q = await sourceDb.MetadataItemViews
				//.Include(m => m.Account)
				.Include(m => m.LibrarySection)
				.Where(m => Domain.Constants.ValidSectionTypes.Contains(m.LibrarySection.SectionType) && m.MetadataItem != null)
				//.Take(100)
				.ToArrayAsync();

			Logger.LogInformation("View count: {views}", q.Length);

			// 2: get a list of old/source library sections (with types 1,2)
			var librarySections = await sourceDb.LibrarySections
				.Where(lib => Domain.Constants.ValidSectionTypes.Contains(lib.SectionType))
				.ToArrayAsync();

			Logger.LogInformation("Library Section count: {views}", librarySections.Length);

			// 3: get a list of old/source accounts
			var accounts = await sourceDb.Accounts
				.ToArrayAsync();
			Logger.LogInformation("User count: {accounts}", accounts.Length);

			// 4: get a list of media added dates for preserving ordering in the new system
			var metaDates = await sourceDb.MetadataItems
				//.Include(mi => mi.LibrarySection)
				.Where(mi => Domain.Constants.ValidSectionTypes.Contains(mi.LibrarySection.SectionType))
				.ToArrayAsync();

			Logger.LogInformation("Meta Dates count: {views}", metaDates.Length);

			var repoFactory = default(IRepositoryFactory);

			var repo = repoFactory.GetRepository<IAccountRepository,Infrastructure.EntitiesSqlite.Account>(DbContextType.Source);


			await Task.CompletedTask;

			return default;
		}
	}

	public interface ILoadLibrarySectionsHandler
	{
		Task<IEnumerable<Domain.LibrarySection>> GetLibrarySectionsAsync();
	}

	// TODO: Jon - not sure how I really feel about this tbh... I have a feeling I'm going to need to load from both the source and destination (due to comparisons)
	public class LoadLibrarySectionsHandler : ILoadLibrarySectionsHandler
	{
		public LoadLibrarySectionsHandler(
			ILogger<MigrateViewDataActionHandler> logger,
			ILocalDbContextFactory dbContextFactory,
			IMapper mapper)
		{
			Logger = logger;
			DbContextFactory = dbContextFactory;
			Mapper = mapper;
			PlexDbContext = DbContextFactory.GetContext(DbContextType.Source);
		}

		private ILogger<MigrateViewDataActionHandler> Logger { get; }
		private ILocalDbContextFactory DbContextFactory { get; }
		private IMapper Mapper { get; }

		private PlexMigratorContext PlexDbContext { get; }

		public async Task<IEnumerable<Domain.LibrarySection>> GetLibrarySectionsAsync()
		{
			var librarySections = await PlexDbContext.LibrarySections
				.Where(lib => Domain.Constants.ValidSectionTypes.Contains(lib.SectionType))
				.ToArrayAsync();

			return Mapper.Map<Domain.LibrarySection[]>(librarySections);
		}
	}
}
