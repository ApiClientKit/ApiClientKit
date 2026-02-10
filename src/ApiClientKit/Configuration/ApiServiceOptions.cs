using System;
using System.Collections.Generic;
using System.Text;

namespace ApiClientKit.Configuration;

/// <summary>
/// Defines options for the API Service
/// </summary>
public class ApiServiceOptions
{
    /// <summary>
    /// Base Url for Api Calls
    /// </summary>
    public string? BaseUrl { get; set; }

    /// <summary>
    /// Represents the maximum timeout allowed for a transaction to complete
    /// </summary>
    /// <remarks>The default is 30 seconds</remarks>
    public int TimeoutSeconds { get; set; } = 30;
}