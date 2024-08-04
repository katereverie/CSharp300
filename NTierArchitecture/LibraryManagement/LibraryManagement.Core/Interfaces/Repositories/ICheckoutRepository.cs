using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Repositories
{
    public interface ICheckoutRepository
    {
        List<Media> GetAvailableMedia();
        List<CheckoutLog> GetCheckoutLogsByBorrowerID(int borrowerID);
        List<CheckoutLog> GetCheckedoutMediaByBorrowerID(int borrowerID);
        List<CheckoutLog> GetAllCheckedoutMedia();
        Borrower? GetByEmail(string email);
        void Update(int checkoutLogID);
        int Add(CheckoutLog newCheckoutLog);
    }
}
