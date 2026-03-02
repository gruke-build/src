// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.Collections.Generic;
using Nuke.Components;

partial class Build
{
    static Dictionary<string, string> CustomNames =
        new()
        {
            { nameof(ICompile.Compile), "⚙️" },
            { nameof(ITest.Test), "🚦" },
            { nameof(IPack.Pack), "📦" },
            { nameof(IReportCoverage.ReportCoverage), "📊" },
            { nameof(IReportDuplicates.ReportDuplicates), "🎭" },
            { nameof(IReportIssues.ReportIssues), "💣" },
            { nameof(IPublish.Publish), "🚚" },
            { nameof(Announce), "🗣" }
        };
}
