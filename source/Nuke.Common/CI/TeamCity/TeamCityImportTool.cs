// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;

namespace Nuke.Common.CI.TeamCity;

[PublicAPI]
public enum TeamCityImportTool
{
    /// <summary>dotCover reports</summary>
    dotcover,

    /// <summary>PartCover reports</summary>
    partcover,

    /// <summary>NCover reports</summary>
    ncover,

    /// <summary>NCover3 reports</summary>
    ncover3
}
