namespace PlexWatchlistMigrator.Domain
{
	public class MetadataItemSimple
	{
		public long Id { get; set; }
		public string? Guid { get; set; }
		public DateTime? AddedAt { get; set; }
		public DateTime? CreatedAt { get; set; }
	}
}
