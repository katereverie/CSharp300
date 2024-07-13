namespace Hangman.BLL
{
    public class GameManager
    {
        public bool CheckDuplicateGuess(List<string> guessRecord, string guess)
        {
            return guessRecord.Contains(guess);
        }

        // word and guess are guaranteed to be clean: all lowercase and etc. because they have been handled by IO-related methods.
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

        public bool CheckGuesserWin(int matchCount, int wordLength, MatchResult? result)
        {
            return matchCount == wordLength || result == MatchResult.FullMatch;
        }

        public bool CheckGuesserLose(int strikesLeft)
        {
            return strikesLeft == 0;
        }
    }
}
