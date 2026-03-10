using JetBrains.Annotations;

namespace PetrolMuffin.Net.Exceptions;

/// <summary>
///   Exception that is thrown when a variable is empty
/// </summary>
[Serializable]
[PublicAPI]
public sealed class StringNullOrEmptyException : CollectionNullOrEmptyException<string>
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="StringNullOrEmptyException" /> class
    /// </summary>
    /// <param name="message">exception message</param>
    /// <param name="variableName">name of the variable</param>
    public StringNullOrEmptyException(string? message, string variableName)
        : base(message, variableName)
    {
    }
}