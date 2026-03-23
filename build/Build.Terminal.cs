// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Execution;

[DisableDefaultOutput<Terminal>(
    DefaultOutput.Logo,
    DefaultOutput.Timestamps,
    DefaultOutput.TargetHeader,
    DefaultOutput.ErrorsAndWarnings,
    DefaultOutput.TargetOutcome,
    DefaultOutput.BuildOutcome)]
partial class Build;
