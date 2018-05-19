using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using newvisionsproject.boardgame.enums;

namespace newvisionsproject.boardgame.dto
{
    public class CheckMovesResult
    {
        public List<PlayerMove> PossibleMoves;
        public bool CanMove;
        public string Msg;
        public bool AdditionalRollGranted;
        public List<PlayerFigure> PlayerFigures;
        public PlayerColors PlayerColor;
        public int DiceValue;
        public string LastActiveRule;

        public CheckMovesResult()
        {
            PossibleMoves = new List<PlayerMove>();
            PlayerFigures = new List<PlayerFigure>();
        }
    }
}
