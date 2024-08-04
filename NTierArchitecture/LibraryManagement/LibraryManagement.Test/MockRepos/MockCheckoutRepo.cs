using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;

namespace LibraryManagement.Test.MockRepos
{
    public class MockCheckoutRepo : ICheckoutRepository
    {
        private List<CheckoutLog> _checkoutLogs = new List<CheckoutLog>
        {
            new CheckoutLog
            {
                CheckoutLogID = 1,
                BorrowerID = 1,
                MediaID = 1,
                CheckoutDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(7),
                ReturnDate = null,
                Borrower = new Borrower
                {
                    BorrowerID = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Phone = "1667777555",
                },
                Media = new Media
                {
                    MediaID = 1,
                    MediaTypeID = 1,
                    Title = "Book 1",
                    IsArchived = false
                }
            },
            new CheckoutLog
            {
                CheckoutLogID = 2,
                BorrowerID = 1,
                MediaID = 2,
                CheckoutDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(7),
                ReturnDate = null,
                Borrower = new Borrower
                {
                    BorrowerID = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Phone = "1667777555",
                },
                Media = new Media
                {
                    MediaID = 2,
                    MediaTypeID = 2,
                    Title = "DVD 1",
                    IsArchived = false,
                }
            },
            new CheckoutLog
            {
                CheckoutLogID= 3,
                BorrowerID = 1,
                MediaID = 3,
                CheckoutDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(7),
                ReturnDate = null,
                Borrower = new Borrower
                {
                    BorrowerID = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Phone = "1667777555",
                },
                Media = new Media
                {
                    MediaID= 3,
                    MediaTypeID = 3,
                    Title = "Digital Audio 1",
                    IsArchived = false,
                }
            },
            new CheckoutLog
            {
                CheckoutLogID = 4,
                BorrowerID = 3,
                MediaID = 4,
                CheckoutDate = new DateTime(2024, 1, 1),
                DueDate = new DateTime(2024, 1, 1).AddDays(7),
                ReturnDate = null,
                Borrower = new Borrower
                {
                    BorrowerID = 3,
                    FirstName = "Issac",
                    LastName = "Doe",
                    Email = "issac.doe@example.com",
                    Phone = "1776666555",
                }
            }
        };

        public int Add(CheckoutLog newCheckoutLog)
        {
            _checkoutLogs.Add(newCheckoutLog);

            return _checkoutLogs.Last().CheckoutLogID;
        }

        public List<CheckoutLog> GetAllCheckedoutMedia()
        {
            return _checkoutLogs.FindAll(cl => cl.ReturnDate == null);
        }

        public Borrower? GetByEmail(string email)
        {
            var log = _checkoutLogs.Find(cl => cl.Borrower.Email == email);

            if (log != null)
            {
                return log.Borrower;
            }

            return null;
        }

        public List<CheckoutLog> GetCheckedoutMediaByBorrowerID(int borrowerID)
        {
            List<CheckoutLog> list = new List<CheckoutLog>();

            var logs = _checkoutLogs.FindAll(cl => cl.ReturnDate == null && cl.BorrowerID == borrowerID);
            if (logs != null) 
            {
                foreach (var cl in logs)
                {
                    list.Add(new CheckoutLog
                    {
                        CheckoutLogID = cl.BorrowerID,
                        Media = cl.Media
                    });
                }
            }

            return list;
        }

        public List<CheckoutLog> GetCheckoutLogsByBorrowerID(int borrowerID)
        {
            return _checkoutLogs.FindAll(cl => cl.BorrowerID == borrowerID);
        }

        public List<Media> GetAvailableMedia()
        {
            List<Media> list = new List<Media>();

            var logs = _checkoutLogs.FindAll(cl => cl.ReturnDate != null && !cl.Media.IsArchived);
            if (logs.Any())
            {
                foreach (var cl in logs)
                {
                    list.Add(cl.Media);
                }
            }

            return list;
        }

        public void Update(int checkoutLogID)
        {
            var log = _checkoutLogs.Find(m => m.CheckoutLogID == checkoutLogID);

            if (log != null)
            {
                int index = _checkoutLogs.IndexOf(log);
                _checkoutLogs[index].ReturnDate = DateTime.Today;
            } 
        }
    }
}
