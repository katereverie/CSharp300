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
                    return new HumanPlayer(playerName, playerWordSource);
                default:
                    Console.WriteLine($"The God of Chance will bless {playerName} with a destined word.");
                    return new ComputerPlayer(playerName);
            }
        }
    }
}
