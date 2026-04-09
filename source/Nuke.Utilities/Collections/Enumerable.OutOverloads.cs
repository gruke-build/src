// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Nuke.Common.Utilities.Collections;

public static partial class EnumerableExtensions
{
    public static bool TryGetFirst<TSource>([NotNull] this IEnumerable<TSource> source,
        [NotNull] Func<TSource, bool> predicate,
#if !NETSTANDARD2_0
        [System.Diagnostics.CodeAnalysis.NotNullWhen(true)]
#endif
        out TSource value)
        where TSource : class
    {
        return (value = source.FirstOrDefault(predicate)) != null;
    }
}
