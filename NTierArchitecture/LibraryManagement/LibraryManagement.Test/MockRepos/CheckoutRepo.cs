using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;

namespace LibraryManagement.Test.MockRepos
{
    public class CheckoutRepo : ICheckoutRepository
    {
        private readonly ICheckoutRepository _checkoutRepo;

        public CheckoutRepo(ICheckoutRepository checkoutRepo)
        {
            _checkoutRepo = checkoutRepo;
        }

        public int Add(CheckoutLog newCheckoutLog)
        {
            throw new NotImplementedException();
        }

        public List<CheckoutLog> GetAllCheckedoutMedia()
        {
            throw new NotImplementedException();
        }

        public Borrower? GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public List<CheckoutLogDto> GetCheckedoutMediaByBorrowerID(int borrowerID)
        {
            throw new NotImplementedException();
        }

        public List<CheckoutLog> GetCheckoutLogsByBorrowerID(int borrowerID)
        {
            throw new NotImplementedException();
        }

        public List<Media> GetUncheckedoutUnarchivedMedia()
        {
            throw new NotImplementedException();
        }

        public bool Update(int checkoutLogID)
        {
            throw new NotImplementedException();
        }
    }
}
