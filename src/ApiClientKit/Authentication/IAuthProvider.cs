using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiClientKit.Authentication
{
    /// <summary>
    /// Defines an interface to be implemented by any authentication provider
    /// </summary>
    public interface IAuthProvider
    {
        /// <summary>
        /// Apply authentication headers to the request
        /// </summary>
        /// <param name="request">The Http Message</param>
        /// <returns>A task that apply authentication headers to the request</returns>
        Task ApplyAsync(HttpRequestMessage request);
    }

}
