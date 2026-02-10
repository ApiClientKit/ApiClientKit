using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientKit.Authentication;

/// <summary>
/// Defines an interface to be implemented by token providers
/// </summary>
public interface IAuthTokenProvider
{
    /// <summary>
    /// Gets the token
    /// </summary>
    /// <returns></returns>
    Task<string> GetTokenAsync();
}
