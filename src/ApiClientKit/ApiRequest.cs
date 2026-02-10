using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ApiClientKit;

/// <summary>
/// Defines the details of an Api Request
/// </summary>
public sealed class ApiRequest
{
    /// <summary>
    /// Method used when performing the request (GET, POST, PUT, DELETE, PATCH)
    /// </summary>
    public HttpMethod Method { get; set; } = HttpMethod.Get;

    /// <summary>
    /// Path to submit the request
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// Defines the body accompanying the request
    /// </summary>
    public object? Body { get; set; }

    /// <summary>
    /// A dictionary of headers to pass to the request
    /// </summary>
    public Dictionary<string, string> Headers { get; internal set; } = new();

    /// <summary>
    /// A dictionary containing query string parameters
    /// </summary>
    public Dictionary<string, string> Query { get; internal set; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiRequest"/> class
    /// </summary>
    public ApiRequest()
    {
        
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiRequest"/> class
    /// </summary>
    /// <param name="method">The method to use</param>
    /// <param name="path">The path to submit the request to</param>
    public ApiRequest(HttpMethod method, string path)
    {
        Method = method;
        Path = path;
    }
    
}