using Hangman.BLL;
using Hangman.Tests.WordSources;
using NUnit.Framework;

namespace Hangman.Tests
{
    [TestFixture]
    public class TestGameManager
    {
        private string _word = new DefaultWordSource().GetWord();
        private GameManager _mgr = new GameManager();

        [Test]
        public void DuplicateGuess()
        {
            List<string> guesses = new List<string> { "a", "b", "c" };

            var result1 = _mgr.CheckDuplicateGuess(guesses, "a");
            var result2 = _mgr.CheckDuplicateGuess(guesses, "d");

            Assert.That(result1, Is.True);
            Assert.That(result2, Is.False);
        }

        [Test]
        public void GuessMatch()
        {
            var result1 = _mgr.CheckGuessMatch(_word, "z");
            var result2 = _mgr.CheckGuessMatch(_word, "a");
            var result3 = _mgr.CheckGuessMatch(_word, "hangman");

            Assert.That(result1, Is.EqualTo(MatchResult.NoMatch));
            Assert.That(result2, Is.EqualTo(MatchResult.PartialMatch));
            Assert.That(result3, Is.EqualTo(MatchResult.FullMatch));
        }

        [Test]
        public void CheckGuesserWin()
        {
            int wordLength = _word.Length;
            var result1 = _mgr.CheckGuesserWin(3, wordLength, MatchResult.FullMatch);
            var result2 = _mgr.CheckGuesserWin(7, wordLength, MatchResult.PartialMatch);
            var result3 = _mgr.CheckGuesserWin(4, wordLength, MatchResult.NoMatch);

            Assert.That(result1, Is.True);
            Assert.That(result2, Is.True);
            Assert.That(result3, Is.False);
        }

        [Test]
        public void CheckGusserLose()
        {
            var result1 = _mgr.CheckGuesserLose(3);
            var result2 = _mgr.CheckGuesserLose(0);

            Assert.That(result1, Is.False);
            Assert.That(result2, Is.True);
        }
    }
}
