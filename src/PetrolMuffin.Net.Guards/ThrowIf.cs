using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using PetrolMuffin.Net.Exceptions;
using PetrolMuffin.Net.Extensions;

namespace PetrolMuffin.Net.Guards;

/// <summary>
///   ThrowIf class is a helper class for checking and throwing exceptions in a more readable way.
/// </summary>
[PublicAPI]
public static class ThrowIf
{
    /// <summary>
    ///   Throw helper class for stream related checks.
    /// </summary>
    [PublicAPI]
    public static class Stream
    {
        /// <summary>
        ///   Checks if the stream is null or not readable and throws an exception if it is.
        /// </summary>
        /// <param name="stream">stream to check</param>
        /// <param name="objectName">name of the object</param>
        /// <param name="customMessage">custom message to use in the exception</param>
        /// <exception cref="NullReferenceException">thrown if the stream is null</exception>
        /// <exception cref="InvalidDataException">thrown if the stream is not readable</exception>
        [ContractAnnotation("stream:null => halt")]
        public static void IsNullOrNotReadable(System.IO.Stream? stream, string objectName, string? customMessage = null)
        {
            if (stream == null) ThrowHelper.NullReference(objectName, customMessage);
            if (stream!.CanRead == false) throw new InvalidDataException($"'{objectName}' stream is not readable. {customMessage}");
        }

        /// <summary>
        ///   Checks if the stream is null or not writable and throws an exception if it is.
        /// </summary>
        /// <param name="stream">stream to check</param>
        /// <param name="objectName">name of the object</param>
        /// <param name="customMessage">custom message to use in the exception</param>
        /// <exception cref="NullReferenceException">thrown if the stream is null</exception>
        /// <exception cref="InvalidDataException">thrown if the stream is not writable</exception>
        [ContractAnnotation("stream:null => halt")]
        public static void IsNullOrNotWritable(System.IO.Stream? stream, string objectName, string? customMessage = null)
        {
            if (stream == null) ThrowHelper.NullReference(objectName, customMessage);
            if (stream!.CanWrite == false) throw new InvalidDataException($"'{objectName}' stream is not writable. {customMessage}");
        }

        /// <summary>
        ///   Checks if the stream is null or not seekable and throws an exception if it is.
        /// </summary>
        /// <param name="stream">stream to check</param>
        /// <param name="objectName">name of the object</param>
        /// <param name="customMessage">custom message to use in the exception</param>
        /// <exception cref="NullReferenceException">thrown if the stream is null</exception>
        /// <exception cref="InvalidDataException">thrown if the stream is not seekable</exception>
        [ContractAnnotation("stream:null => halt")]
        public static void IsNullOrNotSeekable(System.IO.Stream? stream, string objectName, string? customMessage = null)
        {
            if (stream == null) ThrowHelper.NullReference(objectName, customMessage);
            if (stream!.CanSeek == false) throw new InvalidDataException($"'{objectName}' stream is not seekable. {customMessage}");
        }
    }

    /// <summary>
    ///   Throw helper class for variable related checks.
    /// </summary>
    [PublicAPI]
    public static class Variable
    {
        /// <summary>
        ///   Checks if the variable is null and throws an exception if it is.
        /// </summary>
        /// <param name="variable">variable to check</param>
        /// <param name="variableName">name of the variable</param>
        /// <param name="customMessage">custom message to use in the exception</param>
        /// <typeparam name="T">type of the variable</typeparam>
        /// <exception cref="NullReferenceException">thrown if the variable is null</exception>
        [ContractAnnotation("variable:null => halt")]
        public static void IsNull<T>([NoEnumeration] T? variable, string variableName, string? customMessage = null)
            where T : class
        {
            if (variable != null) return;

            ThrowHelper.NullReference(variableName, customMessage);
        }

        /// <summary>
        ///   Checks if the variable is null and throws an exception if it is.
        /// </summary>
        /// <param name="variable">variable to check</param>
        /// <param name="variableName">name of the variable</param>
        /// <param name="customMessage">custom message to use in the exception</param>
        /// <typeparam name="T">type of the variable</typeparam>
        /// <exception cref="NullReferenceException">thrown if the variable is null</exception>
        [ContractAnnotation("variable:null => halt")]
        public static void IsNull<T>(T? variable, string variableName, string? customMessage = null)
            where T : struct
        {
            if (variable != null) return;

            ThrowHelper.NullReference(variableName, customMessage);
        }

        /// <summary>
        ///   Checks if the variable is null or empty and throws an exception if it is.
        /// </summary>
        /// <param name="variable">variable to check</param>
        /// <param name="variableName">name of the variable</param>
        /// <param name="customMessage">custom message to use in the exception</param>
        /// <typeparam name="T">type of the variable</typeparam>
        /// <exception cref="NullReferenceException">thrown if the variable is null</exception>
        /// <exception cref="StringNullOrEmptyException">thrown if the variable is empty</exception>
        [ContractAnnotation("variable:null => halt")]
        public static void IsNullOrEmpty<T>(IEnumerable<T>? variable, string variableName, string? customMessage = null)
        {
            if (variable == null || variable.IsEmpty()) ThrowHelper.StringNullOrEmpty(variableName, customMessage);
        }

