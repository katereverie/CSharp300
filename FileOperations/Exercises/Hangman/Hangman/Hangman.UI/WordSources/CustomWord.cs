using Hangman.BLL.Interfaces;

namespace Hangman.UI.WordSources
{
    public class CustomWord : IWordSource
    {
        /// <summary>
        /// evaluates string input against being empty, or numeric, or consisting of multiple words.
        /// </summary>
        /// <returns>a single non-numeric, non-empty, word</returns>
        public string GetWord()
        {
            while (true)
            {
                Console.Write("Enter Word: ");
                string? word = Console.ReadLine();

                if (string.IsNullOrEmpty(word))
                {
                    Console.WriteLine("Your word mustn't be empty.");
                    continue;
                }
                else if (int.TryParse(word, out int nonword))
                {
                    Console.WriteLine("Your word musn't be numeric.");
                    continue;
                }
                else if (word.Trim().Split(' ').Length != 1)
                {
                    Console.WriteLine("Your word mustn't contain space in between.");
                    continue;
                }

                return word.ToLower();
            }
        }
    }
}
