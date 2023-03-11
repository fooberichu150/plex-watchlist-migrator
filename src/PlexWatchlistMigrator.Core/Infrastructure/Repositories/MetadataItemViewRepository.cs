﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;
using DomainModels = PlexWatchlistMigrator.Domain;

namespace PlexWatchlistMigrator.Infrastructure.Repositories
{
	public interface IMetadataItemViewRepository : IRepository<MetadataItemView>
	{
		IEnumerable<DomainModels.MediaItemUserView> GetUserViews();
	}

	public class MetadataItemViewRepository : RepositoryBase, IMetadataItemViewRepository
	{
		public MetadataItemViewRepository(
			IMapper mapper)
		{
			Mapper = mapper;
		}

		private IMapper Mapper { get; }

		public IEnumerable<DomainModels.MediaItemUserView> GetUserViews()
		{
			return null;
		}
	}
}
