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


        public void Update(Borrower borrower)
        {
            _dbContext.Borrower.Update(borrower);
            _dbContext.SaveChanges();
        }

        public void Delete(Borrower borrower)
        {
            _dbContext.Borrower.Remove(borrower);
            _dbContext.SaveChanges();
        }

        public List<Borrower> GetAll()
        {
            return _dbContext.Borrower.ToList();
        }

        public Borrower? GetByEmail(string email)
        {
            return _dbContext.Borrower.FirstOrDefault(b => b.Email == email);
        }

        public Borrower? GetById(int id)
        {
            return _dbContext.Borrower.FirstOrDefault(b => b.BorrowerID == id);
        }
    }
}
