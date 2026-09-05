// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using Nuke.Common.Tooling;
using Serilog.Events;

namespace Nuke.Common.Tools.Pulumi;

[LogLevelPattern(LogEventLevel.Warning, "^warning:")]
partial class PulumiTasks;
