using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;
using DomainModels = PlexWatchlistMigrator.Domain;

namespace PlexWatchlistMigrator.Infrastructure.Repositories
{
	public interface IMetadataItemSettingRepository : IRepository<MetadataItemSetting>
	{
		Task<IEnumerable<DomainModels.MetadataItemSetting>> GetAllAsync();
	}

	public class MetadataItemSettingRepository : RepositoryBase, IMetadataItemSettingRepository
	{
		public MetadataItemSettingRepository(
			IMapper mapper)
		{
			Mapper = mapper;
		}

		private IMapper Mapper { get; }

		public async Task<IEnumerable<DomainModels.MetadataItemSetting>> GetAllAsync()
		{
			//             'select account_id,guid,rating,view_offset,view_count,last_viewed_at,created_at,'
			//             'skip_count,last_skipped_at,changed_at,extra_data '
			//             'from metadata_item_settings '
			//             'where account_id = ?', (user["id"],))

			var settings = await DbContext.MetadataItemSettings
				.ToArrayAsync();

			return Mapper.Map<DomainModels.MetadataItemSetting[]>(settings);
		}
	}
}
