namespace Hangman.BLL.Interfaces
{
    public interface IPlayer
    {
        string Name { get; } 
        bool IsHuman {  get; } 
        IWordSource WordSource { get; }
        string GetGuess(); 
    }
}
