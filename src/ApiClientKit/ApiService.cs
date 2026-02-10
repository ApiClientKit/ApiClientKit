using ApiClientKit.Authentication;
using ApiClientKit.Diagnostics;
using ApiClientKit.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiClientKit;

/// <summary>
/// Defines the base class for any API Service
/// </summary>
public abstract class ApiService: IDisposable
{
    // *************************************
    // Properties
    // *************************************

    /// <summary>
    /// The gateway to communicate with the API
    /// </summary>
    protected IApiGateway ApiGateway => _apiGateway;
    private readonly IApiGateway _apiGateway;

    /// <summary>
    /// The data serializer component
    /// </summary>
    protected IApiDataSerializer ApiSerializer => _apiSerializer;
    private readonly IApiDataSerializer _apiSerializer;

    /// <summary>
    /// The authentication provider
    /// </summary>
    protected IAuthProvider? AuthProvider => _authProvider;
    private readonly IAuthProvider? _authProvider;

    /// <summary>
    /// The API Logger
    /// </summary>
    protected IApiLogger? ApiLogger => _apiLogger;
    private readonly IApiLogger? _apiLogger;
    private bool disposedValue;

    // *************************************
    // Constructors
    // *************************************

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiService"/>
    /// </summary>
    /// <param name="apiGateway">The gateway to communicate with the API</param>
    /// <param name="serializer">The data serializer component</param>
    /// <param name="authProvider">The authentication provider</param>
    /// <param name="apiLogger">The API logger</param>
    protected ApiService(IApiGateway apiGateway, IApiDataSerializer serializer, IAuthProvider? authProvider, IApiLogger? apiLogger)
    {
        _apiGateway = apiGateway;
        _apiSerializer = serializer;
        _authProvider = authProvider;
        _apiLogger = apiLogger;
    }

    /// <summary>
    /// Sends a message to the API thru the gateway
    /// </summary>
    /// <typeparam name="T">The type of the object in the message</typeparam>
    /// <param name="request">The API request</param>
    /// <param name="ct">A cancellation token</param>
    /// <returns></returns>
    protected async Task<T?> SendAsync<T>(ApiRequest request, CancellationToken ct = default)
    {
        return await _apiGateway.SendAsync<T>(request, _apiSerializer, _authProvider, _apiLogger, ct);
    }

    // *************************************
    // IDisposable Implementation
    // *************************************

    /// <inheritdoc/>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // Dispose Gateway if Disposable
                if (_apiGateway is IDisposable d) d?.Dispose();
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
