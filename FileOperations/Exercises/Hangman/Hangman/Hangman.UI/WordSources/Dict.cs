using Hangman.UI.Interfaces;

namespace Hangman.UI.WordSources
{
    public class Dict : IWordSource
    {
        private Random _wordPicker = new Random();

        public string? GetWord()
        {
            string path = @"D:\GitHub\CloneRepository\C#300\FileOperations\Exercises\Hangman\Hangman\Data\dictionary.txt";

            try
            {
                if (File.Exists(path))
                {
                    string[] words = File.ReadAllLines(path);
                    if (words.Length > 0)
                    {
                        int index = _wordPicker.Next(0, words.Length);
                        return words[index];
                    }
                    else
                    {
                        return null; // No words found in the file
                    }
                }
                else
                {
                    return null; // File does not exist
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading dictionary file: {ex.Message}");
                return null; // Handle file IO exception
            }
        }

    }
}
