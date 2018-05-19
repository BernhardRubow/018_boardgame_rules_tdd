using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGame.gameLogic;
using newvisionsproject.boardgame.dto;
using newvisionsproject.boardgame.enums;
using NUnit.Framework;

namespace UnitTests.GameLogicTests
{
    [TestFixture]
    public class singleRuleTests
    {
        [Test]
        public void test_rule_must_kill_v0()
        {
            CheckMovesResult result = new CheckMovesResult {DiceValue = 2};

            var rf1 = new PlayerFigure(PlayerColors.red, 0, 1, 1, 0);
            var rf2 = new PlayerFigure(PlayerColors.red, 1, 2, 2, 0);
            var gf1 = new PlayerFigure(PlayerColors.green, 0, 13, 3, 10);
            result.PlayerFigures.AddRange(new[] {rf1, rf2, gf1});

            var rule = new nvp_Rule_50_HaveToKill().SetNextRule(new nvp_RuleDefault_class());
            result = rule.CheckRule(result);
            Assert.AreEqual(true, result.CanMove);
            Assert.AreEqual(false, result.AdditionalRollGranted);
            Assert.AreEqual(1, result.PossibleMoves.Count);
            Assert.AreEqual(0, result.PossibleMoves[0].Index);
        }

        [Test]
        public void test_rule_must_kill_v1()
        {
            CheckMovesResult result = new CheckMovesResult {DiceValue = 2};

            var rf1 = new PlayerFigure(PlayerColors.red, 0, 1, 1, 0);
            var rf2 = new PlayerFigure(PlayerColors.red, 1, 2, 2, 0);
            var gf1 = new PlayerFigure(PlayerColors.green, 0, 13, 3, 10);
            var gf2 = new PlayerFigure(PlayerColors.green, 0, 4, 4, 10);
            result.PlayerFigures.AddRange(new[] { rf1, rf2, gf1, gf2 });

            var rule = new nvp_Rule_50_HaveToKill().SetNextRule(new nvp_RuleDefault_class());
            result = rule.CheckRule(result);
            Assert.AreEqual(true, result.CanMove);
            Assert.AreEqual(false, result.AdditionalRollGranted);
            Assert.AreEqual(2, result.PossibleMoves.Count);
            Assert.AreEqual(0, result.PossibleMoves[0].Index);
            Assert.AreEqual(1, result.PossibleMoves[1].Index);

        }
    }
}
