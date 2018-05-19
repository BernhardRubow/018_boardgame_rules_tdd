using System.Collections.Generic;
using BoardGame.interfaces.newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.dto;
using newvisionsproject.boardgame.enums;

namespace BoardGame.gameLogic
{
    public class nvp_Rule_20_MustLeaveStart : IRule
    {
        private IRule _nextRule;
        public CheckMovesResult CheckRule(CheckMovesResult result)
        {
            if (!nvp_RuleHelper.IsLocalPositionAvailable(result.PlayerColor, result.PlayerFigures, 0))
            {
                if (nvp_RuleHelper.CanExitStart(result.PlayerColor, result.PlayerFigures, result.DiceValue))
                {
                    var pf = nvp_RuleHelper.GetPlayerFigureFromByLocalPosition(result.PlayerColor, result.PlayerFigures, 0);
                    result.CanMove = true;
                    result.LastActiveRule = "Must Leave Start";

                    var pm = new PlayerMove
                    {
                        Color = result.PlayerColor,
                        Index = pf.Index,
                        DiceValue = result.DiceValue
                    };

                    result.PossibleMoves = new List<PlayerMove> {pm};
                    return result;
                }
                else
                {
                    result.CanMove = false;
                    result.AdditionalRollGranted = false;
                    result.LastActiveRule = "Must Leave Start";
                    return result;
                }
            }
            else
            {
                return _nextRule.CheckRule(result);
            }
        }

        public IRule SetNextRule(IRule nextRule)
        {
            _nextRule = nextRule;
            return this;
        }
    }
}