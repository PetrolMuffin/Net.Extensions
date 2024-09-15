using JetBrains.Annotations;

namespace PetrolMuffin.Net.Scaffolding;

/// <summary>
///   Base class for disposable objects
/// </summary>
[PublicAPI]
public abstract class AsyncDisposable : IAsyncDisposable
{
    private StateHolder _stateHolder = new();

    /// <summary>
    ///   Dispose the object asynchronously
    /// </summary>
    /// <remarks>This method checks if the object has been disposed and calls <see cref="DisposeAsyncCore" /></remarks>
    public async ValueTask DisposeAsync()
    {
        if (_stateHolder.IsDisposed) return;

        await DisposeAsyncCore();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///   Dispose the object asynchronously (override this method to dispose asynchronous resources)
    /// </summary>
    protected virtual async ValueTask DisposeAsyncCore() => await default(ValueTask);

    /// <summary>
    ///   Throw an exception if the object has been disposed
    /// </summary>
    /// <exception cref="ObjectDisposedException">The object has been disposed</exception>
    protected void ThrowIfDisposed() => _stateHolder.ThrowIfDisposed();
}