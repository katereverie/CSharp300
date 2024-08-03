using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services
{
    public interface IMediaService
    {
        Result<List<Media>> GetMediaByType(int typeId);
        Result<List<Media>> GetAllArchivedMedia();
        Result<List<Media>> GetUnarchivedMediaByType(int typeID);
        Result<List<MediaType>> GetAllMediaTypes();
        Result<List<Top3Media>> GetTop3MostPopularMedia();
        Result<Media> GetMediaByID(int mediaID);
        Result<int> AddMedia(Media newMedia);
        Result ArchiveMedia(int mediaID);
        Result EditMedia(Media request);
    }
}
