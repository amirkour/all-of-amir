using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Boggle;
using Newtonsoft.Json;
using DotNetUtils;

namespace BoggleTest
{
    [TestClass]
    public class BoggleResultTest
    {
        [TestMethod]
        public void BoggleResultTest_ToJson_WorksForNullProperties()
        {
            BoggleResult result = new BoggleResult() { Word = null, Coords = null };
            string json = JsonConvert.SerializeObject(result);
            Assert.AreEqual(result.ToJson(), json);
        }

        [TestMethod]
        public void BoggleResultTest_ToJson_WorksForNonNulls()
        {
            BoggleResult result = new BoggleResult() { Word = "hi there", Coords = new System.Collections.Generic.List<BoggleCoord>() };
            result.Coords.Add(new BoggleCoord()
            {
                Row = 1,
                Col = 32
            });

            string json = JsonConvert.SerializeObject(result);
            Assert.AreEqual(result.ToJson(), json);
        }

        [TestMethod]
        public void BoggleResultTest_FromJson_ReturnsNull_ForNull()
        {
            BoggleResult result = BoggleResult.FromJson(null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void BoggleResultTest_FromJson_ReturnsNull_FromInvalidJson()
        {
            BoggleResult result = BoggleResult.FromJson("hi world");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void BoggleResultTest_FromJson_WorksForValidJson()
        {
            BoggleResult result = new BoggleResult() { Word = "bla", Coords = new List<BoggleCoord>() };
            result.Coords.Add(new BoggleCoord() { Row = 1, Col = 2 });
            string json = JsonConvert.SerializeObject(result);
            BoggleResult deserialized = BoggleResult.FromJson(json);
            Assert.AreEqual(result, deserialized);

            // actually, this should work too:
            Assert.AreEqual(new BoggleResult(), BoggleResult.FromJson("{}"));

            // and so should this:
            Assert.AreEqual(new BoggleResult(), BoggleResult.FromJson("{hi:'world'}"));
        }

        [TestMethod]
        public void BoggleResultTest_TryDeserializeAsJson_ReturnsFalse_ForBadJson()
        {
            BoggleResult result = null;
            Assert.IsFalse(BoggleResult.TryDeserializeAsJson(null, out result));
            Assert.IsNull(result);

            result = null;
            Assert.IsFalse(BoggleResult.TryDeserializeAsJson("hi world", out result));
            Assert.IsNull(result);
        }

        [TestMethod]
        public void BoggleResultTest_TryDeserializeAsJson_ReturnsTrue_ForGoodJson()
        {
            BoggleResult result = new BoggleResult() { Word = "hi" };
            string json = result.ToJson();

            BoggleResult deserialized = null;
            Assert.IsTrue(BoggleResult.TryDeserializeAsJson(json, out deserialized));
            Assert.AreEqual(result, deserialized);
        }

        [TestMethod]
        public void BoggleResultTest_Equals_ReturnsTrue_WhenListsAreBothNull()
        {
            BoggleResult one = new BoggleResult() { Coords = null };
            BoggleResult two = new BoggleResult() { Coords = null };
            Assert.AreEqual(one, two);
            Assert.AreEqual(two, one);
        }

        [TestMethod]
        public void BoggleResultTest_Equals_ReturnsTrue_WhenListsAreBothEmpty()
        {
            BoggleResult one = new BoggleResult() { Coords = new List<BoggleCoord>() };
            BoggleResult two = new BoggleResult() { Coords = new List<BoggleCoord>() };
            Assert.AreEqual(one, two);
            Assert.AreEqual(two, one);
        }

        [TestMethod]
        public void BoggleResultTest_Equals_ReturnsFalse_WhenOneListIsNull()
        {
            BoggleResult one = new BoggleResult() { Coords = new List<BoggleCoord>() };
            BoggleResult two = new BoggleResult() { Coords = null };
            Assert.AreNotEqual(one, two);
            Assert.AreNotEqual(two, one);
        }

        [TestMethod]
        public void BoggleResultTest_Equals_ReturnsTrue_WhenCoordListsAreEqual()
        {
            BoggleResult one = new BoggleResult() { Coords = new List<BoggleCoord>() };
            BoggleResult two = new BoggleResult() { Coords = new List<BoggleCoord>() };

            BoggleCoord coordOne = new BoggleCoord() { Row = 1, Col = 2 };
            one.Coords.Add(coordOne);
            two.Coords.Add(coordOne);
            Assert.AreEqual(one, two);
            Assert.AreEqual(two, one);

            one.Coords.Add(new BoggleCoord() { Row = 123123, Col = 234234 });
            two.Coords.Add(new BoggleCoord() { Row = 123123, Col = 234234 });
            Assert.AreEqual(one, two);
            Assert.AreEqual(two, one);
        }

        [TestMethod]
        public void BoggleResultTest_Equals_ReturnsFalse_WhenCoordListsAreUnequal()
        {
            BoggleResult one = new BoggleResult() { Coords = new List<BoggleCoord>() };
            BoggleResult two = new BoggleResult() { Coords = new List<BoggleCoord>() };

            one.Coords.Add(new BoggleCoord() { Row = 1 });
            two.Coords.Add(new BoggleCoord() { Row = 2 });

            Assert.AreNotEqual(one, two);
            Assert.AreNotEqual(two, one);
        }

        [TestMethod]
        public void BoggleResultTest_Equals_ReturnsTrue_WhenWordsAreEqual()
        {
            BoggleResult one = new BoggleResult() { Word = "hi" };
            BoggleResult two = new BoggleResult() { Word = "hi" };
            Assert.AreEqual(one, two);
            Assert.AreEqual(two, one);
        }

        [TestMethod]
        public void BoggleResultTest_Equals_ReturnsTrue_WhenWordsBothNull()
        {
            BoggleResult one = new BoggleResult() { Word = null };
            BoggleResult two = new BoggleResult() { Word = null };
            Assert.AreEqual(one, two);
            Assert.AreEqual(two, one);
        }

        [TestMethod]
        public void BoggleResultTest_Equals_ReturnsFalse_WhenWordsNotEqual()
        {
            BoggleResult one = new BoggleResult() { Word = null };
            BoggleResult two = new BoggleResult() { Word = "hi" };
            Assert.AreNotEqual(one, two);
            Assert.AreNotEqual(two, one);

            one.Word = "bye";
            Assert.AreNotEqual(one, two);
            Assert.AreNotEqual(two, one);
        }

        [TestMethod]
        public void BoggleResultTest_AddLetterForNewResult_ReturnsNewResult()
        {
            BoggleResult original = new BoggleResult()
            {
                Word = "hell",
                Coords = new List<BoggleCoord>()
            };
            original.Coords.Add(new BoggleCoord() { Row = 0, Col = 0 });

            BoggleLetter letter = new BoggleLetter() { Row = 0, Col = 1, Letter = 'o' };

            BoggleResult result = BoggleResult.AddLetterForNewResult(original, letter);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Coords != null && result.Coords.Count == original.Coords.Count + 1);
        }

        [TestMethod]
        public void BoggleResultTest_AddLetterForNewResult_ThrowsException_IfEitherArgNull()
        {
            Exception e = null;
            try { BoggleResult.AddLetterForNewResult(null, new BoggleLetter()); }
            catch(Exception ex) { e = ex; }

            Assert.IsNotNull(e);

            e = null;
            try { BoggleResult.AddLetterForNewResult(new BoggleResult(), null); }
            catch (Exception ex) { e = ex; }
            Assert.IsNotNull(e);
        }
    }
}
