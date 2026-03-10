namespace PetrolMuffin.Net.Scaffolding;

internal sealed class StateHolder
{
    private readonly string _name;
    private volatile int _disposed;

    internal StateHolder(string name)
    {
        _name = name;
    }

    /// <summary>
    ///   Attempts to mark the object as disposed. Returns true for the first caller, false if already disposed.
    /// </summary>
    internal bool TryMarkDisposed() => Interlocked.CompareExchange(ref _disposed, 1, 0) == 0;

    /// <summary>
    ///   Throw an exception if the object has been disposed
    /// </summary>
    /// <exception cref="ObjectDisposedException">The object has been disposed</exception>
    public void ThrowIfDisposed()
    {
        if (_disposed == 1)
        {
            throw new ObjectDisposedException($"{_name} has been disposed");
        }
    }
}