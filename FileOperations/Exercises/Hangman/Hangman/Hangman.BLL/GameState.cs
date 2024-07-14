namespace Hangman.BLL
{
    public class GameState
    {
        public List<string> GuessRecord { get; set; } = new List<string>();
        public Dictionary<string, int> PlayerScores { get; set; }
        public int Round { get; set; } = 0;
        public int StrikesLeft { get; set; } = 5;

        public GameState(string p1Name, string p2Name)
        {
            PlayerScores = new Dictionary<string, int>
            {
                {p1Name, 0},
                {p2Name, 0}
            };
        }

        public void Reset()
        {
            GuessRecord = new List<string>();
            StrikesLeft = 5;
        }
    }
}
