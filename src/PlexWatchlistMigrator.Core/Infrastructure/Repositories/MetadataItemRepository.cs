using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;
using DomainModels = PlexWatchlistMigrator.Domain;

namespace PlexWatchlistMigrator.Infrastructure.Repositories
{
	public interface IMetadataItemRepository : IRepository<MetadataItem>
	{
		Task<IEnumerable<DomainModels.MetadataItemSimple>> GetAllSimpleAsync();
	}

	public class MetadataItemRepository : RepositoryBase, IMetadataItemRepository
	{
		public MetadataItemRepository(
			IMapper mapper)
		{
			Mapper = mapper;
		}

		private IMapper Mapper { get; }

		public async Task<IEnumerable<DomainModels.MetadataItemSimple>> GetAllSimpleAsync()
		{
			//'select guid,added_at,metadata_items.created_at from metadata_items '
			//'inner join library_sections on library_sections.id = metadata_items.library_section_id '
			//'where library_sections.section_type in (1,2)').fetchall()

			var metaDates = await DbContext.MetadataItems
				.Where(mi => DomainModels.Constants.ValidSectionTypes.Contains(mi.LibrarySection.SectionType))
				.ToArrayAsync();

			return Mapper.Map<DomainModels.MetadataItemSimple[]>(metaDates);
		}
	}
}
