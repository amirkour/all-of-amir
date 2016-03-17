using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetUtils;
using Boggle;
using DictionaryUtils;

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
            Info("File system path: '{0}'", this.Request.PhysicalPath);
            Info("{0}", this.Request.Path);
            Info("{0}", this.Request.Url.LocalPath);
            Info("{0}", this.HttpContext.Server.MapPath("/"));
            Info("{0}", this.Request.PhysicalApplicationPath);
            Info("{0}", this.Request.PhysicalPath);

            string affFilePath = Path.Combine(this.Request.PhysicalApplicationPath, "en_US.aff");
            string dicFilePath = Path.Combine(this.Request.PhysicalApplicationPath, "en_US.dic");
            IWordDefiner injectedWordDefiner = new HunspellWordDefiner(affFilePath, dicFilePath);

            BoggleBoardHelper helper = new BoggleBoardHelper(board, injectedWordDefiner);
            List<BoggleResult> results = helper.GetAllWords(false, true); // TODO - expose these two bools to the client later ...
            
            return this.Json(results);
        }
    }
}