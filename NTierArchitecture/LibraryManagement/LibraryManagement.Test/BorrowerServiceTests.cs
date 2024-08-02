using LibraryManagement.Core.Entities;
using NUnit.Framework;

namespace LibraryManagement.Test
{
    [TestFixture]
    public class BorrowerServiceTests
    {
        [Test]
        public void GetAllBorrowers()
        {
            var service = MockServiceFactory.CreateBorrowerService();
            var r = service.GetAllBorrowers();

            Assert.That(r.Ok, Is.True);
            Assert.That(r.Data, Is.Not.Null);
            Assert.That(r.Data.Count, Is.EqualTo(3));
        }

        [Test]
        public void GetBorrowers()
        {
            var service = MockServiceFactory.CreateBorrowerService();

            var r1 = service.GetBorrower("john.doe@example.com");
            var r2 = service.GetBorrower("test@example.com");

            Assert.That(r1.Ok, Is.True);
            Assert.That(r2.Ok, Is.False);
        }

        [Test]
        public void UpdateBorrower()
        {
            var service = MockServiceFactory.CreateBorrowerService();

            var getResult1 = service.GetBorrower("john.doe@example.com");

            getResult1.Data.Email = "john.doe@gmail.com";

            var r1 = service.UpdateBorrower(getResult1.Data);

            var getResult2 = service.GetBorrower("john.doe@example.com");
            var getResult3 = service.GetBorrower("john.doe@gmail.com");

            Assert.That(r1.Ok, Is.True);
            Assert.That(getResult1.Ok, Is.True);
            Assert.That(getResult2.Ok, Is.False);
            Assert.That(getResult3.Ok, Is.True);
        }

        [Test]
        public void AddBorrower()
        {
            var service = MockServiceFactory.CreateBorrowerService();

            Borrower newB1 = new Borrower
            {
                BorrowerID = 3,
                FirstName = "Issac",
                LastName = "Doe",
                Email = "issac@example.com",
                Phone = "1989999666",
                CheckoutLogs = null
            };

            Borrower newB2 = new Borrower
            {
                BorrowerID = 4,
                FirstName = "Issac\'s",
                LastName = "Mom",
                Email = "issac@example.com",
                Phone = "1989999666",
                CheckoutLogs = null
            };

            var addResult1 = service.AddBorrower(newB1);
            var addResult2 = service.AddBorrower(newB2);
            var getAllResult = service.GetAllBorrowers();

            Assert.That(addResult1.Ok, Is.True);
            Assert.That(addResult2.Ok, Is.False);
            Assert.That(getAllResult.Data.Count, Is.EqualTo(4));
        }

        [Test] 
        public void DeleteBorrower()
        {
            var service = MockServiceFactory.CreateBorrowerService();

            var getResult = service.GetBorrower("jane.doe@example.com");

            var deleteResult1 = service.DeleteBorrower(getResult.Data);
            var deleteResult2 = service.DeleteBorrower(new Borrower());

            var countResult = service.GetAllBorrowers();

            Assert.That(deleteResult1.Ok, Is.True); 
            Assert.That(deleteResult2.Ok, Is.False);
            Assert.That(countResult.Data.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetCheckoutLogsByBorrower()
        {
            var service = MockServiceFactory.CreateBorrowerService();

            var getBorrowerResult1 = service.GetBorrower("john.doe@example.com");
            var getBorrowerResult2 = service.GetBorrower("jane.doe@example.com");

            var getCheckoutLogsResult1 = service.GetCheckoutLogsByBorrower(getBorrowerResult1.Data);
            var getCheckoutLogsResult2 = service.GetCheckoutLogsByBorrower(getBorrowerResult2.Data);

            Assert.That(getCheckoutLogsResult1.Ok, Is.True);
            Assert.That(getCheckoutLogsResult2.Ok, Is.False);
            Assert.That(getCheckoutLogsResult1.Data.Count, Is.EqualTo(3));
        }
    }
}
