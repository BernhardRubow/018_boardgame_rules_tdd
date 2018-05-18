using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGame.gameLogic;
using newvisionsproject.boardgame.dto;
using newvisionsproject.boardgame.enums;
using NUnit.Framework;

namespace newvisionsproject.boardgame.tests
{
    public class gameBoardTests
    {
        // +++ private fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private nvp_GameBoard_class _gameboard;

        // +++ setup and Teardown +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [SetUp]
        public void Setup()
        {
            _gameboard = new nvp_GameBoard_class(4);
        }

        [TearDown]
        public void TearDown()
        {
            _gameboard = null;
        }

        public CheckMovesResult CheckRules(string diceRoll)
        {
            PlayerColors playerColor = PlayerColors.red;
            if (diceRoll.Contains("r")) playerColor = PlayerColors.red;
            if (diceRoll.Contains("y")) playerColor = PlayerColors.yellow;
            if (diceRoll.Contains("b")) playerColor = PlayerColors.black;
            if (diceRoll.Contains("g")) playerColor = PlayerColors.green;

            var diceValue = Convert.ToInt32(diceRoll.Substring(1,1));

            return _gameboard.CheckPossiblePlayerMoves(playerColor, diceValue);
        }

        public PlayerFigure CheckWorldPosition(int worldPosition)
        {
            return nvp_RuleHelper.GetFigureOnWorldPosition(_gameboard.playerFigures, worldPosition);
        }


        // +++ tests ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        // +++ check moves tests +++
        [Test]
        public void test_rules_h4_b0_s0_d_r6()
        {

            // on a default game board with 4 players the red player rolls a 6
            int diceValue = 6;
            var result = CheckRules("r6");

            Assert.AreEqual(true, result.CanMove);
            Assert.AreEqual(true, result.AdditionalThrowGranted);

            // do the move and test
            _gameboard.Move(result);
            var pf = CheckWorldPosition(0);
            Assert.AreEqual(0, pf.Index);
            Assert.AreEqual(PlayerColors.red, pf.Color);
        }

        [Test]
        public void test_rules_h4_b0_s0_d_r6_r6_r4()
        {

            // roll a 6
            var result = _gameboard.Move(CheckRules("r6"));
            var pf = CheckWorldPosition(0);
            Assert.AreEqual(0, pf.Index);
            Assert.AreEqual(PlayerColors.red, pf.Color);

            // roll another 6
            result = _gameboard.Move(CheckRules("r6"));
            Assert.AreEqual(true, result.CanMove);
            Assert.AreEqual(true, result.AdditionalThrowGranted);
            pf = CheckWorldPosition(6);
            Assert.AreEqual(0, pf.Index);
            Assert.AreEqual(PlayerColors.red, pf.Color);

            // roll another 4
            result = _gameboard.Move(CheckRules("r4"));Assert.AreEqual(true, result.CanMove);
            Assert.AreEqual(false, result.AdditionalThrowGranted);
            pf = CheckWorldPosition(10);
            Assert.AreEqual(0, pf.Index);
            Assert.AreEqual(PlayerColors.red, pf.Color);
        }


        [Test]
        public void test_rules_h4_b0_s0_d_r5_r5_r6()
        {

            
            var result = _gameboard.Move(CheckRules("r5"));
            Assert.AreEqual(false, result.CanMove);
            Assert.AreEqual(true, result.AdditionalThrowGranted);

            
            if (result.AdditionalThrowGranted)
            {
                result = _gameboard.Move(CheckRules("r5"));
                Assert.AreEqual(false, result.CanMove);
                Assert.AreEqual(true, result.AdditionalThrowGranted);
            }

            if (result.AdditionalThrowGranted)
            {
                result = _gameboard.Move(CheckRules("r6"));
                Assert.AreEqual(true, result.CanMove);
                Assert.AreEqual(true, result.AdditionalThrowGranted);
            }

            var pf = CheckWorldPosition(0);
            Assert.AreEqual(0, pf.Index);
            Assert.AreEqual(PlayerColors.red, pf.Color);
        }

        [Test]
        public void test_rules_h4_b0_s0_d_r5_r6()
        {

            int diceValue = 5;
            var result = _gameboard.Move(CheckRules("r5"));
            Assert.AreEqual(false, result.CanMove);
            Assert.AreEqual(true, result.AdditionalThrowGranted);

            diceValue = 6;
            if (result.AdditionalThrowGranted)
            {
                result = _gameboard.Move(CheckRules("r6"));
                Assert.AreEqual(true, result.CanMove);
                Assert.AreEqual(true, result.AdditionalThrowGranted);
            }

            // do the move and test
            var pf = CheckWorldPosition(0);
            Assert.AreEqual(0, pf.Index);
            Assert.AreEqual(0, pf.LocalPosition);
            Assert.AreEqual(PlayerColors.red, pf.Color);
        }

