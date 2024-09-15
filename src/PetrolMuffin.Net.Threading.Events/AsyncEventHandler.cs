namespace PetrolMuffin.Net.Threading.Events;

/// <summary>
///   Async event handler
/// </summary>
/// <typeparam name="TSender">type of the sender</typeparam>
/// <typeparam name="TEventArgs">type of the event arguments</typeparam>
public delegate Task AsyncEventHandler<in TSender, in TEventArgs>(TSender sender, TEventArgs e, CancellationToken cancellationToken = default)
    where TEventArgs : EventArgs;

/// <summary>
///   Async event handler
/// </summary>
/// <typeparam name="TEventArgs">type of the event arguments</typeparam>
public delegate Task AsyncEventHandler<in TEventArgs>(object sender, TEventArgs e, CancellationToken cancellationToken = default)
    where TEventArgs : EventArgs;