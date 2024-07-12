using Hangman.BLL;
using Hangman.UI.Interfaces;


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
            if (p == _p1)
            {
                return _p2;
            }

            return _p1;
        }

        public void Run()
        {
            /* initialize State object, StateManager object
             * assign WordPicker and WordGuesser
             */
            GameState state = new GameState(_p1.Name, _p2.Name);
            GameManager mgr = new GameManager();
            IPlayer WordPicker = _p1;
            IPlayer WordGuesser = _p2;

            // loop runs until player quits
            do
            {
                Console.WriteLine($"{WordPicker.Name}, pick a word to guess. {WordGuesser.Name}, look away!\n");
                state.Word = WordPicker.WordSource.GetWord();
                MatchResult? result = null;
                Console.Clear();

                // loop runs until no strikes left or a full match has been found
                do
                {
                    GameConsole.PrintGameState(state);

                    string guess = WordGuesser.GetGuess() ?? "empty";
                    bool isDuplicate = mgr.CheckDuplicateGuess(state.GuessRecord, guess);
                    if (isDuplicate)
                    {
                        Console.WriteLine("Invalid: duplicate guess.");
                        continue;
                    }
                    result = mgr.CheckGuessMatch(state.Word, guess);
                    state.GuessRecord.Add(guess);
                    Console.WriteLine(guess);

                    switch (result)
                    {
                        case MatchResult.NoMatch:
                            state.StrikesLeft--;
                            switch (state.StrikesLeft)
                            {
                                case 0:
                                    Console.WriteLine($"{WordGuesser.Name} has struck out, {WordPicker.Name} wins!");
                                    state.PlayerScores[WordPicker.Name]++;
                                    break;
                                default:
                                    Console.WriteLine("No match found.");
                                    break;
                            }
                            break;
                        case MatchResult.PartialMatch:
                            int count = 0;
                            foreach (char letter in state.Word)
                            {
                                if (letter.ToString() == guess)
                                {
                                    count++;
                                }
                            }
                            Console.WriteLine($"{count} matches!");
                            break;
                        default:
                            Console.WriteLine($"Congradulation! {WordGuesser.Name} won!");
                            state.PlayerScores[WordGuesser.Name]++;
                            break;
                    }

                    GameConsole.AnyKey();
                    Console.Clear();

                } while (state.StrikesLeft != 0 && result != MatchResult.FullMatch);

                state.Reset();
                WordGuesser = SwitchRole(WordGuesser);
                WordPicker = SwitchRole(WordPicker);

                GameConsole.PrintScores(state.PlayerScores);

            } while (GameConsole.Continue());

        }
    }
}
