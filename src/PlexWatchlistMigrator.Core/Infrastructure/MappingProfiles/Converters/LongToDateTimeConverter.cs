using AutoMapper;
using Microsoft.Extensions.Logging;

namespace PlexWatchlistMigrator.Infrastructure.MappingProfiles.Converters
{
	public class LongToDateTimeConverter : IValueConverter<long?, DateTime?>
	{
		public LongToDateTimeConverter(ILogger<LongToDateTimeConverter> logger = null)
		{
			Logger = logger;
		}

		private ILogger<LongToDateTimeConverter> Logger { get; }

		public DateTime? Convert(long? sourceMember, ResolutionContext context)
		{
			if (!sourceMember.HasValue)
				return new DateTime?();

			if (sourceMember.Value < 0)
				return new DateTime?();

			return new DateTime(sourceMember.Value);
		}
	}
}
