using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repositories
{
    public interface IMediaRepository
    {
        int Add(Media newMedia);
        void Update(Media request);
        bool Archive(int mediaID);
        List<Media> GetAll();
        List<Media> GetAllUnarchived();
        List<Media> GetUnarchivedByType(int typeID);
        List<Media> GetAllArchived();
        List<Media> GetMediaByType(int mediaTypeID);
        List<MediaCheckoutCount> GetTopThreeMostPopularMedia();
        Media? GetMediaById(int mediaId);
    }
}
