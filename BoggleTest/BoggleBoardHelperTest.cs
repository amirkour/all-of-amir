using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNetUtils;
using Boggle;
using System.Collections.Generic;

namespace BoggleTest
{
    [TestClass]
    public class BoggleBoardHelperTest
    {
        // test boards will default to these dimensions
        public const int DEFAULT_NUMBER_OF_BOARD_ROWS = 3;
        public const int DEFAULT_NUMBER_OF_BOARD_COLS = 3;

        // just store the 26 lower-case letters of the english alphabet
        public static char[] ALPHABET = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        /// <summary>
        /// Use this helper to generate 2D arrays of characters (boards) during tests
        /// of arbitrary length/width.
        /// </summary>
        private char[][] GenerateTestBoard(int rows = DEFAULT_NUMBER_OF_BOARD_ROWS, int cols = DEFAULT_NUMBER_OF_BOARD_COLS)
        {
            rows = (rows < 0) ? DEFAULT_NUMBER_OF_BOARD_ROWS : rows;
            cols = (cols < 0) ? DEFAULT_NUMBER_OF_BOARD_COLS : cols;

            Random random = new Random();
            char[][] result = new char[rows][];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new char[cols];
                for (int j = 0; j < result[i].Length; j++)
                    result[i][j] = ALPHABET[random.Next() % ALPHABET.Length];
            }

