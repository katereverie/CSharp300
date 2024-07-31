using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Repositories.EF
{
    public class EFCheckoutRepository : ICheckoutRepository
    {
        private LibraryContext _dbContext;

        public EFCheckoutRepository(string connectionString)
        {
            _dbContext = new LibraryContext(connectionString);
        }

        public int Add(CheckoutLog newCheckoutLog)
        {
            _dbContext.CheckoutLog.Add(newCheckoutLog);
            _dbContext.SaveChanges();

            return newCheckoutLog.CheckoutLogID;
        }

        public List<CheckoutLog> GetAllCheckedoutMedia()
        {
            return _dbContext.CheckoutLog
                             .Include("Borrower")
                             .Include("Media")
                             .Where(cl => cl.ReturnDate == null)
                             .ToList();
        }

        public List<Media> GetUncheckedoutUnarchivedMedia()
        {
            return _dbContext.Media
                             .Include(m => m.CheckoutLogs)
                             .Where(m => !m.IsArchived)
                             .Where(m => !m.CheckoutLogs.Any() ||
                                         (m.CheckoutLogs.Any() &&
                                          m.CheckoutLogs.OrderByDescending(cl => cl.CheckoutLogID)
                                                        .First().ReturnDate != null))
                             .ToList();
        }

        public List<CheckoutLog> GetCheckoutLogsByBorrowerID (int borrowerID)
        {
            return _dbContext.CheckoutLog
                             .Where(cl => cl.BorrowerID == borrowerID)
                             .ToList();
        }

        public List<CheckoutLogDto> GetCheckedoutMediaByBorrowerID (int borrowerID)
        {
            return _dbContext.CheckoutLog
                             .Where(cl => cl.BorrowerID == borrowerID && cl.ReturnDate == null)
                             .Select(cl => new CheckoutLogDto
                             {
                                 CheckoutLogID = cl.CheckoutLogID,
                                 Title = cl.Media.Title
                             })
                             .ToList();
        }

        public void Update(int checkoutLogID)
        {
            var checkoutLog = _dbContext.CheckoutLog
                              .FirstOrDefault(cl => cl.CheckoutLogID == checkoutLogID);

            if (checkoutLog != null)
            {
                checkoutLog.ReturnDate = DateTime.Now;

                _dbContext.SaveChanges();
            }
        }

        public Borrower? GetBorrowerByEmail(string email)
        {
            return _dbContext.Borrower
                             .FirstOrDefault(b => b.Email == email);
        }
    }
}
