using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AonAwareDictionary;
using DotNetUtils;

namespace AonAwareDictionaryTest
{
    [TestClass]
    public class AADSvcTest
    {
        [TestMethod]
        public void AADSvcTest_IsPrefx_ReturnsFalse_ForExactWords()
        {
            AADSvc svc = new AADSvc();
            string word = "jealousy";
            Assert.IsFalse(svc.IsPrefix(word));
        }

        [TestMethod]
        public void AADSvcTest_IsPrefix_ReturnsFalse_ForGibberish()
        {
            AADSvc svc = new AADSvc();
            string gibberish = "afasdfasfasd";
            Assert.IsFalse(svc.IsPrefix(gibberish));
        }

        [TestMethod]
        public void AADSvcTest_IsPrefix_ReturnsFalse_ForNullOrWhitespace()
        {
            AADSvc svc = new AADSvc();
            Assert.IsFalse(svc.IsPrefix(null));
            Assert.IsFalse(svc.IsPrefix("   "));
        }

        [TestMethod]
        public void AADSvcTest_IsPrefix_ReturnsTrue_ForPrefix()
        {
            AADSvc svc = new AADSvc();
            string prefix = "hell";
            Assert.IsTrue(svc.IsPrefix(prefix));
        }

        [TestMethod]
        public void AADSvcTest_IsExactWord_ReturnsFalse_ForNullOrWhitespace()
        {
            AADSvc svc = new AADSvc();
            string word = null;
            Assert.IsFalse(svc.IsExactWord(word));

            word = "   ";
            Assert.IsFalse(svc.IsExactWord(word));
        }

        [TestMethod]
        public void AADSvcTest_IsExactWord_ReturnsFalse_ForGibberish()
        {
            AADSvc svc = new AADSvc();
            string gibberish = "adsfasdfaf";
            Assert.IsFalse(svc.IsExactWord(gibberish));
        }

        [TestMethod]
        public void AADSvcTest_IsExactWord_ReturnsFalse_ForNonWordPrefix()
        {
            AADSvc svc = new AADSvc();
            string prefix = "jealo"; // jealousy prefix
            Assert.IsFalse(svc.IsExactWord(prefix));
        }

        [TestMethod]
        public void AADSvcTest_IsExactWord_ReturnsTrue_ForWord()
        {
            AADSvc svc = new AADSvc();
            string word = "hello";
            Assert.IsTrue(svc.IsExactWord(word));
        }
    }
}
