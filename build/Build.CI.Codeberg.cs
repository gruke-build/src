// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using Nuke.Common.CI.ForgejoActions;
using Nuke.Components;

[ForgejoActions(
    "test",
    CodebergRunners.MediumLazy,
    FetchDepth = 0,
    OnPushBranchesIgnore = [MasterBranch, $"{ReleaseBranchPrefix}/*"],
    OnPullRequestBranches = [DevelopBranch],
    InvokedTargets = [nameof(ITest.Test)],
    PublishArtifacts = true
)]
[ForgejoActions(
    ReleaseWorkflow,
    CodebergRunners.MediumLazy,
    FetchDepth = 0,
    OnPushBranches = [MasterBranch, $"{ReleaseBranchPrefix}/*"],
    InvokedTargets = [nameof(ITest.Test), nameof(IPack.Pack), nameof(IPublish.Publish)],
    PublishArtifacts = false)]
public partial class Build;
