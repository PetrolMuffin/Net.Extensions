namespace PetrolMuffin.Net.Date;

/// <summary>
///   Helper class for date operations
/// </summary>
[PublicAPI]
public sealed class DateMath
{
    /// <summary>
    /// Calculate a minimum date between two dates 
    /// </summary>
    /// <param name="first">First date to compare</param>
    /// <param name="second">Second date to compare</param>
    /// <returns>The minimum date between two dates</returns>
    [Pure]
    public static DateTime Min(DateTime first, DateTime second) => first < second ? first : second;

    /// <summary>
    /// Calculate a maximum date between two dates
    /// </summary>
    /// <param name="first">First date to compare</param>
    /// <param name="second">Second date to compare</param>
    /// <returns>The maximum date between two dates</returns>
    [Pure]
    public static DateTime Max(DateTime first, DateTime second) => first > second ? first : second;
    
    /// <summary>
    /// Calculate a minimum date between two dates 
    /// </summary>
    /// <param name="first">First date to compare</param>
    /// <param name="second">Second date to compare</param>
    /// <returns>The minimum date between two dates</returns>
    [Pure]
    public static DateTimeOffset Min(DateTimeOffset first, DateTimeOffset second) => first < second ? first : second;

    /// <summary>
    /// Calculate a maximum date between two dates
    /// </summary>
    /// <param name="first">First date to compare</param>
    /// <param name="second">Second date to compare</param>
    /// <returns>The maximum date between two dates</returns>
    [Pure]
    public static DateTimeOffset Max(DateTimeOffset first, DateTimeOffset second) => first > second ? first : second;
}