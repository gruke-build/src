// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Nuke.Components;

[PublicAPI]
public interface IRestore : IHazSolution
{
    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(_ => _
                .Apply(RestoreSettingsBase)
                .Apply(RestoreSettings));
        });

    sealed Configure<DotNetRestoreSettings> RestoreSettingsBase => _ => _
        .When(DisableDotNetBuildServers, s => s.DisableBuildServers())
        .SetProjectFile(Solution)
        .SetIgnoreFailedSources(IgnoreFailedSources);
    // RestorePackagesWithLockFile
    // .SetProperty("RestoreLockedMode", true));

    Configure<DotNetRestoreSettings> RestoreSettings => _ => _;

    [Parameter("Ignore unreachable sources during " + nameof(Restore))]
    bool IgnoreFailedSources => TryGetValue<bool?>(() => IgnoreFailedSources) ?? false;
}
