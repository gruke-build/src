// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.Tools.GitVersion;

namespace Nuke.Components;

[PublicAPI]
public interface IHazGitVersion : INukeBuild
{
    [GitVersion(NoFetch = true, Framework = "net8.0")]
    [Required]
    GitVersion Versioning => TryGetValue(() => Versioning);
}

[PublicAPI]
public interface IHazDebuggableGitVersion : INukeBuild
{
    [GitVersion(NoFetch = true, Framework = "net8.0", PrintOutput = true)]
    [Required]
    GitVersion Versioning => TryGetValue(() => Versioning);
}

[PublicAPI]
public interface IHazFetchingGitVersion : INukeBuild
{
    [GitVersion(NoFetch = false, Framework = "net8.0")]
    [Required]
    GitVersion Versioning => TryGetValue(() => Versioning);
}

[PublicAPI]
public interface IHazDebuggableFetchingGitVersion : INukeBuild
{
    [GitVersion(NoFetch = false, Framework = "net8.0", PrintOutput = true)]
    [Required]
    GitVersion Versioning => TryGetValue(() => Versioning);
}
