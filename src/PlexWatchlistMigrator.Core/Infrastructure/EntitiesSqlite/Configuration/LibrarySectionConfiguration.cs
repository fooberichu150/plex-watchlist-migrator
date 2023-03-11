using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlexWatchlistMigrator.Infrastructure.EntitiesSqlite.Configuration
{
	public class LibrarySectionConfiguration : IEntityTypeConfiguration<LibrarySection>
	{
		public void Configure(EntityTypeBuilder<LibrarySection> builder)
		{
			builder.ToTable("library_sections");

			builder.HasIndex(e => e.ChangedAt, "index_library_sections_on_changed_at");

			builder.HasIndex(e => e.Name, "index_library_sections_on_name");

			builder.HasIndex(e => e.NameSort, "index_library_sections_on_name_sort");

			builder.Property(e => e.Id).HasColumnName("id");
			builder.Property(e => e.Agent)
				.HasColumnType("varchar(255)")
				.HasColumnName("agent");
			builder.Property(e => e.ChangedAt)
				.HasDefaultValueSql("0")
				.HasColumnType("integer(8)")
				.HasColumnName("changed_at");
			builder.Property(e => e.ContentChangedAt)
				.HasDefaultValueSql("'0'")
				.HasColumnType("integer(8)")
				.HasColumnName("content_changed_at");
			builder.Property(e => e.CreatedAt)
				.HasColumnType("datetime")
				.HasColumnName("created_at");
			builder.Property(e => e.DisplaySecondaryLevel)
				.HasColumnType("boolean")
				.HasColumnName("display_secondary_level");
			builder.Property(e => e.Language)
				.HasColumnType("varchar(255)")
				.HasColumnName("language");
			builder.Property(e => e.LibraryId).HasColumnName("library_id");
			builder.Property(e => e.Name)
				.HasColumnType("varchar(255)")
				.HasColumnName("name");
			builder.Property(e => e.NameSort)
				.HasColumnType("varchar(255)")
				.HasColumnName("name_sort");
			builder.Property(e => e.Public)
				.HasColumnType("boolean")
				.HasColumnName("public");
			builder.Property(e => e.QueryType).HasColumnName("query_type");
			builder.Property(e => e.QueryXml).HasColumnName("query_xml");
			builder.Property(e => e.ScannedAt)
				.HasColumnType("datetime")
				.HasColumnName("scanned_at");
			builder.Property(e => e.Scanner)
				.HasColumnType("varchar(255)")
				.HasColumnName("scanner");
			builder.Property(e => e.SectionType).HasColumnName("section_type");
			builder.Property(e => e.UpdatedAt)
				.HasColumnType("datetime")
				.HasColumnName("updated_at");
			builder.Property(e => e.UserArtUrl)
				.HasColumnType("varchar(255)")
				.HasColumnName("user_art_url");
			builder.Property(e => e.UserFields)
				.HasColumnType("varchar(255)")
				.HasColumnName("user_fields");
			builder.Property(e => e.UserThemeMusicUrl)
				.HasColumnType("varchar(255)")
				.HasColumnName("user_theme_music_url");
			builder.Property(e => e.UserThumbUrl)
				.HasColumnType("varchar(255)")
				.HasColumnName("user_thumb_url");
			builder.Property(e => e.Uuid)
				.HasColumnType("varchar(255)")
				.HasColumnName("uuid");
		}
	}
}
