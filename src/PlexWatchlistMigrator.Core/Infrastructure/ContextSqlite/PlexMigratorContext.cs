using Microsoft.EntityFrameworkCore;
using PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;
using PlexWatchlistMigrator.Infrastructure.EntitiesSqlite.Configuration;

namespace PlexWatchlistMigrator.Infrastructure.ContextSqlite;

public partial class PlexMigratorContext : DbContext
{
    public PlexMigratorContext(DbContextOptions<PlexMigratorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<LibrarySection> LibrarySections { get; set; }

    public virtual DbSet<MetadataItem> MetadataItems { get; set; }

    public virtual DbSet<MetadataItemSetting> MetadataItemSettings { get; set; }

    public virtual DbSet<MetadataItemView> MetadataItemViews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    // TODO: Jon - Plex tables have triggers. See: https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/breaking-changes#mitigations-2
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		modelBuilder.ApplyConfigurations();
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
