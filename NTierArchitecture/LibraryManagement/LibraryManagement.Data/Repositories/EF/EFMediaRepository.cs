﻿using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Repositories.EF
{
    public class EFMediaRepository : IMediaRepository
    {
        private LibraryContext _dbContext;

        public EFMediaRepository(string connectionString)
        {
            _dbContext = new LibraryContext(connectionString);
        }

        public int Add(Media newMedia)
        {
            _dbContext.Media.Add(newMedia);
            _dbContext.SaveChanges();

            return newMedia.MediaID;
        }

        public void Update(Media request)
        {
            var media = _dbContext.Media.FirstOrDefault(m => m.MediaID == request.MediaID);

            if (media != null)
            {
                media.Title = request.Title;
                media.MediaTypeID = request.MediaTypeID;

                _dbContext.SaveChanges();
            }
        }

        public void Archive(int mediaID)
        {
            var media = _dbContext.Media.FirstOrDefault(m => m.MediaID == mediaID);

            if (media != null)
            {
                media.IsArchived = true;

                _dbContext.SaveChanges();
            }
        }

        public List<Media> GetAll()
        {
            return _dbContext.Media.ToList();
        }

        public Media? GetByID(int mediaId)
        {
            return _dbContext.Media.FirstOrDefault(m => m.MediaID == mediaId);
        }

        public List<Media> GetByType(int typeId)
        {
            return _dbContext.Media
                             .Include(m => m.MediaType)
                             .Where(m => m.MediaTypeID == typeId)
                             .ToList();
        }

        public List<Media> GetAllUnarchived()
        {
            return _dbContext.Media
                             .Where(m => m.IsArchived == false)
                             .ToList();
        }

        public List<Media> GetUnarchivedByType(int typeId)
        {
            return _dbContext.Media
                             .Where(m => !m.IsArchived && m.MediaTypeID == typeId)
                             .ToList();
        }

        public List<Media> GetAllArchived()
        {
            return _dbContext.Media
                             .Include(m => m.MediaType)
                             .Where(m => m.IsArchived == true)
                             .OrderBy(m => m.MediaTypeID)
                             .ThenBy(m => m.Title)
                             .ToList();
        }

        public List<Top3Media> GetTopThreeMostPopularMedia()
        {
            return _dbContext.Media
                             .Include(m => m.MediaType)
                             .Include(m => m.CheckoutLogs)
                             .Where(m => m.MediaType != null && m.CheckoutLogs != null)
                             .Select(m => new Top3Media
                                             {
                                                 MediaID = m.MediaID,
                                                 Title = m.Title,
                                                 MediaTypeName = m.MediaType.MediaTypeName,
                                                 CheckoutCount = m.CheckoutLogs.Count()
                                             })
                             .OrderByDescending(m => m.CheckoutCount)
                             .Take(3)
                             .ToList();
        }

        public List<MediaType> GetAllMediaTypes()
        {
            return _dbContext.MediaType.ToList();
        }
    }
}
