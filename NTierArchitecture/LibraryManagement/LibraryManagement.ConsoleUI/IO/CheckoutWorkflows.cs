using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.ConsoleUI.IO
{
    public static class CheckoutWorkflows
    {
        public static void Checkout(ICheckoutService serivce)
        {
            Console.Clear();

            string email = Utilities.GetRequiredString("Enter Borrower's Email: ");
            var getBorrowerResult = serivce.GetBorrowerByEmail(email);

            if (getBorrowerResult.Data == null || !getBorrowerResult.Ok)
            {
                Console.WriteLine(getBorrowerResult.Message);
                Utilities.AnyKey();
                return;
            }

            Console.WriteLine("Borrower found.");

            do
            {
                var checkResult = serivce.CheckBorrowStatus(getBorrowerResult.Data.BorrowerID);
                if (!checkResult.Ok)
                {
                    Console.WriteLine(checkResult.Message);
                    Utilities.AnyKey();
                    return;
                }

                var getMediaResult = serivce.GetAllUncheckedoutUnarchivedMedia();

                if (!getMediaResult.Ok)
                {
                    Console.WriteLine(getMediaResult.Message);
                    Utilities.AnyKey();
                    return;
                }

                Console.WriteLine($"{"Media ID",-20} {"Title",-30}");
                Console.WriteLine(new string('=', 60));
                foreach (var log in getMediaResult.Data)
                {
                    Console.WriteLine($"{log.MediaID,-20} " +
                                      $"{log.Title,-30}");
                }

                Console.WriteLine();

                int mediaID = 0;

                do
                {
                    mediaID = Utilities.GetPositiveInteger("Enter the ID of the media to check out: ");
                    bool hasMatch = getMediaResult.Data.Any(m => m.MediaID == mediaID);
                    if (!hasMatch)
                    {
                        Console.WriteLine("Please enter an available Media ID.");
                        continue;
                    }

                    break;
                } while (true);

                CheckoutLog newLog = new CheckoutLog
                {
                    BorrowerID = getBorrowerResult.Data.BorrowerID,
                    MediaID = mediaID,
                    CheckoutDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7),
                    ReturnDate = null
                };


                var checkoutResult = serivce.CheckoutMedia(newLog);
                if (!checkoutResult.Ok)
                {
                    Console.WriteLine(checkoutResult.Message);
                    Utilities.AnyKey();
                    continue;
                }

                Console.WriteLine($"New checkout log registered with ID: {checkoutResult.Data}");

                int exitChoice = 0;

                do
                {
                    Menus.DisplayCheckoutOptions();
                    int userChoice = Utilities.GetPositiveInteger("Enter choice (1-2): ");
                    if (userChoice >= 1 && userChoice <= 2)
                    {
                        exitChoice = userChoice;
                        break;
                    }
                    Console.WriteLine("Invalid choice.");
                } while (true);

                switch (exitChoice)
                {
                    case 1:
                        Utilities.AnyKey();
                        continue;
                    case 2:
                        return;
                }

            } while (true);
        }

        public static void Return(ICheckoutService serivce)
        {
            Console.Clear();

            string email = Utilities.GetRequiredString("Enter Borrower's Email: ");
            var getBorrowerResult = serivce.GetBorrowerByEmail(email);

            if (!getBorrowerResult.Ok)
            {
                Console.WriteLine(getBorrowerResult.Message);
                Utilities.AnyKey();
                return;
            }

            do
            {
                var getMediaResult = serivce.GetCheckedOutMediaByBorrowerID(getBorrowerResult.Data.BorrowerID);
                if (!getMediaResult.Ok)
                {
                    Console.WriteLine(getMediaResult.Message);
                    Utilities.AnyKey();
                    return;
                }

                Console.WriteLine($"{"Log ID",-10} {"Title",-30}");
                Console.WriteLine(new string('=', 50));

                foreach (var log in getMediaResult.Data)
                {
                    Console.WriteLine($"{log.CheckoutLogID,-10} " +
                                      $"{log.Title,-30} ");
                }

                Console.WriteLine();

                int logID;
                int exitChoice;
                bool hasMatch = false;

                do
                {
                    logID = Utilities.GetPositiveInteger("Enter the Log ID to return: ");

                    hasMatch = getMediaResult.Data.Any(dto => dto.CheckoutLogID == logID);
                    if (!hasMatch)
                    {
                        Console.WriteLine("Please enter an available checkout log ID.");
                        continue;
                    }
                    break;
                } while (true);

                var returnResult = serivce.ReturnMedia(logID);

                if (returnResult.Ok)
                {
                    Console.WriteLine("Return successful.");
                }
                else
                {
                    Console.WriteLine(returnResult.Message);
                }

                if (getMediaResult.Data.Count() == 1)
                {
                    break;
                }

                do
                {
                    Menus.DisplayReturnOptions();
                    exitChoice = Utilities.GetPositiveInteger("Enter choice (1-2): ");

                    switch (exitChoice)
                    {
                        case 1:
                            break;
                        case 2:
                            Utilities.AnyKey();
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            continue;
                    }
                    break;
                } while (true);

            } while (true);

            Utilities.AnyKey();
        }

        public static void CheckoutLog(ICheckoutService serivce)
        {
            Console.Clear();

            var getMediaResult = serivce.GetAllCheckedoutMedia();

            if (!getMediaResult.Ok)
            {
                Console.WriteLine(getMediaResult.Message);
                Utilities.AnyKey();
                return;
            }

            Console.WriteLine($"{"Name", -20} {"Email", -30} {"Title", -30} {"Checkout Date", -20} {"Due Date", -20}");
            Console.WriteLine(new string('=', 120));

            foreach (var log in getMediaResult.Data)
            {
                Console.Write($"{log.Borrower.LastName + ", " + log.Borrower.FirstName,-20} " +
                              $"{log.Borrower.Email,-30} " +
                              $"{log.Media.Title,-30} " +
                              $"{log.CheckoutDate,-20:MM/dd/yyyy} "); 
                if (log.DueDate < DateTime.Now)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{log.DueDate,-20:MM/dd/yyyy}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{log.DueDate,-20:MM/dd/yyyy}");
                }
            }

            Utilities.AnyKey();
        }
    }
}
