using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;
using DomainModels = PlexWatchlistMigrator.Domain;

namespace PlexWatchlistMigrator.Infrastructure.Repositories
{
	public interface IMetadataItemViewRepository : IRepository<MetadataItemView>
	{
		Task<IEnumerable<DomainModels.MediaItemUserView>> GetUserViewsAsync();
	}

	public class MetadataItemViewRepository : RepositoryBase, IMetadataItemViewRepository
	{
		public MetadataItemViewRepository(
			IMapper mapper)
		{
			Mapper = mapper;
		}

		private IMapper Mapper { get; }

		public async Task<IEnumerable<DomainModels.MediaItemUserView>> GetUserViewsAsync()
		{
			//            "select account_id,metadata_item_views.guid,metadata_item_views.metadata_type,metadata_item_views.library_section_id,grandparent_title,"
			//            "parent_index,parent_title,'index',metadata_item_views.title,thumb_url,viewed_at,grandparent_guid,metadata_item_views.originally_available_at "
			//            "from metadata_item_views "
			//            "inner join library_sections on library_sections.id = metadata_item_views.library_section_id "
			//            "inner join metadata_items on metadata_items.guid = metadata_item_views.guid "
			//            "where account_id=? and library_sections.section_type in (1,2)", (user["id"],))

			var views = await DbContext.MetadataItemViews
				.Where(view => DomainModels.Constants.ValidSectionTypes.Contains(view.LibrarySectionId) 
					&& view.MetadataItem != null)
				.ToArrayAsync();

			return Mapper.Map<DomainModels.MediaItemUserView[]>(views);
		}
	}
}
