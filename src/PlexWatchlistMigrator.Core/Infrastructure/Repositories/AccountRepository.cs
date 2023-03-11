using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PlexWatchlistMigrator.Infrastructure.ContextSqlite;
using PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;
using DomainModels = PlexWatchlistMigrator.Domain;

namespace PlexWatchlistMigrator.Infrastructure.Repositories
{
	public interface IAccountRepository : IRepository<Account>
	{
		IEnumerable<DomainModels.Account> GetAll();
	}

	public class AccountRepository : RepositoryBase, IAccountRepository
	{
		public AccountRepository(
			IMapper mapper)
		{
			Mapper = mapper;
		}

		private IMapper Mapper { get; }

		public IEnumerable<DomainModels.Account> GetAll()
		{
			return null;
		}
	}
}
