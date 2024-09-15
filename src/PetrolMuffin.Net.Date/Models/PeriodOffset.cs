using System.Runtime.Serialization;
using PetrolMuffin.Net.Guards;

namespace PetrolMuffin.Net.Date.Models;

/// <summary>
///   Represents a period between two dates
/// </summary>
[PublicAPI]
[Serializable]
public sealed record PeriodOffset
{
    /// <summary>
    ///   Construct period
    /// </summary>
    /// <param name="first">Start date of period</param>
    /// <param name="second">End date of period</param>
    /// <exception cref="ArgumentOutOfRangeException">If period start is less than <see cref="GlobalConfiguration.MinValue"/> or period end is greater than <see cref="GlobalConfiguration.MaxValue"/></exception>
    /// <remarks>Set the <see cref="GlobalConfiguration.MinValue"/> and <see cref="GlobalConfiguration.MaxValue"/> to validate the period. Default values are the start of January 1st, 1900 and the end of December 31st, 2999</remarks>
    public PeriodOffset(DateTimeOffset first, DateTimeOffset second)
    {
        var realFrom = DateMath.Min(first, second);
        var minValue = GlobalConfiguration.MinValue;
        ThrowIf.Argument.IsLessThan(realFrom, minValue, nameof(realFrom), $"Date cannot be less than {minValue}. Consider changing the value in ${typeof(GlobalConfiguration)}");

        var realTo = DateMath.Max(first, second);
        var maxValue = GlobalConfiguration.MaxValue;
        ThrowIf.Argument.IsGreaterThan(realTo, maxValue, nameof(realTo), $"Date cannot be greater than {maxValue}. Consider changing the value in ${typeof(GlobalConfiguration)}");

        From = realFrom;
        To = realTo;
        Interval = To - From;
    }

    /// <summary>
    ///   Start of the <see cref="PeriodOffset"/>
    /// </summary>
    public DateTimeOffset From { get; }

    /// <summary>
    ///   End of the <see cref="PeriodOffset"/>
    /// </summary>
    public DateTimeOffset To { get; }

    /// <summary>
    ///   Interval between the start and the end of the <see cref="PeriodOffset"/>
    /// </summary>
    [IgnoreDataMember]
    public TimeSpan Interval { get; }

    /// <summary>
    ///   Deconstructs the period into two dates
    /// </summary>
    /// <param name="from">Start of the period</param>
    /// <param name="to">End of the period</param>
    public void Deconstruct(out DateTimeOffset from, out DateTimeOffset to)
    {
        from = From;
        to = To;
    }
    
    /// <summary>
    ///   Deconstructs the period into two dates and interval
    /// </summary>
    /// <param name="from">Start of the period</param>
    /// <param name="to">End of the period</param>
    /// <param name="interval">Interval between the start and the end of the period</param>
    public void Deconstruct(out DateTimeOffset from, out DateTimeOffset to, out TimeSpan interval)
    {
        from = From;
        to = To;
        interval = Interval;
    }
    
    /// <summary>
    ///   Check if <see cref="From"/> and <see cref="To"/> are equal to the other <see cref="PeriodOffset"/>
    /// </summary>
    /// <param name="other">Other <see cref="PeriodOffset"/> to compare</param>
    /// <returns>true if <see cref="From"/> and <see cref="To"/> are equal to the other <see cref="PeriodOffset"/>, otherwise false</returns>
    [Pure]
    public bool Equals(PeriodOffset? other) => other != null && From.Equals(other.From) && To.Equals(other.To);
    
    /// <summary>
    ///   Get hash code of the <see cref="PeriodOffset"/>
    /// </summary>
    /// <returns>Hash code of the <see cref="PeriodOffset"/></returns>
    [Pure]
    public override int GetHashCode() => HashCode.Combine(From, To);

    /// <summary>
    ///   Global configuration for <see cref="PeriodOffset"/>
    /// </summary>
    [PublicAPI]
    public static class GlobalConfiguration
    {
        /// <summary>
        ///   Maximum value for a period. Default is the end of December 31st, 2999 with zero offset
        /// </summary>
        /// <remarks>Can be changed to any value to validate <see cref="PeriodOffset"/></remarks>
        public static DateTimeOffset MaxValue { get; set; } = new(2999, 12, 31, 23, 59, 59, 999, TimeSpan.Zero);

        /// <summary>
        ///   Minimum value for a period. Default is the start of January 1st, 1900 with zero offset
        /// </summary>
        /// <remarks>Can be changed to any value to validate <see cref="PeriodOffset"/></remarks>
        public static DateTimeOffset MinValue { get; set; } = new(1900, 1, 1, 0, 0, 0, TimeSpan.Zero);
    }
}