using LibraryManagement.Core.Entities;
using NUnit.Framework;

namespace LibraryManagement.Test
{
    [TestFixture]
    public class CheckoutServiceTests
    {
        [Test]
        public void CheckBorrowerStatus()
        {
            var service = MockServiceFactory.CreateCheckoutService();

            // Borrower with ID 1 has 3 unreturned checked out items
            // Borrower with ID 2 has none checked out items
            // Borrower with ID 3 has overdue item
            var result1 = service.CheckBorrowStatus(1);
            var result2 = service.CheckBorrowStatus(2);
            var result3 = service.CheckBorrowStatus(3);

            Assert.That(result1.Ok, Is.False);
            Assert.That(result1.Message, Is.EqualTo("Borrower has more than 3 checked-out items."));
            Assert.That(result2.Ok, Is.True);
            Assert.That(result3.Ok, Is.False);
            Assert.That(result3.Message, Is.EqualTo("Borrower has overdue item."));
        }

        [Test] 
        public void CheckoutMedia()
        {
            var service = MockServiceFactory.CreateCheckoutService();

            var result = service.CheckoutMedia(new CheckoutLog
            {
                CheckoutLogID = 5,
                BorrowerID = 2,
                MediaID = 4,
                CheckoutDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(7),
                ReturnDate = null
            });

            Assert.That(result.Ok, Is.True);
            Assert.That(result.Data, Is.EqualTo(5));
        }

        [Test]
        public void GetAllCheckedoutMedia()
        {
            var service = MockServiceFactory.CreateCheckoutService();

            var result1 = service.GetAllCheckedoutMedia();

            Assert.That(result1.Ok, Is.True);
            Assert.That(result1.Data.Count, Is.EqualTo(4));

            var result2 = service.CheckoutMedia(new CheckoutLog
            {
                CheckoutLogID = 5,
                BorrowerID = 2,
                MediaID = 4,
                CheckoutDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(7),
                ReturnDate = null
            });

            var result3 = service.GetAllCheckedoutMedia();

            Assert.That(result3.Data.Count, Is.EqualTo(5));

            service.ReturnMedia(1);
            service.ReturnMedia(2);
            service.ReturnMedia(3);
            service.ReturnMedia(4);
            service.ReturnMedia(5);

            var result4 = service.GetAllCheckedoutMedia();

            Assert.That(result4.Ok, Is.False);
            Assert.That(result4.Message, Is.EqualTo("Currently no checked-out media."));
        }

        [Test]
        public void GetAllUncheckedoutUnarchivedMedia()
        {
            var service = MockServiceFactory.CreateCheckoutService();   

            var result1 = service.GetAllUncheckedoutUnarchivedMedia();

            Assert.That(result1.Ok, Is.False);
            Assert.That(result1.Message, Is.EqualTo("All media are either checked out or archived."));

            service.ReturnMedia(3);

            var result2 = service.GetAllUncheckedoutUnarchivedMedia();

            Assert.That(result2.Ok, Is.True);
            Assert.That(result2.Data.Count, Is.EqualTo(1));
            Assert.That(result2.Data.First().Title, Is.EqualTo("Digital Audio 1"));
        }

        [Test] 
        public void GetBorrowerByEmail()
        {
            var service = MockServiceFactory.CreateCheckoutService();

            var result1 = service.GetBorrowerByEmail("issac.doe@example.com");
            var result2 = service.GetBorrowerByEmail("mom@example.com");

            Assert.That(result1.Ok, Is.True);
            Assert.That(result2.Ok, Is.False);
            Assert.That(result1.Data.FirstName, Is.EqualTo("Issac"));
        }

        [Test]
        public void GetCheckedoutMediaByBorrowerID()
        {
            var service = MockServiceFactory.CreateCheckoutService();

            var result1 = service.GetCheckedOutMediaByBorrowerID(1);

            Assert.That(result1.Ok, Is.True);
            Assert.That(result1.Data.Count, Is.EqualTo(3));

            var result2 = service.GetCheckedOutMediaByBorrowerID(2);

            Assert.That(result2.Ok, Is.False);
            Assert.That(result2.Message, Is.EqualTo("Borrower hasn't checked out any media."));
        }

        [Test]
        public void GetCheckoutLogsByBorrowerID()
        {
            var service = MockServiceFactory.CreateCheckoutService();

            var result1 = service.GetCheckoutLogsByBorrowerID(1);

            Assert.That(result1.Ok, Is.True);
            Assert.That(result1.Data.Count, Is.EqualTo(3));

            var result2 = service.GetCheckoutLogsByBorrowerID(2);

            Assert.That(result2.Ok, Is.False);
            Assert.That(result2.Message, Is.EqualTo("No checkout log by Borrrower ID 2 found."));
        }

        [Test]
        public void GetMediaById()
        {
            var service = MockServiceFactory.CreateCheckoutService();

            var result1 = service.GetMediaByID(1);
            var result2 = service.GetMediaByID(10);

            Assert.That(result1.Ok, Is.True);
            Assert.That(result1.Data.Title, Is.EqualTo("Book 1"));
            Assert.That(result2.Ok, Is.False);
            Assert.That(result2.Message, Is.EqualTo("No media by ID: 10 found."));
        }

        [Test]
        public void ReturnMedia()
        {
            var service = MockServiceFactory.CreateCheckoutService();

            var result1 = service.ReturnMedia(1);
            var result2 = service.ReturnMedia(10);
            var result3 = service.GetAllUncheckedoutUnarchivedMedia();

            Assert.That(result1.Ok, Is.True);
            Assert.That(result2.Ok, Is.False);
            Assert.That(result2.Message, Is.EqualTo("Return attempt failed."));
            Assert.That(result3.Ok, Is.True);
            Assert.That(result3.Data.First().MediaID, Is.EqualTo(1));
        }
    }
}
