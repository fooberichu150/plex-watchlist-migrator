using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;
using DomainModels = PlexWatchlistMigrator.Domain;

namespace PlexWatchlistMigrator.Infrastructure.Repositories
{
	public interface IAccountRepository : IRepository<Account>
	{
		Task<IEnumerable<DomainModels.Account>> GetAllAsync();
	}

	public class AccountRepository : RepositoryBase, IAccountRepository
	{
		public AccountRepository(
			IMapper mapper)
		{
			Mapper = mapper;
		}

		private IMapper Mapper { get; }

		public async Task<IEnumerable<DomainModels.Account>> GetAllAsync()
		{
			// 'select id,name,hashed_password,salt,created_at from accounts where id != 0 and id < 1000'
			var users = await DbContext.Accounts
				.ToArrayAsync();

			return Mapper.Map<DomainModels.Account[]>(users);
		}
	}
}
