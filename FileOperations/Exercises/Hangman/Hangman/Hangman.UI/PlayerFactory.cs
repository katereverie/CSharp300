using Hangman.BLL.Interfaces;
using Hangman.UI.Players;

namespace Hangman.UI
{
    public static class PlayerFactory
    {
        private static bool _hasComputerPlayer = false;
        private static string? _p1Name = null; 

        public static IPlayer InitializePlayer(int playerNumber)
        {
            PlayerType playerType = GameConsole.GetPlayerType(playerNumber, _hasComputerPlayer);
            string playerName = playerType == PlayerType.Computer ? "Grim the Reaper" : GameConsole.GetPlayerName(playerNumber, _p1Name);
            if (playerNumber == 1)
            {
                _p1Name = playerName;
            }
            IWordSource playerWordSource;

            do
            {
                switch (playerType)
                {
                    case PlayerType.Human:
                        playerWordSource = GameConsole.GetWordSource(playerName);
                        GameConsole.AnyKey();
                        return new HumanPlayer(playerName, playerWordSource);
                    default:
                        _hasComputerPlayer = true;
                        GameConsole.AnyKey();
                        return new ComputerPlayer(playerName);
                        
                }

            } while (true);

        }
    }
}
