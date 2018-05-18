using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using newvisionsproject.boardgame.dto;
using newvisionsproject.boardgame.enums;

namespace BoardGame.interfaces
{
    namespace newvisionsproject.boardgame.interfaces
    {
        public interface IRule
        {
            IRule SetNextRule(IRule nextRule);
            CheckMovesResult CheckRule(CheckMovesResult result);
        }
    }
}
