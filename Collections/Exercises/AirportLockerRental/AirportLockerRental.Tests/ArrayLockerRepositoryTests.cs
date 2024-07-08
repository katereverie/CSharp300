using AirportLockerRental.UI.DTOs;
using AirportLockerRental.UI.Storage;
using NUnit.Framework;

namespace AirportLockerRental.Tests
{

    public class ArrayLockerRepositoryTests
    {
        private ArrayLockerRepository? _repo;

        [SetUp]
        public void InitializeRepo()
        {
            _repo = new ArrayLockerRepository(10);

            _repo.AddContents(new LockerContents
            {
                LockerNumber = 1,
                Description = "Computer",
                RenterName = "Computer Owner"
            });

            _repo.AddContents(new LockerContents
            {
                LockerNumber = 5,
                Description = "Knife",
                RenterName = "Knife Owner"
            });

        }

        [Test]
        public void AddContents_Availability()
        {
            var addResult1 = _repo?.AddContents(new LockerContents
            {
                LockerNumber = 1,
                Description = "Test",
                RenterName = "Test Owner"
            });

            var addResult2 = _repo?.AddContents(new LockerContents
            {
                LockerNumber = 2,
                Description = "Test",
                RenterName = "Test Owner"
            });

            Assert.That(addResult1, Is.False);
            Assert.That(addResult2, Is.True);
        }

        [Test]
        public void GetLockerContents_NullOrNotNull()
        {
            var getResult1 = _repo?.GetLockerContents(1);
            var getResult2 = _repo?.GetLockerContents(3);

            Assert.That(getResult1, Is.Not.Null);
            Assert.That(getResult2, Is.Null);
        }

        [Test]
        public void IsAvailable_NullOrNotNull()
        {
            var checkResult1 = _repo?.IsAvailable(1);
            var checkResult2 = _repo?.IsAvailable(3);

            Assert.That(checkResult1, Is.False);
            Assert.That(checkResult2, Is.True);
        }

        [Test]
        public void RemoveContents_ReturnNullOrNot()
        {
            var removeResult1 = _repo?.RemoveContents(1);
            var removeResult2 = _repo?.RemoveContents(3);

            Assert.That(removeResult1, Is.Not.Null);
            Assert.That(removeResult1?.RenterName, Is.EqualTo("Computer Owner"));
            Assert.That(removeResult2, Is.Null);
        }

    }
}
