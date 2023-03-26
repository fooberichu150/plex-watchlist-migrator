using Microsoft.EntityFrameworkCore;

namespace PlexWatchlistMigrator.Infrastructure.EntitiesSqlite.Configuration
{
	public static class ModelBuilderExtensions
	{
		public static ModelBuilder ApplyConfigurations(this ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new AccountConfiguration());
			modelBuilder.ApplyConfiguration(new LibrarySectionConfiguration());
			modelBuilder.ApplyConfiguration(new MetadataItemConfiguration());
			modelBuilder.ApplyConfiguration(new MetadataItemSettingConfiguration());
			modelBuilder.ApplyConfiguration(new MetadataItemViewConfiguration());

			return modelBuilder;
		}
	}
}
