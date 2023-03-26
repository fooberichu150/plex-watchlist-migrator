using System.Diagnostics.CodeAnalysis;
using PlexWatchlistMigrator.Domain;

namespace PlexWatchlistMigrator.Engines.Comparers
{
	public class AccountEqualityComparer : IEqualityComparer<Account>
	{
		private static AccountEqualityComparer s_instance;

		static AccountEqualityComparer()
		{
			s_instance = new AccountEqualityComparer();
		}

		public static AccountEqualityComparer Instance => s_instance;

		public bool Equals(Account? x, Account? y)
		{
			if (x == null && y == null)
				return false;

			if ((x is null && y is not null) || y is null && x is null)
				return false;

			return x.Id == y.Id;
		}

		public int GetHashCode([DisallowNull] Account obj)
		{
			if (obj is null)
				return -1;

			return obj.Id.GetHashCode();
		}
	}
}
