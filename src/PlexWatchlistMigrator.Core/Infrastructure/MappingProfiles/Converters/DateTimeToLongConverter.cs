using AutoMapper;

namespace PlexWatchlistMigrator.Infrastructure.MappingProfiles.Converters
{
	public class DateTimeToLongConverter : IValueConverter<DateTime?, long?>
	{
		public long? Convert(DateTime? sourceMember, ResolutionContext context)
		{
			if (!sourceMember.HasValue)
				return new long?();

			return sourceMember.Value.Ticks;
		}
	}
}
