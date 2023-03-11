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
	public interface ILibrarySectionRepository : IRepository<LibrarySection>
	{
		IEnumerable<DomainModels.LibrarySection> GetAll();
	}

	public class LibrarySectionRepository : RepositoryBase, ILibrarySectionRepository
	{
		public LibrarySectionRepository(
			IMapper mapper)
		{
			Mapper = mapper;
		}

		private IMapper Mapper { get; }

		public IEnumerable<DomainModels.LibrarySection> GetAll()
		{
			return null;
		}
	}
}
