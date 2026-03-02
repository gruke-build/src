// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/GreemDev/NUKE/blob/master/LICENSE

using Nuke.Common.CI.AppVeyor;
using Nuke.Components;

[AppVeyor(
    suffix: null,
    AppVeyorImage.VisualStudio2022,
    BranchesOnly = [MasterBranch, $"/{ReleaseBranchPrefix}\\/*/"],
    SkipTags = true,
    InvokedTargets = [nameof(IPack.Pack), nameof(ITest.Test), nameof(IPublish.Publish)],
    Secrets = [])]
[AppVeyor(
    suffix: "continuous",
    AppVeyorImage.VisualStudioLatest,
    AppVeyorImage.UbuntuLatest,
    AppVeyorImage.MacOsLatest,
    BranchesExcept = [MasterBranch, $"/{ReleaseBranchPrefix}\\/*/"],
    SkipTags = true,
    InvokedTargets = [nameof(ITest.Test), nameof(IPack.Pack)],
    Secrets = [])]
partial class Build;
