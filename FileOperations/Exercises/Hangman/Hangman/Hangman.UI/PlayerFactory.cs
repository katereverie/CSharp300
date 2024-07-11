using Hangman.UI.Interfaces;
using Hangman.UI.Players;
using Hangman.UI.WordSources;

namespace Hangman.UI
{
    public static class PlayerFactory
    {
        public static PlayerType GetPlayerType(int playerNumber)
        {
            do
            {
                Console.Write($"Choose Player Type for Player {playerNumber}:\n1. Human\n2. Computer\nEnter 1 or 2: ");
                if (int.TryParse(Console.ReadLine(), out int playerType))
                {
                    switch (playerType)
                    {
                        case 1:
                            return PlayerType.Human;
                        case 2:
                            return PlayerType.Computer;
                        default:
                            Console.WriteLine("Invalid Choice. Please enter either 1 or 2.");
                            continue;
                    }
                }

            } while (true);

        }

        public static string GetPlayerName(int playerNumber)
        {
            do
            {
                Console.Write($"Enter Name for Player {playerNumber}: ");
                string? playerName = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(playerName))
                {
                    Console.WriteLine("Invalid Name. Please do not leave Player Name empty.");
                    continue;
                }

                return playerName;

            } while (true);

        }

        public static IWordSource GetWordSource(string playerName)
        {
            Console.WriteLine($"{playerName}, Your life is out of your hands.\nBut, your words aren't\n");
            Console.WriteLine("1. My word, my choice.");
            Console.WriteLine("2. I surrender my choice to the God of Chance.\n");

            do
            {
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice != 1 && choice != 2)
                    {
                        Console.WriteLine("Invalid choice. Please enter either 1 or 2.\n");
                        continue;
                    }

                    return choice == 1 ? new PlayerIO() : new Dict();
                }

            } while (true);
        }

        public static IPlayer? InitializePlayer(int playerNumber)
        {
            PlayerType playerType = GetPlayerType(playerNumber);
            string playerName = GetPlayerName(playerNumber);
            IWordSource playerWordSource;

            switch (playerType)
            {
                case PlayerType.Human:
                    playerWordSource = GetWordSource(playerName);
                    return new HumanPlayer(playerName, playerWordSource);
                case PlayerType.Computer:
                    Console.WriteLine($"The God of Chance will bless {playerName} with a destined word.");
                    return new ComputerPlayer(playerName);
                default:
                    return null;
            }
        }
    }
}
