using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    /// <summary>
    /// A BoggleResult is basically a word that appears on a boggle board,
    /// along with any metadata that might be important for that word.
    /// </summary>
    [Serializable]
    public class BoggleResult
    {
        public string Word { get; set; }
        public List<BoggleCoord> Coords { get; set; }
    }
}
