// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.IO;
using JetBrains.Annotations;
using Nuke.Common.Git;
using Nuke.Common.Utilities;
using static Nuke.Common.IO.PathConstruction;

namespace Nuke.Common.Tools.GitHub;

public static partial class GitRepositoryExtensions
{
    [ContractAnnotation("path: null => null; path: notnull => notnull")]
    internal static string GetRepositoryRelativePath(this GitRepository repository, [CanBeNull] string path)
    {
        if (path == null)
            return null;

        if (!Path.IsPathRooted(path))
            return path;

        var localDirectory = repository.LocalDirectory.NotNull();
        Assert.True(IsDescendantPath(localDirectory, path), $"Path {path.SingleQuote()} must be descendant of {localDirectory:s}");
        return GetRelativePath(localDirectory, path).Replace(oldChar: '\\', newChar: '/');
    }
}
