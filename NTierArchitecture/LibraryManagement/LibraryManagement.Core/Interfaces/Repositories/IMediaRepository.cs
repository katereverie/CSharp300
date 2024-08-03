using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repositories
{
    public interface IMediaRepository
    {
        int Add(Media newMedia);
        void Update(Media request);
        void Archive(int mediaID);
        List<Media> GetAll();
        List<Media> GetAllUnarchived();
        List<Media> GetUnarchivedByType(int typeID);
        List<Media> GetAllArchived();
        List<Media> GetByType(int mediaTypeID);
        List<MediaType> GetAllMediaTypes();
        List<Top3Media> GetTopThreeMostPopularMedia();
        Media? GetByID(int mediaId);  
    }
}
