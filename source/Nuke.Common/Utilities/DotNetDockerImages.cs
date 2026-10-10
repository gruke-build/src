// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Nuke.Common.Utilities;

[PublicAPI]
public static class DotNetDockerImages
{
    public static readonly IReadOnlyDictionary<int, string> Mapping =
        new ReadOnlyDictionary<int, string>(
            new Dictionary<int, string>
            {
                { 11, "mcr.microsoft.com/dotnet/sdk:11.0.100-rc.1" },
                { 10, "mcr.microsoft.com/dotnet/sdk:10.0.401" },
                { 9, "mcr.microsoft.com/dotnet/sdk:9.0.318" },
                { 8, "mcr.microsoft.com/dotnet/sdk:8.0.425" },
                { 7, "mcr.microsoft.com/dotnet/sdk:7.0.410" },
                { 6, "mcr.microsoft.com/dotnet/sdk:6.0.428-1" }
            }
        );

    [CanBeNull]
    public static string ForCurrentRuntime
    {
        get
        {
            return Mapping.TryGetValue(Environment.Version.Major, out var dockerImage)
                ? dockerImage
                : null;
        }
    }
}
