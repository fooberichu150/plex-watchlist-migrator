using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlexWatchlistMigrator.Infrastructure.EntitiesSqlite.Configuration
{
	public class AccountConfiguration : IEntityTypeConfiguration<Account>
	{
		public void Configure(EntityTypeBuilder<Account> builder)
		{
			builder.ToTable("accounts");

			builder.HasIndex(e => e.Name, "index_accounts_on_name");

			builder.Property(e => e.Id).HasColumnName("id");
			builder.Property(e => e.AutoSelectAudio)
				.HasDefaultValueSql("'t'")
				.HasColumnType("boolean")
				.HasColumnName("auto_select_audio");
			builder.Property(e => e.AutoSelectSubtitle)
				.HasDefaultValueSql("'t'")
				.HasColumnType("boolean")
				.HasColumnName("auto_select_subtitle");
			builder.Property(e => e.CreatedAt)
				.HasColumnType("datetime")
				.HasColumnName("created_at");
			builder.Property(e => e.DefaultAudioLanguage)
				.HasColumnType("varchar(255)")
				.HasColumnName("default_audio_language");
			builder.Property(e => e.DefaultSubtitleLanguage)
				.HasColumnType("varchar(255)")
				.HasColumnName("default_subtitle_language");
			builder.Property(e => e.HashedPassword)
				.HasColumnType("varchar(255)")
				.HasColumnName("hashed_password");
			builder.Property(e => e.Name)
				.HasColumnType("varchar(255)")
				.HasColumnName("name");
			builder.Property(e => e.Salt)
				.HasColumnType("varchar(255)")
				.HasColumnName("salt");
			builder.Property(e => e.UpdatedAt)
				.HasColumnType("datetime")
				.HasColumnName("updated_at");
		}
	}
}
