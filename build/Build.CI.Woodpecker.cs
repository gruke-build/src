// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using Nuke.Common.CI.WoodpeckerCI;
using Nuke.Components;

[WoodpeckerCI(
    name: "test-woodpecker",
    OnlyOnBranches = [default],
    Triggers = [WoodpeckerCIEvent.Push],
    InvokedTargets = [nameof(ITest.Test)],
    MinimalFetch = false
)]
public partial class Build;
