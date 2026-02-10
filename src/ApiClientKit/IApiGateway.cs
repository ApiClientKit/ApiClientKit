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
/// Defines an interface to be implemented by classes that communicate with an API
/// </summary>
/// <remarks>By default, communication is done using the <see cref="HttpApiGateway"/>. 
/// You can create your own gateways other types of communication, or for Unit Testing purposes.</remarks>
public interface IApiGateway
{
    /// <summary>
    /// Sends a request to the Api
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request">The Api Request</param>
    /// <param name="serializer">Reference to the Serializer</param>
    /// <param name="authProvider">Reference to the Authentication Provider</param>
    /// <param name="logger">Reference to the Api Logger</param>
    /// <param name="ct">The cancellation token</param>
    /// <returns>A task that sends a request to the Api</returns>
    Task<T?> SendAsync<T>(ApiRequest request, IApiDataSerializer serializer, IAuthProvider? authProvider, IApiLogger? logger, CancellationToken ct = default);
}
