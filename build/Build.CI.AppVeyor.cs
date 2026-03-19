// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using Nuke.Common.CI.AppVeyor;
using Nuke.Components;

[AppVeyor(
    suffix: null,
    AppVeyorImage.VisualStudioLatest,
    AppVeyorImage.UbuntuLatest,
    AppVeyorImage.MacOsLatest,
    BranchesExcept = [MasterBranch, $"/{ReleaseBranchPrefix}\\/*/"],
    SkipTags = true,
    InvokedTargets = [nameof(ITest.Test)],
    Secrets = [])]
partial class Build;
