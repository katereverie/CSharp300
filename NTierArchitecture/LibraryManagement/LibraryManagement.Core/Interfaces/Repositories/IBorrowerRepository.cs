using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repositories
{
    public interface IBorrowerRepository
    {
        void Add(Borrower newBorrower);
        void Delete(string email);
        void Update(Borrower request);
        List<Borrower> GetAll(); 
        Borrower? GetById(int id);
        Borrower? GetByEmail(string email);
    }
}
