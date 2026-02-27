// Copyright 2024 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/GreemDev/NUKE/blob/master/LICENSE

using System;

namespace Nuke.Common.Tooling;

partial class ToolOptions
{
    internal Func<ToolOptions, IProcess, object> ProcessExitHandler { get; set; }
}
