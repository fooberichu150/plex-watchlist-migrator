using PlexWatchlistMigrator.Infrastructure.ContextSqlite;

namespace PlexWatchlistMigrator.Infrastructure.Repositories
{
	public interface IRepository
	{
		void InitializeDbContext(PlexMigratorContext context);
	}

	public interface IRepository<TEntity> : IRepository
	{
	}

	public abstract class RepositoryBase : IRepository
	{
		protected PlexMigratorContext? DbContext { get; set; } = default!;

		public virtual void InitializeDbContext(PlexMigratorContext context)
		{
			DbContext = context;
		}
	}
}
