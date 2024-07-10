using Hangman.UI.Interfaces;
using Hangman.UI.WordSources;

namespace Hangman.UI.Players
{
    public class ComputerPlayer : IPlayer
    {
        public string Name { get; }
        public bool IsHuman { get; }

        public ComputerPlayer(string name)
        {
            Name = name;
            IsHuman = false;
        }

        public string GetGuess()
        {
            char[] alphabet = new char[26];
            Random letterPicker = new Random();

            for (int i = 0; i < 26; i++)
            {
                alphabet[i] = (char)('A' + i);
            }

            return $"{alphabet[letterPicker.Next(0, 26)]}";
        }

        public string? GetWord()
        {
            Dictionary dict = new Dictionary();
            return dict.GetWord();
        }
    }
}
