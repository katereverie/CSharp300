using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;

namespace LibraryManagement.Data.Repositories.EF
{
    public class EFBorrowerRepository : IBorrowerRepository
    {
        private LibraryContext _dbContext;

        public EFBorrowerRepository(string connectionString)
        {
            _dbContext = new LibraryContext(connectionString);
        }

        public void Add(Borrower newBorrower)
        {
            _dbContext.Borrower.Add(newBorrower);
            _dbContext.SaveChanges();
        }


        public void Update(Borrower request)
        {
            var borrower = _dbContext.Borrower.
                FirstOrDefault(b => b.BorrowerID == request.BorrowerID);

            if (borrower != null)
            {
                borrower.FirstName = request.FirstName;
                borrower.LastName = request.LastName;
                borrower.Email = request.Email;
                borrower.Phone = request.Phone;

                _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes all data associated with the passed in borrowerID.
        /// </summary>
        /// <param name="borrowerID"></param>
        public void Delete(Borrower borrower)
        {

            var checkoutLogs = _dbContext.CheckoutLog.
                Where(cl => cl.BorrowerID == borrower.BorrowerID);

            _dbContext.Remove(borrower);
            _dbContext.SaveChanges();

            if (checkoutLogs != null )
            {
                _dbContext.RemoveRange(checkoutLogs);
                _dbContext.SaveChanges();
            }

            // explicit transaction
            //using (var transaction = _dbContext.Database.BeginTransaction())
            //{
            //    _dbContext.Borrower
            //        .Where(b => b.BorrowerID == borrowerID)
            //        .ExecuteDelete();

            //    _dbContext.CheckoutLog
            //        .Where(cl => cl.BorrowerID == borrowerID)
            //        .ExecuteDelete();

            //    transaction.Commit();
            //}
        }

        public List<Borrower> GetAll()
        {
            return _dbContext.Borrower.ToList();
        }

        public Borrower? GetByEmail(string email)
        {
            return _dbContext.Borrower.FirstOrDefault(b => b.Email == email);
        }
    }
}
