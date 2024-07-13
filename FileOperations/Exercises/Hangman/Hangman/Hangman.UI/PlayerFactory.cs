using Hangman.UI.Interfaces;
using Hangman.UI.Players;

namespace Hangman.UI
{
    public static class PlayerFactory
    {
        public static IPlayer InitializePlayer(int playerNumber)
        {
            PlayerType playerType = GameConsole.GetPlayerType(playerNumber);
            string playerName = GameConsole.GetPlayerName(playerNumber);
            IWordSource playerWordSource;

            switch (playerType)
            {
                case PlayerType.Human:
                    playerWordSource = GameConsole.GetWordSource(playerName);
                    Console.WriteLine($"{playerName}, You shall choose your own last words.");
                    GameConsole.AnyKey();
                    return new HumanPlayer(playerName, playerWordSource);
                default:
                    Console.WriteLine($"{playerName} shall receive a word from the God of Chance.");
                    GameConsole.AnyKey();
                    return new ComputerPlayer(playerName);
            }
        }
    }
}
