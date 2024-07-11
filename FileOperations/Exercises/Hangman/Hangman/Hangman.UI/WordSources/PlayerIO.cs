using Hangman.UI.Interfaces;

namespace Hangman.UI.WordSources
{
    public class PlayerIO : IWordSource
    {
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
                else
                {
                    
                    var words = word.Trim().Split(' ');
                    if (words.Length != 1)
                    {
                        Console.WriteLine("Your word mustn't contain space in between.");
                        continue;
                    }
                    else
                    {
                        return word;
                    }
                }
            }
        }
    }
}
