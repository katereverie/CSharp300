using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services
{
    public interface ICheckoutService
    {
        Result<List<CheckoutLog>> GetAllCheckedoutMedia();
        Result<List<Media>> GetAllUncheckedoutUnarchivedMedia();
        Result<List<CheckoutLog>> GetCheckoutLogsByBorrowerID(int borrowerID);
        Result<List<CheckoutLogDto>> GetCheckedOutMediaByBorrowerID(int borrowerID);
        Result<int> CheckoutMedia(CheckoutLog newCheckoutLog); 
        Result<Borrower> GetBorrowerByEmail(string email);
        Result CheckBorrowStatus(int borrowerID);
        Result<Media> GetMediaByID(int mediaID);
        Result ReturnMedia(int checkoutLogID); 
    }
}
