using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ApiClientKit.Diagnostics;

/// <summary>
/// Defines an interface to be implemented by Api Loggers
/// </summary>
public interface IApiLogger
{
    /// <summary>
    /// Logs a request
    /// </summary>
    /// <param name="request">The request to be logged</param>
    void LogRequest(HttpRequestMessage request);
    
    /// <summary>
    /// Logs a response
    /// </summary>
    /// <param name="response">The response to be logged</param>
    /// <param name="body">The body of the response being logged</param>
    void LogResponse(HttpResponseMessage response, string body);
}
