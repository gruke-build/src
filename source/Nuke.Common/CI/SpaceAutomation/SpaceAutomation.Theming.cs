// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using Nuke.Common.Execution;

namespace Nuke.Common.CI.SpaceAutomation;

public partial class SpaceAutomation
{
    internal override string OutputTemplate => Logging.StandardOutputTemplate;
}
