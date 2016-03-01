using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetUtils;
using Boggle;

namespace AllOfAmir.Controllers
{
    public class BoggleController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Helper that will return a pretty string representation of the given
        /// 2D array/board, or something useful if the board is null/empty
        /// </summary>
        private string Stringify(char[][] board)
        {
            if (board == null) return "null board";

            StringBuilder bldr = new StringBuilder();
            bldr.Append("[");
            for(int i = 0; i < board.Length; i++)
            {
                bldr.Append("[");
                for (int j = 0; j < board[i].Length; j++)
                {
                    bldr.Append(board[i][j]);

                    // if there are more elements coming up, slap a comma to the tail for prettiness
                    if (j < (board[i].Length - 1))
                        bldr.Append(",");
                }
                bldr.Append("]");
            }
            bldr.Append("]");
            return bldr.ToString();
        }

        protected BoggleResult GenerateRandomResultFor(char[][] board, int newRandomSeed)
        {
            if (board == null || board.Length <= 0 || board[0].Length <= 0)
                throw new Exception("Can't generate random boggle results for a board that has no length/height");

            BoggleResult result = new BoggleResult()
            {
                Word = "",
                Coords = new List<BoggleCoord>()
            };

            // generate a random starting point in the bounds of the given board
            Random rand = new Random();
            int randomRow = rand.Next(0, board.Length - 1);

            rand = new Random(newRandomSeed);
            int randomCol = rand.Next(0, board[0].Length - 1);

            // now just pick a direction that's available and generate gibberish
            // words in that direction

            // can we go left?
            if (randomCol > 0)
            {
                while(randomCol >= 0)
                {
                    result.Word += board[randomRow][randomCol];
                    result.Coords.Add(new BoggleCoord() { Row = randomRow, Col = randomCol });
                    randomCol--;
                }
            }

            // can we go right?
            else if (randomCol < board[randomRow].Length - 1)
            {
                while(randomCol <= board[randomRow].Length - 1)
                {
                    result.Word += board[randomRow][randomCol];
                    result.Coords.Add(new BoggleCoord() { Row = randomRow, Col = randomCol });
                    randomCol++;
                }
            }

            // can we go up?
            else if (randomRow > 0)
            {
                while(randomRow >= 0)
                {
                    result.Word += board[randomRow][randomCol];
                    result.Coords.Add(new BoggleCoord() { Row = randomRow, Col = randomCol });
                    randomRow--;    
                }
            }

            // can we go down?
            else if (randomRow < board.Length - 1)
            {
                while(randomRow < board.Length - 1)
                {
                    result.Word += board[randomRow][randomCol];
                    result.Coords.Add(new BoggleCoord() { Row = randomRow, Col = randomCol });
                    randomRow++;
                }
            }

            return result;
        }

        /// <summary>
        /// Service API that takes a boggle board and returns all the words
        /// in that board (along with some metadata for those words.)
        /// 
        /// TODO - actually implement this API!
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetBoggleWords(char[][] board)
        {
            Info("GetBoggleWords service: {0}", Stringify(board));

            // for now, create a dummy BoggleResult to send back and render
            List<BoggleResult> results = new List<BoggleResult>();
            
            // let's put two words in the result list, starting at two random
            // places in the caller's board. they don't have to be actual
            // words - just put contiugous sequences of letters in there 
            // for now
            for(int i = 0; i < 2; i++)
                results.Add(this.GenerateRandomResultFor(board, i));
            
            return this.Json(results);
        }
    }
}