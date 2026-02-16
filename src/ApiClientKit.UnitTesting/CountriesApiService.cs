using ApiClientKit.Authentication;
using ApiClientKit.Diagnostics;
using ApiClientKit.Http;
using ApiClientKit.Serialization;
using ApiClientKit.UnitTesting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientKit.UnitTesting
{
    internal class CountriesApiService : ApiService
    {
        public const string DEFAULT_PATH = "countries/europe";

        public CountriesApiService(IApiGateway apiGateway, IApiDataSerializer serializer) : base(apiGateway, serializer)
        {

        }

        public async Task<Country[]?> GetCountriesAsync()
        {
            var apiRequest = new HttpApiRequest(HttpMethod.Get, DEFAULT_PATH);
            var apiResponse = await SendAsync<Country[]>(apiRequest);
            return apiResponse.Contents;
        }
    }
}