        [Test]
        public void test_rules_h4_b0_s0_d_b5_b6()
        {

            int diceValue = 5;
            var result = _gameboard.Move(CheckRules("b5"));
            Assert.AreEqual(false, result.CanMove);
            Assert.AreEqual(true, result.AdditionalThrowGranted);

            diceValue = 6;
            if (result.AdditionalThrowGranted)
            {
                result = _gameboard.Move(CheckRules("b6"));
                Assert.AreEqual(true, result.CanMove);
                Assert.AreEqual(true, result.AdditionalThrowGranted);
            }

            // do the move and test
            var pf = CheckWorldPosition(10);
            Assert.AreEqual(0, pf.Index);
            Assert.AreEqual(0, pf.LocalPosition);
            Assert.AreEqual(PlayerColors.black, pf.Color);
        }

        [Test]
        public void test_rules_h4_b0_s0_d_y5_y6()
        {

            int diceValue = 5;
            var result = _gameboard.Move(CheckRules("y5"));
            Assert.AreEqual(false, result.CanMove);
            Assert.AreEqual(true, result.AdditionalThrowGranted);

            diceValue = 6;
            if (result.AdditionalThrowGranted)
            {
                result = _gameboard.Move(CheckRules("y6"));
                Assert.AreEqual(true, result.CanMove);
                Assert.AreEqual(true, result.AdditionalThrowGranted);
            }

            // do the move and test
            var pf = CheckWorldPosition(20);
            Assert.AreEqual(0, pf.Index);
            Assert.AreEqual(0, pf.LocalPosition);
            Assert.AreEqual(PlayerColors.yellow, pf.Color);
        }

        [Test]
        public void test_rules_h4_b0_s0_d_g5_g6()
        {

            int diceValue = 5;
            var result = _gameboard.Move(CheckRules("g5"));
            Assert.AreEqual(false, result.CanMove);
            Assert.AreEqual(true, result.AdditionalThrowGranted);

            diceValue = 6;
            if (result.AdditionalThrowGranted)
            {
                result = _gameboard.Move(CheckRules("g6"));
                Assert.AreEqual(true, result.CanMove);
                Assert.AreEqual(true, result.AdditionalThrowGranted);
            }

            // do the move and test
            var pf = CheckWorldPosition(30);
            Assert.AreEqual(0, pf.Index);
            Assert.AreEqual(0, pf.LocalPosition);
            Assert.AreEqual(PlayerColors.green, pf.Color);
        }

        [Test]
        public void test_rules_h4_b0_s0_d_r5_r5_r5()
        {
            var result = _gameboard.CheckPossiblePlayerMoves(PlayerColors.red, 5);

            Assert.AreEqual(false, result.CanMove);
            Assert.AreEqual(true, result.AdditionalThrowGranted);

            if (result.AdditionalThrowGranted)
            {
                result = _gameboard.CheckPossiblePlayerMoves(PlayerColors.red, 5);
                Assert.AreEqual(false, result.CanMove);
                Assert.AreEqual(true, result.AdditionalThrowGranted);
            }

            if (result.AdditionalThrowGranted)
            {
                result = _gameboard.CheckPossiblePlayerMoves(PlayerColors.red, 5);
                Assert.AreEqual(false, result.CanMove);
                Assert.AreEqual(false, result.AdditionalThrowGranted);
            }
        }



        [Test]
        public void test_rules_h4_b0_s0_d_r6_r6_r5_r5_r5()
        {
            CheckMovesResult result = null;
            var diceRolls = new [] {"r6", "r6", "r5", "r5", "r5"};
            for (int i = 0, n = diceRolls.Length; i < n; i++)
            {
                result = _gameboard.Move(CheckRules(diceRolls[i]));
            }

            Assert.AreEqual(true, result.CanMove);
            Assert.AreEqual(false, result.AdditionalThrowGranted);
            var pf = CheckWorldPosition(21);
            Assert.IsNotNull(pf);
            Assert.AreEqual(0, pf.Index);
            Assert.AreEqual(PlayerColors.red, pf.Color);
            Assert.AreEqual(21, pf.LocalPosition);
            Assert.AreEqual(21, pf.WorldPosition);
        }

        [Test]
        public void test_rules_h4_b0_s0_d_b6_b6_b5_b5_b5()
        {
            CheckMovesResult result = null;
            var diceRolls = new[] { "b6", "b6", "b5", "b5", "b5" };
            for (int i = 0, n = diceRolls.Length; i < n; i++)
            {
                result = _gameboard.Move(CheckRules(diceRolls[i]));
            }

            Assert.AreEqual(true, result.CanMove);
            Assert.AreEqual(false, result.AdditionalThrowGranted);
            var pf = CheckWorldPosition(21 + 10);
            Assert.IsNotNull(pf);
            Assert.AreEqual(0, pf.Index);
            Assert.AreEqual(21, pf.LocalPosition);
            Assert.AreEqual(31, pf.WorldPosition);

        }

