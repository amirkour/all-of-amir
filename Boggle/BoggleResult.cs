using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetUtils;
using Newtonsoft.Json;

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

        public override bool Equals(object obj)
        {
            BoggleResult other = obj as BoggleResult;
            if (other == null) return false;
            
            if (!String.Equals(this.Word, other.Word))
                return false;

            if (this.Coords != null && other.Coords == null) return false;
            if (this.Coords == null && other.Coords != null) return false;
            if (this.Coords != null && other.Coords != null)
            {
                if (this.Coords.Count == other.Coords.Count)
                {
                    foreach (BoggleCoord coord in this.Coords)
                    {
                        if (!other.Coords.Contains(coord))
                            return false;
                    }
                }
                else
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            int code = 0;
            if (this.Word != null) code ^= this.Word.GetHashCode();
            if(this.Coords != null)
            {
                foreach (BoggleCoord coord in this.Coords)
                    code ^= coord.GetHashCode();
            }

            return code;
        }

        public override string ToString()
        {
            StringBuilder bldr = new StringBuilder();
            bldr.Append("BoggleResult: ");
            if (this.Word.IsNullOrEmpty())
                bldr.Append("<no word>");
            else
                bldr.Append(this.Word);

            bldr.Append(", Coords:");
            if (this.Coords.IsNullOrEmpty())
                bldr.Append(" <none>");
            else
            {
                foreach (BoggleCoord coord in this.Coords)
                    bldr.Append(" " + coord.ToString());
            }

            return bldr.ToString();
        }

        /// <summary>
        /// Helper that simply serializes this instance and returns it
        /// </summary>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Helper that will deserialize the given json string to a BoggleResult
        /// and return it (or null if the given string is invalid json.)
        /// </summary>
        public static BoggleResult FromJson(string json)
        {
            if (json.IsNullOrEmpty()) return null;
            try
            {
                return JsonConvert.DeserializeObject<BoggleResult>(json);
            }
            catch (Exception) { }

            return null;
        }

        /// <summary>
        /// Fashioned after TryParse* helpers: returns true and populates
        /// the given output param when the given json string is successfully deserialized,
        /// false/null otherwise.
        /// </summary>
        public static bool TryDeserializeAsJson(string json, out BoggleResult deserialized)
        {
            deserialized = BoggleResult.FromJson(json);
            return deserialized == null ? false : true;
        }
    }
}
