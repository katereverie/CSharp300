namespace Hangman.BLL
{
    public class GameState
    {
        public List<string> GuessRecord { get; set; }
        public Dictionary<string, int> PlayerScores { get; set; }
        public string? Word{ get; set; }
        public int StrikesLeft { get; set; }

        public GameState(string p1Name, string p2Name)
        {
            GuessRecord = new List<string>();
            // what if players name are duplicates?
            PlayerScores = new Dictionary<string, int>
            {
                {p1Name, 0},
                {p2Name, 0}
            };
            StrikesLeft = 5;
        }

        public void Reset()
        {
            GuessRecord = new List<string>();
            StrikesLeft = 5;
            Word = null;
        }
    }
}
