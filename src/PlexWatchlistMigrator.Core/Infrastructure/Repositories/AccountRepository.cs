using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DomainModels = PlexWatchlistMigrator.Domain;
using Entities = PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;

namespace PlexWatchlistMigrator.Infrastructure.Repositories
{
	public interface IAccountRepository : IRepository<Entities.Account>
	{
		Task<IEnumerable<DomainModels.Account>> GetAllAsync();
		Task<int> AddAccountsAsync(DomainModels.Account[] users);
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
			var users = await DbContext.Accounts
				.Where(user => user.Id > 0)
				.ToArrayAsync();

			return Mapper.Map<DomainModels.Account[]>(users);
		}

		public async Task<int> AddAccountsAsync(params DomainModels.Account[] users)
		{
			var newAccounts = Mapper.Map<Entities.Account[]>(users);
			await DbContext.Accounts.AddRangeAsync(newAccounts);

			var updatedRows = await DbContext.SaveChangesAsync();
			return updatedRows;
		}
	}
}
