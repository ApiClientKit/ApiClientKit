using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiClientKit.Serialization;

/// <summary>
/// A class to convert the <see cref="JsonPatchBody"/> into a format compatible to send as the request body to an API
/// </summary>
internal class JsonPatchBodyConverter : JsonConverter<JsonPatchBody>
{
    public override JsonPatchBody? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // TODO: Implement this functionality
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, JsonPatchBody value, JsonSerializerOptions options)
    {
        // Only serialize the array of elements
        JsonSerializer.Serialize(writer, value.Elements, options);
    }
}
