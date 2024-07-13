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
                switch (WordPicker.IsHuman)
                {
                    case true:
                        Console.WriteLine($"{WordPicker.Name}, pick a word to guess. {WordGuesser.Name}, look away!\n");
                        break;
                    default:
                        Console.WriteLine($"{WordPicker.Name} has picked a word.");
                        break;
                }
                
                string word = WordPicker.WordSource.GetWord()?? "";
                string? guess = null;
                int matchTotal = 0;
                MatchResult? result = null;
                GameConsole.AnyKey();

                // loop runs until no strikes left or a full match or match count equals to length of word has been found
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
                                    Console.WriteLine($"{WordGuesser.Name} has struck out, {WordPicker.Name} wins!");
                                    Console.WriteLine($"The word was: {word}");
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
                            Console.WriteLine($"{matchCount} matches!");
                            break;
                        default:
                            Console.WriteLine($"Congradulation! {WordGuesser.Name} won!");
                            state.PlayerScores[WordGuesser.Name]++;
                            break;
                    }

                    GameConsole.AnyKey();

                    // round continues until guesser either wins or loses
                } while (!mgr.CheckGuesserWin(matchTotal, word.Length, result) && !mgr.CheckGuesserLose(state.StrikesLeft));

                state.Reset();
                WordGuesser = SwitchRole(WordGuesser);
                WordPicker = SwitchRole(WordPicker);

                GameConsole.PrintScores(state.PlayerScores);

            } while (GameConsole.Continue());

        }
    }
}