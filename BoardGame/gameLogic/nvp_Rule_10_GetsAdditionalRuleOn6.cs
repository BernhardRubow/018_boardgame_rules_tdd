using BoardGame.interfaces.newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.dto;

namespace BoardGame.gameLogic
{
    public class nvp_Rule_10_GetsAdditionalRuleOn6 : IRule
    {
        private IRule _nextRule;

        public CheckMovesResult CheckRule(CheckMovesResult result)
        {
            if (result.DiceValue == 6) result.AdditionalRollGranted = true;
            return _nextRule.CheckRule(result);
        }

        public IRule SetNextRule(IRule nextRule)
        {
            _nextRule = nextRule;
            return this;
        }
    }
}