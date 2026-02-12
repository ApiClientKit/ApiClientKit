using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientKit.UnitTesting
{
    [TestClass]
    public class TestHttpApiRequest
    {

        [TestMethod]
        public void Test001_QueryStringFormatting()
        {
            var h = new HttpApiRequest(HttpMethod.Get, "data");
            h.AppendQueryStringParameter("status", "released");

            Assert.AreEqual("?status=released", h.QueryString);
        }

        [TestMethod]
        public void Test002_QueryStringFormatting()
        {
            var h = new HttpApiRequest(HttpMethod.Get, "data");
            h.AppendQueryStringParameter("status", "created");
            h.AppendQueryStringParameter("status", "released");

            Assert.AreEqual("?status=created&status=released", h.QueryString);
        }

        [TestMethod]
        public void Test003_QueryStringFormatting()
        {
            var h = new HttpApiRequest(HttpMethod.Get, "data", QueryStringArrayStyles.CommaSeparated);
            h.AppendQueryStringParameter("status", "created");
            h.AppendQueryStringParameter("status", "released");

            Assert.AreEqual("?status=created,released", h.QueryString);
        }

        [TestMethod]
        public void Test004_QueryStringFormatting()
        {
            var h = new HttpApiRequest(HttpMethod.Get, "data", QueryStringArrayStyles.CommaSeparated);
            h.AppendQueryStringParameter("status", "created");
            h.AppendQueryStringParameter("status", "released");
            h.AppendQueryStringParameter("name", "usa");

            Assert.AreEqual("?status=created,released&name=usa", h.QueryString);
        }

        [TestMethod]
        public void Test005_QueryStringFormatting_Combined()
        {
            var h = new HttpApiRequest(HttpMethod.Get, "data", "region=america");
            h.AppendQueryStringParameter("status", "created");
            h.AppendQueryStringParameter("status", "released");
            h.AppendQueryStringParameter("name", "usa");

            Assert.AreEqual("?status=created&status=released&name=usa&region=america", h.QueryString);
        }

        [TestMethod]
        public void Test006_QueryStringFormatting_Combined()
        {
            var h = new HttpApiRequest(HttpMethod.Get, "data", "region=america", QueryStringArrayStyles.CommaSeparated);
            h.AppendQueryStringParameter("status", "created");
            h.AppendQueryStringParameter("status", "released");
            h.AppendQueryStringParameter("name", "usa");

            Assert.AreEqual("?status=created,released&name=usa&region=america", h.QueryString);
        }

    }
}
