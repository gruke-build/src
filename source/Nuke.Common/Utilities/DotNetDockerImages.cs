// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Nuke.Common.Utilities;

public static class DotNetDockerImages
{
    [CanBeNull]
    public static string Current => Lookup(Environment.Version.Major);

    public static bool LookupCurrent(
        [CanBeNull]
        [NotNullWhen(true)]
        out string currentSdkImage)
    {
        return (currentSdkImage = Current) != null;
    }

    [CanBeNull]
    public static string Lookup(int majorVersion)
    {
        return s_sdkVersionToDockerImageVersions.TryGetValue(majorVersion, out var dockerImage)
            ? dockerImage
            : null;
    }

    private static readonly Dictionary<int, string> s_sdkVersionToDockerImageVersions = new();

    static DotNetDockerImages()
    {
        s_sdkVersionToDockerImageVersions.Add(key: 11, "mcr.microsoft.com/dotnet/sdk:11.0.100-preview.2");
        s_sdkVersionToDockerImageVersions.Add(key: 10, "mcr.microsoft.com/dotnet/sdk:10.0.201");
        s_sdkVersionToDockerImageVersions.Add(key: 9, "mcr.microsoft.com/dotnet/sdk:9.0.312");
        s_sdkVersionToDockerImageVersions.Add(key: 8, "mcr.microsoft.com/dotnet/sdk:8.0.419");
        s_sdkVersionToDockerImageVersions.Add(key: 7, "mcr.microsoft.com/dotnet/sdk:7.0.410");
        s_sdkVersionToDockerImageVersions.Add(key: 6, "mcr.microsoft.com/dotnet/sdk:6.0.428-1");
        s_sdkVersionToDockerImageVersions.Add(key: 5, "mcr.microsoft.com/dotnet/sdk:5.0.408");
    }
}
