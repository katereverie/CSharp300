using Hangman.UI.Interfaces;


namespace Hangman.UI
{
    public class App
    {
        private IPlayer? _p1;
        private IPlayer? _p2;

        public App()
        {
            _p1 = PlayerFactory.InitializePlayer(1);
            _p2 = PlayerFactory.InitializePlayer(2);
        }

        public void Run()
        {
            // Default: Player 1 begins the first round

            Console.WriteLine("Success.");
        }
    }
}
