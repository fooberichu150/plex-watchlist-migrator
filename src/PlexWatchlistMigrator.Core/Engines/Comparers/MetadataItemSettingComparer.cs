using System.Diagnostics.CodeAnalysis;
using PlexWatchlistMigrator.Domain;

namespace PlexWatchlistMigrator.Engines.Comparers
{
	public class MetadataItemSettingComparer : IEqualityComparer<MetadataItemSetting>
	{
		private static MetadataItemSettingComparer s_instance;

		static MetadataItemSettingComparer()
		{
			s_instance = new MetadataItemSettingComparer();
		}

		public static MetadataItemSettingComparer Instance => s_instance;

		public bool Equals(MetadataItemSetting? x, MetadataItemSetting? y)
		{
			if (x == null && y == null)
				return false;

			if (x is null && y is not null || y is null && x is null)
				return false;

			if (x.AccountId != y.AccountId)
				return false;

			return string.Compare(x.Guid, y.Guid, StringComparison.OrdinalIgnoreCase) == 0;
		}

		public int GetHashCode([DisallowNull] MetadataItemSetting obj)
		{
			if (obj is null)
				return -1;

			return obj.AccountId.GetHashCode()
				+ obj.Guid.GetHashCode();
		}
	}
}
