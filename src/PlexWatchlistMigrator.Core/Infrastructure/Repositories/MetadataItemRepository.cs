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
	public interface IMetadataItemRepository : IRepository<MetadataItem>
	{
		IEnumerable<DomainModels.MetadataItemSimple> GetAllSimple();
	}

	public class MetadataItemRepository : RepositoryBase, IMetadataItemRepository
	{
		public MetadataItemRepository(
			IMapper mapper)
		{
			Mapper = mapper;
		}

		private IMapper Mapper { get; }

		public IEnumerable<DomainModels.MetadataItemSimple> GetAllSimple()
		{
			return null;
		}
	}
}
