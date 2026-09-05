// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

namespace Nuke.Common.Utilities;

public static partial class StringExtensions
{
    private static readonly string[] KnownWords =
    [
        "DotNet",
        "GitHub",
        "GitVersion",
        "MSBuild",
        "NuGet",
        "ReSharper",
        "AppVeyor",
        "TeamCity",
        "GitLab",
        "SignPath",
        "JetBrains"
    ];
}
