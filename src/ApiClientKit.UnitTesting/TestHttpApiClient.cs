using ApiClientKit;
using ApiClientKit.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientKit.UnitTesting
{
    [TestClass]
    public sealed class TestHttpApiClient
    {
        private static IHost? Host;

#pragma warning disable IDE0060 // Remove unused parameter

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            // Create In-Memory API
            var builder = new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder.UseTestServer();
                    webBuilder.Configure(app =>
                    {
                        app.Map("/test", app =>
                        {
                            app.Run(async ctx =>
                            {
                                await ctx.Response.WriteAsync("In-memory API!");
                            });
                        });

                        app.Map($"/{CountriesApiService.DEFAULT_PATH}", app =>
                        {
                            app.Run(async ctx =>
                            {
                                var serializer = new DefaultJsonSerializer();
                                await ctx.Response.WriteAsync(serializer.Serialize(Models.Country.Countries));
                            });
                        });
                    });
                });

            Host = builder.Start();
        }

#pragma warning restore IDE0060 // Remove unused parameter

        [TestMethod]
        public async Task Test001_LocalAPICall()
        {
            Assert.IsNotNull(Host);

            var httpClient = Host.GetTestClient();

            var service = new CountriesApiService(new HttpApiGateway(httpClient), new DefaultJsonSerializer(), null, null);
            var countries = await service.GetCountriesAsync();

            // Validations
            Assert.IsNotNull(countries);



        }
    }

}
