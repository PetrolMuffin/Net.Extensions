using JetBrains.Annotations;

namespace PetrolMuffin.Net.Exceptions;

/// <summary>
///   Exception that is thrown when a variable is null or empty
/// </summary>
[Serializable]
[PublicAPI]
public class CollectionNullOrEmptyException<T> : Exception
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="CollectionNullOrEmptyException{T}" /> class
    /// </summary>
    /// <param name="message">exception message</param>
    /// <param name="variableName">name of the variable</param>
    public CollectionNullOrEmptyException(string? message, string variableName)
        : base($"'{variableName}' is null or empty. {message}")
    {
        VariableName = variableName;
    }
        
    /// <summary>
    ///   Gets the name of the variable that is empty
    /// </summary>
    public string VariableName { get; }
}