using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PlexWatchlistMigrator.Infrastructure.Repositories
{
	public interface IRepositoryFactory
	{
		TRepository GetRepository<TRepository, TEntity>(DbContextType contextType)
			where TRepository : IRepository<TEntity>
			where TEntity : class;
	}

	public class RepositoryFactory : IRepositoryFactory
	{
		public RepositoryFactory(
			ILogger<RepositoryFactory> logger,
			ILocalDbContextFactory dbContextFactory,
			IServiceProvider serviceProvider)
		{
			Logger = logger;
			DbContextFactory = dbContextFactory;
			ServiceProvider = serviceProvider;
		}

		private ILogger<RepositoryFactory> Logger { get; }
		private ILocalDbContextFactory DbContextFactory { get; }
		private IServiceProvider ServiceProvider { get; }

		public TRepository GetRepository<TRepository, TEntity>(DbContextType contextType)
			where TRepository : IRepository<TEntity>
			where TEntity : class
		{
			// TODO: Jon - should have local cache of the repositories based on contextType:TEntity?
			var dbContext = DbContextFactory.GetContext(contextType);

			var repository = (TRepository)ServiceProvider.GetRequiredService(typeof(TRepository));
			repository.InitializeDbContext(dbContext);

			return repository;
		}
	}
}
