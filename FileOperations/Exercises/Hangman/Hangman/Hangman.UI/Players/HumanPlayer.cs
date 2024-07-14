using Hangman.UI.Interfaces;

namespace Hangman.UI.Players
{
    public class HumanPlayer : IPlayer
    {
        public string Name { get; }
        public bool IsHuman { get; } = true;
        public IWordSource WordSource { get; }

        public HumanPlayer(string name, IWordSource wordSource)
        {
            Name = name;
            WordSource = wordSource;
        }

        public string GetGuess()
        {
            return GameConsole.GetGuess();
        }
    }
}
