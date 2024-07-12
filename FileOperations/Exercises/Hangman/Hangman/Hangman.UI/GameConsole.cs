using Hangman.BLL;
using Hangman.UI.Interfaces;
using Hangman.UI.WordSources;

namespace Hangman.UI
{
    public static class GameConsole
    {
        public static PlayerType GetPlayerType(int playerNumber)
        {
            do
            {
                Console.Write($"\nChoose Player Type for Player {playerNumber}:\n1. Human\n2. Computer\nEnter 1 or 2: ");
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
            Console.WriteLine($"{playerName}, Your life is out of your hands.\nBut, you may choose your last word.\n");
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

                    return choice == 1 ? new CustomWord() : new DefaultDictionary();
                }

            } while (true);
        }

        public static string GetGuess()
        {
            while (true)
            {
                Console.Write("Enter Guess: ");
                string? guess = Console.ReadLine();

                if (string.IsNullOrEmpty(guess))
                {
                    Console.WriteLine("Invalid: Empty Guess.");
                    continue;
                }
                else if (int.TryParse(guess, out int nonguess))
                {
                    Console.WriteLine("Invalid: Numeric Guess.");
                    continue;
                }
                else if (guess.Trim().Split(' ').Length != 1)
                {
                    Console.WriteLine("Invalid: Space in Guess.");
                    continue;
                }

                return guess;
            }
        }

        public static void PrintGameState(GameState state)
        {
            Console.WriteLine($"Strikes remaining: {state.StrikesLeft}");
            Console.WriteLine($"Previous Guesses: {string.Join(",", state.GuessRecord)}\n");
            // as long as any previous guesses contain any letter of the word, print it out
            string? word = state.Word;
            var guesses = state.GuessRecord;

            Console.Write("Word: ");
            for (int i = 0; i < word?.Length; i++)
            {
                if (guesses.Contains(word[i].ToString()))
                {
                    Console.Write(word[i]);
                }

                Console.Write("- ");
            }
        }

        public static void AnyKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void PrintScores(Dictionary<string, int> playerScores)
        {
            Console.WriteLine(new string('=', 10) + "Score Board" + new string('=', 10));
            foreach (var playerScore in playerScores)
            {
                Console.WriteLine($"{playerScore.Key} - {playerScore.Value}");
            }
        }

        public static bool Continue()
        {
            do
            {
                Console.Write("\nPlay another round?\nEnter 'y' for yes or 'n' for no: ");
                string? userInput = Console.ReadLine()?.Trim().ToLower();

                if (char.TryParse(userInput, out char answer))
                {
                    switch (answer)
                    {
                        case 'y':
                            return true;
                        case 'n':
                            return false;
                        default:
                            Console.WriteLine($"{answer} is not a valid option.");
                            continue;
                    }
                }

                Console.WriteLine("Invalid input.");
                
            } while (true);

        }
    }
}
