using System.Collections.Generic;
using System.Linq;
using BoardGame.interfaces.newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.dto;
using newvisionsproject.boardgame.enums;

namespace BoardGame.gameLogic
{
    public class nvp_Rule_30_MustLeaveHouse : IRule
    {
        private IRule _nextRule;

        public CheckMovesResult CheckRule(CheckMovesResult result)
        {
            // no player figure is on the game field which means no player figure has movepoint score between 0 and up to 40
            var numberOfFiguresOnBoard = nvp_RuleHelper.CountPlayersOnBoard(result.PlayerColor, result.PlayerFigures);

            // at least on player figure is in the house
            var numberOfPlayerInTheHouse = nvp_RuleHelper.CountPlayersInHouse(result.PlayerColor, result.PlayerFigures);

            // general rule
            if (numberOfPlayerInTheHouse == 0 || result.DiceValue < 6) return _nextRule.CheckRule(result);

            // get figure to move
            var pf = result.PlayerFigures
                .Where(x => x.LocalPosition < 0 && x.Color == result.PlayerColor)
                .OrderBy(x => x.Index)
                .First();

            result.CanMove = true;
            result.AdditionalRollGranted = true;
            result.LastActiveRule = "Must leave house";
            result.PossibleMoves.Add(new PlayerMove
            {
                Color = result.PlayerColor,
                Index = nvp_RuleHelper.GetNextFigureToLeaveHouse(result.PlayerColor, result.PlayerFigures).Index,
                DiceValue = 0
            });

            return result;
        }

        public IRule SetNextRule(IRule nextRule)
        {
            _nextRule = nextRule;
            return this;
        }
    }
}