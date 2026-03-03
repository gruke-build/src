// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using Nuke.Common.CI.GitLab;
using Nuke.Components;

[GitLabCI(
    InvokedTargets = [nameof(ITest.Test)],
    UploadProducedArtifacts = true
)]
partial class Build;