        [Test]
        public void test_rules_h4_b0_s0_d_g6_g6_g5_g5_g5()
        {
            CheckMovesResult result = null;
            var diceRolls = new[] { "g6", "g6", "g5", "g5", "g5" };
            for (int i = 0, n = diceRolls.Length; i < n; i++)
            {
                result = _gameboard.Move(CheckRules(diceRolls[i]));
            }

            Assert.AreEqual(true, result.CanMove);
            Assert.AreEqual(false, result.AdditionalThrowGranted);
            var pf = CheckWorldPosition((21 + 30) % 41);
            Assert.IsNotNull(pf);
            Assert.AreEqual(0, pf.Index);
            Assert.AreEqual(21, pf.LocalPosition);
            Assert.AreEqual((21 + 30) % 41, pf.WorldPosition);
            Assert.AreEqual(PlayerColors.green, pf.Color);
        }

        [Test]
        public void test_rules_h4_b0_s0_d_rbyg6_rbyg6_rbyg2()
        {
            CheckMovesResult result = null;
            var diceRolls = new[] { "r6", "r6", "r2", "b6", "b6", "b2", "y6", "y6", "y2", "g6", "g6", "g2" };
            for (int i = 0, n = diceRolls.Length; i < n; i++)
            {
                result = _gameboard.Move(CheckRules(diceRolls[i]));
            }

            int numberOfRedFigures = nvp_RuleHelper.CountPlayersOnBoard(PlayerColors.red, result.PlayerFigures);
            int numberOfGreenFigures = nvp_RuleHelper.CountPlayersOnBoard(PlayerColors.green, result.PlayerFigures);
            int numberOfYellowFigures = nvp_RuleHelper.CountPlayersOnBoard(PlayerColors.yellow, result.PlayerFigures);
            int numberOfBlackFigures = nvp_RuleHelper.CountPlayersOnBoard(PlayerColors.black, result.PlayerFigures);

            Assert.AreEqual(1, numberOfRedFigures);
            Assert.AreEqual(1, numberOfYellowFigures);
            Assert.AreEqual(1, numberOfGreenFigures);
            Assert.AreEqual(1, numberOfBlackFigures);
        }
        



        // +++ creation tests +++
        [Test]
        public void test_init_game_for_2_players()
        {
            nvp_GameBoard_class gameboard = new nvp_GameBoard_class(2);

            var numberOfRedFigures = gameboard.playerFigures.Count(x => x.Color == PlayerColors.red);
            var numberOfYellowFigures = gameboard.playerFigures.Count(x => x.Color == PlayerColors.yellow);

            Assert.AreEqual(4, numberOfRedFigures);
            Assert.AreEqual(4, numberOfYellowFigures);

            var playersInHouse = gameboard.playerFigures.Where(x => x.WorldPosition == -1).Count();
            Assert.AreEqual(gameboard.playerFigures.Count, playersInHouse);

            var sumOfPlayerIndices = gameboard.playerFigures.Sum(x => x.Index);
            Assert.AreEqual(gameboard.playerFigures.Count / 2 * 3, sumOfPlayerIndices);
        }

        [Test]
        public void test_init_game_for_3_players()
        {
            nvp_GameBoard_class gameboard = new nvp_GameBoard_class(3);

            var numberOfRedFigures = gameboard.playerFigures.Count(x => x.Color == PlayerColors.red);
            var numberOfYellowFigures = gameboard.playerFigures.Count(x => x.Color == PlayerColors.yellow);
            var numberOfBlackFigures = gameboard.playerFigures.Count(x => x.Color == PlayerColors.black);

            Assert.AreEqual(4, numberOfRedFigures);
            Assert.AreEqual(4, numberOfYellowFigures);
            Assert.AreEqual(4, numberOfBlackFigures);

            var playersInHouse = gameboard.playerFigures.Count(x => x.WorldPosition == -1);
            Assert.AreEqual(gameboard.playerFigures.Count, playersInHouse);

            var sumOfPlayerIndices = gameboard.playerFigures.Sum(x => x.Index);
            Assert.AreEqual(gameboard.playerFigures.Count / 2 * 3, sumOfPlayerIndices);
        }

        [Test]
        public void test_init_game_for_4_players()
        {
            nvp_GameBoard_class gameboard = new nvp_GameBoard_class(4);

            var numberOfRedFigures = gameboard.playerFigures.Count(x => x.Color == PlayerColors.red);
            var numberOfYellowFigures = gameboard.playerFigures.Count(x => x.Color == PlayerColors.yellow);
            var numberOfBlackFigures = gameboard.playerFigures.Count(x => x.Color == PlayerColors.black);
            var numberOfGreenFigures = gameboard.playerFigures.Count(x => x.Color == PlayerColors.green);

            Assert.AreEqual(4, numberOfRedFigures);
            Assert.AreEqual(4, numberOfYellowFigures);
            Assert.AreEqual(4, numberOfBlackFigures);
            Assert.AreEqual(4, numberOfGreenFigures);

            var playersInHouse = gameboard.playerFigures.Count(x => x.WorldPosition == -1);
            Assert.AreEqual(gameboard.playerFigures.Count, playersInHouse);

            var sumOfPlayerIndices = gameboard.playerFigures.Sum(x => x.Index);
            Assert.AreEqual(gameboard.playerFigures.Count / 2 * 3, sumOfPlayerIndices);
        }
    }
}
