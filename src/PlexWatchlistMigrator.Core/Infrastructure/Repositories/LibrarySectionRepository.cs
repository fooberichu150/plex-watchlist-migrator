using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;
using DomainModels = PlexWatchlistMigrator.Domain;

namespace PlexWatchlistMigrator.Infrastructure.Repositories
{
	public interface ILibrarySectionRepository : IRepository<LibrarySection>
	{
		Task<IEnumerable<DomainModels.LibrarySection>> GetAllAsync();
	}

	public class LibrarySectionRepository : RepositoryBase, ILibrarySectionRepository
	{
		public LibrarySectionRepository(
			IMapper mapper)
		{
			Mapper = mapper;
		}

		private IMapper Mapper { get; }

		public async Task<IEnumerable<DomainModels.LibrarySection>> GetAllAsync()
		{
			var librarySections = await DbContext.LibrarySections
				.Where(lib => DomainModels.Constants.ValidSectionTypes.Contains(lib.SectionType))
				.ToArrayAsync();

			return Mapper.Map<DomainModels.LibrarySection[]>(librarySections);
		}
	}
}
