using System.Linq;
using BoardGame.interfaces.newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.dto;
using NUnit.Framework.Constraints;

namespace BoardGame.gameLogic
{
    public class nvp_Rule_60_JustMove : IRule
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
            var playerFigures = nvp_RuleHelper.GetFiguresOnBoardByColor(result.PlayerColor, result.PlayerFigures, result.DiceValue);
            if (playerFigures.Count == 0) return _nextRule.CheckRule(result);

            for (int i = 0, n = playerFigures.Count; i < n; i++)
            {
                var pf = playerFigures[i];
                if (pf.LocalPosition + result.DiceValue < 45)
                {
                    result.CanMove = true;
                    result.LastActiveRule = "JustMove";

                    result.PossibleMoves.Add(new PlayerMove
                    {
                        Color = result.PlayerColor,
                        DiceValue = result.DiceValue,
                        Index = pf.Index
                    });
                }
            }

            result.PossibleMoves = result.PossibleMoves.OrderBy(x => x.Index).ToList();
            return result;
        }
    }
}