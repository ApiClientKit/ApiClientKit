using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ApiClientKit.Http;

/// <summary>
/// Defines an HTTP API Response
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class HttpApiResponse<T>: ApiResponse<T>
{
    /// <summary>
    /// The Response Status Code
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// The origin of the <see cref="ApiResponse"/>
    /// </summary>
    public HttpResponseMessage? OriginalMessage { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpApiResponse{T}"/> class
    /// </summary>
    /// <param name="statusCode">The response status code</param>
    /// <remarks>This constructor assumes the execution was succesful (no exceptions where thrown).</remarks>
    public HttpApiResponse(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpApiResponse{T}"/> class
    /// </summary>
    /// <param name="statusCode">The response status code</param>
    /// <param name="contents">The deserialized contents of the response</param>
    /// <remarks>This constructor assumes the execution was succesful (no exceptions where thrown).</remarks>
    public HttpApiResponse(HttpStatusCode statusCode, T? contents)
    {
        StatusCode = statusCode;
        Contents = contents;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpApiResponse{T}"/> class
    /// </summary>
    /// <param name="originalMessage">The origin of the response</param>
    /// <param name="contents">The deserialized contents of the response</param>
    /// <remarks>This constructor assumes the execution was succesful (no exceptions where thrown).</remarks>
    public HttpApiResponse(HttpResponseMessage originalMessage, T? contents)
    {
        OriginalMessage = originalMessage;
        Contents = contents;

        StatusCode = originalMessage.StatusCode;
        IsExecutionSuccesful = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpApiResponse{T}"/> class
    /// </summary>
    /// <param name="exception">The execption that caused the execution to fail</param>
    /// <remarks>This constructor assumes the execution was not succesful (an exception was thrown).</remarks>
    public HttpApiResponse(Exception exception) : base(exception) { }
}
