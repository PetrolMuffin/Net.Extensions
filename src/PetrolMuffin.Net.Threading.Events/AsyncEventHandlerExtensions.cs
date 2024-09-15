using JetBrains.Annotations;
using PetrolMuffin.Net.Extensions;

namespace PetrolMuffin.Net.Threading.Events;

/// <summary>
///   Async event handler extensions
/// </summary>
[PublicAPI]
public static class AsyncEventHandlerExtensions
{
    /// <summary>
    ///   Invoke async event handler
    /// </summary>
    /// <param name="eventHandler">event handler</param>
    /// <param name="cancellationToken">cancellation token for the operation</param>
    public static async ValueTask InvokeAsync(this Func<CancellationToken, Task> eventHandler, CancellationToken cancellationToken = default)
    {
        await InvokeAsync(eventHandler, InvokeEventAsync).ConfigureAwait(false);
        return;

        Task InvokeEventAsync(Func<CancellationToken, Task> handler) => Task.Run(async () => await handler(cancellationToken), cancellationToken);
    }

    /// <summary>
    ///   Invoke async event handler
    /// </summary>
    /// <param name="eventHandler">event handler</param>
    /// <param name="sender">sender of the event</param>
    /// <param name="eventArgs">event arguments</param>
    /// <param name="cancellationToken">cancellation token for the operation</param>
    /// <typeparam name="TSender">type of the sender</typeparam>
    /// <typeparam name="TEventArgs">type of the event arguments</typeparam>
    /// <exception cref="AggregateException">thrown when one of the handlers throws an exception</exception>
    public static async ValueTask InvokeAsync<TSender, TEventArgs>(this AsyncEventHandler<TSender, TEventArgs> eventHandler, TSender sender, TEventArgs eventArgs,
                                                                   CancellationToken cancellationToken = default)
        where TEventArgs : EventArgs
    {
        await InvokeAsync(eventHandler, InvokeEventAsync).ConfigureAwait(false);
        return;

        Task InvokeEventAsync(AsyncEventHandler<TSender, TEventArgs> handler) => handler(sender, eventArgs, cancellationToken);
    }

    /// <summary>
    ///   Invoke async event handler
    /// </summary>
    /// <param name="eventHandler">event handler</param>
    /// <param name="sender">sender of the event</param>
    /// <param name="eventArgs">event arguments</param>
    /// <param name="cancellationToken">cancellation token for the operation</param>
    /// <typeparam name="TEventArgs">type of the event arguments</typeparam>
    /// <exception cref="AggregateException">thrown when one of the handlers throws an exception</exception>
    public static async ValueTask InvokeAsync<TEventArgs>(this AsyncEventHandler<TEventArgs> eventHandler, object sender, TEventArgs eventArgs,
                                                          CancellationToken cancellationToken = default)
        where TEventArgs : EventArgs
    {
        await InvokeAsync(eventHandler, InvokeEventAsync).ConfigureAwait(false);
        return;

        Task InvokeEventAsync(AsyncEventHandler<TEventArgs> handler) => Task.Run(async () => await handler(sender, eventArgs, cancellationToken), cancellationToken);
    }

    private static async ValueTask InvokeAsync<TDelegate>(TDelegate handler, Func<TDelegate, Task> invokeAsync)
        where TDelegate : Delegate
    {
        var invocationList = handler.GetInvocationList();
        if (invocationList.IsNullOrEmpty()) return;

        var tasks = invocationList.Cast<TDelegate>().Select(invokeAsync).ToArray(); 
        await tasks.WhenAllWithExceptionAggregation().ConfigureAwait(false);
    }
}