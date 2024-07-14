using Hangman.UI.Interfaces;
using Hangman.UI.WordSources;

namespace Hangman.UI.Players
{
    public class ComputerPlayer : IPlayer
    {
        private static Random _letterPicker = new Random();
        private static char[] _alphabet = new char[26];
        private static List<char> _guessedLetter = new List<char>();
        public string Name { get; }
        public bool IsHuman { get; } = false;
        public IWordSource WordSource { get; } = new DefaultDictionary();
        

        public ComputerPlayer(string name)
        {
            Name = name;
            for (int i = 0; i < 26; i++)
            {
                _alphabet[i] = (char)('A' + i);
            }
        }

        public string GetGuess()
        {
            char guess;

            do
            {
                guess = _alphabet[_letterPicker.Next(0, 26)];

                if (_guessedLetter.Contains(guess))
                {
                    continue;
                }

                _guessedLetter.Add(guess);

                Console.WriteLine($"\n{Name}'s guess is: {guess.ToString().ToLower()}");
                return guess.ToString().ToLower();

            } while (true);

        }
    }
}
