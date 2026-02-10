using System;
using System.Collections.Generic;
using System.Text;

namespace ApiClientKit;

/// <summary>
/// Defines an exception throw during the Api call
/// </summary>
public sealed class ApiException: Exception
{
    /// <summary>
    /// The status code
    /// </summary>
    public int StatusCode { get; }
    
    /// <summary>
    /// The body of the API response
    /// </summary>
    public string? ResponseBody { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiException"/> class
    /// </summary>
    /// <param name="message">The error message</param>
    /// <param name="statusCode">The status code</param>
    /// <param name="body">The response body</param>
    public ApiException(string message, int statusCode, string? body) : base(message)
    {
        StatusCode = statusCode;
        ResponseBody = body;
    }

}
