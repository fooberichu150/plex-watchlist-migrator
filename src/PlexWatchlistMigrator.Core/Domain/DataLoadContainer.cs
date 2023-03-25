namespace PlexWatchlistMigrator.Domain
{
	public class DataLoadContainer
	{
		public Account[] Accounts { get; set; } = default!;
		public LibrarySection[] LibrarySections { get; set; } = default!;
	}
}
