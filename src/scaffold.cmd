dotnet ef dbcontext scaffold "Data Source=J:\Temp\Plex_Stuffs\source-com.plexapp.plugins.library.db;" Microsoft.EntityFrameworkCore.Sqlite --project PlexWatchlistMigrator.Core --startup-project PlexWatchlistMigrator.Core -o Infrastructure\EntitiesSqlite --context-dir Infrastructure\ContextSqlite -c PlexMigratorContext --force -t accounts -t library_sections -t metadata_item_settings -t metadata_item_views -t metadata_items
