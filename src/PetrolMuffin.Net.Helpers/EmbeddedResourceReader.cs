using System.Reflection;
using JetBrains.Annotations;
using PetrolMuffin.Net.Guards;

namespace PetrolMuffin.Net.Helpers;

/// <summary>
///   Helper to read embedded resources
/// </summary>
[PublicAPI]
public static class EmbeddedResourceReader
{
    /// <summary>
    ///   Get a stream to an embedded resource
    /// </summary>
    /// <param name="resourcePath">Path to the resource</param>
    /// <param name="resourceAssembly">Assembly containing the resource. If null, the calling assembly is used</param>
    /// <param name="isStrongName">Is the resource name a strong name</param>
    /// <returns>Stream to the resource</returns>
    public static Stream GetResourceStream(string resourcePath, Assembly? resourceAssembly = null, bool isStrongName = false)
    {
        var actualAssembly = resourceAssembly;
        if (actualAssembly == null)
        {
            actualAssembly = Assembly.GetCallingAssembly();
        }

        var actualPath = resourcePath;
        if (!isStrongName)
        {
            actualPath = actualAssembly.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith(resourcePath));
        }

        ThrowIf.Variable.IsNullOrWhitespace(actualPath, nameof(actualPath),
                                            $"Can't find EmbeddedResource with name '{resourcePath}' in assembly '{actualAssembly.FullName}'");

        return actualAssembly.GetManifestResourceStream(actualPath!)!;
    }
}