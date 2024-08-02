using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;

namespace LibraryManagement.Test.MockRepos
{
    public class MockBorrowerRepo : IBorrowerRepository
    {
        private List<Borrower> _borrowers = new List<Borrower>
        {
            new Borrower
            {
                BorrowerID = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "1667777555",
                CheckoutLogs = new List<CheckoutLog>
                {
                    new CheckoutLog
                    {
                        CheckoutLogID = 1,
                        BorrowerID = 1,
                        MediaID = 1,
                        CheckoutDate = DateTime.Today,
                        DueDate = DateTime.Today.AddDays(7),
                        ReturnDate = null,
                    },
                    new CheckoutLog
                    {
                        CheckoutLogID = 2,
                        BorrowerID = 1,
                        MediaID = 2,
                        CheckoutDate = DateTime.Today,
                        DueDate = DateTime.Today.AddDays(7),
                        ReturnDate = null,
                    },
                    new CheckoutLog
                    {
                        CheckoutLogID= 3,
                        BorrowerID = 1,
                        MediaID = 3,
                        CheckoutDate = DateTime.Today,
                        DueDate = DateTime.Today.AddDays(7),
                        ReturnDate = null,
                    }
                }
            },
                new Borrower
            {
                BorrowerID = 2,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                Phone = "1776666555",
                CheckoutLogs = null
            },
                new Borrower
            {
                BorrowerID = 3,
                FirstName = "Issac",
                LastName = "Doe",
                Email = "issac.doe@example.com",
                Phone = "1776666555",
                CheckoutLogs = new List<CheckoutLog>
                {
                    new CheckoutLog
                    {
                        CheckoutLogID = 4,
                        BorrowerID = 3,
                        MediaID = 4,
                        CheckoutDate = new DateTime(2024, 1, 1),
                        DueDate = new DateTime(2024, 1, 1).AddDays(7),
                        ReturnDate = null,
                    },
                }
            },
        };

        public int Add(Borrower newBorrower)
        {
            _borrowers.Add(newBorrower);
            return _borrowers.Last().BorrowerID;
        }

        public bool Delete(Borrower borrower)
        {
            return _borrowers.Remove(borrower);
        }

        public List<Borrower> GetAll()
        {
            return _borrowers;
        }

        public Borrower? GetByEmail(string email)
        {
            foreach (var b in _borrowers)
            {
                if (b.Email == email)
                {
                    return b;
                }
            }

            return null;
        }

        public List<CheckoutLog>? GetCheckoutLogs(Borrower borrower)
        {
            int index = _borrowers.IndexOf(borrower);

            if (index == -1)
            {
                return null;
            }
            else
            {
                return _borrowers[index].CheckoutLogs;
            }
        }

        public bool Update(Borrower request)
        {
            int index = _borrowers.IndexOf(request);

            if (index == -1)
            {
                return false;
            }
            else
            {
                _borrowers[index] = request;
                return true;
            }
        }
    }
}
