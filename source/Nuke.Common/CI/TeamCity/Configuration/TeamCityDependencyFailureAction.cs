// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;

namespace Nuke.Common.CI.TeamCity.Configuration;

[PublicAPI]
public enum TeamCityDependencyFailureAction
{
    // TODO: add description from web UI
    AddProblem,
    FailToStart,
    Ignore,
    Cancel
}
