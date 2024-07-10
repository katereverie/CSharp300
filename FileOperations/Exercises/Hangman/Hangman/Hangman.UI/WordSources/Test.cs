using Hangman.UI.Interfaces;

namespace Hangman.UI.WordSources
{
    public class Test : IWordSource
    {
        public string GetWord()
        {
            return "Hangman";
        }
    }
}
