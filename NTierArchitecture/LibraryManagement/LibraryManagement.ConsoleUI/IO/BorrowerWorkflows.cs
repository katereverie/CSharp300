using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.ConsoleUI.IO
{
    public static class BorrowerWorkflows
    {
        public static void ListAllBorrowers(IBorrowerService service)
        {
            Console.Clear();

            var result = service.GetAllBorrowers();

            if (!result.Ok)
            {
                Console.WriteLine(result.Message);
                Utilities.AnyKey();
                return;
            }
            else if (result.Data is not null && result.Data.Any())
            {
                Console.WriteLine($"{"ID",-5} {"Name",-32} Email");
                Console.WriteLine(new string('=', 70));
                foreach (var b in result.Data)
                {
                    Console.WriteLine($"{b.BorrowerID,-5} {b.LastName + ", " + b.FirstName,-32} {b.Email}");
                }
            }
            else
            {
                Console.WriteLine("Currently, there's no registered borrower.");
            }

            Utilities.AnyKey();
        }

        public static void ViewBorrower(IBorrowerService service)
        {
            Console.Clear();
            var email = Utilities.GetRequiredString("Enter borrower email: ");
            var getBorrowerResult = service.GetBorrower(email);

            if (!getBorrowerResult.Ok)
            {
                Console.WriteLine(getBorrowerResult.Message);
            }
            else
            {
                Console.WriteLine($"Id: {getBorrowerResult.Data.BorrowerID}");
                Console.WriteLine($"Name: {getBorrowerResult.Data.LastName}, {getBorrowerResult.Data.FirstName}");
                Console.WriteLine($"Email: {getBorrowerResult.Data.Email}");

                var getCheckoutLogsResult = service.GetCheckoutLogsByBorrower(getBorrowerResult.Data);
                if (!getCheckoutLogsResult.Ok)
                {
                    Console.WriteLine(getCheckoutLogsResult.Message);
                }
                else 
                {
                    Console.WriteLine("Checkout Record");
                    Console.WriteLine(new string('=', 100));
                    Console.WriteLine($"{"Media ID",-10} {"Title",-40} {"Checkout Date",-20} {"Return Date",-20}");
                    foreach (var cl in getCheckoutLogsResult.Data)
                    {
                        Console.WriteLine($"{cl.MediaID,-10} " +
                            $"{cl.Media.Title,-40} " +
                            $"{cl.CheckoutDate,-20:MM/dd/yyyy} " +
                            $"{(cl.ReturnDate is null ? "Unreturned" : cl.ReturnDate),-20:MM/dd/yyyy}");
                    }

                    Console.WriteLine();
                }
            }

            Utilities.AnyKey();
        }

        public static void AddBorrower(IBorrowerService service)
        {
            Console.Clear();

            Borrower newBorrower = new Borrower();

            newBorrower.FirstName = Utilities.GetRequiredString("First Name: ");
            newBorrower.LastName = Utilities.GetRequiredString("Last Name: ");
            newBorrower.Email = Utilities.GetRequiredString("Email: ");
            newBorrower.Phone = Utilities.GetRequiredString("Phone: ");

            var addResult = service.AddBorrower(newBorrower);

            if (addResult.Ok)
            {
                Console.WriteLine($"New Borrower successfully registered with the ID {addResult.Data}");
            }
            else
            {
                Console.WriteLine(addResult.Message);
            }

            Utilities.AnyKey();
        }

        public static void EditBorrower(IBorrowerService service)
        {
            Console.Clear();

            var getBorrowerResult = service.GetBorrower(Utilities.GetRequiredString("Enter the Email of the Borrower to be edited: "));

            if (!getBorrowerResult.Ok || getBorrowerResult.Data == null)
            {
                Console.WriteLine(getBorrowerResult.Message);    
            }
            else
            {
                int option = 0;

                do
                {
                    Console.WriteLine("\nBorrower match found.\nHere's a list of edit options.");
                    Menus.DisplayEditBorrowerOptions();
                    option = Utilities.GetPositiveInteger("Enter edit options (1-5) or return (6): ");

                    if (option >= 1 && option <= 6)
                    {
                        break;
                    }

                    Console.WriteLine("Invalid option.");
                    Utilities.AnyKey();
                    continue;

                } while (true);

                if (option == 6)
                {
                    return;
                }
                else if (option == 5)
                {
                    getBorrowerResult.Data.FirstName = Utilities.GetRequiredString("Enter new first name: ");
                    getBorrowerResult.Data.LastName = Utilities.GetRequiredString("Enter new last name:");
                    do
                    {
                        string newEmail = Utilities.GetRequiredString("Enter new Email address: ");
                        var duplicateResult = service.GetBorrower(newEmail);
                        if (duplicateResult.Data == null)
                        {
                            getBorrowerResult.Data.Email = newEmail;
                            break;
                        } 
                        else
                        {
                            Console.WriteLine($"{newEmail} has already been taken.");
                        }
                    } while (true);

                    getBorrowerResult.Data.Phone = Utilities.GetRequiredString("Enter new phone number:");
                }
                else if (option == 4)
                {
                    getBorrowerResult.Data.Phone = Utilities.GetRequiredString("Enter new phone number: ");
                }
                else if (option == 3)
                {
                    do
                    {
                        string newEmail = Utilities.GetRequiredString("Enter new Email address: ");
                        var duplicateResult = service.GetBorrower(newEmail);
                        if (duplicateResult.Data == null)
                        {
                            getBorrowerResult.Data.Email = newEmail;
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"{newEmail} has already been taken.");
                        }
                    } while (true);
                }
                else
                {
                    switch (option)
                    {
                        case 1:
                            getBorrowerResult.Data.FirstName = Utilities.GetRequiredString("Enter new first name: ");
                            break;
                        case 2:
                            getBorrowerResult.Data.LastName = Utilities.GetRequiredString("Enter new last name:");
                            break;
                    }
                }

                var finalResult = service.UpdateBorrower(getBorrowerResult.Data);

                if (finalResult.Ok)
                {
                    Console.WriteLine($"Borrower successfully edited.");
                }
                else
                {
                    Console.WriteLine(finalResult.Message);
                }
            }

            Utilities.AnyKey();
        }

        public static void DeleteBorrower(IBorrowerService service)
        {
            Console.Clear();
            var getResult = service.GetBorrower(Utilities.GetRequiredString("Enter the Email of the borrower to be deleted: "));

            if (getResult.Ok && getResult.Data != null)
            {
                Console.WriteLine($"Borrower with matching Email found.");
                int choice = 0;

                do
                {
                    Console.WriteLine("Deleting a borrower will result in erasing all information related to the borrower.");
                    Console.WriteLine("Are you sure you'd like to proceed?\n1. Proceed\n2. Cancel");
                    choice = Utilities.GetPositiveInteger("Enter choice: ");
                    if (choice != 1 && choice != 2)
                    {
                        Console.WriteLine("Invalid choice. Please enter either 1 or 2.");
                        continue;
                    }
                    break;
                } while (true);

                switch (choice)
                {
                    case 1:
                        try
                        {
                            var deleteResult = service.DeleteBorrower(getResult.Data);
                            Console.WriteLine("Borrower successfully deleted.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 2:
                        Console.WriteLine("Delete Process cancelled.");
                        break;
                }
            }
            else
            {
                Console.WriteLine(getResult.Message);
            }

            Utilities.AnyKey();
        }
    }
}
