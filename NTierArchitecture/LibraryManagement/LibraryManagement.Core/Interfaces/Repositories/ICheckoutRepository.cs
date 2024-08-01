using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repositories
{
    public interface ICheckoutRepository
    {
        List<Media> GetUncheckedoutUnarchivedMedia();
        List<CheckoutLog> GetCheckoutLogsByBorrowerID(int borrowerID);
        List<CheckoutLogDto> GetCheckedoutMediaByBorrowerID(int borrowerID);
        List<CheckoutLog> GetAllCheckedoutMedia();
        Borrower? GetByEmail(string email);
        bool Update(int checkoutLogID);
        int Add(CheckoutLog newCheckoutLog);
    }
}
