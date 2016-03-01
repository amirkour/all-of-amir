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
    }
}
