using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PlexWatchlistMigrator.Infrastructure.ContextSqlite;

namespace PlexWatchlistMigrator.Infrastructure
{
	public interface ILocalDbContextFactory
	{
		PlexMigratorContext GetContext(DbContextType dbContextType);
	}

	public class LocalDbContextFactory : ILocalDbContextFactory
	{
		private ConcurrentDictionary<DbContextType, PlexMigratorContext> _contexts;

        public LocalDbContextFactory(
			IConfiguration configuration)
        {
			Configuration = configuration;
			_contexts = new ConcurrentDictionary<DbContextType, PlexMigratorContext>();
		}

		private IConfiguration Configuration { get; }

		public PlexMigratorContext GetContext(DbContextType dbContextType)
		{
			if (!_contexts.TryGetValue(dbContextType, out var dbContext))
			{
				var connectionString = Configuration.GetConnectionString($"{dbContextType}");
				var optsBuilder = new DbContextOptionsBuilder<PlexMigratorContext>()
					.UseSqlite(connectionString);

				dbContext = new PlexMigratorContext(optsBuilder.Options);
			}

			return dbContext;
		}
	}
}
