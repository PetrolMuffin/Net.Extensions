using JetBrains.Annotations;

namespace PetrolMuffin.Net.Scaffolding;

/// <summary>
///   Base class for asynchronously disposable objects
/// </summary>
[PublicAPI]
public abstract class AsyncDisposable : IAsyncDisposable
{
    private volatile StateHolder _stateHolder;

    /// <summary>
    ///   Initializes a new instance of the <see cref="AsyncDisposable" /> class
    /// </summary>
    protected AsyncDisposable()
    {
        _stateHolder = new StateHolder(GetType().Name);
    }

    ~AsyncDisposable()
    {
        DisposeUnmanagedOnly();
    }

    /// <summary>
    ///   Dispose the object asynchronously
    /// </summary>
    /// <remarks>This method checks if the object has been disposed and calls <see cref="ReleaseManagedResourcesAsync" /> and <see cref="ReleaseUnmanagedResources" /></remarks>
    public async ValueTask DisposeAsync()
    {
        if (!_stateHolder.TryMarkDisposed()) return;

        await ReleaseManagedResourcesAsync();
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///   Throw an exception if the object has been disposed
    /// </summary>
    /// <exception cref="ObjectDisposedException">The object has been disposed</exception>
    protected void ThrowIfDisposed() => _stateHolder.ThrowIfDisposed();

    /// <summary>
    ///   Release managed resources asynchronously (override this method to dispose managed resources)
    /// </summary>
    protected virtual ValueTask ReleaseManagedResourcesAsync() => DisposeAsyncCore();

    /// <summary>
    ///   Dispose the object asynchronously (override this method to dispose asynchronous resources).
    ///   Obsolete: Override <see cref="ReleaseManagedResourcesAsync" /> and/or <see cref="ReleaseUnmanagedResources" /> instead.
    /// </summary>
    [Obsolete("Override ReleaseManagedResourcesAsync and/or ReleaseUnmanagedResources instead.")]
    protected virtual ValueTask DisposeAsyncCore() => default;

    /// <summary>
    ///   Release unmanaged resources (override this method to dispose unmanaged resources).
    ///   This method is also invoked from the finalizer if <see cref="DisposeAsync" /> was not called.
    /// </summary>
    protected virtual void ReleaseUnmanagedResources()
    {
    }

    private void DisposeUnmanagedOnly()
    {
        if (!_stateHolder.TryMarkDisposed()) return;
        ReleaseUnmanagedResources();
    }
}