using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repositories
{
    public interface IBorrowerRepository
    {
        int Add(Borrower newBorrower);
        bool Delete(Borrower borrower);
        bool Update(Borrower request);
        List<Borrower> GetAll(); 
        List<CheckoutLog>? GetCheckoutLogs(Borrower borrower);
        Borrower? GetByEmail(string email);
    }
}
