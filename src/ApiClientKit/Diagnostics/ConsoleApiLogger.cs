using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ApiClientKit.Diagnostics;

/// <summary>
/// A class that logs Api communications to the console
/// </summary>
public sealed class ConsoleApiLogger: IApiLogger
{
    /// <inheritdoc/>
    public void LogRequest(HttpRequestMessage request) => Console.WriteLine($"> {request.Method} {request.RequestUri}");

    /// <inheritdoc/>
    public void LogResponse(HttpResponseMessage response, string body) => Console.WriteLine($"< {(int)response.StatusCode} {body}");
}
