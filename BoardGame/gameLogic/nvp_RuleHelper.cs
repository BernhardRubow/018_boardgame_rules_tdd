using System.Collections.Generic;
using System.Linq;
using newvisionsproject.boardgame.dto;
using newvisionsproject.boardgame.enums;

namespace BoardGame.gameLogic
{
    public static class nvp_RuleHelper
    {

        public static int CountPlayersOnBoard(PlayerColors playerColor, List<PlayerFigure> playerFigures)
        {
            int count = playerFigures.Count(x => 
                x.LocalPosition >= 0 && 
                x.LocalPosition <= 40 && 
                x.Color == playerColor);

            return count;
        }

        public static int CountPlayersInHouse(PlayerColors playerColor, List<PlayerFigure> playerFigures)
        {
            int count = playerFigures.Count(x => 
                x.LocalPosition < 0 && 
                x.Color == playerColor);
            return count;
        }

        public static bool IsLocalPositionAvailable(PlayerColors playerColor, List<PlayerFigure> playerFigures, int field)
        {
            return playerFigures.Count(x => x.Color == playerColor && x.LocalPosition == field) == 0;
        }

        public static bool CanExitStart(PlayerColors playerColor, List<PlayerFigure> playerFigures, int diceValue)
        {
            return playerFigures.Count(x => x.Color == playerColor && x.LocalPosition == diceValue) == 0;
        }

        public static PlayerFigure GetPlayerFigureFromByLocalPosition(PlayerColors playerColor, List<PlayerFigure> playerFigures, int field)
        {
            return playerFigures.Single(x => x.Color == playerColor && x.LocalPosition == field);
        }

        public static PlayerFigure GetNextFigureToLeaveHouse(PlayerColors playerColor, List<PlayerFigure> playerFigures)
        {
            return playerFigures.First(x => x.Color == playerColor && x.LocalPosition == -1);
        }

        public static PlayerFigure GetFigureOnWorldPosition(List<PlayerFigure> playerFigures, int worldPosition)
        {
            return playerFigures.SingleOrDefault(x => x.WorldPosition == worldPosition);
        }

        public static List<PlayerFigure> GetFiguresOnBoardByColor(PlayerColors playerColor, List<PlayerFigure> playerfigures, int diceValue)
        {
            var list = playerfigures.Where(x => 
                x.Color == playerColor && 
                x.LocalPosition > 0 && 
                x.LocalPosition + diceValue < 45).ToList();
            return list;
        }
    }
}