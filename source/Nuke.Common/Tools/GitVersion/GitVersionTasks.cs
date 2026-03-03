// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace Nuke.Common.Tools.GitVersion;

partial class GitVersionTasks
{
    protected override object GetResult<T>(ToolOptions options, IReadOnlyCollection<Output> output)
    {
        try
        {
            return output.EnsureOnlyStd().StdToJson<GitVersion>();
        }
        catch (Exception exception)
        {
            throw new Exception($"Cannot parse {nameof(GitVersion)} output:".Concat(output.Select(x => x.Text)).JoinNewLine(), exception);
        }
    }
}

[PublicAPI]
public record GitVersion(
    int Major,
    int Minor,
    int Patch,
    string MajorMinorPatch,
    string PreReleaseTag,
    string PreReleaseTagWithDash,
    string PreReleaseLabel,
    string PreReleaseLabelWithDash,
    string PreReleaseNumber,
    string WeightedPreReleaseNumber,
    string BuildMetaData,
    string BuildMetaDataPadded,
    string FullBuildMetaData,
    string SemVer,
    string LegacySemVer,
    string LegacySemVerPadded,
    string AssemblySemVer,
    string AssemblySemFileVer,
    string FullSemVer,
    string InformationalVersion,
    string BranchName,
    string EscapedBranchName,
    string Sha,
    string ShortSha,
    string VersionSourceSha,
    string CommitsSinceVersionSource,
    string CommitsSinceVersionSourcePadded,
    int? UncommittedChanges,
    string CommitDate);