            return result;
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsNull_ForNullArg()
        {
            char[][] board = GenerateTestBoard();
            BoggleBoardHelper helper = new BoggleBoardHelper(board);
            Assert.IsNull(helper.GetNeighborsAt(null));
            Assert.IsNull(helper.GetNeighborsAt(null, true));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsTwoNeighbors_ForTopLeftCoord()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 0, Col = 0 });
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 2);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 0));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsThreeNeighbors_ForTopLeftCoord_WithDiagonals()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 0, Col = 0 }, true);
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 3);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 0));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 1));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsThreeNeighbors_ForTopCenterCoord()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 0, Col = 1 });
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 3);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 0));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 2));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 1));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsFiveNeighbors_ForTopCenterCoord_WithDiagonals()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 0, Col = 1 }, true);
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 5);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 0));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 2));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 0));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 2));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsTwoNeighbors_ForTopRightCoord()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 0, Col = 2 });
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 2);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 2));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsThreeNeighbors_ForTopRightCoord_WithDiagonals()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 0, Col = 2 }, true);
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 3);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 2));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsThreeNeighbors_ForRightCenterCoord()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 1, Col = 2 });
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 3);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 2));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 2));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsFiveNeighbors_ForRightCenterCoord_WithDiagonals()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 1, Col = 2 }, true);
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 5);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 2));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 2));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 2));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsTwoNeighbors_ForBottomRightCoord()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 2, Col = 2 });
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 2);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 2));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 1));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsThreeNeighbors_ForBottomRightCoord_WithDiagonals()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 2, Col = 2 }, true);
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 3);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 2));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 1));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsThreeNeighbors_ForBottomCenterCoord()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 2, Col = 1 });
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 3);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 0));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 2));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsFiveNeighbors_ForBottomCenterCoord_WithDiagonals()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 2, Col = 1 }, true);
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 5);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 0));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 2));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 0));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 2));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsTwoNeighbors_ForBottomLeftCoord()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 2, Col = 0 });
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 2);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 0));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 1));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsThreeNeighbors_ForBottomLeftCoord_WithDiagonals()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 2, Col = 0 }, true);
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 3);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 0));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 1));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsThreeNeighbors_ForLeftCenterCoord()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 1, Col = 0 });
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 3);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 0));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 0));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsFiveNeighbors_ForLeftCenterCoord_WithDiagonals()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 1, Col = 0 }, true);
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 5);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 0));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 0));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 1));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsFourNeighbors_ForCenterCoord()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 1, Col = 1 });
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 4);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 0));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 2));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 1));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetNeighborsAt_ReturnsEightNeighbors_ForCenterCoord_WithDiagonals()
        {
            char[][] board = GenerateTestBoard(3, 3);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);

            List<BoggleLetter> neighbors = helper.GetNeighborsAt(new BoggleCoord() { Row = 1, Col = 1 }, true);
            Assert.IsNotNull(neighbors);
            Assert.AreEqual(neighbors.Count, 8);
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 0));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 0 && c.Col == 2));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 0));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 1 && c.Col == 2));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 0));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 1));
            Assert.IsTrue(neighbors.Exists(c => c.Row == 2 && c.Col == 2));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetAllWordsStartingAt_ReturnsNull_ForNullCoords()
        {
            char[][] board = GenerateTestBoard();
            BoggleBoardHelper helper = new BoggleBoardHelper(board);
            Assert.IsNull(helper.GetAllWordsStartingAt(null, false, false));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetAllWordsStartingAt_ReturnsNull_ForInvalidCoords()
        {
            char[][] board = GenerateTestBoard(1, 1);
            BoggleBoardHelper helper = new BoggleBoardHelper(board);
            Assert.IsNull(helper.GetAllWordsStartingAt(new BoggleCoord() { Row = 123, Col = 123 }, false, false));
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetAllWordsStartingAt_TestOne()
        {
            // let's start simple - a 2x2 board with the word "hell" starting at 0,0
            char[][] board = new char[2][];
            board[0] = new char[] { 'h', 'e' };
            board[1] = new char[] { 'l', 'l' };

            BoggleBoardHelper helper = new BoggleBoardHelper(board);
            List<BoggleResult> results = helper.GetAllWordsStartingAt(new BoggleCoord() { Row = 0, Col = 0 }, false, true);
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count >= 1);

            BoggleResult hell = results.First(re => re.Word != null && re.Word.Equals("hell"));
            Assert.IsNotNull(hell);
            Assert.IsNotNull(hell.Coords);
            Assert.AreEqual(hell.Coords.Count, 4);
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetAllWordsStartingAt_TestTwo()
        {
            // exactly the same as TestOne, but position the word somewhere else in the board
            // and spell it backwards
            char[][] board = new char[2][];
            board[0] = new char[] { 'l', 'e' };
            board[1] = new char[] { 'l', 'h' };

            BoggleBoardHelper helper = new BoggleBoardHelper(board);
            List<BoggleResult> results = helper.GetAllWordsStartingAt(new BoggleCoord() { Row = 1, Col = 1 }, false, true);
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count >= 1);

            BoggleResult hell = results.First(re => re.Word != null && re.Word.Equals("hell"));
            Assert.IsNotNull(hell);
            Assert.IsNotNull(hell.Coords);
            Assert.AreEqual(hell.Coords.Count, 4);
        }

        [TestMethod]
        public void BoggleBoardHelperTest_GetAllWordsStartingAt_TestThree()
        {
            // let's try an actual game board - i got this one online.
            // the word 'super' starts at 1,3 but it includes diagonals,
            // so let's see if we can pick it up
            char[][] board = new char[4][];
            board[0] = new char[] { 'd', 'g', 'h', 'i' };
            board[1] = new char[] { 'k', 'l', 'p', 's' };
            board[2] = new char[] { 'y', 'e', 'u', 't' };
            board[3] = new char[] { 'e', 'o', 'r', 'n' };

            BoggleBoardHelper helper = new BoggleBoardHelper(board);
            List<BoggleResult> results = helper.GetAllWordsStartingAt(new BoggleCoord() { Row = 1, Col = 3 }, true, true);
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count >= 1);

            BoggleResult super = results.Find(re => re.Word != null && re.Word.Equals("super"));
            Assert.IsNotNull(super);
            Assert.IsNotNull(super.Coords);
            Assert.AreEqual(super.Word, "super");
            Assert.AreEqual(super.Coords.Count, 5);
        }
    }
}
