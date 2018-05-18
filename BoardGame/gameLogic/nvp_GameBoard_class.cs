using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGame.interfaces.newvisionsproject.boardgame.interfaces;
using newvisionsproject.boardgame.dto;
using newvisionsproject.boardgame.enums;

namespace BoardGame.gameLogic
{
    public class nvp_GameBoard_class
    {
        // +++ public fields ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public List<PlayerFigure> playerFigures { get; private set; }


        // +++ private fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private List<IRule> _ruleChain;


        // +++ constructor ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public nvp_GameBoard_class(int numberOfPlayers)
        {
            playerFigures = new List<PlayerFigure>();
            InitPlayers(numberOfPlayers);

            _ruleChain = new List<IRule>();

            _ruleChain.Add(new nvp_Rule_10_GetsAdditionalRuleOn6());
            _ruleChain.Add(new nvp_Rule_20_MustLeaveStart());
            _ruleChain.Add(new nvp_Rule_30_MustLeaveHouse());
            _ruleChain.Add(new nvp_Rule_40_Cant_leave_house_class());
            _ruleChain.Add(new nvp_Rule_50_HaveToKill());
            _ruleChain.Add(new nvp_Rule_60_JustMove());
            _ruleChain.Add(new nvp_RuleDefault_class());

            for (int i = 0, n = _ruleChain.Count - 1; i < n; i++)
            {
                _ruleChain[i].SetNextRule(_ruleChain[i + 1]);
            }
        }


        // +++ public Methods +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public CheckMovesResult CheckPossiblePlayerMoves(PlayerColors playerColor, int diceValue)
        {
            var defaultResult = new CheckMovesResult();
            defaultResult.PlayerColor = playerColor;
            defaultResult.DiceValue = diceValue;
            defaultResult.PlayerFigures = playerFigures;
            return _ruleChain[0].CheckRule(defaultResult);
        }

        public CheckMovesResult Move(CheckMovesResult movesInfo, int indexMoveToMake = 0)
        {
            if (!movesInfo.CanMove) return movesInfo;    

            var move = movesInfo.PossibleMoves[indexMoveToMake];

            var figure = playerFigures.Single(x => 
                x.Color == move.Color && 
                x.Index == move.Index);
            figure.Move(move.DiceValue);
            return movesInfo;
        }

        // +++ private Methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        void InitPlayers(int numberOfPlayers)
        {
            switch (numberOfPlayers)
            {
                case 2:
                    AddPlayerColor(PlayerColors.red);
                    AddPlayerColor(PlayerColors.yellow);
                    break;

                case 3:
                    AddPlayerColor(PlayerColors.red);
                    AddPlayerColor(PlayerColors.black);
                    AddPlayerColor(PlayerColors.yellow);
                    break;

                case 4:
                    AddPlayerColor(PlayerColors.red);
                    AddPlayerColor(PlayerColors.black);
                    AddPlayerColor(PlayerColors.yellow);
                    AddPlayerColor(PlayerColors.green);
                    break;
            }
        }

        void AddPlayerColor(PlayerColors color)
        {
            int offset = 0;

            if (color == PlayerColors.black) offset = 10;
            if (color == PlayerColors.yellow) offset = 20;
            if (color == PlayerColors.green) offset = 30;


            for (int i = 0, n = 4; i < n; i++)
            {
                playerFigures.Add(
                  new PlayerFigure(color, i, offset)
                );
            }
        }
    }
}
