﻿using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services
{
    public interface IMediaService
    {
        Result<List<Media>> GetMediaByType(int typeId);
        Result<List<Media>> GetAllArchivedMedia();
        Result<List<Media>> GetUnarchivedMediaByType(int typeID);
        Result<List<MediaCheckoutCount>> GetTop3MostPopularMedia();
        Result<Media> GetMediaByID(int mediaID);
        Result<Media> AddMedia(Media newMedia);
        Result ArchiveMedia(int mediaID);
        Result EditMedia(Media request);
    }
}