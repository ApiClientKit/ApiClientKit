using ApiClientKit;
using ApiClientKit.Serialization;
using System.Net;
using System.Security.Cryptography;

namespace ApiClientKit.UnitTesting
{
    [TestClass]
    public sealed class TestStaticApiClient
    {
        [TestMethod]
        public void TestStaticAPI()
        {
            // Create Static Api Service
            var service = new CountriesApiService(new StaticApiGateway(), new DefaultDataSerializer());
            var countries = service.GetCountriesAsync().GetAwaiter().GetResult();

            // Validations
            Assert.IsNotNull(countries);
            Assert.AreEqual(Models.Country.Countries.Length, countries.Length);
        }
    }
}
