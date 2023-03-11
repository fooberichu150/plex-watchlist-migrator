using System;
using System.Collections.Generic;

namespace PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;

public partial class MetadataItemView
{
    public long Id { get; set; }

    public long? AccountId { get; set; }

    public string? Guid { get; set; }

    public long? MetadataType { get; set; }

    public long? LibrarySectionId { get; set; }

    public string? GrandparentTitle { get; set; }

    public long? ParentIndex { get; set; }

    public string? ParentTitle { get; set; }

    public long? Index { get; set; }

    public string? Title { get; set; }

    public string? ThumbUrl { get; set; }

    public long? ViewedAt { get; set; }

    public string? GrandparentGuid { get; set; }

    public long? OriginallyAvailableAt { get; set; }

    public long? DeviceId { get; set; }

    public virtual Account Account { get; set; }
    public virtual LibrarySection LibrarySection { get; set; }
    public virtual MetadataItem MetadataItem { get; set; }
}
