using ApiClientKit.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ApiClientKit.Serialization;

/// <summary>
/// A class that serializes json contents into objects
/// </summary>
/// <remarks>This class uses the <see cref="System.Text.Json"/> serializer</remarks>
public sealed class DefaultJsonSerializer: IApiSerializer
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    /// <inheritdoc/>
    public string Serialize<T>(T obj) => JsonSerializer.Serialize(obj, _options);

    /// <inheritdoc/>
    public T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, _options);

}
