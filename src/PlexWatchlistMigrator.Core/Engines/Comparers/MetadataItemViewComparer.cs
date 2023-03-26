using System.Diagnostics.CodeAnalysis;
using PlexWatchlistMigrator.Domain;

namespace PlexWatchlistMigrator.Engines.Comparers
{
	public class MetadataItemViewComparer : IEqualityComparer<MediaItemUserView>
	{
		private static MetadataItemViewComparer s_instance;
		static MetadataItemViewComparer()
		{
			s_instance = new MetadataItemViewComparer();
		}

		public static MetadataItemViewComparer Instance => s_instance;

		public bool Equals(MediaItemUserView? x, MediaItemUserView? y)
		{
			if (x == null && y == null)
				return false;

			if (x is null && y is not null || y is null && x is null)
				return false;

			if (x.AccountId != y.AccountId)
				return false;

			if (x.ViewedAt != y.ViewedAt)
				return false;

			return string.Compare(x.Guid, y.Guid, StringComparison.OrdinalIgnoreCase) == 0;
		}

		public int GetHashCode([DisallowNull] MediaItemUserView obj)
		{
			if (obj is null)
				return -1;

			return obj.AccountId.GetHashCode()
				+ obj.Guid.GetHashCode()
				+ obj.GrandParentGuid?.GetHashCode() ?? 0
				+ obj.ViewedAt?.GetHashCode() ?? 0;
		}
	}
}
