// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;

namespace Nuke.Common.CI.TeamCity;

[PublicAPI]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum TeamCityStatus
{
    NORMAL,
    WARNING,
    ERROR,
    FAILURE
}
