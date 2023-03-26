using Microsoft.Extensions.DependencyInjection;
using PlexWatchlistMigrator.Infrastructure;

namespace PlexWatchlistMigrator.Engines.Handlers
{
	public interface IDataLoadHandlerFactory
	{
		IDataLoadHandler GetHandler(DbContextType dbContextType);
		IUpdateDataHandler GetUpdateHandler();
	}

	public class DataLoadHandlerFactory : IDataLoadHandlerFactory
	{
		public DataLoadHandlerFactory(
			IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider;
		}

		private IServiceProvider ServiceProvider { get; }

		public IDataLoadHandler GetHandler(DbContextType dbContextType)
		{
			var handler = ServiceProvider
				.GetRequiredService<IDataLoadHandler>()
				.Initialize(dbContextType);

			return handler;
		}

		public IUpdateDataHandler GetUpdateHandler()
		{
			var handler = ServiceProvider
				.GetRequiredService<IUpdateDataHandler>();

			return handler;
		}
	}
}
