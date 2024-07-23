using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;

namespace LibraryManagement.Data.Repositories
{
    public class EFBorrowerRepository : IBorrowerRepository
    {
        private LibraryContext _dbContext;

        public EFBorrowerRepository(string connectionString)
        {
            _dbContext = new LibraryContext(connectionString);
        }

        public void Add(Borrower borrower)
        {
            _dbContext.Borrower.Add(borrower);
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

        public void Update(Borrower borrower)
        {
            _dbContext.Borrower.Update(borrower);
            _dbContext.SaveChanges();
        }
    }
}