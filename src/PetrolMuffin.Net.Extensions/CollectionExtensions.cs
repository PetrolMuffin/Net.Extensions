using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using JetBrains.Annotations;

namespace PetrolMuffin.Net.Extensions;

/// <summary>
///     Extension methods for collections a la Linq
/// </summary>
[PublicAPI]
public static class CollectionExtensions
{
    /// <summary>
    ///   Returns a new <see cref="ConcurrentDictionary{TKey,TValue}" /> from the given <paramref name="source" />
    /// </summary>
    /// <param name="source">source collection</param>
    /// <param name="keySelector">a function to extract a key from each element</param>
    /// <param name="elementSelector">a transform function to produce a result element value from each element</param>
    /// <typeparam name="TSource">The type of source collection items</typeparam>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" /></typeparam>
    /// <typeparam name="TValue">The type of the value returned by <paramref name="elementSelector" /></typeparam>
    /// <returns><see cref="ConcurrentDictionary{TKey,TValue}" /> that contains values selected from the input collection</returns>
    [Pure]
    public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source,
                                                                                                   Func<TSource, TKey> keySelector,
                                                                                                   Func<TSource, TValue> elementSelector)
        where TKey : notnull =>
        new(source.ToDictionary(keySelector, elementSelector));

    /// <summary>
    ///   Returns a new <see cref="ConcurrentDictionary{TKey,TValue}" /> from the given <paramref name="source" />
    /// </summary>
    /// <param name="source">source collection</param>
    /// <param name="keySelector">a function to extract a key from each element</param>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" /></typeparam>
    /// <typeparam name="TSource">The type of source collection items</typeparam>
    /// <returns><see cref="ConcurrentDictionary{TKey,TValue}" /> that contains values selected from the input collection</returns>
    [Pure]
    public static ConcurrentDictionary<TKey, TSource> ToConcurrentDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        where TKey : notnull =>
        new(source.ToDictionary(keySelector));

    /// <summary>
    ///   Creates <see cref="Dictionary{TKey,TValue}" /> based on source collection
    /// </summary>
    /// <param name="keyValueCollection">Source collection that used as a base for <see cref="Dictionary{TKey,TValue}" /></param>
    /// <typeparam name="TKey">Type of keys in the collection</typeparam>
    /// <typeparam name="TValue">Type of values in the collection</typeparam>
    /// <returns><see cref="Dictionary{TKey,TValue}" /> that contains values selected from the input collection</returns>
    [Pure]
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey Key, TValue Value)> keyValueCollection)
        where TKey : notnull =>
        keyValueCollection.ToDictionary(pair => pair.Key, pair => pair.Value);

    /// <summary>
    ///   Creates <see cref="Dictionary{TKey,TValue}" /> based on source collection
    /// </summary>
    /// <param name="keyValueCollection">Source collection that used as a base for <see cref="Dictionary{TKey,TValue}" /></param>
    /// <typeparam name="TKey">Type of keys in the collection</typeparam>
    /// <typeparam name="TValue">Type of values in the collection</typeparam>
    /// <returns><see cref="Dictionary{TKey,TValue}" /> that contains values selected from the input collection</returns>
    [Pure]
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<Tuple<TKey, TValue>> keyValueCollection)
        where TKey : notnull =>
        keyValueCollection.ToDictionary(pair => pair.Item1, pair => pair.Item2);

    /// <summary>
    ///   Creates <see cref="Dictionary{TKey,TValue}" /> based on source collection
    /// </summary>
    /// <param name="keyValueCollection">Source collection that used as a base for <see cref="Dictionary{TKey,TValue}" /></param>
    /// <typeparam name="TKey">Type of keys in the collection</typeparam>
    /// <typeparam name="TValue">Type of values in the collection</typeparam>
    /// <returns><see cref="Dictionary{TKey,TValue}" /> that contains values selected from the input collection</returns>
    [Pure]
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValueCollection)
        where TKey : notnull =>
        keyValueCollection.ToDictionary(pair => pair.Key, pair => pair.Value);

    /// <summary>
    ///   Creates <see cref="IReadOnlyDictionary{TKey,TValue}"/> based on source collection
    /// </summary>
    /// <param name="keyValueCollection">Source collection that used as a base for <see cref="IReadOnlyDictionary{TKey,TValue}"/></param>
    /// <typeparam name="TKey">Type of keys in the collection</typeparam>
    /// <typeparam name="TValue">Type of values in the collection</typeparam>
    /// <returns><see cref="IReadOnlyDictionary{TKey,TValue}"/> that contains values selected from the input collection</returns>
    [Pure]
    public static IReadOnlyDictionary<TKey, TValue> ToReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> keyValueCollection)
        where TKey : notnull =>
        new ReadOnlyDictionary<TKey, TValue>(keyValueCollection);

    /// <summary>
    ///   Creates <see cref="IReadOnlyList{T}"/> based on source collection
    /// </summary>
    /// <param name="source">Source collection that used as a base for <see cref="IReadOnlyList{T}"/></param>
    /// <typeparam name="T">Type of items in the collection</typeparam>
    /// <returns><see cref="IReadOnlyList{T}"/> that contains values selected from the input collection</returns>
    [Pure]
    public static IReadOnlyList<T> ToReadOnly<T>(this IEnumerable<T> source) => new ReadOnlyCollection<T>(source.ToList());

    /// <summary>
    ///   Adds the elements of the specified collection to the end of the <see cref="ICollection{T}"/>.
    /// </summary>
    /// <param name="collection">collection to add to</param>
    /// <param name="listToAdd">collection to add from</param>
    /// <typeparam name="T">The type of the elements of the collection.</typeparam>
    /// <remarks>If the <paramref name="listToAdd"/> is null, nothing will be added</remarks>
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T>? listToAdd) => listToAdd?.ForEach(collection.Add);

    /// <summary>
    ///   Removes the elements of the specified collection from the <see cref="ICollection{T}"/>.
    /// </summary>
    /// <param name="collection">collection to remove from</param>
    /// <param name="listToRemove">collection to remove</param>
    /// <typeparam name="T">The type of the elements of the collection.</typeparam>
    /// <remarks>If the <paramref name="listToRemove"/> is null, nothing will be removed</remarks>
    public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T>? listToRemove) => listToRemove?.ForEach(item => collection.Remove(item));

    /// <summary>
    ///   Adds the elements of the specified collection to the end of the <see cref="IDictionary{TKey,TValue}"/>.
    /// </summary>
    /// <param name="dictionary">dictionary to add to</param>
    /// <param name="itemsToAdd">items to add to the dictionary</param>
    /// <typeparam name="TKey">The type of the keys of the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values of the dictionary.</typeparam>
    /// <remarks>If the <paramref name="itemsToAdd"/> is null, nothing will be added</remarks>
    public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>>? itemsToAdd) =>
        itemsToAdd?.ForEach(dictionary.Add);

    /// <summary>
    ///   Adds the elements of the specified collection to the end of the <see cref="IDictionary{TKey,TValue}"/>.
    /// </summary>
    /// <param name="dictionary">dictionary to add to</param>
    /// <param name="items">source collection to add from</param>
    /// <param name="keySelector">item key selector</param>
    /// <param name="valueSelector">item value selector</param>
    /// <typeparam name="TKey">The key type of the dictionary</typeparam>
    /// <typeparam name="TValue">The value type of the dictionary</typeparam>
    /// <typeparam name="TItem">The type of the items in the source collection</typeparam>
    /// <remarks>If the <paramref name="items"/> is null, nothing will be added</remarks>
    public static void AddRange<TKey, TValue, TItem>(this IDictionary<TKey, TValue> dictionary,
                                                     IEnumerable<TItem>? items,
                                                     Func<TItem, TKey> keySelector,
                                                     Func<TItem, TValue> valueSelector) =>
        items?.ForEach(item => dictionary.Add(keySelector(item), valueSelector(item)));

    /// <summary>
    ///   Removes the elements with the specified keys from the <see cref="IDictionary{TKey,TValue}"/>.
    /// </summary>
    /// <param name="dictionary">dictionary to remove from</param>
    /// <param name="keysToRemove">keys to remove from the dictionary</param>
    /// <typeparam name="TKey">The type of the keys of the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values of the dictionary.</typeparam>
    /// <remarks>If the <paramref name="keysToRemove"/> is null, nothing will be removed</remarks>
    public static void RemoveRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey>? keysToRemove) =>
        keysToRemove?.ForEach(k => dictionary.Remove(k));

    /// <summary>
    ///   Returns items from the source collection that are not null
    /// </summary>
    /// <param name="collection">source collection</param>
    /// <typeparam name="T">Type of items in the collection</typeparam>
    /// <returns>Items from the source collection that are not null</returns>
    [Pure]
    public static IEnumerable<T> NotNull<T>(this IEnumerable<T?> collection)
        where T : class =>
        collection.Where(i => i != null).Cast<T>();

    /// <summary>
    ///   Returns items from the source collection that are not null
    /// </summary>
    /// <param name="collection">source collection</param>
    /// <typeparam name="T">Type of items in the collection</typeparam>
    /// <returns>Items from the source collection that are not null</returns>
    [Pure]
    public static IEnumerable<T> NotNull<T>(this IEnumerable<T?> collection)
        where T : struct =>
        collection.Where(i => i != null).Cast<T>();

    /// <summary>
    ///   Checks if the collection is empty
    /// </summary>
    /// <param name="enumerable">collection to check</param>
    /// <typeparam name="T">Type of items in the collection</typeparam>
    /// <returns>True if the collection is empty, false otherwise</returns>
    [Pure]
    public static bool IsEmpty<T>(this IEnumerable<T> enumerable) => enumerable.Any() == false;

    /// <summary>
    ///   Checks if the collection is null or empty
    /// </summary>
    /// <param name="collection">collection to check</param>
    /// <typeparam name="T">Type of items in the collection</typeparam>
    /// <returns>True if the collection is null or empty, false otherwise</returns>
    [Pure]
    [ContractAnnotation("collection:null => true")]
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? collection) => collection == null || collection.IsEmpty();


    /// <summary>
    ///   Checks if the collection has only one element
    /// </summary>
    /// <param name="enumerable">collection to check</param>
    /// <typeparam name="T">Type of items in the collection</typeparam>
    /// <returns>True if the collection has only one element, false otherwise</returns>
    [Pure]
    public static bool IsSingle<T>(this IEnumerable<T> enumerable) => enumerable.Count() == 1;

    /// <summary>
    ///   Gets the items types from the collection
    /// </summary>
    /// <param name="source">source collection</param>
    /// <returns>Items types from the collection</returns>
    [Pure]
    public static IReadOnlyCollection<Type> GetItemsTypes(this IEnumerable<object> source) => source.Select(i => i.GetType()).ToList();

    /// <summary>
    ///   Excludes the specified item from the collection
    /// </summary>
    /// <param name="source">source collection</param>
    /// <param name="itemToExclude">item to exclude</param>
    /// <typeparam name="T">Type of items in the collection</typeparam>
    /// <returns>Collection without the specified item</returns>
    [Pure]
    public static IEnumerable<T> Exclude<T>(this IEnumerable<T> source, T itemToExclude) => source.Except(new[] { itemToExclude });

    /// <summary>
    ///   Converts specified item to <see cref="IEnumerable{T}"/> with one item 
    /// </summary>
    /// <param name="item">item to convert</param>
    /// <typeparam name="T">Type of items in the collection</typeparam>
    /// <returns><see cref="IEnumerable{T}"/> with one item</returns>
    [Pure]
    public static IEnumerable<T> Yield<T>(this T item)
    {
        yield return item;
    }

    /// <summary>
    ///   Converts specified item to <see cref="IEnumerable{T}"/> with the specified count of items
    /// </summary>
    /// <param name="item">item to convert</param>
    /// <param name="count">count of items</param>
    /// <typeparam name="T">Type of items in the collection</typeparam>
    /// <returns><see cref="IEnumerable{T}"/> with the specified count of items</returns>
    [Pure]
    public static IEnumerable<T> Yield<T>(this T item, int count) => Enumerable.Repeat(item, count);

    /// <summary>
    ///   Iterates through the specified source and executes the specified action for each item
    /// </summary>
    /// <param name="source">source collection</param>
    /// <param name="action">action to execute</param>
    /// <typeparam name="T">Type of items in the collection</typeparam>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var obj in source)
        {
            action(obj);
        }
    }
}