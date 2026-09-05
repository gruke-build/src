// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;

namespace Nuke.Common.Execution;

public enum ExecutionStatus
{
    None,
    Scheduled,
    NotRun,
    Skipped,
    Succeeded,
    Failed,
    Running,
    Aborted,
    Collective
}
