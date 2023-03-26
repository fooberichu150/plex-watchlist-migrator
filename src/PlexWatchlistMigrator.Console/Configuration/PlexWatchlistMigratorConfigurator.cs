using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlexWatchlistMigrator.Engines.Handlers;
using PlexWatchlistMigrator.Infrastructure;
using PlexWatchlistMigrator.Infrastructure.Adapters;
using PlexWatchlistMigrator.Infrastructure.Repositories;

namespace PlexWatchlistMigrator.ConsoleApp.Configuration
{
	internal static class PlexWatchlistMigratorConfigurator
	{
		public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, HostBuilderContext hostBuilder)
		{
			var configuration = hostBuilder.Configuration;

			services.AddSingleton<Application>();
			services.AddScoped<ILocalDbContextFactory, LocalDbContextFactory>();

			services.AddScoped<IDataLoadHandlerFactory, DataLoadHandlerFactory>();
			services.AddScoped<IMigrateViewDataActionHandler, MigrateViewDataActionHandler>();
			services.AddScoped<IMetadataItemViewAdapter, MetadataItemViewAdapter>();

			// data load handlers...
			services.AddTransient<IDataLoadHandler, DataLoadHandler>();

			// repositories
			services.AddScoped<IRepositoryFactory, RepositoryFactory>();
			services.AddScoped<IAccountRepository, AccountRepository>();
			services.AddScoped<ILibrarySectionRepository, LibrarySectionRepository>();
			services.AddScoped<IMetadataItemRepository, MetadataItemRepository>();
			services.AddScoped<IMetadataItemSettingRepository, MetadataItemSettingRepository>();
			services.AddScoped<IMetadataItemViewRepository, MetadataItemViewRepository>();

			services
				.WireupAutoMapper();
			//services.AddDbContext<Infrastructure.ContextSqlite.PlexMigratorContext>(options =>
			//{
			//	var connectionString = configuration.GetConnectionString("");

			//	options.UseSqlite(connectionString, opt => { });
			//});

			return services;
		}

		private static IServiceCollection WireupAutoMapper(this IServiceCollection services)
		{
			List<Type> scanTypes = new List<Type>(new[] { typeof(Infrastructure.MappingProfiles.AccountProfile) });
			services.AddAutoMapper(cfg => { }, scanTypes);

			return services;
		}

		//public static IMapperConfigurationExpression WireupAutoMapper(this IMapperConfigurationExpression mapperConfig)
		//{
		//	mapperConfig.AddProfile<Infrastructure.Data.SqlRepository.Profiles.EntityMappingProfile>();

		//	return mapperConfig;
		//}

	}
}
