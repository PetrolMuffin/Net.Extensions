using JetBrains.Annotations;
using PetrolMuffin.Net.Extensions;

namespace PetrolMuffin.Net.Threading;

/// <summary>
///   Extensions for <see cref="Task"/>
/// </summary>
[PublicAPI]
public static class TaskExtensions
{
    /// <summary>
    ///   Wait for all tasks to complete
    /// </summary>
    /// <param name="tasks">tasks to wait for</param>
    /// <typeparam name="T">type of task result</typeparam>
    /// <exception cref="AggregateException">thrown when several tasks throw an exception</exception>
    public static async Task<T[]> WhenAllWithExceptionAggregation<T>(this IEnumerable<Task<T>> tasks)
    {
        var tasksArray = tasks as Task<T>[] ?? tasks.ToArray();

        try
        {
            return await Task.WhenAll(tasksArray).ConfigureAwait(false);
        }
        catch (Exception)
        {
            tasksArray.Cast<Task>().ToArray().ThrowAggregatedExceptions();
            throw;
        }
    }

    /// <summary>
    ///   Wait for all tasks to complete
    /// </summary>
    /// <param name="tasks">tasks to wait for</param>
    /// <exception cref="AggregateException">thrown when several tasks throw an exception</exception>
    public static async Task WhenAllWithExceptionAggregation(this IEnumerable<Task> tasks)
    {
        var tasksArray = tasks as Task[] ?? tasks.ToArray();

        try
        {
            await Task.WhenAll(tasksArray).ConfigureAwait(false);
        }
        catch (Exception)
        {
            tasksArray.ThrowAggregatedExceptions();
            throw;
        }
    }

    /// <summary>
    ///   Wait for all tasks to complete
    /// </summary>
    /// <param name="task">task to wait for</param>
    public static async Task SkipCancellationException(this Task task)
    {
        try
        {
            await task.ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            // ignore
        }
    }

    private static void ThrowAggregatedExceptions(this Task[] tasks)
    {
        var exceptions = tasks.Where(t => t.Exception != null).SelectMany(t => t.Exception!.Flatten().InnerExceptions).ToArray();
        if (exceptions.IsEmpty()) return;
                
        throw exceptions.IsSingle() ? exceptions[0] : new AggregateException(exceptions);
    }
}