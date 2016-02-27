﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetUtils;

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
            return this.Json(new { msg = "it worked!?" });
        }
    }
}