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
    /// A BoggleCoord is just a coordinate on the boggle board as a (row,col) tuple.
    /// </summary>
    [Serializable]
    public class BoggleCoord
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public override bool Equals(object obj)
        {
            BoggleCoord other = obj as BoggleCoord;
            return other != null &&
                other.Row == Row &&
                other.Col == Col;
        }
        public override int GetHashCode()
        {
            return Row.GetHashCode() ^ Col.GetHashCode();
        }
        public override string ToString()
        {
            return "{Row: " + Row + ", Col: " + Col + "}";
        }

        /// <summary>
        /// Helper that simply serializes this instance and returns it
        /// </summary>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Helper that will deserialize the given json string to a BoggleCoord
        /// and return it (or null if the given string is invalid json.)
        /// </summary>
        public static BoggleCoord FromJson(string json)
        {
            try
            {
                return json.IsNullOrEmpty() ? null : JsonConvert.DeserializeObject<BoggleCoord>(json);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Fashioned after TryParse* helpers: returns true and populates
        /// the given output param when the given json string is successfully deserialized,
        /// false/null otherwise.
        /// </summary>
        public static bool TryDeserializeAsJson(string json, out BoggleCoord deserialized)
        {
            deserialized = BoggleCoord.FromJson(json);
            return deserialized == null ? false : true;
        }
    }
}
