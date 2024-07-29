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
            var getResult = serivce.GetBorrowerByEmail(email);

            if (getResult.Data != null && getResult.Ok)
            {
                Console.WriteLine("Borrower found.");
            
                do
                {
                    var canBorrowerCheckout = serivce.CanBorrowerCheckout(getResult.Data.BorrowerID);
                    if (canBorrowerCheckout.Ok)
                    {
                        var logsResult = serivce.GetAllUncheckedoutUnarchivedMedia();

                        if (logsResult.Data != null && logsResult.Data.Any())
                        {
                            Console.WriteLine($"{"Media ID",-20} {"Title",-30}");
                            Console.WriteLine(new string('=', 60));
                            foreach (var log in logsResult.Data)
                            {
                                Console.WriteLine($"{log.MediaID,-20} " +
                                                  $"{log.Title,-30}");
                            }

                            Console.WriteLine();

                            do
                            {
                                int mediaID = Utilities.GetPositiveInteger("Enter the ID of the media to check out: ");
                                var getMediaResult = serivce.GetMediaByID(mediaID);

                                if (getMediaResult.Data != null && getMediaResult.Ok)
                                {
                                    CheckoutLog newLog = new CheckoutLog();
                                    newLog.BorrowerID = getResult.Data.BorrowerID;
                                    newLog.MediaID = mediaID;
                                    newLog.CheckoutDate = DateTime.Now;
                                    newLog.DueDate = newLog.CheckoutDate.AddDays(7);
                                    newLog.ReturnDate = null;

                                    var checkoutResult = serivce.CheckoutMedia(newLog);
                                    if (checkoutResult.Ok)
                                    {
                                        Console.WriteLine($"New checkout log registered with ID: {checkoutResult.Data}");
                                    }
                                    else
                                    {
                                        Console.WriteLine(checkoutResult.Message);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(getMediaResult.Message);
                                }

                                break;

                            } while (true);
                        }
                        else
                        {
                            Console.WriteLine(logsResult.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine(canBorrowerCheckout.Message);
                    }

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
            else
            {
                Console.WriteLine(getResult.Message);
            }

            Utilities.AnyKey();
        }

        public static void Return(ICheckoutService serivce)
        {
            Console.Clear();

            string email = Utilities.GetRequiredString("Enter Borrower's Email: ");
            var getResult = serivce.GetBorrowerByEmail(email);

            if (getResult.Data != null && getResult.Ok)
            {
                Console.WriteLine("Borrower found.");

                var getLogsResult = serivce.GetCheckedOutMediaByBorrowerID(getResult.Data.BorrowerID);
                if (getLogsResult.Data != null && getLogsResult.Ok)
                {
                    Console.WriteLine($"{"Log ID", -10} {"Title", -30}");
                    Console.WriteLine(new string('=', 50));

                    foreach (var log in getLogsResult.Data)
                    {
                        Console.WriteLine($"{log.CheckoutLogID,-10} " +
                                          $"{log.Title,-30} ");
                    }

                    Console.WriteLine();

                    do
                    {
                        int logID = Utilities.GetPositiveInteger("Enter the Log ID to return: ");

                        var returnResult = serivce.ReturnMedia(logID);

                        if (returnResult.Ok)
                        {
                            Console.WriteLine("Return successful.");
                        }
                        else
                        {
                            Console.WriteLine(returnResult.Message);
                        }

                        break;

                    } while (true);
                    
                }
                else
                {
                    Console.WriteLine(getLogsResult.Message);
                }
            }
            else
            {
                Console.WriteLine(getResult.Message);
            }

            Utilities.AnyKey();
        }

        public static void CheckoutLog(ICheckoutService serivce)
        {
            Console.Clear();

            var result = serivce.GetAllCheckedoutMedia();

            if (result.Ok && result.Data != null)
            {
                Console.WriteLine($"{"Name", -20} {"Email", -30} {"Title", -30} {"Checkout Date", -20} {"Due Date", -20}");
                Console.WriteLine(new string('=', 120));

                foreach (var log in result.Data)
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
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }
    }
}
