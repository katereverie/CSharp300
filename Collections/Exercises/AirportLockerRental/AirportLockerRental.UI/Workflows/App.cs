using AirportLockerRental.UI.Actions;
using AirportLockerRental.UI.DTOs;
using AirportLockerRental.UI.Storage;

namespace AirportLockerRental.UI.Workflows
{
    public static class App
    {
        public static void Run()
        {
            ILockerRepository storage = ConsoleIO.GetStorageType();

            while (true)
            {
                int choice = ConsoleIO.GetMenuOption();

                if(choice == 5)
                {
                    return;
                }
                else if(choice == 4)
                {
                    storage.ListContents();
                }
                else
                {
                    // we need a locker number for these three choices
                    int lockerNumber = ConsoleIO.GetLockerNumber(storage.Capacity);

                    if(choice == 1)
                    {
                        var contents = storage.GetLockerContents(lockerNumber);
                        if (contents == null)
                        {
                            Console.WriteLine("This Locker is empty.");
                        }
                        else
                        {
                            ConsoleIO.DisplayLockerContents(contents);
                        }
                    }
                    else if(choice == 2)
                    {
   
                        LockerContents contents = ConsoleIO.GetLockerContentsFromUser();
                        contents.LockerNumber = lockerNumber;

                        if (storage.AddContents(contents))
                        {
                            Console.WriteLine($"Locker {lockerNumber} is rented, stop by later to pick up your stuff!");
                        }
                        else
                        {
                            Console.WriteLine($"Sorry, but locker {lockerNumber} has already been rented!");
                        }
                        
                    }
                    else
                    {
                        LockerContents? contents = storage.RemoveContents(lockerNumber);

                        if(contents == null)
                        {
                            Console.WriteLine($"Locker {lockerNumber} is not currently rented.");
                        }
                        else
                        {
                            Console.WriteLine($"Locker {lockerNumber} rental has ended, please take your {contents.Description}.");
                        }
                    }
                }

                ConsoleIO.AnyKey();
            }
        }
    }
}
