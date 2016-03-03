using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    /// <summary>
    /// A BoggleLetter is just a BoggleCoord (a row/col tuple) and a letter.
    /// Conceptually: it's a letter on a boggle board!
    /// </summary>
    [Serializable]
    public class BoggleLetter : BoggleCoord
    {
        public char Letter { get; set; }
        public override bool Equals(object obj)
        {
            BoggleLetter other = obj as BoggleLetter;
            if (other == null) return false;

            return base.Equals(obj) && other.Letter == this.Letter;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() ^ this.Letter.GetHashCode();
        }
        public override string ToString()
        {
            return "{Letter: " + this.Letter + ", Row: " + Row + ", Col: " + Col + "}";
        }
    }
}
