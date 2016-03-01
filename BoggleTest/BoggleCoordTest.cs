using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Boggle;
using Newtonsoft.Json;

namespace BoggleTest
{
    [TestClass]
    public class BoggleCoordTest
    {
        [TestMethod]
        public void BoggleCoordTest_FromJson_ReturnsNull_WhenNullPassedIn()
        {
            BoggleCoord coord = BoggleCoord.FromJson(null);
            Assert.IsNull(coord);
        }

        [TestMethod]
        public void BoggleCoordTest_FromJson_ReturnsNull_ForInvalidJson()
        {
            BoggleCoord coord = BoggleCoord.FromJson("hi world");
            Assert.IsNull(coord);
        }

        [TestMethod]
        public void BoggleCoordTest_FromJson_ReturnsBoggleCoord_ForValidJson()
        {
            BoggleCoord coord = new BoggleCoord() { Row = 2, Col = 5 };
            string json = JsonConvert.SerializeObject(coord);

            BoggleCoord deserialized = BoggleCoord.FromJson(json);
            Assert.AreEqual(coord, deserialized);
        }

        [TestMethod]
        public void BoggleCoordTest_TryDeserializeAsJson_ReturnsTrue_ForValidJson()
        {
            BoggleCoord coord = new BoggleCoord() { Row = 2, Col = 5 };
            string json = JsonConvert.SerializeObject(coord);

            BoggleCoord deserialized = null;
            bool result = BoggleCoord.TryDeserializeAsJson(json, out deserialized);
            Assert.IsTrue(result);
            Assert.AreEqual(coord, deserialized);
        }

        [TestMethod]
        public void BoggleCoordTest_TryDeserializeAsJson_ReturnsFalse_ForAnythingButValidJson()
        {
            // pass in null
            BoggleCoord result = null;
            Assert.IsFalse(BoggleCoord.TryDeserializeAsJson(null, out result));
            Assert.IsNull(result);

            // pass in bad json
            result = null;
            Assert.IsFalse(BoggleCoord.TryDeserializeAsJson("hi there", out result));
            Assert.IsNull(result);
        }

        [TestMethod]
        public void BoggleCoordTest_ToJson_Works()
        {
            BoggleCoord coord = new BoggleCoord();
            string json = JsonConvert.SerializeObject(coord);
            Assert.AreEqual(coord.ToJson(), json);

            coord = new BoggleCoord() { Row = 2, Col = 23 };
            json = JsonConvert.SerializeObject(coord);
            Assert.AreEqual(coord.ToJson(), json);
        }
    }
}
