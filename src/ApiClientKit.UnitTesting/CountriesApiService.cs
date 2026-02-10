using ApiClientKit;
using ApiClientKit.Authentication;
using ApiClientKit.Diagnostics;
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

        public CountriesApiService(IApiGateway apiGateway, IApiDataSerializer serializer, IAuthProvider? authProvider, IApiLogger? apiLogger) : base(apiGateway, serializer, authProvider, apiLogger)
        {

        }

        public async Task<Country[]?> GetCountriesAsync()
        {
            var apiRequest = new ApiRequest(HttpMethod.Get, DEFAULT_PATH);
            return await SendAsync<Country[]>(apiRequest);
        }
    }
}
