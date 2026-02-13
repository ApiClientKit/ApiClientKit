using ApiClientKit.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiClientKit.Serialization;

/// <summary>
/// A class that serializes json contents into objects and vice-versa
/// </summary>
/// <remarks>This class uses the <see cref="System.Text.Json"/> serializer</remarks>
public sealed class DefaultDataSerializer: IApiDataSerializer
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultDataSerializer"/> class
    /// </summary>
    public DefaultDataSerializer()
    {
        // Append converters for enumerations using the EnumMember attribute
        _options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    }

    /// <inheritdoc/>
    public string Serialize<T>(T obj) => JsonSerializer.Serialize(obj, _options);

    /// <inheritdoc/>
    public T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, _options);

}
