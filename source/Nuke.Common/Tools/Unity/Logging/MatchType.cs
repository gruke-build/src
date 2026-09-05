// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;

namespace Nuke.Common.Tools.Unity.Logging;

internal enum MatchType
{
    None = 0,
    Inclusive = 1,
    Exclusive = 2
}
