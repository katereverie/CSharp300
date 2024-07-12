using Hangman.UI.Interfaces;
using Hangman.UI.WordSources;

namespace Hangman.UI.Players
{
    public class HumanPlayer : IPlayer
    {
        public string Name { get; }
        public bool IsHuman { get; }
        public IWordSource WordSource { get; }

        public HumanPlayer(string name, IWordSource wordSource)
        {
            Name = name;
            IsHuman = true;
            WordSource = wordSource;
        }

        public string GetGuess()
        {
            return GameConsole.GetGuess();
        }
    }
}
