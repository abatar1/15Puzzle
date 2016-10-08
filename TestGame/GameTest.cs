using _15Puzzle;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestGame
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void Constructor_InvalidInputSize_ThrowException()
        {
            int[] a = { 1, 3, 5 };
            int[] b = { };

            try
            {
                var gameA = new Game(a);
                var iGameA = new ImmutableGame(a);
                var gameB = new Game(b);
                var iGameB = new ImmutableGame(b);
                Assert.Fail("No exception thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is Exception, "Input matrix isn't square matrix.");
            }
        }

        [TestMethod]
        public void Indexer_BasicTest()
        {
            var game = new Game(3, 2, 1, 5, 7, 6, 0, 8, 4);

            Assert.AreEqual(game[0, 0], 3);
            Assert.AreEqual(game[2, 2], 4);
            try
            {
                var a = game[0, 4];
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is IndexOutOfRangeException);
            }
        }

        [TestMethod]
        public void Indexer_ShiftedGameTest()
        {
            int[] input = new int[] { 3, 2, 1, 5, 7, 6, 0, 8, 4 };
            var game = new Game(input);
            var iGame = new ImmutableGame(input);

            game.Shift(5);
            var newGame = iGame.Shift(5);         

            Assert.AreEqual(game[0, 1], 0);
            Assert.AreEqual(game[0, 2], 5);

            Assert.AreEqual(newGame[0, 1], 0);
            Assert.AreEqual(newGame[0, 2], 5);
        }

        

        [TestMethod]
        public void GetLocation_BasicTest()
        {
            var game = new Game(3, 2, 1, 5, 7, 6, 0, 8, 4);

            Assert.AreEqual(game.GetLocation(3), new Point(0, 0));
            Assert.AreEqual(game.GetLocation(0), new Point(0, 2));
            Assert.AreEqual(game.GetLocation(7), new Point(1, 1));

            try
            {
                Point c = game.GetLocation(10);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is IndexOutOfRangeException);
            }
        }

        [TestMethod]
        public void GetLocation_ShiftedGameTest()
        {
            var game = new Game(3, 2, 1, 5, 7, 6, 0, 8, 4);

            game.Shift(5);

            Assert.AreEqual(game.GetLocation(5), new Point(0, 2));
            Assert.AreEqual(game.GetLocation(0), new Point(0, 1));
        }

        [TestMethod]
        public void Shift_CannotShiftValue_ThrowException()
        {
            var game = new Game(3, 2, 1, 5, 7, 6, 0, 8, 4);

            try
            {
                game.Shift(7);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is Exception, "This value cannot be shifted.");
            }
        }

        [TestMethod]
        public void Indexer_ImmutableTest()
        {
            var game = new ImmutableGame(3, 2, 1, 5, 7, 6, 0, 8, 4);

            var newGame = game.Shift(5);

            Assert.AreEqual(game[0, 1], 5);
            Assert.AreEqual(game[0, 2], 0);
            Assert.AreEqual(newGame[0, 1], 0);
            Assert.AreEqual(newGame[0, 2], 5);
        }

        [TestMethod]
        public void GetLocation_ImmutableTest()
        {
            var game = new ImmutableGame(3, 2, 1, 5, 7, 6, 0, 8, 4);

            var newGame = game.Shift(5);

            Assert.AreEqual(game.GetLocation(5), new Point(0, 1));
            Assert.AreEqual(game.GetLocation(0), new Point(0, 2));
            Assert.AreEqual(newGame.GetLocation(5), new Point(0, 2));
            Assert.AreEqual(newGame.GetLocation(0), new Point(0, 1));
        }

        [TestMethod]
        public void Shift_ImmutableTest()
        {
            var game = new ImmutableGame(3, 2, 1, 5, 7, 6, 0, 8, 4);

            var firstState = game;

            var secondState = game.Shift(5);

            Assert.AreEqual(game, firstState);
            Assert.AreNotEqual(game, secondState);
        }
    }
}
