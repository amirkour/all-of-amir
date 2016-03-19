using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHunspell;
using DotNetUtils;

namespace DictionaryUtils
{
    public class HunspellWordDefiner : IWordDefiner, IDisposable
    {
        private Hunspell _hunspellDictionary;

        public const string LANGUAGE_AFF_FILENAME_EN_US = "en_us.aff";
        public const string LANGUAGE_DIC_FILENAME_EN_US = "en_us.dic";

        protected string _affFilepath;
        protected string _dicFilepath;

        public HunspellWordDefiner()
        {
            _affFilepath = LANGUAGE_AFF_FILENAME_EN_US;
            _dicFilepath = LANGUAGE_DIC_FILENAME_EN_US;
            _hunspellDictionary = new Hunspell(_affFilepath, _dicFilepath);
        }
        public HunspellWordDefiner(string affFilePath, string dicFilePath)
        {
            if (affFilePath.IsNullOrEmpty()) throw new Exception("Cannot initialize hunspell word definer without file path to aff file");
            if (dicFilePath.IsNullOrEmpty()) throw new Exception("Cannot initialize hunspell word definer without file path to dic file");
            _affFilepath = affFilePath;
            _dicFilepath = dicFilePath;
            _hunspellDictionary = new Hunspell(_affFilepath, _dicFilepath);
        }

        /// <summary>
        /// The Hunspell lib/api doesn't give us a clean way to test for prefixes,
        /// so this class will consider everything a prefix and hope that when the
        /// caller tries to go for an exact definition, then they'll get the right
        /// answer...
        /// </summary>
        public bool IsPrefix(string prefix) { return true; }

        /// <summary>
        /// Checks if the given word is an exact word by testing to see if it's
        /// spelled correctly in the Hunspell english dictionary ... returns true
        /// if so and false otherwise.
        /// </summary>
        public bool IsExactWord(string word)
        {
            if (word.IsNullOrEmpty())
                return false;

            bool spelledCorrectly = _hunspellDictionary.Spell(word);
            return spelledCorrectly;
        }

        /// <summary>
        /// Ensure the resources in our global dictionary object are cleaned
        /// </summary>
        public void Dispose()
        {
            if (_hunspellDictionary != null)
            {
                _hunspellDictionary.Dispose();
                _hunspellDictionary = null;
            }
        }
    }
}
