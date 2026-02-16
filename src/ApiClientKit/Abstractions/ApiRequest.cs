using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ApiClientKit;

/// <summary>
/// Defines the minimum values expected for an API request
/// </summary>
public abstract class ApiRequest
{
    /// <summary>
    /// Path to submit the request
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// Defines the body accompanying the request
    /// </summary>
    public object? Body { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiRequest"/> class
    /// </summary>
    public ApiRequest()
    {
        
    }
}