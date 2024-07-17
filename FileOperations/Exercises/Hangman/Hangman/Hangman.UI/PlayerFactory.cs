using Hangman.BLL.Interfaces;
using Hangman.UI.Players;

namespace Hangman.UI
{
    public static class PlayerFactory
    {
        public static IPlayer InitializePlayer(int playerNumber)
        {
            PlayerType playerType = GameConsole.GetPlayerType(playerNumber);
            string playerName = playerType == PlayerType.Computer ? "Grim Reaper" : GameConsole.GetPlayerName(playerNumber);
            IWordSource playerWordSource;

            switch (playerType)
            {
                case PlayerType.Human:
                    playerWordSource = GameConsole.GetWordSource(playerName);
                    Console.WriteLine($"{playerName}, You shall choose your own last words.");
                    GameConsole.AnyKey();
                    return new HumanPlayer(playerName, playerWordSource);
                default:
                    Console.WriteLine($"{playerName} shall receive a word from the Hell Librarian.");
                    GameConsole.AnyKey();
                    return new ComputerPlayer(playerName);
            }
        }
    }
}
