using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.Application.Services
{
    public class BorrowerService : IBorrowerService
    {
        private IBorrowerRepository _borrowerRepo;

        public BorrowerService(IBorrowerRepository borrowerRepository)
        {
            _borrowerRepo = borrowerRepository;
        }

        public Result<List<Borrower>> GetAllBorrowers()
        {
            try
            {
                var list = _borrowerRepo.GetAll();
                return ResultFactory.Success(list);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<Borrower>>(ex.Message);
            }
        }

        public Result<Borrower> GetBorrower(string email)
        {
            try
            {
                var borrower = _borrowerRepo.GetByEmail(email);

                return borrower is null ?
                    ResultFactory.Fail<Borrower>($"Borrower registered with {email} not found!") :
                    ResultFactory.Success(borrower);
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<Borrower>(ex.Message);
            }
        }

        public Result UpdateBorrower(Borrower borrower)
        {
            try
            {
                return _borrowerRepo.Update(borrower)
                    ? ResultFactory.Success(borrower)
                    : ResultFactory.Fail("Edit attempt failed.");
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }
        }

        public Result<int> AddBorrower(Borrower newBorrower)
        {
            try
            {
                var duplicate = _borrowerRepo.GetByEmail(newBorrower.Email);
                if (duplicate != null)
                {
                    return ResultFactory.Fail<int>($"{newBorrower.Email} has already been taken!");
                }

                int newID = _borrowerRepo.Add(newBorrower);
                switch (newID)
                {
                    case -1:
                        return ResultFactory.Fail<int>("New Borrower Registration failed.");
                    default:
                        return ResultFactory.Success(newID);
                }
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<int>(ex.Message);
            }
        }

        public Result DeleteBorrower(Borrower borrower)
        {
            try
            {
                return _borrowerRepo.Delete(borrower)
                    ? ResultFactory.Success()
                    : ResultFactory.Fail("Delete attempt failed.");
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail(ex.Message);
            }

        }

        public Result<List<CheckoutLog>> GetCheckoutLogsByBorrower(Borrower borrower)
        {
            try
            {
                var list = _borrowerRepo.GetCheckoutLogs(borrower);

                return list is not null
                    ? ResultFactory.Success(list)
                    : ResultFactory.Fail<List<CheckoutLog>>("Borrower has no checkout logs records.");
            }
            catch (Exception ex)
            {
                return ResultFactory.Fail<List<CheckoutLog>>(ex.Message);
            }
        }
    }
}
