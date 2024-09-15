using JetBrains.Annotations;

namespace PetrolMuffin.Net.Scaffolding;

/// <summary>
///   Base class for disposable objects
/// </summary>
[PublicAPI]
public abstract class Disposable : IDisposable
{
    private StateHolder _stateHolder = new();

    ~Disposable()
    {
        Dispose(false);
    }

    /// <summary>
    ///   Dispose the object
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///   Throw an exception if the object has been disposed
    /// </summary>
    /// <exception cref="ObjectDisposedException">The object has been disposed</exception>
    protected void ThrowIfDisposed() => _stateHolder.ThrowIfDisposed();

    /// <summary>
    ///   Release managed resources (override this method to dispose managed resources)
    /// </summary>
    protected virtual void ReleaseManagedResources()
    {
    }
        
    /// <summary>
    ///   Release unmanaged resources (override this method to dispose unmanaged resources)
    /// </summary>
    protected virtual void ReleaseUnmanagedResources()
    {
    }

    private void Dispose(bool disposing)
    {
        if (_stateHolder.IsDisposed) return;
        if (disposing) ReleaseManagedResources();
        ReleaseUnmanagedResources();
        _stateHolder.Disposed();
    }
}