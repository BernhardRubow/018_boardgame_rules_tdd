using System.Collections.Generic;
using BoardGame.interfaces.newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.dto;

namespace BoardGame.gameLogic
{
    public class nvp_Rule_50_HaveToKill : IRule
    {
        private IRule _nextRule;
        public IRule SetNextRule(IRule nextRule)
        {
            _nextRule = nextRule;
            return this;
        }

        public CheckMovesResult CheckRule(CheckMovesResult result)
        {
            // get players on board
            var ownFigures = nvp_RuleHelper.GetFiguresOnBoardByColor(result.PlayerColor, result.PlayerFigures, result.DiceValue);

            if (ownFigures.Count == 0) return _nextRule.CheckRule(result);

            if (ownFigures.Count > 1)
            {
                var haveToKill = false;
                haveToKill |= CheckRuleForFigure(result, ownFigures[0]);
                haveToKill |= CheckRuleForFigure(result, ownFigures[1]);
                if(haveToKill) return result;
            }

            return _nextRule.CheckRule(result);
        }

        private bool CheckRuleForFigure(CheckMovesResult result, PlayerFigure figureToCheck)
        {
            int worlPositionToCheck = (figureToCheck.WorldPosition + result.DiceValue)%41;
            PlayerFigure playerFigureFound = nvp_RuleHelper.GetFigureOnWorldPosition(result.PlayerFigures, worlPositionToCheck);
            if (playerFigureFound == null || (playerFigureFound != null && playerFigureFound.Color == result.PlayerColor))
            {
                return false;
            }

            result.CanMove = true;
            result.LastActiveRule = "nvp_Rule_50_HaveToKill";
            result.PossibleMoves.Add(new PlayerMove
            {
                Color = result.PlayerColor,
                DiceValue = result.DiceValue,
                Index = figureToCheck.Index
            });

            return true;
        }
    }
}