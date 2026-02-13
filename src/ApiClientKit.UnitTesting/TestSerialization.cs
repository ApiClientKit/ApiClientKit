using ApiClientKit.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientKit.UnitTesting
{
    [TestClass]
    public sealed class TestSerialization
    {
        private readonly DefaultDataSerializer Serializer = new();


        [TestMethod]
        public void Test001_TestJsonPatch()
        {
            var body = new JsonPatchBody(new JsonPatchBodyElement(JsonPatchBodyOperationTypes.Add, "name", "John Smith"),
                                         new JsonPatchBodyElement(JsonPatchBodyOperationTypes.Replace, "age", 34),
                                         new JsonPatchBodyElement(JsonPatchBodyOperationTypes.Replace, "location", "New York City, USA"));

            var json = Serializer.Serialize(body);

            Assert.IsNotNull(json);

            Assert.IsTrue(json.StartsWith('['));
            Assert.IsTrue(json.EndsWith(']'));
        }

    }
}
