using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repositories
{
    public interface IBorrowerRepository
    {
        int Add(Borrower newBorrower);
        void Delete(Borrower borrower);
        void Update(Borrower request);
        List<Borrower> GetAll(); 
        Borrower? GetByEmail(string email);
    }
}
