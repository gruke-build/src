// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using Nuke.Common.CI.GitHubActions;
using Nuke.Components;

[GitHubActions(
    ReleaseWorkflow, 
    GitHubActionsImage.UbuntuLatest,
    FetchDepth = 0,
    OnPushBranches = [MasterBranch, $"{ReleaseBranchPrefix}/*"],
    InvokedTargets = [nameof(ITest.Test), nameof(IPack.Pack), nameof(IPublish.Publish)],
    EnableGitHubToken = true,
    PublishArtifacts = true,
    ImportSecrets = [nameof(PublicNuGetApiKey), nameof(DiscordWebhook)])]
[GitHubActions(
    "windows-latest",
    GitHubActionsImage.WindowsLatest,
    FetchDepth = 0,
    OnPushBranchesIgnore = [MasterBranch, $"{ReleaseBranchPrefix}/*"],
    OnPullRequestBranches = [DevelopBranch],
    InvokedTargets = [nameof(ITest.Test), nameof(IPack.Pack)],
    PublishArtifacts = false)]
[GitHubActions(
    "macos-latest",
    GitHubActionsImage.MacOsLatest,
    FetchDepth = 0,
    OnPushBranchesIgnore = [MasterBranch, $"{ReleaseBranchPrefix}/*"],
    OnPullRequestBranches = [DevelopBranch],
    InvokedTargets = [nameof(ITest.Test), nameof(IPack.Pack)],
    PublishArtifacts = false)]
[GitHubActions(
    "ubuntu-latest",
    GitHubActionsImage.UbuntuLatest,
    FetchDepth = 0,
    OnPushBranchesIgnore = [MasterBranch, $"{ReleaseBranchPrefix}/*"],
    OnPullRequestBranches = [DevelopBranch],
    InvokedTargets = [nameof(ITest.Test), nameof(IPack.Pack)],
    PublishArtifacts = false)]
[GitHubActions(
    AlphaDeployment,
    GitHubActionsImage.UbuntuLatest,
    FetchDepth = 0,
    OnPushBranches = [DevelopBranch],
    InvokedTargets = [nameof(IPublish.Publish)],
    EnableGitHubToken = true,
    PublishArtifacts = false,
    ImportSecrets = [nameof(FeedzNuGetApiKey)])]
partial class Build
{
    const string ReleaseWorkflow = "release";
    const string AlphaDeployment = "alpha-deployment";
}
