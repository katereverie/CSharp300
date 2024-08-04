using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.ConsoleUI.IO
{
    public static class CheckoutWorkflows
    {
        public static void Checkout(ICheckoutService serivce)
        {
            Console.Clear();

            Borrower borrower;
            string email = Utilities.GetRequiredString("Enter Borrower's Email: ");
            var getBorrowerResult = serivce.GetBorrowerByEmail(email);

            if (!getBorrowerResult.Ok)
            {
                Console.WriteLine(getBorrowerResult.Message);
                Utilities.AnyKey();
                return;
            }

            borrower = getBorrowerResult.Data;

            int exitChoice = 0;
            while (exitChoice != 2) 
            {
                var checkResult = serivce.CheckBorrowStatus(borrower.BorrowerID);
                if (!checkResult.Ok)
                {
                    Console.WriteLine(checkResult.Message);
                    Utilities.AnyKey();
                    return;
                }

                var getMediaResult = serivce.GetAvailableMedia();

                if (!getMediaResult.Ok)
                {
                    Console.WriteLine(getMediaResult.Message);
                    Utilities.AnyKey();
                    return;
                }

                var mediaList = getMediaResult.Data;
                Utilities.PrintAvailableMedia(mediaList);

                int mediaID = Utilities.GetMediaID(mediaList, "Enter the ID of the media to check out: ");

                CheckoutLog newLog = new CheckoutLog
                {
                    BorrowerID = borrower.BorrowerID,
                    MediaID = mediaID,
                    CheckoutDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7),
                    ReturnDate = null
                };

                var checkoutResult = serivce.CheckoutMedia(newLog);
                if (!checkoutResult.Ok)
                {
                    Console.WriteLine(checkoutResult.Message);
                }
                else
                {
                    Console.WriteLine($"New checkout log registered with ID: {checkoutResult.Data}");
                }

                exitChoice = Utilities.GetCheckoutOption();
            } 
        }

        public static void Return(ICheckoutService serivce)
        {
            Console.Clear();

            Borrower borrower;
            string email = Utilities.GetRequiredString("Enter Borrower's Email: ");
            var getBorrowerResult = serivce.GetBorrowerByEmail(email);

            if (!getBorrowerResult.Ok)
            {
                Console.WriteLine(getBorrowerResult.Message);
                Utilities.AnyKey();
                return;
            }

            borrower = getBorrowerResult.Data;

            int returnOption = 0;
            while (returnOption != 2)
            {
                var getCheckoutLogResult = serivce.GetCheckedOutMediaByBorrowerID(borrower.BorrowerID);
                if (!getCheckoutLogResult.Ok)
                {
                    Console.WriteLine(getCheckoutLogResult.Message);
                    Utilities.AnyKey();
                    return;
                }

                var checkoutLogList = getCheckoutLogResult.Data;

                Utilities.PrintBorrowersCheckedoutMedia(checkoutLogList);

                int logID = Utilities.GetCheckoutLogID(checkoutLogList, "Enter the Log ID to return: ");
                var returnResult = serivce.ReturnMedia(logID);
                if (returnResult.Ok)
                {
                    Console.WriteLine("Return successfull.");
                }
                else
                {
                    Console.WriteLine(returnResult.Message);
                }

                if (checkoutLogList.Count == 1)
                {
                    break;
                }
                else
                {
                    returnOption = Utilities.GetReturnOption();
                }
            }

            Utilities.AnyKey();
        }

        public static void CheckoutLog(ICheckoutService serivce)
        {
            Console.Clear();

            var getMediaResult = serivce.GetAllCheckedoutMedia();

            if (!getMediaResult.Ok)
            {
                Console.WriteLine(getMediaResult.Message);
            }
            else
            {
                Utilities.PrintCheckoutLogList(getMediaResult.Data);
            }

            Utilities.AnyKey();
        }
    }
}
