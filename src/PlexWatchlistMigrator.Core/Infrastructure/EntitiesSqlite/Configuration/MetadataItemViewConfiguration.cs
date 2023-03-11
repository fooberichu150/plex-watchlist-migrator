using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlexWatchlistMigrator.Infrastructure.EntitiesSqlite.Configuration
{
	public class MetadataItemViewConfiguration : IEntityTypeConfiguration<MetadataItemView>
	{
		public void Configure(EntityTypeBuilder<MetadataItemView> builder)
		{
			builder.ToTable("metadata_item_views");

			builder.HasIndex(e => e.GrandparentGuid, "index_metadata_item_views_on_grandparent_guid");

			builder.HasIndex(e => e.Guid, "index_metadata_item_views_on_guid");

			builder.HasIndex(e => e.LibrarySectionId, "index_metadata_item_views_on_library_section_id");

			builder.HasIndex(e => e.ViewedAt, "index_metadata_item_views_on_viewed_at");

			builder.Property(e => e.Id).HasColumnName("id");
			builder.Property(e => e.AccountId).HasColumnName("account_id");
			builder.Property(e => e.DeviceId).HasColumnName("device_id");
			builder.Property(e => e.GrandparentGuid)
				.HasColumnType("varchar(255)")
				.HasColumnName("grandparent_guid");
			builder.Property(e => e.GrandparentTitle)
				.HasColumnType("varchar(255)")
				.HasColumnName("grandparent_title");
			builder.Property(e => e.Guid)
				.HasColumnType("varchar(255)")
				.HasColumnName("guid");
			builder.Property(e => e.Index).HasColumnName("index");
			builder.Property(e => e.LibrarySectionId).HasColumnName("library_section_id");
			builder.Property(e => e.MetadataType).HasColumnName("metadata_type");
			builder.Property(e => e.OriginallyAvailableAt)
				.HasColumnType("dt_integer(8)")
				.HasColumnName("originally_available_at");
			builder.Property(e => e.ParentIndex).HasColumnName("parent_index");
			builder.Property(e => e.ParentTitle)
				.HasColumnType("varchar(255)")
				.HasColumnName("parent_title");
			builder.Property(e => e.ThumbUrl)
				.HasColumnType("varchar(255)")
				.HasColumnName("thumb_url");
			builder.Property(e => e.Title)
				.HasColumnType("varchar(255)")
				.HasColumnName("title");
			builder.Property(e => e.ViewedAt)
				.HasColumnType("dt_integer(8)")
				.HasColumnName("viewed_at");

			builder.HasOne(e => e.Account)
				.WithMany(e => e.MetadataItemViews)
				.HasForeignKey(e => e.AccountId);

			builder.HasOne(e => e.LibrarySection)
				.WithMany(e => e.MetadataItemViews)
				.HasForeignKey(e => e.LibrarySectionId);

			builder.HasOne(e => e.MetadataItem)
				.WithMany(e => e.MetadataItemViews)
				.HasForeignKey(e => e.Guid)
				.HasPrincipalKey(e => e.Guid);
		}
	}
}
