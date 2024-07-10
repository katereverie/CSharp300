namespace Hangman.BLL
{
    public class GameData
    {
        public Dictionary<string, int>? P1GuessRecord { get; private set; }
        public Dictionary<string, int>? P2GuessRecord { get; private set; }
        public string? Word { get; private set; }

        public GameData() { }
    }
}
