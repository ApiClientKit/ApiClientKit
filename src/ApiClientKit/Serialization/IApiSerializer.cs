using System;
using System.Collections.Generic;
using System.Text;

namespace ApiClientKit.Serialization;

/// <summary>
/// Defines an interface to be implemented by an Api Serializer
/// </summary>
public interface IApiSerializer
{
    /// <summary>
    /// Serializes an object into a string
    /// </summary>
    /// <typeparam name="T">The type of the object</typeparam>
    /// <param name="obj">The object to be serialized</param>
    /// <returns>A serialized representation of the object</returns>
    string Serialize<T>(T obj);
    
    /// <summary>
    /// Deserializes an object from a string into an object
    /// </summary>
    /// <typeparam name="T">The type of the object</typeparam>
    /// <param name="json">The json representation of the object</param>
    /// <returns>An instance of the object based on the json input</returns>
    T? Deserialize<T>(string json);
}