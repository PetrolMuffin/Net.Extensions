using JetBrains.Annotations;
using PetrolMuffin.Net.Exceptions;

namespace PetrolMuffin.Net.Guards;

/// <summary>
///   Helper class for throwing exceptions
/// </summary>
internal static class ThrowHelper
{
    /// <summary>
    ///   Throws <see cref="ArgumentNullException" /> with argument name and custom message
    /// </summary>
    /// <param name="argumentName">name of the argument</param>
    /// <param name="customMessage">custom message</param>
    /// <exception cref="ArgumentNullException">exception that is thrown</exception>
    [ContractAnnotation(" => halt")]
    internal static void ArgumentNull(string argumentName, string? customMessage = null)
    {
        if (customMessage == null)
        {
            throw new ArgumentNullException(argumentName);
        }
            
        throw new ArgumentNullException(argumentName, customMessage);
    }

    /// <summary>
    ///   Throws <see cref="NullReferenceException" /> with the message
    /// </summary>
    /// <param name="objectName">name of the variable</param>
    /// <param name="customMessage">custom message</param>
    /// <exception cref="NullReferenceException">exception that is thrown</exception>
    [ContractAnnotation(" => halt")]
    internal static void NullReference(string objectName, string? customMessage = null)
    {
        if (customMessage == null)
        {
            throw new NullReferenceException($"'{objectName}' is null.");
        }
            
        throw new NullReferenceException($"'{objectName}' is null. {customMessage}");
    }

    /// <summary>
    ///   Throws <see cref="StringNullOrEmptyException" /> with argument name and custom message
    /// </summary>
    /// <param name="variableName">name of the variable</param>
    /// <param name="customMessage">custom message</param>
    /// <exception cref="StringNullOrEmptyException">exception that is thrown</exception>
    [ContractAnnotation(" => halt")]
    internal static void StringNullOrEmpty(string variableName, string? customMessage = null) => throw new StringNullOrEmptyException(customMessage, variableName);
}