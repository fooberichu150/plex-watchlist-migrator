namespace PlexWatchlistMigrator.Domain.Responses
{
    public class DataLoadContainer
    {
        public Account[] Accounts { get; set; } = default!;
        public LibrarySection[] LibrarySections { get; set; } = default!;
        public MediaItemUserView[] UserViewData { get; set; } = default!;
        public MetadataItemSetting[] MetadataItemSettings { get; set; } = default!;
        public MetadataItemSimple[] MetadataItems { get; set; } = default!;
    }
}
