// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using Nuke.Common.CI.ForgejoActions;
using Nuke.Components;

[ForgejoActions(
    "forgejo-integration-test",
    CodebergRunners.Medium,
    FetchDepth = 0,
    OnPushBranchesIgnore = [MasterBranch, $"{ReleaseBranchPrefix}/*"],
    OnPullRequestBranches = [DevelopBranch],
    InvokedTargets = [nameof(ITest.Test)],
    PublishArtifacts = true
)]
public partial class Build;
