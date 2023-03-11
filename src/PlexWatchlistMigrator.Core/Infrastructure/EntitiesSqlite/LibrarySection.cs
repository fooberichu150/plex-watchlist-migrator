using System;
using System.Collections.Generic;

namespace PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;

public partial class LibrarySection
{
    public long Id { get; set; }

    public long? LibraryId { get; set; }

    public string? Name { get; set; }

    public string? NameSort { get; set; }

    public long? SectionType { get; set; }

    public string? Language { get; set; }

    public string? Agent { get; set; }

    public string? Scanner { get; set; }

    public string? UserThumbUrl { get; set; }

    public string? UserArtUrl { get; set; }

    public string? UserThemeMusicUrl { get; set; }

    public bool? Public { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ScannedAt { get; set; }

    public bool? DisplaySecondaryLevel { get; set; }

    public string? UserFields { get; set; }

    public string? QueryXml { get; set; }

    public long? QueryType { get; set; }

    public string? Uuid { get; set; }

    public long? ChangedAt { get; set; }

    public long? ContentChangedAt { get; set; }

    public virtual ICollection<MetadataItemView> MetadataItemViews { get; set; }
    public virtual ICollection<MetadataItem> MetadataItems { get; set; }
}
