namespace Hangman.BLL
{
    public class GameData
    {
        public List<string> GuessRecord { get; set; }
        public Dictionary<string, int> Player1Scores { get; set; }
        public Dictionary<string, int> Player2Scores { get; set; }
        public string? Word { get; set; }

        public GameData(string p1Name, string p2Name)
        {
            GuessRecord = new List<string>();
            Player1Scores = new Dictionary<string, int>
            {
                {p1Name, 0}
            };
            Player2Scores = new Dictionary<string, int>
            {
                {p2Name, 1}
            };
        }
    }
}
