// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Nuke.Common;

namespace Nuke.Utilities.Text.Json;

public static class JTokenExtensions
{
    /// <summary>
    /// Sets the given (potentially nested via dot notation) <paramref name="compositeKey"/> to the provided <see cref="JToken"/> value.
    /// </summary>
    /// <param name="token">The root <see cref="JToken"/>.</param>
    /// <param name="compositeKey">The (potentially) nested key of the value to set.</param>
    /// <param name="value">The new value to set for the key.</param>
    public static void SetNested(this JToken token, string compositeKey, JToken value)
    {
        var parts = compositeKey.Contains('.') ? compositeKey.Split('.') : [ compositeKey ];

        switch (parts)
        {
            case [{ } singleKey]:
                token[singleKey] = value;
                break;
            case [{ } parent, { } subKey]:
                token[parent].NotNull()[subKey] = value;
                break;
            default:
            {
                token = parts.SkipLast(1).Aggregate(token, (current, subKey) => current.NotNull()[subKey]);

                token![parts.Last()] = value;
                break;
            }
        }
    }

    /// <summary>
    /// Gets the value set for the given (potentially nested via dot notation) <paramref name="compositeKey"/>.
    /// </summary>
    /// <param name="token">The root <see cref="JToken"/>.</param>
    /// <param name="compositeKey">The (potentially) nested key of the value to get.</param>
    /// <typeparam name="T">The .NET type of the JSON value at the provided <paramref name="compositeKey"/>.</typeparam>
    /// <returns>The <see cref="JToken"/> at <paramref name="compositeKey"/>, parsed as the provided type <typeparamref name="T"/>.</returns>
    public static T GetNested<T>(this JToken token, string compositeKey)
    {
        var parts = compositeKey.Contains('.') ? compositeKey.Split('.') : [ compositeKey ];

        switch (parts)
        {
            case [{ } singleKey]:
                return token[singleKey].NotNull().Value<T>();
            case [{ } parent, { } subKey]:
                return token[parent].NotNull()[subKey].NotNull().Value<T>();
            default:
                token = parts.Aggregate(token, (current, k) => current.NotNull()[k]);
                return token!.Value<T>();
        }
    }
}
