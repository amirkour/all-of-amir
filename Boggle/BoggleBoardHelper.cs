using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetUtils;
using AonAwareDictionary;

namespace Boggle
{
    /// <summary>
    /// This class is a helper/wrapper on a boggle board, which is just a 2D
    /// array of characters.
    /// </summary>
    public class BoggleBoardHelper
    {
        protected char[][] _board;
        protected AADSvc _dictionaryService;

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
            _dictionaryService = new AADSvc();
        }

        /// <summary>
        /// This helper returns true if the given coordinates are in-bounds for the
        /// board in this class, false otherwise.
        /// 
        /// The helper assumes that boggle boards are perfect squares.
        /// </summary>
        protected bool CoordsAreInBounds(BoggleCoord coord)
        {
            return coord != null && _board != null &&
                coord.Row >= 0 && coord.Row < _board.Length &&
                coord.Col >= 0 && coord.Col < _board[0].Length;
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

            if (!CoordsAreInBounds(coord))
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

        /// <summary>
        /// Get all valid English words for the current board, starting at the given coordinates.
        /// 
        /// Returns null for out-of-bounds or null coordinates.
        /// </summary>
        public List<BoggleResult> GetAllWordsStartingAt(BoggleCoord coord, bool allowDiagonalsForNeighbors, bool excludeSingleLetterWords)
        {
            if (_board.IsNullOrEmpty())
                throw new Exception("Cannot get all words for row/col in the absence of a board - did you somehow corrupt the game board?");

            if (!CoordsAreInBounds(coord))
                return null;

            BoggleResult startingPoint = new BoggleResult()
            {
                Coords = new List<BoggleCoord>(),
                Word = _board[coord.Row][coord.Col].ToString()
            };
            startingPoint.Coords.Add(coord);

            List<BoggleResult> results = new List<BoggleResult>();
            this.GetWordHelper(startingPoint, results, allowDiagonalsForNeighbors, excludeSingleLetterWords);

            return results;
        }

        /// <summary>
        /// When we're trying to find a word in the boggle board, we'll use a recursive strategy
        /// to "bleed" across the board, starting from a given letter.  The following helper
        /// is the recursive "bleed" portion/algorithm - it'll inspect the last coordinates of
        /// the given boggle result and continue to construct english words from there, adding
        /// results to the given list of completed words as it goes.
        /// 
        /// If the given 'result so far' has no letters or coordinates, this helper will halt.
        /// The assumption is that this helper is called from a starting point of some kind - 
        /// in other words, the caller has to start the given result at a letter/coordinate,
        /// because this helper doesn't know where to start.
        /// </summary>
        protected void GetWordHelper(BoggleResult resultSoFar, 
                                     List<BoggleResult> completedResults, 
                                     bool useDiagonalsForNeighbors = false,
                                     bool excludeSingleLetterWords = true)
        {
            if (resultSoFar == null || 
                completedResults == null || 
                resultSoFar.Word.IsNullOrEmpty() ||
                resultSoFar.Coords == null)
                return;

            // if the result thus far isn't a proper prefix for any english words,
            // then there's no point in continuing to recurse, so we can stop
            if (!_dictionaryService.IsPrefix(resultSoFar.Word))
            {
                // before halting, ensure this isn't an exact word - don't wanna miss it
                if (excludeSingleLetterWords && resultSoFar.Word.Length == 0)
                    return;
                else if(_dictionaryService.IsExactWord(resultSoFar.Word))
                    completedResults.Add(resultSoFar);
                else
                    return;
            }

            // ok - let's pickup where we leftoff while building this word:
            BoggleCoord lastCoord = resultSoFar.Coords.Last();

            // we're gonna recurse through the neighbors, but we don't wanna back-track,
            // to a letter that we already came from in the sequence, so pickup the 2nd-
            // to-last coordinate as well (if it's there.)
            BoggleCoord secondToLastCoord = (resultSoFar.Coords.Count > 1) ? resultSoFar.Coords[resultSoFar.Coords.Count - 2] : null;

            List<BoggleLetter> neighbors = this.GetNeighborsAt(lastCoord, useDiagonalsForNeighbors);
            if (neighbors.IsNullOrEmpty())
                return;

            foreach(BoggleLetter nextNeighbor in neighbors)
            {
                // if this neighbor has already been seen, don't go back
                // TODO - does equality work here?
                if (secondToLastCoord != null && secondToLastCoord.Equals(nextNeighbor))
                    continue;

                // and then recurse down to the next neighbor so we can continue
                this.GetWordHelper(BoggleResult.AddLetterForNewResult(resultSoFar, nextNeighbor),
                                   completedResults,
                                   useDiagonalsForNeighbors);
            }

            // that's it - we visited all the neighbors.  finally: if the current result
            // is a word, add it to the results before winding out of this recursive call...

            // ...but don't include single-letter words if caller instructed us not to:
            if (excludeSingleLetterWords && resultSoFar.Word.Length == 1)
                return;
            
            if (_dictionaryService.IsExactWord(resultSoFar.Word))
                completedResults.Add(resultSoFar);
        }
    }
}
