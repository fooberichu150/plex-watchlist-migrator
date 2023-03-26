using AutoMapper;
using Entities = PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;
using DomainModels = PlexWatchlistMigrator.Domain;
using PlexWatchlistMigrator.Infrastructure.MappingProfiles.Converters;

namespace PlexWatchlistMigrator.Infrastructure.MappingProfiles
{
	public class AccountProfile : Profile
	{
		public AccountProfile()
		{
			CreateMap<Entities.Account, DomainModels.Account>()
				.ReverseMap()
				.ForMember(dest => dest.MetadataItemViews, opts => opts.Ignore());
		}
	}

	public class LibrarySectionProfile : Profile
	{
		public LibrarySectionProfile()
		{
			CreateMap<Entities.LibrarySection, DomainModels.LibrarySection>()
				.ForMember(dest => dest.CreatedAt, opts => opts.ConvertUsing<LongToDateTimeConverter, long?>())
				.ForMember(dest => dest.UpdatedAt, opts => opts.ConvertUsing<LongToDateTimeConverter, long?>())
				.ForMember(dest => dest.ScannedAt, opts => opts.ConvertUsing<LongToDateTimeConverter, long?>())
				.ReverseMap()
				.ForMember(dest => dest.MetadataItemViews, opts => opts.Ignore())
				.ForMember(dest => dest.MetadataItems, opts => opts.Ignore());
		}
	}

	public class MetadataItemProfile : Profile
	{
		public MetadataItemProfile()
		{
			CreateMap<Entities.MetadataItem, DomainModels.MetadataItemSimple>()
				.ForMember(dest => dest.AddedAt, opts => opts.ConvertUsing<LongToDateTimeConverter, long?>())
				.ForMember(dest => dest.CreatedAt, opts => opts.ConvertUsing<LongToDateTimeConverter, long?>())
				.ReverseMap()
				.ForMember(dest => dest.LibrarySection, opts => opts.Ignore())
				.ForMember(dest => dest.MetadataItemViews, opts => opts.Ignore());
		}
	}

	public class MetadataItemSettingProfile : Profile
	{
		public MetadataItemSettingProfile()
		{
			CreateMap<Entities.MetadataItemSetting, DomainModels.MetadataItemSetting>()
				.ForMember(dest => dest.ChangedAt, opts => opts.ConvertUsing<LongToDateTimeConverter, long?>())
				.ForMember(dest => dest.CreatedAt, opts => opts.ConvertUsing<LongToDateTimeConverter, long?>())
				.ForMember(dest => dest.LastRatedAt, opts => opts.ConvertUsing<LongToDateTimeConverter, long?>())
				.ForMember(dest => dest.LastSkippedAt, opts => opts.ConvertUsing<LongToDateTimeConverter, long?>())
				.ForMember(dest => dest.LastViewedAt, opts => opts.ConvertUsing<LongToDateTimeConverter, long?>())
				.ForMember(dest => dest.UpdatedAt, opts => opts.ConvertUsing<LongToDateTimeConverter, long?>())
				.ReverseMap();
		}
	}

	public class MetadataItemViewProfile : Profile
	{
		public MetadataItemViewProfile()
		{
			CreateMap<Entities.MetadataItemView, DomainModels.MediaItemUserView>()
				.ForMember(dest => dest.OriginallyAvailableAt, opts => opts.ConvertUsing<LongToDateTimeConverter, long?>())
				.ForMember(dest => dest.ViewedAt, opts => opts.ConvertUsing<LongToDateTimeConverter, long?>());
		}
	}
}
