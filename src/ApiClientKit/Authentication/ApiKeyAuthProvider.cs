using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientKit.Authentication;

/// <summary>
/// Defines an authentication provider that uses an Api Key for authentication
/// </summary>
public sealed class ApiKeyAuthProvider: IAuthProvider
{
    private readonly string _apiKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiKeyAuthProvider"/> class
    /// </summary>
    /// <param name="apiKey">The Api key to use for authentication</param>
    public ApiKeyAuthProvider(string apiKey)
    {
        _apiKey = apiKey;
    }

    /// <inheritdoc/>
    public Task ApplyAsync(HttpRequestMessage request)
    {
        request.Headers.Add("Authorization", $"Bearer {_apiKey}");
        return Task.CompletedTask;
    }
}
