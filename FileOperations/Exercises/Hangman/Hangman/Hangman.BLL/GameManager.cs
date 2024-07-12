namespace Hangman.BLL
{
    public class GameManager
    {
        public bool CheckDuplicateGuess(List<string> guessRecord, string guess)
        {
            return guessRecord.Contains(guess);
        }

        public MatchResult CheckGuessMatch(string word, string guess)
        {
            switch (guess.Length)
            {
                case 1:
                    return word.Contains(guess)? MatchResult.PartialMatch : MatchResult.NoMatch;
                default:
                    return word == guess ? MatchResult.FullMatch : MatchResult.NoMatch;
            }
        }
    }
}
