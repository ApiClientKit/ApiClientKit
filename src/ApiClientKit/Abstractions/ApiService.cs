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
    protected IApiGateway? ApiGateway { get; set; }

    /// <summary>
    /// The data serializer component
    /// </summary>
    protected IApiDataSerializer? ApiSerializer { get; set; }

    /// <summary>
    /// The authentication provider
    /// </summary>
    protected IAuthProvider? AuthProvider { get; set; }

    /// <summary>
    /// The API Logger
    /// </summary>
    protected IApiLogger? ApiLogger { get; set; }

    // *************************************
    // Constructors
    // *************************************

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiService"/> class
    /// </summary>
    protected ApiService() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiService"/> class
    /// </summary>
    /// <param name="apiGateway">The gateway to communicate with the API</param>
    /// <param name="serializer">The data serializer component</param>
    protected ApiService(IApiGateway apiGateway, IApiDataSerializer serializer)
    {
        ApiGateway = apiGateway;
        ApiSerializer = serializer;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiService"/> class
    /// </summary>
    /// <param name="apiGateway">The gateway to communicate with the API</param>
    /// <param name="serializer">The data serializer component</param>
    /// <param name="authProvider">The authentication provider</param>
    protected ApiService(IApiGateway apiGateway, IApiDataSerializer serializer, IAuthProvider authProvider)
    {
        ApiGateway = apiGateway;
        ApiSerializer = serializer;
        AuthProvider = authProvider;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiService"/> class
    /// </summary>
    /// <param name="apiGateway">The gateway to communicate with the API</param>
    /// <param name="serializer">The data serializer component</param>
    /// <param name="authProvider">The authentication provider</param>
    /// <param name="apiLogger">The API logger</param>
    protected ApiService(IApiGateway apiGateway, IApiDataSerializer serializer, IAuthProvider authProvider, IApiLogger apiLogger)
    {
        ApiGateway = apiGateway;
        ApiSerializer = serializer;
        AuthProvider = authProvider;
        ApiLogger = apiLogger;
    }

    /// <summary>
    /// Sends a message to the API thru the gateway
    /// </summary>
    /// <typeparam name="T">The type of the object to return from the message</typeparam>
    /// <param name="request">The API request</param>
    /// <param name="ct">A cancellation token</param>
    /// <returns>A task with the results of the execution</returns>
    /// <exception cref="NullReferenceException">Throw when the <see cref="ApiGateway"/> or the <see cref="ApiSerializer"/> are not defined.</exception>
    protected async Task<ApiResponse<T?>> SendAsync<T>(ApiRequest request, CancellationToken ct = default)
    {
        if (ApiGateway is null) throw new NullReferenceException("API Gateway not defined");
        if (ApiSerializer is null) throw new NullReferenceException("API Serializer not defined");

        return await ApiGateway.SendAsync<T>(request, ApiSerializer, AuthProvider, ApiLogger, ct);
    }

    // *************************************
    // IDisposable Implementation
    // *************************************

    private bool disposedValue;

    /// <inheritdoc/>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // Dispose Gateway if Disposable
                if (ApiGateway is IDisposable d) d?.Dispose();
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
