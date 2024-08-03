using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.Application.Services
{
    public class MediaService : IMediaService
    {
        private IMediaRepository _mediaRepo;

        public MediaService(IMediaRepository mediaRepository)
        {
            _mediaRepo = mediaRepository;
        }

        public Result<int> AddMedia(Media newMedia)
        {
            try
            {
                int newID = _mediaRepo.Add(newMedia);
                return newID != 0 && newID != -1
                    ? ResultFactory.Success(newID)
                    : ResultFactory.Fail<int>("Add attempt failed.");
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<int>(ex.Message);
            }
        }

        public Result ArchiveMedia(int mediaID)
        {
            try
            {
                _mediaRepo.Archive(mediaID);

                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result EditMedia(Media request)
        {
            try
            {
                _mediaRepo.Update(request);

                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<List<Media>> GetAllArchivedMedia()
        {
            try
            {
                var list = _mediaRepo.GetAllArchived();

                return list.Any() ?
                       ResultFactory.Success(list) :
                       ResultFactory.Fail<List<Media>>("No media is currently archived.");
                    
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }

        public Result<Media> GetMediaByID(int mediaID)
        {
            try
            {
                var media = _mediaRepo.GetByID(mediaID);

                return media != null
                    ? ResultFactory.Success(media)
                    : ResultFactory.Fail<Media>($"Media with ID {mediaID} not found");
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Media>(ex.Message);
            }
        }

        public Result<List<Media>> GetMediaByType(int typeId)
        {
            try
            {
                var list = _mediaRepo.GetByType(typeId);

                return list.Any()
                    ? ResultFactory.Success(list)
                    : ResultFactory.Fail<List<Media>>("Media of this type not found.");
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }

        public Result<List<Top3Media>> GetTop3MostPopularMedia()
        {
            try
            {
                var list = _mediaRepo.GetTopThreeMostPopularMedia();

                return list.Any()
                    ? ResultFactory.Success(list)
                    : ResultFactory.Fail<List<Top3Media>>("Top 3 Most Popular Media not found.");   
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Top3Media>>(ex.Message);
            }
        }

        public Result<List<Media>> GetUnarchivedMediaByType(int typeID)
        {
            try
            {
                var list = _mediaRepo.GetUnarchivedByType(typeID);

                return list.Any()
                    ? ResultFactory.Success(list)
                    : ResultFactory.Fail<List<Media>>("No unarchived media of this type is found.");
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }
    }
}
