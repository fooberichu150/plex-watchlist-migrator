namespace PlexWatchlistMigrator.Domain
{
	public class MediaItemUserView
	{
		public long? AccountId { get; set; }
		public string? Guid { get; set; }
		public long? MetadataType { get; set; }
		public long? LibrarySectionId { get; set; }
		public string? GrandParentTitle { get; set; }
		public long? ParentIndex { get; set; }
		public string? ParentTitle { get; set; }
		public long? Index { get; set; }
		public string? Title { get; set; }
		public string? ThumbUrl { get; set; }
		public DateTime? ViewedAt { get; set; }
		public string? GrandParentGuid { get; set; }
		public DateTime? OriginallyAvailableAt { get; set; }
	}
}
