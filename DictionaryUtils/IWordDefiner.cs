using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryUtils
{
    /// <summary>
    /// Impelementations of this interface know how to perform some word-related operations,
    /// such as whether or not a string is considered a 'word' or a 'prefix'.
    /// 
    /// For now, implementors only need to care about English.
    /// </summary>
    public interface IWordDefiner
    {
        /// <summary>
        /// Returns true if the given string is considered a proper prefix.
        /// 
        /// For example: "hell" is a proper prefix for "hello"
        /// 
        /// Note that a word is not it's own prefix - in other words, "hello" is
        /// not a prefix for "hello" - in this case, "hello" is an exact word
        /// (see the API for exact words in this interface.)
        /// </summary>
        bool IsPrefix(string prefix);

        /// <summary>
        /// Returns true if the given string is an exact word, false otherwise.
        /// 
        /// An "exact word" is one that has a definition in a dictionary - which
        /// dictionary?  That's up to the implementor ...
        /// </summary>
        bool IsExactWord(string word);
    }
}
