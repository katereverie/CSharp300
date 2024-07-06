namespace RestaurantGuestGame
{
    public static class ConsoleIO
    {
        public static void DisplayMenu()
        {
            Console.WriteLine("\n---- Main Menu ----");
            Console.WriteLine("1. Register Party");
            Console.WriteLine("2. Call Next Party");
            Console.WriteLine("3. Remove Party");
            Console.WriteLine("4. List All Parties");
            Console.WriteLine("5. View On-Deck Party");
            Console.WriteLine("6. Exit\n");
        }

        public static Party GetParty()
        {
            Party p = new Party();

            Console.WriteLine("---- Register Party ----");

            p.Name = GetPartyName();
            p.GuestCount = GetGuestCount();
            
            return p;
        }

        public static void CallNextParty(Party p)
        {
            Console.WriteLine("---- Call Party ----");
            Console.WriteLine($"Calling {p.Name} ... Party of {p.GuestCount}");
        }

        public static string GetPartyName()
        {
            do
            {
                Console.Write("Enter the Name for the Party: ");
                string name = Console.ReadLine().Trim().ToLower();

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Name for the Party cannot be empty. Please enter again.");
                    continue;
                }

                // Capitalize first letter of party name
                
                char firstLetter = char.ToUpper(name[0]);

                return firstLetter + name.Substring(1);

            } while (true);
        }

        public static int GetGuestCount()
        {
            do
            {
                Console.Write("Enter the Number of Guests of the Party: ");
                if (int.TryParse(Console.ReadLine(), out int guestCount))
                {
                    switch (guestCount)
                    {
                        case <= 0:
                            Console.WriteLine("Number of Guests cannot be zero or negative. Please enter again.");
                            continue;
                        default:
                            return guestCount;
                    }
                }

            } while (true);
        }

        public static void DisplayWaitList(Queue<Party> waitList)
        {
            Console.WriteLine("---- Wait List ----");

            int index = 1;

            foreach (Party p in waitList)
            {
                Console.WriteLine($"{index}. Party Name: {p.Name} | Guest Number: {p.GuestCount}");
                index++;
            }
        }

        public static void DisplayNextParty(Party nextP)
        {
            Console.WriteLine("---- Next Party ----");
            Console.WriteLine($"Next: | Party Name: {nextP.Name} | Guest Count: {nextP.GuestCount} |");
        }

        public static void AnyKey()
        {
            Console.Write("\nPress any key to continue ...");
            Console.ReadKey();
        }
    }
}
