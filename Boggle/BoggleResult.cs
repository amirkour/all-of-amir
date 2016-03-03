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

        /// <summary>
        /// This is sort of a "concatenation" operation for BoggleResult objects - 
        /// it takes a boggle result and a boggle letter and returns a new boggle
        /// result: the product of appending the given letter to the end of the given
        /// boggle result.
        /// </summary>
        public static BoggleResult AddLetterForNewResult(BoggleResult original, BoggleLetter letterToAdd)
        {
            if (original == null || letterToAdd == null)
                throw new Exception("Cannot add a null letter to a null boggle result");

            BoggleResult result = new BoggleResult() { Word = original.Word, Coords = new List<BoggleCoord>() };

            // slap the new letter to the end of this result's word
            result.Word = (result.Word == null) ? letterToAdd.Letter.ToString() : result.Word + letterToAdd.Letter;

            // and then append the new letter's coordinates to the end of the
            // result's list
            if(!original.Coords.IsNullOrEmpty())
            {
                foreach (BoggleCoord coord in original.Coords)
                    result.Coords.Add(coord);
            }

            result.Coords.Add(new BoggleCoord()
            {
                Row = letterToAdd.Row,
                Col = letterToAdd.Col
            });

            return result;
        }

        /// <summary>
        /// Helper that returns true if this BoggleResult's coordinate list
        /// contains the given coord, false otherwise.
        /// </summary>
        public bool ContainsCoord(BoggleCoord coord)
        {
            if (coord == null || this.Coords.IsNullOrEmpty())
                return false;

            return this.Coords.Exists(next => next.Col == coord.Col && next.Row == coord.Row);
        }

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
                    for(int i = 0; i < this.Coords.Count; i++)
                    {
                        if (!other.Coords[i].Equals(this.Coords[i]))
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
