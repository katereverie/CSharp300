using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services
{
    public interface IBorrowerService
    {
        Result<List<Borrower>> GetAllBorrowers();
        Result<Borrower> GetBorrower(string email);
        Result<List<CheckoutLog>> GetCheckoutLogsByBorrower(Borrower borrower);
        Result UpdateBorrower(Borrower borrower);
        Result<int> AddBorrower(Borrower newBorrower);
        Result DeleteBorrower(Borrower Borrower);
    }
}
