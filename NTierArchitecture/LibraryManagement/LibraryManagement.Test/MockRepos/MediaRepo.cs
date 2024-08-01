using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;

namespace LibraryManagement.Test.MockRepos
{
    public class MediaRepo : IMediaRepository
    {
        private readonly IMediaRepository _mediaRepo;

        public MediaRepo(IMediaRepository mediaRepo)
        {
            _mediaRepo = mediaRepo;
        }

        public int Add(Media newMedia)
        {
            throw new NotImplementedException();
        }

        public bool Archive(int mediaID)
        {
            throw new NotImplementedException();
        }

        public List<Media> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Media> GetAllArchived()
        {
            throw new NotImplementedException();
        }

        public List<Media> GetAllUnarchived()
        {
            throw new NotImplementedException();
        }

        public Media? GetByID(int mediaId)
        {
            throw new NotImplementedException();
        }

        public List<Media> GetByType(int mediaTypeID)
        {
            throw new NotImplementedException();
        }

        public List<MediaCheckoutCount> GetTopThreeMostPopularMedia()
        {
            throw new NotImplementedException();
        }

        public List<Media> GetUnarchivedByType(int typeID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Media request)
        {
            throw new NotImplementedException();
        }
    }
}
