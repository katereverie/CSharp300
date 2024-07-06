using AirportLockerRental.UI.DTOs;
using AirportLockerRental.UI.Storage;
using System.Text.RegularExpressions;

namespace AirportLockerRental.UI.Actions
{
    public static class ConsoleIO
    {
        public static string GetRequiredString(string prompt)
        {
            do
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if(!string.IsNullOrEmpty(input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("This data is required.");
                    AnyKey();
                }
            } while (true);
        }

        public static void DisplayLockerContents(LockerContents contents)
        {
            if (contents == null)
            {
                Console.WriteLine("=====================================");
                Console.WriteLine($"Locker #: {contents?.LockerNumber}");
                Console.WriteLine($"Renter Name: N/A");
                Console.WriteLine($"Contents: Empty");
                Console.WriteLine("=====================================");
            }
            else
            {
                Console.WriteLine("=====================================");
                Console.WriteLine($"Locker #: {contents.LockerNumber}");
                Console.WriteLine($"Renter Name: {contents.RenterName}");
                Console.WriteLine($"Contents: {contents.Description}");
                Console.WriteLine("=====================================");
            }
        }

        public static int GetLockerNumber(int capacity)
        {
            int lockerNumber;

            do
            {
                Console.Write($"Enter locker number (1-{capacity}): ");
                if (int.TryParse(Console.ReadLine(), out lockerNumber))
                {
                    if (lockerNumber >= 1 && lockerNumber <= capacity)
                    {
                        return lockerNumber;
                    }

                    Console.WriteLine($"Invalid choice. Please enter a number between 1 and {capacity}.");
                }
            } while (true);
        }

        public static int GetMenuOption()
        {
            int userChoice;

            Console.Clear();

            do
            {
                Console.Clear();
                Console.WriteLine("Airport Locker Rental Menu");
                Console.WriteLine("=============================");
                Console.WriteLine("1. View a locker");
                Console.WriteLine("2. Rent a locker");
                Console.WriteLine("3. End a locker rental");
                Console.WriteLine("4. List all locker contents");
                Console.WriteLine("5. Quit");
                Console.Write("\nEnter your choice (1-5): ");

                if (int.TryParse(Console.ReadLine(), out userChoice))
                {
                    if (userChoice >= 1 && userChoice <= 5)
                    {
                        return userChoice;
                    }

                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    AnyKey();
                }
            } while (true);
        }

        public static void AnyKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static LockerContents GetLockerContentsFromUser()
        {
            LockerContents contents = new LockerContents();

            contents.RenterName = GetRequiredString("Enter your name: ");
            contents.Description = GetRequiredString("Enter the item you want to store in the locker: ");

            return contents;
        }

        public static int GetCapacity()
        {
            while (true)
            {
                Console.Write("Enter storage capacity: ");

                if (int.TryParse(Console.ReadLine(), out int capacity))
                {
                    if (capacity > 0)
                    {
                        return capacity;
                    }

                    Console.WriteLine("Capacity must be positive and greater than zero.");
                }
            }
        }

        public static ILockerRepository GetStorageType()
        {
            Console.WriteLine("Choose storage type");
            Console.WriteLine("===================");
            Console.WriteLine("1. Array");
            Console.WriteLine("2. Dictionary");

            do
            {
                Console.Write("\nEnter choice (e.g. 1): ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice != 1 && choice != 2)
                    {
                        Console.WriteLine("Please enter either 1 or 2.");
                        continue;
                    }
                }

                int capacity = GetCapacity();

                switch (choice)
                {
                    case 1:
                        ArrayLockerRepository arrayStorage = new ArrayLockerRepository(capacity);
                        return arrayStorage;
                    case 2:
                        DictionaryLockerRepository dictionaryStorage = new(capacity);
                        return dictionaryStorage;
                }

            } while (true);
            
        }
    }
}
