using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ApiClientKit;

/// <summary>
/// Defines an API Request done via HTTP/S
/// </summary>
public class HttpApiRequest: ApiRequest
{
    // ============================================================
    // Properties
    // ============================================================

    /// <summary>
    /// Method used when performing the request (GET, POST, PUT, DELETE, PATCH)
    /// </summary>
    public HttpMethod Method { get; set; } = HttpMethod.Get;

    /// <summary>
    /// Defines the formatting rules for arrays in a query string
    /// </summary>
    /// <remarks>Depending on how the API was developed, when the same field is passed multiple times in a query string, it will be displayed differently. Refer to the <see cref="QueryStringArrayStyles"/> for more information.</remarks>
    public QueryStringArrayStyles ArrayStyle { get; set; } = QueryStringArrayStyles.Default;

    /// <summary>
    /// A dictionary of additional headers to pass to the request
    /// </summary>
    /// <remarks>Use this to specify additional headers to the ones already defined by the Http Client</remarks>
    public Dictionary<string, string> Headers { get; } = new();

    /// <summary>
    /// Internal dictionary containing the query string parameters
    /// </summary>
    internal Dictionary<string, List<string>> _queryDictionary = new();

    private string? _queryString;

    /// <summary>
    /// Defines the Query String for the request
    /// </summary>
    /// <remarks>Any additional parameters added thru the <see cref="AppendQueryStringParameter(string, string)"/> will be combined in the final query string.</remarks>
    public string QueryString
    {
        get
        {
            // If the dictionary is empty, nothing was set
            if (_queryDictionary.Count == 0 && string.IsNullOrWhiteSpace(_queryString))
                return string.Empty;

            // Return the query string data based on the dictionary
            var sbReturn = new StringBuilder(50);

            switch (ArrayStyle)
            {
                case QueryStringArrayStyles.CommaSeparated:

                    // Comma Separated:  &status=1,2,3
                    sbReturn.Append(string.Join("&", _queryDictionary.Select(x => $"{x.Key}={string.Join(",", x.Value.Select(y => Uri.EscapeDataString(y)))}")));
                    break;

                default:

                    // Default:  &status=1&status=2&status=3
                    int iItem = 0;

                    foreach (var kvp in _queryDictionary)
                    {
                        foreach (var value in kvp.Value)
                        {
                            if (iItem++ > 0)
                                sbReturn.Append('&');

                            sbReturn.Append(kvp.Key);
                            sbReturn.Append('=');
                            sbReturn.Append(Uri.EscapeDataString(value));
                        }
                    }

                    break;
            }

            // Adds the remaining of the query string
            if (_queryString is not null)
            {
                 if (_queryDictionary.Count > 0)
                    sbReturn.Append('&');
                
                sbReturn.Append(_queryString);
            }

            // Append the "?" symbol to the begining of the query string if there is data
            if (sbReturn.Length > 0)
                sbReturn.Insert(0, "?");

            return sbReturn.ToString();
        }
        set
        {
            _queryString = value;
        }
    }

    // ============================================================
    // Constructors
    // ============================================================

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpApiRequest"/> class
    /// </summary>
    /// <param name="method">The type of request</param>
    /// <param name="path">The path to do the request against</param>
    public HttpApiRequest(HttpMethod method, string path): this(method, path, string.Empty) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpApiRequest"/> class
    /// </summary>
    /// <param name="method">The type of request</param>
    /// <param name="path">The path to do the request against</param>
    /// <param name="queryString">A query string to pass along the request</param>
    public HttpApiRequest(HttpMethod method, string path, string queryString)
    {
        Method = method;
        Path = path;
        QueryString = queryString;
    }

    // ============================================================
    // Methods
    // ============================================================

    /// <summary>
    /// Appends a Query String Parameter
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void AppendQueryStringParameter(string key, string value)
    {
        if (_queryDictionary.ContainsKey(key))
        {
            _queryDictionary[key].Add(value);
            return;
        }

        _queryDictionary.Add(key, new List<string>() { value });
    }
}

/// <summary>
/// Defines the style for formatting arrays in a query string
/// </summary>
public enum QueryStringArrayStyles: byte
{
    /// <summary>
    /// Repeating values will have the same field name set multiple times
    /// </summary>
    /// <example>&amp;status=1&amp;status=2&amp;status=3</example>
    Default = 0,
    /// <summary>
    /// Repeating values will have only one instance of the field displayed, and multiple values separated by comma
    /// </summary>
    /// <example>&amp;status=1,2,3</example>
    CommaSeparated = 1
        
}