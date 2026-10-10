// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;

namespace Nuke.CodeGeneration.Model;

public interface IDeprecatable
{
    [CanBeNull]
    string DeprecationMessage { get; }

    [CanBeNull]
    string DeprecationUrl { get; }

    [CanBeNull]
    IDeprecatable Parent { get; }
}

public static class DeprecatableExtensions
{
    [Pure]
    public static bool IsDeprecated(this IDeprecatable deprecatable)
    {
        if (deprecatable.DeprecationMessage != null || deprecatable.DeprecationUrl != null)
            return true;

        return deprecatable.Parent?.IsDeprecated() ?? false;
    }

    [Pure]
    [CanBeNull]
    public static string GetDeprecationMessage(this IDeprecatable deprecatable)
    {
        var message = deprecatable.DeprecationMessage;
        if (!string.IsNullOrEmpty(message))
            return message;
        return deprecatable.Parent?.GetDeprecationMessage();
    }
    
    [Pure]
    [CanBeNull]
    public static string GetDeprecationUrl(this IDeprecatable deprecatable)
    {
        var message = deprecatable.DeprecationUrl;
        if (!string.IsNullOrEmpty(message))
            return message;
        return deprecatable.Parent?.GetDeprecationUrl();
    }
}
