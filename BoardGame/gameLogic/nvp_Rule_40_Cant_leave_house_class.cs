using System.Collections.Generic;
using BoardGame.interfaces.newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.dto;
using newvisionsproject.boardgame.enums;

namespace BoardGame.gameLogic
{
    public class nvp_Rule_40_Cant_leave_house_class : IRule
    {
        IRule _nextRule;
        int _numberOfTries;
        public CheckMovesResult CheckRule(CheckMovesResult result)
        {
            // no player figure is on the game field which means no player figure has movepoint score between 0 and up to 40
            var numberOfFiguresOnBoard = nvp_RuleHelper.CountPlayersOnBoard(result.PlayerColor, result.PlayerFigures);

            // at least on player figure is in the house
            var numberOfPlayerInTheHouse = nvp_RuleHelper.CountPlayersInHouse(result.PlayerColor, result.PlayerFigures);

            // general rule
            if (numberOfFiguresOnBoard > 0 || numberOfPlayerInTheHouse == 0) return _nextRule.CheckRule(result);

            
            _numberOfTries++;
            if (_numberOfTries >= 3 && result.DiceValue < 6)
            {
                // used all free rolls
                result.LastActiveRule = "nvp_Rule_40_Cant_leave_house_class";
                result.CanMove = false;
                result.AdditionalRollGranted = false;
                _numberOfTries = 0;
                return result;
            }

            if (result.DiceValue == 6)
            {
                result.LastActiveRule = "nvp_Rule_40_Cant_leave_house_class";
                result.CanMove = true;
                result.AdditionalRollGranted = true;
                _numberOfTries = 0;
                return result;
            }
            else
            {
                result.CanMove = false;
                result.AdditionalRollGranted = true;
                result.LastActiveRule = "nvp_Rule_40_Cant_leave_house_class";
                return result;
            }
        }

        public IRule SetNextRule(IRule nextRule)
        {
            _nextRule = nextRule;
            return this;
        }
    }
}