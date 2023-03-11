using PlexWatchlistMigrator.Domain;
using PlexWatchlistMigrator.Infrastructure.EntitiesSqlite;

namespace PlexWatchlistMigrator.Infrastructure.Adapters
{
	public interface IMetadataItemViewAdapter
	{
		MediaItemUserView ToDomain(MetadataItemView entity);
		MetadataItemView ToEntity(MediaItemUserView model);
	}

	public class MetadataItemViewAdapter : IMetadataItemViewAdapter
	{
		public MediaItemUserView ToDomain(MetadataItemView entity)
		{
			var model = new MediaItemUserView
			{
				AccountId = entity.AccountId,
				Guid = entity.MetadataItem.Guid,
				MetadataType = entity.MetadataType,
				LibrarySectionId = entity.LibrarySectionId,
				GrandParentTitle = entity.GrandparentTitle,
				ParentIndex = entity.ParentIndex,
				ParentTitle = entity.ParentTitle,
				Index = entity.Index,
				Title = entity.Title,
				ThumbUrl = entity.ThumbUrl,
				ViewedAt = entity.ViewedAt,
				GrandParentGuid = entity.GrandparentGuid,
				OriginallyAvailableAt = entity.OriginallyAvailableAt
			};

			return model;
		}

		public MetadataItemView ToEntity(MediaItemUserView model)
		{
			return null;
		}
	}
}
