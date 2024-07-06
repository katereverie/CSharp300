using RestaurantGuestGame;

PartyManager partyMgr = new PartyManager();

do
{
    Console.Clear();
    Console.WriteLine("Guest Check-In System");
    ConsoleIO.DisplayMenu();

    Console.Write("Select Action From Menu: ");
    if (int.TryParse(Console.ReadLine(), out int option))
    {
        Console.Clear();

        switch (option)
        {
            case 1:
                partyMgr.AddParty(ConsoleIO.GetParty());
                Console.WriteLine("Party registered.");
                break;
            case 2:
                var nextParty = partyMgr.CallParty();
                switch (nextParty)
                {
                    case null:
                        Console.WriteLine("No party to call.");
                        break;
                    default:
                        ConsoleIO.DisplayNextParty(nextParty);
                        break;
                }
                break;
            case 3:
                bool isPartyRemoved = partyMgr.RemoveParty(ConsoleIO.GetPartyName());
                switch (isPartyRemoved)
                {
                    case true:
                        Console.WriteLine("Party removed.");
                        break;
                    case false:
                        Console.WriteLine("Party not found.");
                        break;
                }
                break;
            case 4:
                partyMgr.ListAllParties();
                break;
            case 5:
                partyMgr.ListNextParty();
                break;
            case 6:
                Console.WriteLine("Thank you for using Guest Check-In System.");
                return;
            default:
                Console.WriteLine("Invalid Action. Please enter from 1-6.");
                continue;
        }
    }

    ConsoleIO.AnyKey();

} while (true);