using ApiClientKit.Authentication;
using ApiClientKit.Diagnostics;
using ApiClientKit.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiClientKit;


/// <summary>
/// Represents a Gateway that communicates with an API using the <see cref="HttpClient"/> component
/// </summary>
public sealed class HttpApiGateway: IApiGateway, IDisposable
{
    private readonly HttpClient? _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpApiGateway"/> class
    /// </summary>
    /// <param name="httpClient">Reference to the HttpClient for API communication</param>
    public HttpApiGateway(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpApiGateway"/> class
    /// </summary>
    /// <param name="httpClientFactory">A function to create the Http Client</param>
    public HttpApiGateway(Func<HttpClient> httpClientFactory)
    {
        _httpClient = httpClientFactory();
    }

    /// <inheritdoc/>
    /// <exception cref="ApiException">Thrown if the API returned a unsuccesfull status</exception>
    public async Task<T?> SendAsync<T>(ApiRequest request, IApiDataSerializer serializer, IAuthProvider? authProvider, IApiLogger? logger, CancellationToken ct = default)
    {
        // Validations
        if (_httpClient is null)
            throw new NullReferenceException("HTTP Client is not defined");

        // Build the Url for the request
        var url = BuildUrl(request.Path, request.Query);

        // Creates the message to be sent
        var message = new HttpRequestMessage(request.Method, url);

        // Append Headers
        foreach (var h in request.Headers)
            message.Headers.Add(h.Key, h.Value);

        // Append Body
        if (request.Method != HttpMethod.Get && request.Body is not null)
        {
            var json = serializer.Serialize(request.Body);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        // Append Authentication Headers
        if (authProvider is not null)
            await authProvider.ApplyAsync(message);

        // Logs Request
        logger?.LogRequest(message);

        // Sends the Request and awaits for results
        var response = await _httpClient.SendAsync(message, ct);
        var body = await response.Content.ReadAsStringAsync();

        // Logs Response
        logger?.LogResponse(response, body);

        // Throws API exception if failure happens
        if (!response.IsSuccessStatusCode)
            throw new ApiException("API call failed", (int)response.StatusCode, body);

        return string.IsNullOrWhiteSpace(body) ? default : serializer.Deserialize<T>(body);
    }

    /// <summary>
    /// Builds the Url for the API call
    /// </summary>
    /// <param name="path">The path for the call</param>
    /// <param name="query">The query string for the call</param>
    /// <returns>A string containing the complete url</returns>
    private static string BuildUrl(string path, Dictionary<string, string> query)
    {
        if (query == null || query.Count == 0)
            return path;

        var q = string.Join("&", query.Select(x => $"{x.Key}={Uri.EscapeDataString(x.Value)}"));
        return $"{path}?{q}";
    }

    // ==================================================
    // IDisposable Implementation
    // ==================================================
    private bool disposedValue;

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _httpClient?.Dispose();
            }

            disposedValue = true;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
