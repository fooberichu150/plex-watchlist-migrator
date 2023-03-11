using System;
using System.Collections.Generic;

namespace PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;

public partial class MetadataItemSetting
{
    public long Id { get; set; }

    public long? AccountId { get; set; }

    public string? Guid { get; set; }

    public double? Rating { get; set; }

    public long? ViewOffset { get; set; }

    public long? ViewCount { get; set; }

    public long? LastViewedAt { get; set; }

    public long? CreatedAt { get; set; }

    public long? UpdatedAt { get; set; }

    public long? SkipCount { get; set; }

    public long? LastSkippedAt { get; set; }

    public long? ChangedAt { get; set; }

    public string? ExtraData { get; set; }

    public long? LastRatedAt { get; set; }
}
