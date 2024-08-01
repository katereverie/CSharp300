using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;

namespace LibraryManagement.Test.MockRepos
{
    public class BorrowerRepo : IBorrowerRepository
    {
        private readonly IBorrowerRepository _borrowerRepo;

        public BorrowerRepo(IBorrowerRepository borrowerRepo)
        {
            _borrowerRepo = borrowerRepo;
        }

        public int Add(Borrower newBorrower)
        {
            throw new NotImplementedException();
        }

        public void Delete(Borrower borrower)
        {
            throw new NotImplementedException();
        }

        public List<Borrower> GetAll()
        {
            throw new NotImplementedException();
        }

        public Borrower? GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public List<CheckoutLog> GetCheckoutLogs(Borrower borrower)
        {
            throw new NotImplementedException();
        }

        public void Update(Borrower request)
        {
            throw new NotImplementedException();
        }
    }
}
