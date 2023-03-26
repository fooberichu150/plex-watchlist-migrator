using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlexWatchlistMigrator.Infrastructure.EntitiesSqlite.Configuration
{
	public class MetadataItemSettingConfiguration : IEntityTypeConfiguration<MetadataItemSetting>
	{
		public void Configure(EntityTypeBuilder<MetadataItemSetting> builder)
		{
			builder.ToTable("metadata_item_settings");

			builder.HasIndex(e => e.AccountId, "index_metadata_item_settings_on_account_id");

			builder.HasIndex(e => e.ChangedAt, "index_metadata_item_settings_on_changed_at");

			builder.HasIndex(e => e.Guid, "index_metadata_item_settings_on_guid");

			builder.HasIndex(e => e.LastViewedAt, "index_metadata_item_settings_on_last_viewed_at");

			builder.HasIndex(e => e.Rating, "index_metadata_item_settings_on_rating");

			builder.HasIndex(e => e.SkipCount, "index_metadata_item_settings_on_skip_count");

			builder.HasIndex(e => e.ViewCount, "index_metadata_item_settings_on_view_count");

			builder.HasIndex(e => e.ViewOffset, "index_metadata_item_settings_on_view_offset");

			builder.Property(e => e.Id).HasColumnName("id");
			builder.Property(e => e.AccountId).HasColumnName("account_id");
			builder.Property(e => e.ChangedAt)
				.HasDefaultValueSql("0")
				.HasColumnType("integer(8)")
				.HasColumnName("changed_at");
			builder.Property(e => e.CreatedAt)
				.HasColumnType("dt_integer(8)")
				.HasColumnName("created_at");
			builder.Property(e => e.ExtraData)
				.HasColumnType("varchar(255)")
				.HasColumnName("extra_data");
			builder.Property(e => e.Guid)
				.HasColumnType("varchar(255)")
				.HasColumnName("guid");
			builder.Property(e => e.LastRatedAt)
				.HasColumnType("dt_integer(8)")
				.HasColumnName("last_rated_at");
			builder.Property(e => e.LastSkippedAt)
				.HasColumnType("dt_integer(8)")
				.HasColumnName("last_skipped_at");
			builder.Property(e => e.LastViewedAt)
				.HasColumnType("dt_integer(8)")
				.HasColumnName("last_viewed_at");
			builder.Property(e => e.Rating)
				.HasColumnType("float")
				.HasColumnName("rating");
			builder.Property(e => e.SkipCount)
				.HasDefaultValueSql("0")
				.HasColumnName("skip_count");
			builder.Property(e => e.UpdatedAt)
				.HasColumnType("dt_integer(8)")
				.HasColumnName("updated_at");
			builder.Property(e => e.ViewCount).HasColumnName("view_count");
			builder.Property(e => e.ViewOffset).HasColumnName("view_offset");
		}
	}
}
