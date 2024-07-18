using Hangman.BLL;
using Hangman.BLL.Interfaces;
using Hangman.UI.WordSources;

namespace Hangman.UI
{
    public class App
    {
        private IPlayer _p1;
        private IPlayer _p2;

        public App()
        {
            _p1 = PlayerFactory.InitializePlayer(1);
            _p2 = PlayerFactory.InitializePlayer(2);
        }

        public IPlayer SwitchRole(IPlayer p)
        {
            return p == _p1 ? _p2 : _p1;
        }

        public void Run()
        {
            // Run audio in a different thread
            Thread audioThread = new Thread(Audio.Play);
            audioThread.Start();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("All our times have come.\n");
            Console.ResetColor();
            GameConsole.AnyKey();


            /* initialize State object, StateManager object
             * assign WordPicker and WordGuesser
             */
            GameState state = new GameState(_p1.Name, _p2.Name);
            GameManager mgr = new GameManager();
            IPlayer WordPicker = _p1;
            IPlayer WordGuesser = _p2;

            // round loop runs until player quits
            do
            {
                state.Round++;
                string? guess = null;
                int matchTotal = 0;
                MatchResult? result = null;
                string word = WordPicker.WordSource.GetWord() ?? "";

                switch (WordPicker.WordSource)
                {
                    case CustomWord:
                        Console.WriteLine($"{WordPicker.Name}, pick a word to guess. {WordGuesser.Name} will look away!\n");
                        break;
                    default:
                        Console.WriteLine($"{WordPicker.Name} has received a word from the Hell Librarian.");
                        break;
                }

                GameConsole.AnyKey();

                // guess loop runs until no strikes are left or a full match or match count equals to length of picked word
                do
                {
                    GameConsole.PrintGameState(state, word);
                    GameConsole.PrintStages(state.StrikesLeft);

                    if (WordGuesser.IsHuman)
                    {
                        guess = WordGuesser.GetGuess();
                        bool isDuplicate = mgr.CheckDuplicateGuess(state.GuessRecord, guess);
                        if (isDuplicate)
                        {
                            Console.WriteLine("Invalid: duplicate guess.");
                            GameConsole.AnyKey();
                            continue;
                        }
                    }
                    else
                    {
                        guess = WordGuesser.GetGuess();
                    }

                    result = mgr.CheckGuessMatch(word, guess);
                    state.GuessRecord.Add(guess);

                    switch (result)
                    {
                        case MatchResult.NoMatch:
                            state.StrikesLeft--;
                            switch (state.StrikesLeft)
                            {
                                case 0:
                                    Console.Clear();
                                    GameConsole.PrintStages(state.StrikesLeft);
                                    Console.WriteLine($"{WordGuesser.Name} has struck out, {WordPicker.Name} wins!");
                                    Console.Write("The word was: ");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(word);
                                    Console.ResetColor();
                                    state.PlayerScores[WordPicker.Name]++;
                                    break;
                                default:
                                    Console.WriteLine("No match found.");
                                    break;
                            }
                            break;
                        case MatchResult.PartialMatch:
                            int matchCount = 0;
                            foreach (char letter in word)
                            {
                                if (letter.ToString() == guess)
                                {
                                    matchCount++;
                                    matchTotal++;
                                }
                            }
                            Console.WriteLine($"{matchCount} match(es)!");
                            break;
                        default:
                            Console.WriteLine($"Congratulations! {WordGuesser.Name} won!");
                            state.PlayerScores[WordGuesser.Name]++;
                            break;
                    }

                    GameConsole.AnyKey();

                    // guess loop continues until guesser either wins or loses
                } while (!mgr.CheckGuesserWin(matchTotal, word.Length, result) && !mgr.CheckGuesserLose(state.StrikesLeft));

                state.Reset();
                WordGuesser = SwitchRole(WordGuesser);
                WordPicker = SwitchRole(WordPicker);

                GameConsole.PrintScores(state.PlayerScores);

            } while (GameConsole.Continue());

            Audio.Stop();
            audioThread.Join();
        }
    }
}
