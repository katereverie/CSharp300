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
            }
            else if (result.Data.Any())
            {
                Utilities.PrintBorrowerList(result.Data);
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
                Utilities.PrintBorrowerInformation(getBorrowerResult.Data);
            }


            var getCheckoutLogsResult = service.GetCheckoutLogsByBorrower(getBorrowerResult.Data);
            if (!getCheckoutLogsResult.Ok)
            {
                Console.WriteLine(getCheckoutLogsResult.Message);
            }
            else 
            {
                Utilities.PrintBorrowerCheckoutLog(getCheckoutLogsResult.Data);
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

            if (!getBorrowerResult.Ok)
            {
                Console.WriteLine(getBorrowerResult.Message);    
            }
            else
            {
                Borrower b = getBorrowerResult.Data;
                int option = 0;

                while (true) 
                {
                    Console.WriteLine("\nYou have the following edit options.");
                    Menus.DisplayEditBorrowerOptions();
                    option = Utilities.GetPositiveInteger("Enter edit options (1-5) or return (6): ");

                    if (option >= 1 && option <= 6)
                    {
                        break;
                    }

                    Console.WriteLine("Invalid option.");
                    Utilities.AnyKey();
                }


                switch (option)
                {
                    case 1:
                        b.FirstName = Utilities.GetRequiredString("Enter new First Name: ");
                        break;
                    case 2:
                        b.LastName = Utilities.GetRequiredString("Enter new Last Name: ");
                        break;
                    case 3:
                        while (true)
                        {
                            string newEmail = Utilities.GetRequiredString("Enter new Email address: ");
                            var duplicateResult = service.GetBorrower(newEmail);
                            if (duplicateResult.Data == null)
                            {
                                b.Email = newEmail;
                                break;
                            }
                            Console.WriteLine($"{newEmail} has already been taken.");
                        }
                        break;
                    case 4:
                        b.Phone = Utilities.GetRequiredString("Enter new phone number: ");
                        break;
                    case 5:
                        b.FirstName = Utilities.GetRequiredString("Enter new First name: ");
                        b.LastName = Utilities.GetRequiredString("Enter new Last name:");
                        while (true)
                        {
                            string newEmail = Utilities.GetRequiredString("Enter new Email address: ");
                            var duplicateResult = service.GetBorrower(newEmail);
                            if (duplicateResult.Data == null)
                            {
                                b.Email = newEmail;
                                break;
                            }
                            Console.WriteLine($"{newEmail} has already been taken.");
                        }
                        b.Phone = Utilities.GetRequiredString("Enter new phone number:");
                        break;
                    case 6:
                        return;
                }

                var updateResult = service.UpdateBorrower(b);

                if (updateResult.Ok)
                {
                    Console.WriteLine($"Borrower successfully edited.");
                }
                else
                {
                    Console.WriteLine(updateResult.Message);
                }
            }

            Utilities.AnyKey();
        }

        public static void DeleteBorrower(IBorrowerService service)
        {
            Console.Clear();

            var getResult = service.GetBorrower(Utilities.GetRequiredString("Enter the Email of the borrower to be deleted: "));

            if (!getResult.Ok)
            {
                Console.WriteLine(getResult.Message);
            }
            else 
            {
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
                        var deleteResult = service.DeleteBorrower(getResult.Data);
                        if (deleteResult.Ok)
                        {
                            Console.WriteLine("Borrower successfully deleted.");
                        }
                        else
                        {
                            Console.WriteLine(deleteResult.Message);
                        }
                        break;
                    case 2:
                        Console.WriteLine("Delete Process cancelled.");
                        break;
                }
            }

            Utilities.AnyKey();
        }
    }
}
