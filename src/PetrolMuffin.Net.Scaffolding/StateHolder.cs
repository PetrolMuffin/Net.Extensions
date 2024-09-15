namespace PetrolMuffin.Net.Scaffolding;

internal sealed class StateHolder
{
    internal bool IsDisposed { get; private set; }

    internal void Disposed() => IsDisposed = true;

    /// <summary>
    ///   Throw an exception if the object has been disposed
    /// </summary>
    /// <exception cref="ObjectDisposedException">The object has been disposed</exception>
    public void ThrowIfDisposed()
    {
        if (IsDisposed) throw new ObjectDisposedException(ToString());
    }
}