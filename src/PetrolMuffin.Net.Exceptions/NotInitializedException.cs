namespace PetrolMuffin.Net.Exceptions;

/// <summary>
///   Thrown if a service was not initialized.
/// </summary>
[Serializable]
public class NotInitializedException : InvalidOperationException
{
    /// <summary>
    ///   Create a new <see cref="NotInitializedException"/>.
    /// </summary>
    /// <param name="service">The service that was not initialized. Will be set automatically.</param>
    /// <remarks>Message will be set to "{service.Name} was not initialized."</remarks>
    public NotInitializedException(Type service)
        : this(service, $"{service.Name} was not initialized.")
    {
    }

    /// <summary>
    ///   Create a new <see cref="NotInitializedException"/>.
    /// </summary>
    /// <param name="service">The service that was not initialized.</param>
    /// <param name="message">The message to display.</param>
    public NotInitializedException(Type service, string message)
        : base(message)
    {
        Service = service;
    }

    /// <summary>
    ///   The service that was not initialized.
    /// </summary>
    public Type Service { get; }
}