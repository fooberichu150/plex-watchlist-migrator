using AutoMapper;
using Entities = PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;
using DomainModels = PlexWatchlistMigrator.Domain;

namespace PlexWatchlistMigrator.Infrastructure.MappingProfiles
{
	public class AccountProfile : Profile
	{
		public AccountProfile()
		{
			CreateMap<DomainModels.Account, Entities.Account>()
				.ReverseMap();
		}
	}

	public class LibrarySectionProfile : Profile
	{
		public LibrarySectionProfile()
		{
			CreateMap<DomainModels.LibrarySection, Entities.LibrarySection>()
				.ReverseMap();
		}
	}

	public class MetadataItemProfile : Profile
	{
		public MetadataItemProfile()
		{
			//CreateMap<DomainModels.MetadataItemSimple, Entities.Account>()
			//	.ReverseMap();
		}
	}

	public class MetadataItemSettingProfile : Profile
	{
		public MetadataItemSettingProfile()
		{
			CreateMap<DomainModels.MetadataItemSetting, Entities.MetadataItemSetting>()
				.ReverseMap();
		}
	}

	public class MetadataItemViewProfile : Profile
	{
		public MetadataItemViewProfile()
		{

		}
	}
}
