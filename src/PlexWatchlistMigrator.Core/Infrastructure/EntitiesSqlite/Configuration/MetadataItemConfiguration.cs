using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlexWatchlistMigrator.Infrastructure.EntitiesSqlite.Configuration
{
	public class MetadataItemConfiguration : IEntityTypeConfiguration<MetadataItem>
	{
		public void Configure(EntityTypeBuilder<MetadataItem> builder)
		{
			builder.ToTable("metadata_items");

			builder.HasIndex(e => e.AbsoluteIndex, "index_metadata_items_on_absolute_index");

			builder.HasIndex(e => e.AddedAt, "index_metadata_items_on_added_at");

			builder.HasIndex(e => e.ChangedAt, "index_metadata_items_on_changed_at");

			builder.HasIndex(e => e.CreatedAt, "index_metadata_items_on_created_at");

			builder.HasIndex(e => e.DeletedAt, "index_metadata_items_on_deleted_at");

			builder.HasIndex(e => e.EditionTitle, "index_metadata_items_on_edition_title");

			builder.HasIndex(e => e.Guid, "index_metadata_items_on_guid");

			builder.HasIndex(e => e.Hash, "index_metadata_items_on_hash");

			builder.HasIndex(e => e.Index, "index_metadata_items_on_index");

			builder.HasIndex(e => e.LibrarySectionId, "index_metadata_items_on_library_section_id");

			builder.HasIndex(e => new { e.LibrarySectionId, e.MetadataType, e.AddedAt }, "index_metadata_items_on_library_section_id_and_metadata_type_and_added_at");

			builder.HasIndex(e => e.MetadataType, "index_metadata_items_on_metadata_type");

			builder.HasIndex(e => e.OriginalTitle, "index_metadata_items_on_original_title");

			builder.HasIndex(e => e.OriginallyAvailableAt, "index_metadata_items_on_originally_available_at");

			builder.HasIndex(e => e.ParentId, "index_metadata_items_on_parent_id");

			builder.HasIndex(e => e.Remote, "index_metadata_items_on_remote");

			builder.HasIndex(e => e.ResourcesChangedAt, "index_metadata_items_on_resources_changed_at");

			builder.HasIndex(e => e.Title, "index_metadata_items_on_title");

			builder.HasIndex(e => e.TitleSort, "index_metadata_items_on_title_sort");

			builder.HasIndex(e => e.TitleSort, "index_title_sort_icu");

			builder.Property(e => e.Id).HasColumnName("id");
			builder.Property(e => e.AbsoluteIndex).HasColumnName("absolute_index");
			builder.Property(e => e.AddedAt)
				.HasColumnType("dt_integer(8)")
				.HasColumnName("added_at");
			builder.Property(e => e.AudienceRating)
				.HasColumnType("float")
				.HasColumnName("audience_rating");
			builder.Property(e => e.AvailableAt)
				.HasColumnType("dt_integer(8)")
				.HasColumnName("available_at");
			builder.Property(e => e.ChangedAt)
				.HasDefaultValueSql("0")
				.HasColumnType("integer(8)")
				.HasColumnName("changed_at");
			builder.Property(e => e.ContentRating)
				.HasColumnType("varchar(255)")
				.HasColumnName("content_rating");
			builder.Property(e => e.ContentRatingAge).HasColumnName("content_rating_age");
			builder.Property(e => e.CreatedAt)
				.HasColumnType("dt_integer(8)")
				.HasColumnName("created_at");
			builder.Property(e => e.DeletedAt)
				.HasColumnType("dt_integer(8)")
				.HasColumnName("deleted_at");
			builder.Property(e => e.Duration).HasColumnName("duration");
			builder.Property(e => e.EditionTitle)
				.HasColumnType("varchar(255)")
				.HasColumnName("edition_title");
			builder.Property(e => e.ExpiresAt)
				.HasColumnType("dt_integer(8)")
				.HasColumnName("expires_at");
			builder.Property(e => e.ExtraData)
				.HasColumnType("varchar(255)")
				.HasColumnName("extra_data");
			builder.Property(e => e.Guid)
				.HasColumnType("varchar(255)")
				.HasColumnName("guid");
			builder.Property(e => e.Hash)
				.HasColumnType("varchar(255)")
				.HasColumnName("hash");
			builder.Property(e => e.Index).HasColumnName("index");
			builder.Property(e => e.LibrarySectionId).HasColumnName("library_section_id");
			builder.Property(e => e.MediaItemCount).HasColumnName("media_item_count");
			builder.Property(e => e.MetadataType).HasColumnName("metadata_type");
			builder.Property(e => e.OriginalTitle)
				.HasColumnType("varchar(255)")
				.HasColumnName("original_title");
			builder.Property(e => e.OriginallyAvailableAt)
				.HasColumnType("dt_integer(8)")
				.HasColumnName("originally_available_at");
			builder.Property(e => e.ParentId).HasColumnName("parent_id");
			builder.Property(e => e.Quotes).HasColumnName("quotes");
			builder.Property(e => e.Rating)
				.HasColumnType("float")
				.HasColumnName("rating");
			builder.Property(e => e.RatingCount).HasColumnName("rating_count");
			builder.Property(e => e.RefreshedAt)
				.HasColumnType("dt_integer(8)")
				.HasColumnName("refreshed_at");
			builder.Property(e => e.Remote).HasColumnName("remote");
			builder.Property(e => e.ResourcesChangedAt)
				.HasDefaultValueSql("0")
				.HasColumnType("integer(8)")
				.HasColumnName("resources_changed_at");
			builder.Property(e => e.Studio)
				.HasColumnType("varchar(255)")
				.HasColumnName("studio");
			builder.Property(e => e.Summary).HasColumnName("summary");
			builder.Property(e => e.Tagline)
				.HasColumnType("varchar(255)")
				.HasColumnName("tagline");
			builder.Property(e => e.TagsCollection)
				.HasColumnType("varchar(255)")
				.HasColumnName("tags_collection");
			builder.Property(e => e.TagsCountry)
				.HasColumnType("varchar(255)")
				.HasColumnName("tags_country");
			builder.Property(e => e.TagsDirector)
				.HasColumnType("varchar(255)")
				.HasColumnName("tags_director");
			builder.Property(e => e.TagsGenre)
				.HasColumnType("varchar(255)")
				.HasColumnName("tags_genre");
			builder.Property(e => e.TagsStar)
				.HasColumnType("varchar(255)")
				.HasColumnName("tags_star");
			builder.Property(e => e.TagsWriter)
				.HasColumnType("varchar(255)")
				.HasColumnName("tags_writer");
			builder.Property(e => e.Title)
				.HasColumnType("varchar(255)")
				.HasColumnName("title");
			builder.Property(e => e.TitleSort)
				.HasColumnType("varchar(255)")
				.HasColumnName("title_sort");
			builder.Property(e => e.Trivia).HasColumnName("trivia");
			builder.Property(e => e.UpdatedAt)
				.HasColumnType("dt_integer(8)")
				.HasColumnName("updated_at");
			builder.Property(e => e.UserArtUrl)
				.HasColumnType("varchar(255)")
				.HasColumnName("user_art_url");
			builder.Property(e => e.UserBannerUrl)
				.HasColumnType("varchar(255)")
				.HasColumnName("user_banner_url");
			builder.Property(e => e.UserFields)
				.HasColumnType("varchar(255)")
				.HasColumnName("user_fields");
			builder.Property(e => e.UserMusicUrl)
				.HasColumnType("varchar(255)")
				.HasColumnName("user_music_url");
			builder.Property(e => e.UserThumbUrl)
				.HasColumnType("varchar(255)")
				.HasColumnName("user_thumb_url");
			builder.Property(e => e.Year).HasColumnName("year");

			builder.HasOne(e => e.LibrarySection)
				.WithMany(e => e.MetadataItems)
				.HasForeignKey(e => e.LibrarySectionId);
		}
	}
}
