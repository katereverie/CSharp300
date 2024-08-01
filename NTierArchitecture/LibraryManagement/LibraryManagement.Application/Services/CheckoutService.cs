using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.Application.Services
{
    public class CheckoutService : ICheckoutService
    {
        private ICheckoutRepository _checkoutRepo;
        private IMediaRepository _mediaRepo;

        public CheckoutService(ICheckoutRepository checkoutRepository, IMediaRepository mediaRepository)
        {
            _checkoutRepo = checkoutRepository;
            _mediaRepo = mediaRepository;
        }

        public Result CheckBorrowStatus(int borrowerID)
        {
            try
            {
                var logs = _checkoutRepo.GetCheckoutLogsByBorrowerID(borrowerID);
                int checkedoutItemCount = 0;

                foreach (var log in logs)
                {
                    if (log.DueDate < DateTime.Now)
                    {
                        return ResultFactory.Fail("Borrower has overdue item.");
                    }
                    else if (log.ReturnDate == null)
                    {
                        checkedoutItemCount++;
                    }
                }

                if (checkedoutItemCount >= 3)
                {
                    return ResultFactory.Fail("Borrower has more than 3 checked-out items.");
                }

                return ResultFactory.Success();
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<int> CheckoutMedia(CheckoutLog newCheckoutLog)
        {
            try
            {
                var newID = _checkoutRepo.Add(newCheckoutLog);

                return newID != 0 && newID != -1
                    ? ResultFactory.Success(newID)
                    : ResultFactory.Fail<int>("Check out attempt failed.");
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<int>(ex.Message);
            }
        }

        public Result<List<CheckoutLog>> GetAllCheckedoutMedia()
        {
            try
            {
                var list = _checkoutRepo.GetAllCheckedoutMedia();

                return list.Any()
                    ? ResultFactory.Success(list) 
                    : ResultFactory.Fail<List<CheckoutLog>>("Currently no checked-out media.");
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<CheckoutLog>>(ex.Message);
            }
        }

        public Result<List<Media>> GetAllUncheckedoutUnarchivedMedia()
        {
            try
            {
                var list = _checkoutRepo.GetUncheckedoutUnarchivedMedia();

                return list.Any() 
                    ? ResultFactory.Success(list)
                    : ResultFactory.Fail<List<Media>>("All media are either checked out or archived.");          
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Media>>(ex.Message);
            }
        }

        public Result<Borrower> GetBorrowerByEmail(string email)
        {
            try
            {
                var borrower = _checkoutRepo.GetByEmail(email);

                return borrower is null ?
                       ResultFactory.Fail<Borrower>($"No Borrower with {email} found.") :
                       ResultFactory.Success(borrower);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Borrower>(ex.Message);
            }
        }

        public Result<List<CheckoutLogDto>> GetCheckedOutMediaByBorrowerID(int borrowerID)
        {
            try
            {
                var list = _checkoutRepo.GetCheckedoutMediaByBorrowerID(borrowerID);

                return list.Any(log => log is not null) 
                    ? ResultFactory.Success(list)
                    : ResultFactory.Fail<List<CheckoutLogDto>>($"Borrower hasn't checked out any media.");
                       
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<CheckoutLogDto>>(ex.Message);
            }
        }

        public Result<List<CheckoutLog>> GetCheckoutLogsByBorrowerID(int borrowerID)
        {
            try
            {
                var list = _checkoutRepo.GetCheckoutLogsByBorrowerID(borrowerID);

                return list is not null
                    ? ResultFactory.Success(list)
                    : ResultFactory.Fail<List<CheckoutLog>>($"No checkout log by Borrrower ID {borrowerID} found.");
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<CheckoutLog>>(ex.Message);
            }
        }

        public Result<Media> GetMediaByID(int mediaID)
        {
            try
            {
                var media = _mediaRepo.GetByID(mediaID);

                return media is not null
                    ? ResultFactory.Success(media)
                    : ResultFactory.Fail<Media>($"No media by ID: {mediaID} found.");      
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Media>(ex.Message);
            }
        }

        public Result ReturnMedia(int checkoutLogID)
        {
            try
            {
                return _checkoutRepo.Update(checkoutLogID)
                    ? ResultFactory.Success()
                    : ResultFactory.Fail("Return attempt failed.");
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }
    }
}
