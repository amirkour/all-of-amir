using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetUtils;
using AonAwareDictionary.com.aonaware.services;

namespace AonAwareDictionary
{
    /// <summary>
    /// AonAware Dictionary Service - a class that encapsulates a few helper functions
    /// that will use AonAware's dictionary web service to help us define a few english
    /// words.
    /// 
    /// See their website/service:
    /// http://services.aonaware.com/DictService/DictService.asmx
    /// </summary>
    public class AADSvc
    {
        // the id of the english dictionary we're gonna use
        // (magic string complements of AAD's service.)
        public static string DICTIONARY_ENGLISH_ID = "gcide";

        // and a pretty description corresponding to the previous dictionary id
        // (magic string complements of AAD's service.)
        public static string DICTIONARY_ENGLISH_DESCRIPTION = "The Collaborative International Dictionary of English v.0.44";

        // we'll have to define words with strategies - here are some pertinent ones:
        // (note that these magic strings were just taken straight from AAD's service.)
        public static string DICTIONARY_STRATEGY_EXACT_ID = "exact";
        public static string DICTIONARY_STRATEGY_PREFIX_ID = "prefix";

        // here's a pointer to the actual web service
        protected DictService _service;

        /// <summary>
        /// Default construction - instantiate the dictionary service so it's ready to go!
        /// </summary>
        public AADSvc() { _service = new com.aonaware.services.DictService(); }

        /// <summary>
        /// Helper that returns true if the given word is an exact word - 
        /// for our purposes, an 'exact word' is simply one that has at 
        /// least one definition.
        /// </summary>
        public bool IsExactWord(string word)
        {
            word = (word == null) ? null : word.Trim();
            if (word.IsNullOrEmpty()) return false;
            WordDefinition wd = _service.DefineInDict(DICTIONARY_ENGLISH_ID, word);

            return wd != null && !wd.Definitions.IsNullOrEmpty();
        }

        /// <summary>
        /// Helper that returns true if the given string is considered a prefix
        /// to at least one english word, false otherwise.  
        /// 
        /// For the purposes of
        /// this API: if the given string has only one prefix that ends up being
        /// exactly the same, then we won't consider the caller's word a prefix.
        /// 
        /// For example, "jealousy" is not a prefix for the word "jealousy"
        /// </summary>
        public bool IsPrefix(string prefix)
        {
            prefix = (prefix == null) ? null : prefix.Trim();
            if (prefix.IsNullOrEmpty())
                return false;

            DictionaryWord[] matches = _service.MatchInDict(DICTIONARY_ENGLISH_ID, prefix, DICTIONARY_STRATEGY_PREFIX_ID);

            if (matches.IsNullOrEmpty())
                return false;

            // if there's only one match and it's the same as the caller's
            // prefix, we won't consider this a prefix - it's most likely
            // an actual word in this case.
            if (matches.Length == 1 && !matches[0].Word.IsNullOrEmpty() && matches[0].Word.ToLower().Equals(prefix.ToLower()))
                return false;

            return true;
        }
    }
}
