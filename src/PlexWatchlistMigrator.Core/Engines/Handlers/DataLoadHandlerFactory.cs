using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlexWatchlistMigrator.Domain;
using PlexWatchlistMigrator.Infrastructure;
using PlexWatchlistMigrator.Infrastructure.ContextSqlite;
using PlexWatchlistMigrator.Infrastructure.Repositories;
using Entities = PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;

namespace PlexWatchlistMigrator.Engines.Handlers
{
	public interface IDataLoadHandlerFactory
	{
		IDataLoadHandler GetHandler(DbContextType dbContextType);
	}

	public class DataLoadHandlerFactory : IDataLoadHandlerFactory
	{
		public DataLoadHandlerFactory(
			IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider;
		}

		private IServiceProvider ServiceProvider { get; }

		public IDataLoadHandler GetHandler(DbContextType dbContextType)
		{
			var handler = ServiceProvider
				.GetRequiredService<IDataLoadHandler>()
				.Initialize(dbContextType);

			return handler;
		}
	}

	public interface IDataLoadHandler
	{
		IDataLoadHandler Initialize(DbContextType dbContextType);
		Task<DataLoadContainer> LoadAsync();
	}

	public class DataLoadHandler : IDataLoadHandler
	{
		public DataLoadHandler(
			IServiceProvider serviceProvider,
			ILogger<DataLoadHandler> logger,
			IRepositoryFactory repositoryFactory,
			IMapper mapper)
		{
			ServiceProvider = serviceProvider;
			Logger = logger;
			RepositoryFactory = repositoryFactory;
			Mapper = mapper;
		}

		private IServiceProvider ServiceProvider { get; set; }
		private ILogger<DataLoadHandler> Logger { get; }
		private IRepositoryFactory RepositoryFactory { get; }
		private IMapper Mapper { get; }

		private PlexMigratorContext DbContext { get; set; } = default!;
		private ILoadLibrarySectionsHandler LibrarySectionLoadHandler { get; set; } = default!;

		private IAccountRepository AccountRepository { get; set; } = default!;
		private ILibrarySectionRepository LibrarySectionRepository { get; set; } = default!;

		public IDataLoadHandler Initialize(DbContextType dbContextType)
		{
			// TODO: Jon - initialize each load handler...
			LibrarySectionLoadHandler = ServiceProvider
				.GetRequiredService<ILoadLibrarySectionsHandler>()
				.Initialize(dbContextType);

			AccountRepository = RepositoryFactory.GetRepository<IAccountRepository, Entities.Account>(dbContextType);
			LibrarySectionRepository = RepositoryFactory.GetRepository<ILibrarySectionRepository, Entities.LibrarySection>(dbContextType);

			return this;
		}

		public async Task<DataLoadContainer> LoadAsync()
		{
			// TODO: Tasks
			// 1: get all the views for [all users -or- specific user(s)]; this will be adapted to a special model...

			// 2: get a list of old/source library sections (with types 1,2)
			var librarySections = await LibrarySectionRepository.GetAllAsync();

			// 3: get a list of old/source accounts
			var users = await AccountRepository.GetAllAsync();

			// 4: get a list of media added dates for preserving ordering in the new system

			var dataContainer = new DataLoadContainer
			{
				Accounts = users.ToArray(),
				LibrarySections = librarySections.ToArray()
			};

			return dataContainer;
		}
	}

	public interface ILoadLibrarySectionsHandler
	{
		ILoadLibrarySectionsHandler Initialize(DbContextType dbContextType);
		Task<IEnumerable<Domain.LibrarySection>> GetLibrarySectionsAsync();
	}

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
		}

		private ILogger<MigrateViewDataActionHandler> Logger { get; }
		private ILocalDbContextFactory DbContextFactory { get; }
		private IMapper Mapper { get; }

		private PlexMigratorContext PlexDbContext { get; set; } = default!;

		public ILoadLibrarySectionsHandler Initialize(DbContextType dbContextType)
		{
			PlexDbContext = DbContextFactory.GetContext(DbContextType.Source);
			return this;
		}

		public async Task<IEnumerable<Domain.LibrarySection>> GetLibrarySectionsAsync()
		{
			// TODO: Jon - direct DB --or-- do we use the repository instead?
			var librarySections = await PlexDbContext.LibrarySections
				.Where(lib => Domain.Constants.ValidSectionTypes.Contains(lib.SectionType))
				.ToArrayAsync();

			return Mapper.Map<Domain.LibrarySection[]>(librarySections);
		}
	}
}
