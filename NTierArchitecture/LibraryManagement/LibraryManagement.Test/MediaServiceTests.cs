using LibraryManagement.Core.Entities;
using NUnit.Framework;

namespace LibraryManagement.Test
{
    [TestFixture]
    public class MediaServiceTests
    {
        [Test]
        public void AddMedia()
        {
            var service = MockServiceFactory.CreateMediaService();

            Media m = new Media
            {
                MediaID = 5,
                MediaTypeID = 2,
                Title = "DVD 2",
                IsArchived = false,
                CheckoutLogs = null
            };

            var result = service.AddMedia(m);


            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.EqualTo(5));
        }

        [Test]
        public void ArchiveMedia()
        {
            var service = MockServiceFactory.CreateMediaService();

            // before archive
            var media = service.GetMediaByID(1);

            Assert.That(media.Data.IsArchived, Is.False);

            var archiveResult1 = service.ArchiveMedia(1);
            var archiveResult2 = service.ArchiveMedia(10);

            Assert.That(archiveResult1.Ok, Is.True);
            Assert.That(archiveResult2.Ok, Is.False);

            var archivedMedia = service.GetMediaByID(1);
            Assert.That(archivedMedia.Data.IsArchived, Is.True);
        }

        [Test]
        public void EditMedia()
        {
            var service = MockServiceFactory.CreateMediaService();

            var media = service.GetMediaByID(1);
            media.Data.Title = "BOOK 1";
            var editResult = service.EditMedia(media.Data);

            var editedMedia = service.GetMediaByID(1);

            Assert.That(editResult.Ok, Is.True);
            Assert.That(editedMedia.Data.Title, Is.Not.EqualTo("Book 1"));
            Assert.That(editedMedia.Data.Title, Is.EqualTo("BOOK 1"));
        }

        [Test]
        public void GetAllArchivedMedia()
        {
            var serivce = MockServiceFactory.CreateMediaService();

            var list = serivce.GetAllArchivedMedia();

            Assert.That(list.Ok, Is.False);
            Assert.That(list.Data, Is.Null);
        }

        [Test]
        public void GetMediaByID()
        {
            var service = MockServiceFactory.CreateMediaService();

            var result1 = service.GetMediaByID(1);
            var result2 = service.GetMediaByID(10);

            Assert.That(result1.Ok, Is.True);
            Assert.That(result2.Ok, Is.False);
        }

        [Test]
        public void GetMediaByType()
        {
            var service = MockServiceFactory.CreateMediaService();

            // non-existent type ID
            var result1 = service.GetMediaByType(4);
            // valid type ID, media also exits by that type ID
            var result2 = service.GetMediaByType(1);

            Assert.That(result1.Ok, Is.False);
            Assert.That(result2.Ok, Is.True);
            Assert.That(result2.Data.Count, Is.EqualTo(2)); 
        }

        [Test]
        public void GetTop3MostPopularMedia()
        {
            var service = MockServiceFactory.CreateMediaService();

            var result = service.GetTop3MostPopularMedia();

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data.Any(), Is.True);
            Assert.That(result.Data.Count, Is.EqualTo(3));
        }

        [Test] 
        public void GetUnarchivedMediaByType()
        {
            var service = MockServiceFactory.CreateMediaService();

            var result1 = service.GetUnarchivedMediaByType(1);

            Assert.That(result1.Data.Count, Is.EqualTo(2));

            service.ArchiveMedia(1);

            var result2 = service.GetUnarchivedMediaByType(1);

            Assert.That(result2.Data.Count, Is.EqualTo(1));
        }
    }
}
