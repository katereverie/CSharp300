using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.Application.Services
{
    public class CheckoutService : ICheckoutService
    {
        private ICheckoutRepository _checkoutRepository;
        private IMediaRepository _mediaRepository;

        public CheckoutService(ICheckoutRepository checkoutRepository, IMediaRepository mediaRepository)
        {
            _checkoutRepository = checkoutRepository;
            _mediaRepository = mediaRepository;
        }

        public Result CheckBorrowStatus(int borrowerID)
        {
            try
            {
                var logs = _checkoutRepository.GetCheckoutLogsByBorrowerID(borrowerID);
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
                var checkoutLogID = _checkoutRepository.Add(newCheckoutLog);
                
                return ResultFactory.Success(checkoutLogID);
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
                var list = _checkoutRepository.GetAllCheckedoutMedia();

                return list.Any() ?
                            ResultFactory.Success(list) :
                            ResultFactory.Fail<List<CheckoutLog>>("No such list found.");
                       
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
                var list = _checkoutRepository.GetUncheckedoutUnarchivedMedia();

                return list.Any() ?
                       ResultFactory.Success(list) :
                       ResultFactory.Fail<List<Media>>("All media are either checked out or archived.");
                       
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
                var borrower = _checkoutRepository.GetByEmail(email);

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
                var list = _checkoutRepository.GetCheckedoutMediaByBorrowerID(borrowerID);

                return list.Any(log => log != null) ?
                       ResultFactory.Success(list) :
                       ResultFactory.Fail<List<CheckoutLogDto>>($"Borrower hasn't checked out any media.");
                       
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
                var list = _checkoutRepository.GetCheckoutLogsByBorrowerID(borrowerID);

                return list is null ?
                       ResultFactory.Fail<List<CheckoutLog>>($"No checkout log by Borrrower ID {borrowerID} found.") :
                       ResultFactory.Success(list);
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
                var media = _mediaRepository.GetMediaById(mediaID);

                return media is null ?
                       ResultFactory.Fail<Media>($"No media by ID: {mediaID} found.") :
                       ResultFactory.Success(media);
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
                return _checkoutRepository.Update(checkoutLogID)
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
