using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;

namespace LibraryManagement.Test.MockRepos
{
    public class MockMediaRepo : IMediaRepository
    {
        private List<Media> _medias = new List<Media>
        {
            new Media {
                MediaID = 1,
                MediaTypeID = 1,
                Title = "Book 1",
                IsArchived = false,
                MediaType = new MediaType
                {
                    MediaTypeID = 1,
                    MediaTypeName = "Book"
                },
                CheckoutLogs = new List<CheckoutLog>
                {
                    new CheckoutLog
                    {
                        CheckoutLogID = 1,
                        BorrowerID = 1,
                        MediaID = 1,
                        CheckoutDate = new DateTime(2024, 1, 1),
                        DueDate = new DateTime(2024, 1, 1).AddDays(7),
                        ReturnDate = null,
                    },
                }
            },
            new Media
            {
                MediaID = 2,
                MediaTypeID = 2,
                Title = "DVD 1",
                IsArchived = false,
                MediaType = new MediaType
                {
                    MediaTypeID = 2,
                    MediaTypeName = "DVD"
                },
                CheckoutLogs = new List<CheckoutLog>
                {
                    new CheckoutLog
                    {
                        CheckoutLogID = 2,
                        BorrowerID = 1,
                        MediaID = 2,
                        CheckoutDate = new DateTime(2024, 1, 1),
                        DueDate = new DateTime(2024, 1, 1).AddDays(7),
                        ReturnDate = null,
                    }
                }
            },
            new Media
            {
                MediaID= 3,
                MediaTypeID = 3,
                Title = "Digital Audio 1",
                IsArchived = false,
                MediaType = new MediaType
                {
                    MediaTypeID = 3,
                    MediaTypeName = "Digital Audio"
                },
                CheckoutLogs = new List<CheckoutLog>
                {
                    new CheckoutLog
                    {
                        CheckoutLogID= 3,
                        BorrowerID = 1,
                        MediaID = 3,
                        CheckoutDate = new DateTime(2024, 1, 1),
                        DueDate = new DateTime(2024, 1, 1).AddDays(7),
                        ReturnDate = null,
                    }
                }
            },
            new Media
            {
                MediaID = 4,
                MediaTypeID = 1,
                Title = "Book 2",
                IsArchived = false,
                MediaType = new MediaType
                {
                    MediaTypeID = 1,
                    MediaTypeName = "Book"
                },
                CheckoutLogs = null
            }
        };

        public int Add(Media newMedia)
        {
            _medias.Add(newMedia);
            return _medias.Last().MediaID;
        }

        public bool Archive(int mediaID)
        {
            var media = _medias.Find(m => m.MediaID == mediaID);
            if (media == null)
            {
                return false;
            }
            else
            {
                media.IsArchived = true;
                return Update(media);
            }
        }

        public List<Media> GetAll()
        {
            return _medias;
        }

        public List<Media> GetAllArchived()
        {
            return _medias.FindAll(m => m.IsArchived == true);
        }

        public List<Media> GetAllUnarchived()
        {
            return _medias.FindAll(m => m.IsArchived == false);  
        }

        public Media? GetByID(int mediaId)
        {

            foreach (var m in _medias)
            {
                if (m.MediaID == mediaId)
                {
                    return m;
                }
            }

            return null;   
        }

        public List<Media> GetByType(int mediaTypeID)
        {
            return _medias.FindAll(m => m.MediaTypeID == mediaTypeID);
        }

        public List<MediaCheckoutCount> GetTopThreeMostPopularMedia()
        {
            List<MediaCheckoutCount> list = new List<MediaCheckoutCount>
            {
                new MediaCheckoutCount
                {
                    MediaID = 1,
                    MediaTypeName = "Book",
                    Title = "Book 1",
                    CheckoutCount = 1
                },
                new MediaCheckoutCount
                {
                    MediaID = 2,
                    MediaTypeName = "DVD",
                    Title = "DVD 1",
                    CheckoutCount = 1
                },
                new MediaCheckoutCount
                {
                    MediaID = 3,
                    MediaTypeName = "Digital Audio",
                    Title = "Digital Audio 1",
                    CheckoutCount = 1
                }
            };

            return list;
        }

        public List<Media> GetUnarchivedByType(int typeID)
        {
            return _medias.FindAll(m => m.IsArchived == false && m.MediaTypeID == typeID);
        }

        public bool Update(Media request)
        {
            var media = _medias.Find(m => m.MediaID == request.MediaID);

            if (media == null)
            {
                return false;
            }
            else
            {
                int index = _medias.FindIndex(m => m.MediaID == request.MediaID);
                _medias[index] = request;
                return true;
            }
        }
    }
}
