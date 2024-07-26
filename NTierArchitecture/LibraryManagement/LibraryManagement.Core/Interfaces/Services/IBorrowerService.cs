using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services
{
    public interface IBorrowerService
    {
        Result<List<Borrower>> GetAllBorrowers();
        Result<Borrower> GetBorrowerByEmail(string email);
        Result<Borrower> UpdateBorrower(Borrower borrower);
        Result<Borrower> DeletBorrower(Borrower borrower);
        Result AddBorrower(Borrower newBorrower);
        Result VerifyDuplicateBorrower(string email);
    }
}
