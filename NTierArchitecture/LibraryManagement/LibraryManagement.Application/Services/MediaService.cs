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
                return ResultFactory.Success(newID);
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
                var isArchived = _mediaRepo.Archive(mediaID);
                if (isArchived)
                {
                    return ResultFactory.Success();
                }
                else
                {
                    return ResultFactory.Fail("Archive attempt failed.");
                }
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
                return _mediaRepo.Update(request)
                    ? ResultFactory.Success()
                    : ResultFactory.Fail("Update attempt failed.");
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
                if (media != null)
                {
                    return ResultFactory.Success(media);
                }
                else
                {
                    return ResultFactory.Fail<Media>($"Media with ID {mediaID} not found");
                }
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

                if (list != null)
                {
                    return ResultFactory.Success(list);
                }
                else
                {
                    return ResultFactory.Fail<List<Media>>("Media of this type not found.");
                }
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }

        public Result<List<MediaCheckoutCount>> GetTop3MostPopularMedia()
        {
            try
            {
                var list = _mediaRepo.GetTopThreeMostPopularMedia();

                return list is null ?
                    ResultFactory.Fail<List<MediaCheckoutCount>>("Top 3 Most Popular Media not found.") :
                    ResultFactory.Success(list);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<MediaCheckoutCount>>(ex.Message);
            }
        }

        public Result<List<Media>> GetUnarchivedMediaByType(int typeID)
        {
            try
            {
                var list = _mediaRepo.GetUnarchivedByType(typeID);
                if (list != null)
                {
                    return ResultFactory.Success(list);
                }
                else
                {
                    return ResultFactory.Fail<List<Media>>("No unarchived media of this type is found.");
                }
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }
    }
}
