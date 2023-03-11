using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;
using DomainModels = PlexWatchlistMigrator.Domain;

namespace PlexWatchlistMigrator.Infrastructure.Repositories
{
	public interface IMetadataItemSettingRepository : IRepository<MetadataItemSetting>
	{
		IEnumerable<DomainModels.MetadataItemSetting> GetAll();
	}

	public class MetadataItemSettingRepository : RepositoryBase, IMetadataItemSettingRepository
	{
		public MetadataItemSettingRepository(
			IMapper mapper)
		{
			Mapper = mapper;
		}

		private IMapper Mapper { get; }

		public IEnumerable<DomainModels.MetadataItemSetting> GetAll()
		{
			return null;
		}
	}
}
