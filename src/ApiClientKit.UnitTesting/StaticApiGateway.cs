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
    /// <summary>
    /// Provides Static Data for the API Call
    /// </summary>
    internal class StaticApiGateway : IApiGateway
    {
        
        public async Task<T?> SendAsync<T>(ApiRequest request, IApiDataSerializer serializer, IAuthProvider? authProvider, IApiLogger? logger, CancellationToken ct = default)
        {
            // If a GET request is done to the "countries" path, it will return the serialized list of countries
            if (request.Method == HttpMethod.Get && string.Equals(request.Path, CountriesApiService.DEFAULT_PATH, StringComparison.OrdinalIgnoreCase))
            {
                var body = serializer.Serialize(Country.Countries);
                await Task.Delay(1, ct);
                return serializer.Deserialize<T>(body);
            }

            return default;
        }
    }
}
