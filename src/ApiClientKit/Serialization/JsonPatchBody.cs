using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiClientKit.Serialization;

/// <summary>
/// Defines the structure for a JSON Patch document, as defined in <see href="https://datatracker.ietf.org/doc/html/rfc6902">RFC 6902</see>
/// </summary>
[JsonConverter(typeof(JsonPatchBodyConverter))]
public sealed class JsonPatchBody
{
    /// <summary>
    /// An array of elements to include in the patch operation
    /// </summary>
    public JsonPatchBodyElement[] Elements { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonPatchBody"/> class
    /// </summary>
    /// <param name="args">The elements to include in the patch operation</param>
    public JsonPatchBody(params JsonPatchBodyElement[] args)
    {
        Elements = args;
    }
}

/// <summary>
/// Defines an element that goes inside a JSON Patch document
/// </summary>
public sealed class JsonPatchBodyElement
{
    /// <summary>
    /// The operation to execute
    /// </summary>
    [JsonPropertyOrder(0)]
    [JsonPropertyName("op")]
    public JsonPatchBodyOperationTypes Operation { get; set; } = JsonPatchBodyOperationTypes.Replace;

    /// <summary>
    /// The path to apply the operation to
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonPropertyName("path")]
    public string Path { get; set; }

    /// <summary>
    /// The value
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonPropertyName("value")]
    public object? Value { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="JsonPatchBodyElement"/> class
    /// </summary>
    /// <param name="operation">The operation to execute</param>
    /// <param name="path">The path to apply the operation to</param>
    /// <param name="value">The value</param>
    public JsonPatchBodyElement(JsonPatchBodyOperationTypes operation, string path, object? value)
    {
        Operation = operation;
        Path = path;
        Value = value;
    }
}

/// <summary>
/// The list of acceptable operations in a JSON Patch Document
/// </summary>
public enum JsonPatchBodyOperationTypes
{
    /// <summary>
    /// Add a new or array element
    /// </summary>
    [EnumMember(Value = "add")]
    Add,
    /// <summary>
    /// Delete a field
    /// </summary>
    [EnumMember(Value = "remove")]
    Remove,
    /// <summary>
    /// Update an existing field
    /// </summary>
    [EnumMember(Value = "replace")]
    Replace,
    /// <summary>
    /// Move a value to a different locatin
    /// </summary>
    [EnumMember(Value = "move")]
    Move,
    /// <summary>
    /// Copy a value to a new location
    /// </summary>
    [EnumMember(Value = "copy")]
    Copy,
    /// <summary>
    /// Verify a value before applying operations
    /// </summary>
    [EnumMember(Value = "test")]
    Test
}
