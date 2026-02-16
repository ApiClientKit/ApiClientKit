using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ApiClientKit;

/// <summary>
/// Defines the structure of an API Response
/// </summary>
public abstract class ApiResponse
{
    /// <summary>
    /// Defines whether the execution of the API call was succesful or not
    /// </summary>
    /// <remarks>When an exception was thrown, there might be no status code response, but this field will inform if that happened.</remarks>
    public bool IsExecutionSuccesful { get; protected set; }

    /// <summary>
    /// The exception that caused the execution to not be succesful
    /// </summary>
    public Exception? ExecutionException { get; set; }
}

/// <summary>
/// Defines an API Response with contents of a specific type
/// </summary>
/// <typeparam name="T">The type for the API Response</typeparam>
public abstract class ApiResponse<T>: ApiResponse
{
    /// <summary>
    /// The contents of the response
    /// </summary>
    public T? Contents { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class
    /// </summary>
    protected ApiResponse()
    {
        
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class
    /// </summary>
    /// <param name="exception">The execption that caused the execution to fail</param>
    /// <remarks>This constructor assumes the execution was not succesful (an exception was thrown).</remarks>
    protected ApiResponse(Exception exception)
    {
        ExecutionException = exception;
        IsExecutionSuccesful = false;
    }
    
}
