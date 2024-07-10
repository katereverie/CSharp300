namespace Hangman.UI.Interfaces
{
    public interface IPlayer
    {
        string? GetWord(); // not empty
        string? GetGuess(); // not empty
        string Name { get; } // not empty
        bool IsHuman {  get; } // must be assigned
    }
}
