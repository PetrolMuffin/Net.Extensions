using System.ComponentModel;
using System.Reflection;
using JetBrains.Annotations;

namespace PetrolMuffin.Net.Extensions;

/// <summary>
/// Extensions for Enum types
/// </summary>
[PublicAPI]
public static class EnumExtensions
{
    /// <summary>
    /// Get custom attribute from enum field
    /// </summary>
    /// <typeparam name="TAttribute">Type of attribute to find</typeparam>
    [Pure]
    public static TAttribute? GetCustomAttribute<TAttribute>(this Enum enumItem)
        where TAttribute : Attribute
    {
        var fieldName = enumItem.ToString();
        return enumItem.GetType().GetField(fieldName)?.GetCustomAttribute<TAttribute>();
    }

    /// <summary>
    /// Get all enum values from <see cref="T"/> enum
    /// </summary>
    [Pure]
    public static IEnumerable<T> GetValues<T>()
        where T : Enum =>
        Enum.GetValues(typeof(T)).Cast<T>();

    /// <summary>
    /// Get description from <see cref="DescriptionAttribute"/> of selected enum field
    /// </summary>
    /// <typeparam name="T">Type of enum</typeparam>
    /// <returns>Description of <see cref="enumValue"/></returns>
    [Pure]
    public static string GetDescription<T>(this T enumValue)
        where T : Enum
    {
        var type = typeof(T);
        var enumName = enumValue.ToString();
        var fieldInfo = type.GetField(enumName);
        var descriptionAttribute = fieldInfo!.GetCustomAttribute<DescriptionAttribute>(false);
        return descriptionAttribute == null ? enumName : descriptionAttribute.Description;
    }

    /// <summary>
    /// Parse a string to enum type
    /// </summary>
    /// <param name="enumString">String that matches one of enum value</param>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <returns>Enum value parsed from specified string</returns>
    /// <exception cref="InvalidOperationException">Throws when can't parse specified string to specified enum type</exception>
    public static TEnum ToEnum<TEnum>(this string enumString)
        where TEnum : struct, Enum
    {
        var isParsed = Enum.TryParse<TEnum>(enumString, true, out var enumValue);
        if (isParsed) return enumValue;

        throw new InvalidOperationException($"Can't parse {enumString} as part of {typeof(TEnum).FullName}");
    }
}