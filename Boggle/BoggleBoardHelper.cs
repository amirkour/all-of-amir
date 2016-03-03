using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    /// <summary>
    /// This class is a helper/wrapper on a boggle board, which is just a 2D
    /// array of characters.
    /// </summary>
    public class BoggleBoardHelper
    {
        protected char[][] _board;

        // default construction isn't allowed, since this class is meaningless
        // without a 2D array of chars
        private BoggleBoardHelper() { }

        /// <summary>
        /// "Default" constructor, which takes the 2D array of chars as the board.
        /// </summary>
        public BoggleBoardHelper(char[][] board)
        {
            if (board == null) throw new Exception("A boggle board helper cannot be instantiated without a non-null 2D board of chars");
            _board = board;
        }

        /// <summary>
        /// Helper that returns all the neighbors of this board at the given
        /// coordinates, optionally considering diagonal coordinates as
        /// neighbors.  
        /// 
        /// Returns null if the given coord is null or out of the bounds of
        /// this board.
        /// </summary>
        public List<BoggleLetter> GetNeighborsAt(BoggleCoord coord, bool allowDiagonalNeighbors = false)
        {
            if (_board == null)
                throw new Exception("Cannot retrieve neighbors in the absence of a 2D board of chars - did you somehow blow away the board of this class?");

            if (coord == null ||
                coord.Row < 0 ||
                coord.Row >= _board.Length ||
                coord.Col < 0 ||
                coord.Col >= _board[coord.Row].Length)
                return null;

            List<BoggleLetter> neighbors = new List<BoggleLetter>();

            // is there a left neighbor?
            int nextRow = coord.Row;
            int nextCol = coord.Col - 1;
            if (nextCol >= 0)
            {
                neighbors.Add(new BoggleLetter()
                {
                    Row = nextRow,
                    Col = nextCol,
                    Letter = _board[nextRow][nextCol]
                });
            }

            // should we go top-left?
            nextRow = coord.Row - 1;
            nextCol = coord.Col - 1;
            if(allowDiagonalNeighbors &&
                nextRow >= 0 &&
                nextCol >= 0)
            {
                neighbors.Add(new BoggleLetter()
                {
                    Row = nextRow,
                    Col = nextCol,
                    Letter = _board[nextRow][nextCol]
                });
            }

            // is there a neighbor above?
            nextRow = coord.Row - 1;
            nextCol = coord.Col;
            if(nextRow >= 0)
            {
                neighbors.Add(new BoggleLetter()
                {
                    Row = nextRow,
                    Col = nextCol,
                    Letter = _board[nextRow][nextCol]
                });
            }

            // should we go top-right?
            nextRow = coord.Row - 1;
            nextCol = coord.Col + 1;
            if(allowDiagonalNeighbors &&
                nextRow >= 0 &&
                nextCol < _board[nextRow].Length)
            {
                neighbors.Add(new BoggleLetter()
                {
                    Row = nextRow,
                    Col = nextCol,
                    Letter = _board[nextRow][nextCol]
                });
            }

            // is there a neighbor to the right?
            nextRow = coord.Row;
            nextCol = coord.Col + 1;
            if(nextCol < _board[nextRow].Length)
            {
                neighbors.Add(new BoggleLetter()
                {
                    Row = nextRow,
                    Col = nextCol,
                    Letter = _board[nextRow][nextCol]
                });
            }

            // should we go down and right?
            nextRow = coord.Row + 1;
            nextCol = coord.Col + 1;
            if(allowDiagonalNeighbors &&
                nextRow < _board.Length &&
                nextCol < _board[nextRow].Length)
            {
                neighbors.Add(new BoggleLetter()
                {
                    Row = nextRow,
                    Col = nextCol,
                    Letter = _board[nextRow][nextCol]
                });
            }

            // is there a neighbor on the bottom?
            nextRow = coord.Row + 1;
            nextCol = coord.Col;
            if(nextRow < _board.Length)
            {
                neighbors.Add(new BoggleLetter()
                {
                    Row = nextRow,
                    Col = nextCol,
                    Letter = _board[nextRow][nextCol]
                });
            }

            // should we go down and left?
            nextRow = coord.Row + 1;
            nextCol = coord.Col - 1;
            if(allowDiagonalNeighbors &&
                nextRow < _board.Length &&
                nextCol >= 0)
            {
                neighbors.Add(new BoggleLetter()
                {
                    Row = nextRow,
                    Col = nextCol,
                    Letter = _board[nextRow][nextCol]
                });
            }

            return neighbors;
        }
    }
}
