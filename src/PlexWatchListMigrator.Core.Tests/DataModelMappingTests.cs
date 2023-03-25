using AutoMapper;
using PlexWatchlistMigrator.Infrastructure.MappingProfiles;

namespace PlexWatchlistMigrator.Tests
{
	public class DataModelMappingTests
	{
        public DataModelMappingTests()
        {
			MappingConfig = new MapperConfiguration(options =>
			{
				// scans and adds all maps in the assembly
				options.AddMaps(typeof(AccountProfile));
			});

		}

		private MapperConfiguration MappingConfig { get; }

		[Fact]
		public void Validate_Profiles_Are_Configured_Correctly()
		{
			MappingConfig.AssertConfigurationIsValid();
		}

		[Fact]
		public void Validate_Something()
		{
			CustomAssert.Fail("Not Yet Implemented");
		}
	}
}