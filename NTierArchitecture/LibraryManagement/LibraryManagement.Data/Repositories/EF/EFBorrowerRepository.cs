using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Repositories.EF
{
    public class EFBorrowerRepository : IBorrowerRepository
    {
        private LibraryContext _dbContext;

        public EFBorrowerRepository(string connectionString)
        {
            _dbContext = new LibraryContext(connectionString);
        }

        public int Add(Borrower newBorrower)
        {
            _dbContext.Borrower.Add(newBorrower);
            _dbContext.SaveChanges();

            return newBorrower.BorrowerID;
        }


        public bool Update(Borrower request)
        {
            var borrower = _dbContext.Borrower.FirstOrDefault(b => b.BorrowerID == request.BorrowerID);

            if (borrower != null)
            {
                borrower.FirstName = request.FirstName;
                borrower.LastName = request.LastName;
                borrower.Email = request.Email;
                borrower.Phone = request.Phone;

                return _dbContext.SaveChanges() != 0;
            }

            return false;
        }

        /// <summary>
        /// Deletes all data associated with the passed in borrowerID.
        /// </summary>
        /// <param name="borrowerID"></param>
        public bool Delete(Borrower borrower)
        {

            var checkoutLogs = _dbContext.CheckoutLog.Where(cl => cl.BorrowerID == borrower.BorrowerID);

            _dbContext.Remove(borrower);
            int changeCount = _dbContext.SaveChanges();

            if (checkoutLogs != null)
            {
                _dbContext.RemoveRange(checkoutLogs);
                _dbContext.SaveChanges();
            }

            return changeCount != 0;
        }

        public List<Borrower> GetAll()
        {
            return _dbContext.Borrower.ToList();
        }

        public Borrower? GetByEmail(string email)
        {
            return _dbContext.Borrower.FirstOrDefault(b => b.Email == email);
        }

        public List<CheckoutLog> GetCheckoutLogs(Borrower borrower)
        {
            return _dbContext.CheckoutLog
                             .Include(cl => cl.Media)
                             .Include(cl => cl.Borrower)
                             .Where(cl => cl.BorrowerID == borrower.BorrowerID)
                             .ToList();
        }
    }
}
