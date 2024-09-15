using PetrolMuffin.Net.Date.Models;

namespace PetrolMuffin.Net.Date.Extensions;

/// <summary>
///   Extensions for <see cref="PeriodOffset"/>
/// </summary>
[PublicAPI]
public static class PeriodOffsetExtensions
{
    /// <summary>
    ///   Calculate if a period contains a date 
    /// </summary>
    /// <param name="period">Period to compare</param>
    /// <param name="current"><see cref="DateTime"/> to compare with the period</param>
    /// <param name="includeEdges">compare <see cref="current"/> with edges of the <see cref="Period"/></param>
    /// <returns>true if the <see cref="current"/> is in the <see cref="Period"/>, otherwise false</returns>
    [Pure]
    public static bool IsInPeriod(this PeriodOffset period, DateTimeOffset current, bool includeEdges = true) =>
        includeEdges ? period.From <= current && current <= period.To : period.From < current && current < period.To;

    /// <summary>
    ///   Calculates intersection of two periods
    /// </summary>
    /// <param name="first">first <see cref="Period"/> to compare</param>
    /// <param name="second">second <see cref="Period"/> to compare</param>
    /// <returns>true if two periods has intersection. Otherwise false</returns>
    [Pure]
    public static bool IsIntersect(this PeriodOffset first, PeriodOffset second) => TryGetIntersection(first, second, out _);

    /// <summary>
    ///   Get intersection <see cref="Period"/> of two periods
    /// </summary>
    /// <param name="first">first <see cref="Period"/> to compare</param>
    /// <param name="second">second <see cref="Period"/> to compare</param>
    /// <param name="result"><see cref="Period"/> that represents intersection between two periods. Default if no intersection</param>
    /// <returns>true if two periods has intersection, otherwise false</returns>
    [Pure]
    public static bool TryGetIntersection(this PeriodOffset first, PeriodOffset second, out PeriodOffset? result)
    {
        if (first.From <= second.From && first.To >= second.To)
        {
            result = new PeriodOffset(second.From, second.To);
            return true;
        }

        if (second.From <= first.From && second.To >= first.To)
        {
            result = new PeriodOffset(first.From, first.To);
            return true;
        }

        if (second.IsInPeriod(first.From) == false && second.IsInPeriod(first.To) == false)
        {
            result = default;
            return false;
        }

        var resultFrom = DateMath.Max(first.From, second.From);
        var resultTo = DateMath.Min(first.To, second.To);
        result = new PeriodOffset(resultFrom, resultTo);
        return true;
    }
}