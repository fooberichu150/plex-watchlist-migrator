using System;
using System.Collections.Generic;

namespace PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;

public partial class Account
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? HashedPassword { get; set; }

    public string? Salt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DefaultAudioLanguage { get; set; }

    public string? DefaultSubtitleLanguage { get; set; }

    public bool? AutoSelectSubtitle { get; set; }

    public bool? AutoSelectAudio { get; set; }

    public virtual ICollection<MetadataItemView> MetadataItemViews { get; set; }
}
