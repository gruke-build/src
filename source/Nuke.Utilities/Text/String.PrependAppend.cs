// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;

namespace Nuke.Common.Utilities;

public static partial class StringExtensions
{
    /// <summary>
    /// Prepends a string to another string.
    /// </summary>
    [Pure]
    public static string Prepend(this string str, string prependText)
    {
        return prependText + str;
    }

    /// <summary>
    /// Appends a string to another string.
    /// </summary>
    [Pure]
    public static string Append(this string str, string appendText)
    {
        return str + appendText;
    }

    /// <summary>
    /// Ensures a string starts with another string, prepending it if it doesn't.
    /// </summary>
    [Pure]
    public static string EnsureStarting(this string str, string prefix, StringComparison stringComparison = StringComparison.Ordinal)
    {
        return str.StartsWith(prefix, stringComparison) 
            ? str 
            : str.Prepend(prefix);
    }

    /// <summary>
    /// Ensures a string ends with another string, appending it if it doesn't.
    /// </summary>
    [Pure]
    public static string EnsureEnding(this string str, string suffix, StringComparison stringComparison = StringComparison.Ordinal)
    {
        return str.EndsWith(suffix, stringComparison) 
            ? str 
            : str.Append(suffix);
    }

#if !NETSTANDARD2_0
    /// <summary>
    /// Ensures a string starts with a specific character, prepending it if it doesn't.
    /// </summary>
    [Pure]
    public static string EnsureStarting(this string str, char prefix)
    {
        return str.StartsWith(prefix)
            ? str
            : prefix + str;
    }

    /// <summary>
    /// Ensures a string ends with another string, appending it if it doesn't.
    /// </summary>
    [Pure]
    public static string EnsureEnding(this string str, char suffix)
    {
        return str.EndsWith(suffix)
            ? str
            : str + suffix;
    }
#endif
}
