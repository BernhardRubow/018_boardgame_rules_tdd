using BoardGame.interfaces.newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.dto;

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

            if (playerFigures.Count == 1)
            {
                var pf = playerFigures[0];
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
                    return result;
                }
            }

            return _nextRule.CheckRule(result);
        }
    }
}