        /// <summary>
        ///   Checks if the variable is null or empty and throws an exception if it is.
        /// </summary>
        /// <param name="variable">variable to check</param>
        /// <param name="variableName">name of the variable</param>
        /// <param name="customMessage">custom message to use in the exception</param>
        /// <exception cref="StringNullOrEmptyException">thrown if the variable is null, empty or whitespace</exception>
        [ContractAnnotation("variable:null => halt")]
        public static void IsNullOrWhitespace(string? variable, string variableName, string? customMessage = null)
        {
            if (variable == null || string.IsNullOrWhiteSpace(variable)) ThrowHelper.StringNullOrEmpty(variableName, customMessage);
        }
    }

    /// <summary>
    ///   Throw helper class for argument related checks.
    /// </summary>
    [PublicAPI]
    public static class Argument
    {
        /// <summary>
        ///   Checks if the argument is less than the value and throws an exception if it is not.
        /// </summary>
        /// <param name="argument">argument to check</param>
        /// <param name="value">value to compare against</param>
        /// <param name="argumentName">name of the argument</param>
        /// <param name="customMessage">custom message to use in the exception</param>
        /// <typeparam name="T">type of the argument</typeparam>
        /// <exception cref="ArgumentOutOfRangeException">thrown if the argument is less than the expected value</exception>
        public static void IsLessThan<T>(T argument, T value, string argumentName, string? customMessage = null)
            where T : IComparable<T>
        {
            if (argument.CompareTo(value) < 0) throw new ArgumentOutOfRangeException(argumentName, argument, customMessage);
        }

        /// <summary>
        ///   Checks if the argument is greater than the value and throws an exception if it is not.
        /// </summary>
        /// <param name="argument">argument to check</param>
        /// <param name="value">value to compare against</param>
        /// <param name="argumentName">name of the argument</param>
        /// <param name="customMessage">custom message to use in the exception</param>
        /// <typeparam name="T">type of the argument</typeparam>
        /// <exception cref="ArgumentOutOfRangeException">thrown if the argument is greater than the expected value</exception>
        public static void IsGreaterThan<T>(T argument, T value, string argumentName, string? customMessage = null)
            where T : IComparable<T>
        {
            if (argument.CompareTo(value) > 0) throw new ArgumentOutOfRangeException(argumentName, argument, customMessage);
        }

        /// <summary>
        ///   Checks if the argument is null and throws an exception if it is.
        /// </summary>
        /// <param name="argument">argument to check</param>
        /// <param name="argumentName">name of the argument</param>
        /// <param name="customMessage">custom message to use in the exception</param>
        /// <typeparam name="T">type of the argument</typeparam>
        /// <exception cref="ArgumentNullException">thrown if the argument is null</exception>
        [ContractAnnotation("argument:null => halt")]
        public static void IsNull<T>([NoEnumeration] T? argument, string argumentName, string? customMessage = null)
            where T : class
        {
            if (argument != null) return;

            ThrowHelper.ArgumentNull(argumentName, customMessage);
        }

        /// <summary>
        ///   Checks if the argument is null and throws an exception if it is.
        /// </summary>
        /// <param name="argument">argument to check</param>
        /// <param name="argumentName">name of the argument</param>
        /// <param name="customMessage">custom message to use in the exception</param>
        /// <typeparam name="T">type of the argument</typeparam>
        /// <exception cref="ArgumentNullException">thrown if the argument is null</exception>
        [ContractAnnotation("argument:null => halt")]
        public static void IsNull<T>(T? argument, string argumentName, string? customMessage = null)
            where T : struct
        {
            if (argument != null) return;

            ThrowHelper.ArgumentNull(argumentName, customMessage);
        }

        /// <summary>
        ///   Checks if the argument is null or empty and throws an exception if it is.
        /// </summary>
        /// <param name="argument">argument to check</param>
        /// <param name="argumentName">name of the argument</param>
        /// <param name="customMessage">custom message to use in the exception</param>
        /// <typeparam name="T">type of the argument</typeparam>
        /// <exception cref="StringNullOrEmptyException">thrown if the argument is null or empty</exception>
        [ContractAnnotation("argument:null => halt")]
        public static void IsNullOrEmpty<T>(IEnumerable<T>? argument, string argumentName, string? customMessage = null)
        {
            if (argument == null || argument.IsEmpty()) ThrowHelper.StringNullOrEmpty(argumentName, customMessage);
        }

        /// <summary>
        ///   Checks if the argument is null or empty and throws an exception if it is.
        /// </summary>
        /// <param name="argument">argument to check</param>
        /// <param name="argumentName">name of the argument</param>
        /// <param name="customMessage">custom message to use in the exception</param>
        /// <exception cref="StringNullOrEmptyException">thrown if the argument is null, empty or whitespace</exception>
        [ContractAnnotation("argument:null => halt")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNullOrWhitespace(string? argument, string argumentName, string? customMessage = null)
        {
            if (argument == null || string.IsNullOrWhiteSpace(argument)) ThrowHelper.StringNullOrEmpty(argumentName, customMessage);
        }
    }
}