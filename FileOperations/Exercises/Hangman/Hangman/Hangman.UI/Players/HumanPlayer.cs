using Hangman.UI.Interfaces;
using Hangman.UI.WordSources;

namespace Hangman.UI.Players
{
    public class HumanPlayer : IPlayer
    {
        public string Name { get; }
        public bool IsHuman { get; }

        public HumanPlayer(string name)
        {
            Name = name;
            IsHuman = true;
        }

        public string GetGuess()
        {
            while (true)
            {
                string? guess = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(guess))
                {
                    Console.WriteLine("Your guess mustn't be empty.");
                    continue;
                }

                return guess;
            }

        }

        public string GetWord()
        {
            ConsoleIO pickWord = new ConsoleIO();
            return pickWord.GetWord();
        }
    }
}
