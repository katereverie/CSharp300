using Hangman.BLL.Interfaces;

namespace Hangman.Tests.WordSources
{
    public class DefaultWordSource : IWordSource
    {
        public string GetWord()
        {
            return "hangman";
        }
    }
}
