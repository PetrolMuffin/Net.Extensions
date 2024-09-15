using JetBrains.Annotations;

namespace PetrolMuffin.Net.Threading;

/// <summary>
///   Extensions for <see cref="ReaderWriterLockSlim" />
/// </summary>
[PublicAPI]
public static class ReaderWriterLockSlimExtensions
{
    /// <summary>
    /// Wrapper of <see cref="ReaderWriterLockSlim.EnterReadLock"/> and <see cref="ReaderWriterLockSlim.ExitReadLock"/>
    /// </summary>
    /// <param name="lockSlim">Lock slim that will be used</param>
    /// <param name="func">Function to be executed with lock</param>
    /// <typeparam name="T">Type of returning value from function</typeparam>
    /// <returns>Function result</returns>
    /// <exception cref="LockRecursionException">See <see cref="ReaderWriterLockSlim.EnterReadLock"/> documentation</exception>
    /// <exception cref="ObjectDisposedException">See <see cref="ReaderWriterLockSlim.EnterReadLock"/> documentation</exception>
    /// <exception cref="SynchronizationLockException">See <see cref="ReaderWriterLockSlim.ExitReadLock"/> documentation</exception>
    public static T WithReadLock<T>(this ReaderWriterLockSlim lockSlim, Func<T> func)
    {
        try
        {
            lockSlim.EnterReadLock();
            return func();
        }
        finally
        {
            lockSlim.ExitReadLock();
        }
    }

    /// <summary>
    /// Wrapper of <see cref="ReaderWriterLockSlim.EnterWriteLock"/> and <see cref="ReaderWriterLockSlim.ExitWriteLock"/>
    /// </summary>
    /// <param name="lockSlim">Lock slim that will be used</param>
    /// <param name="func">Function to be executed with lock</param>
    /// <typeparam name="T">Type of returning value from function</typeparam>
    /// <returns>Function result</returns>
    /// <exception cref="LockRecursionException">See <see cref="ReaderWriterLockSlim.EnterWriteLock"/> documentation</exception>
    /// <exception cref="ObjectDisposedException">See <see cref="ReaderWriterLockSlim.EnterWriteLock"/> documentation</exception>
    /// <exception cref="SynchronizationLockException">See <see cref="ReaderWriterLockSlim.ExitWriteLock"/> documentation</exception>
    public static T WithWriteLock<T>(this ReaderWriterLockSlim lockSlim, Func<T> func)
    {
        try
        {
            lockSlim.EnterWriteLock();
            return func();
        }
        finally
        {
            lockSlim.ExitWriteLock();
        }
    }

    /// <summary>
    /// Wrapper of <see cref="ReaderWriterLockSlim.EnterUpgradeableReadLock"/> and <see cref="ReaderWriterLockSlim.ExitUpgradeableReadLock"/>
    /// </summary>
    /// <param name="lockSlim">Lock slim that will be used</param>
    /// <param name="func">Function to be executed with lock</param>
    /// <typeparam name="T">Type of returning value from function</typeparam>
    /// <returns>Function result</returns>
    /// <exception cref="LockRecursionException">See <see cref="ReaderWriterLockSlim.EnterUpgradeableReadLock"/> documentation</exception>
    /// <exception cref="ObjectDisposedException">See <see cref="ReaderWriterLockSlim.EnterUpgradeableReadLock"/> documentation</exception>
    /// <exception cref="SynchronizationLockException">See <see cref="ReaderWriterLockSlim.ExitUpgradeableReadLock"/> documentation</exception>
    public static T WithUpgradableReadLock<T>(this ReaderWriterLockSlim lockSlim, Func<T> func)
    {
        try
        {
            lockSlim.EnterUpgradeableReadLock();
            return func();
        }
        finally
        {
            lockSlim.ExitUpgradeableReadLock();
        }
    }

    /// <summary>
    /// Wrapper of <see cref="ReaderWriterLockSlim.EnterReadLock"/> and <see cref="ReaderWriterLockSlim.ExitReadLock"/>
    /// </summary>
    /// <param name="lockSlim">Lock slim that will be used</param>
    /// <param name="action">Action to be executed with lock</param>
    /// <exception cref="LockRecursionException">See <see cref="ReaderWriterLockSlim.EnterReadLock"/> documentation</exception>
    /// <exception cref="ObjectDisposedException">See <see cref="ReaderWriterLockSlim.EnterReadLock"/> documentation</exception>
    /// <exception cref="SynchronizationLockException">See <see cref="ReaderWriterLockSlim.ExitReadLock"/> documentation</exception>
    public static void WithReadLock(this ReaderWriterLockSlim lockSlim, Action action)
    {
        try
        {
            lockSlim.EnterReadLock();
            action();
        }
        finally
        {
            lockSlim.ExitReadLock();
        }
    }

    /// <summary>
    /// Wrapper of <see cref="ReaderWriterLockSlim.EnterWriteLock"/> and <see cref="ReaderWriterLockSlim.ExitWriteLock"/>
    /// </summary>
    /// <param name="lockSlim">Lock slim that will be used</param>
    /// <param name="action">Action to be executed with lock</param>
    /// <exception cref="LockRecursionException">See <see cref="ReaderWriterLockSlim.EnterWriteLock"/> documentation</exception>
    /// <exception cref="ObjectDisposedException">See <see cref="ReaderWriterLockSlim.EnterWriteLock"/> documentation</exception>
    /// <exception cref="SynchronizationLockException">See <see cref="ReaderWriterLockSlim.ExitWriteLock"/> documentation</exception>
    public static void WithWriteLock(this ReaderWriterLockSlim lockSlim, Action action)
    {
        try
        {
            lockSlim.EnterWriteLock();
            action();
        }
        finally
        {
            lockSlim.ExitWriteLock();
        }
    }

    /// <summary>
    /// Wrapper of <see cref="ReaderWriterLockSlim.EnterUpgradeableReadLock"/> and <see cref="ReaderWriterLockSlim.ExitUpgradeableReadLock"/>
    /// </summary>
    /// <param name="lockSlim">Lock slim that will be used</param>
    /// <param name="action">Action to be executed with lock</param>
    /// <exception cref="LockRecursionException">See <see cref="ReaderWriterLockSlim.EnterUpgradeableReadLock"/> documentation</exception>
    /// <exception cref="ObjectDisposedException">See <see cref="ReaderWriterLockSlim.EnterUpgradeableReadLock"/> documentation</exception>
    /// <exception cref="SynchronizationLockException">See <see cref="ReaderWriterLockSlim.ExitUpgradeableReadLock"/> documentation</exception>
    public static void WithUpgradableReadLock(this ReaderWriterLockSlim lockSlim, Action action)
    {
        try
        {
            lockSlim.EnterUpgradeableReadLock();
            action();
        }
        finally
        {
            lockSlim.ExitUpgradeableReadLock();
        }
    }
}