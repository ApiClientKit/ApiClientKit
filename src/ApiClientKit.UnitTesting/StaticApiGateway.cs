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
    /// <summary>
    /// Provides Static Data for the API Call
    /// </summary>
    internal class StaticApiGateway : IApiGateway
    {
        
        public async Task<ApiResponse<T?>> SendAsync<T>(ApiRequest request, IApiDataSerializer serializer, IAuthProvider? authProvider, IApiLogger? logger, CancellationToken ct = default)
        {
            if (request is not HttpApiRequest httpApiRequest)
                throw new InvalidCastException($"Request is not of type {nameof(HttpApiRequest)}");
                
            // If a GET request is done to the "countries" path, it will return the serialized list of countries
            if (httpApiRequest.Method == HttpMethod.Get && string.Equals(request.Path, CountriesApiService.DEFAULT_PATH, StringComparison.OrdinalIgnoreCase))
            {
                var body = serializer.Serialize(Country.Countries);
                await Task.Delay(1, ct);
                return new HttpApiResponse<T?>(System.Net.HttpStatusCode.OK, serializer.Deserialize<T>(body));
            }

            return new HttpApiResponse<T?>(System.Net.HttpStatusCode.OK);
        }
    }
}
