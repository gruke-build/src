// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using JetBrains.Annotations;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;

namespace Nuke.Common.Tools.OpenCover;

[PublicAPI]
#pragma warning disable CA1041
[Obsolete(null, UrlFormat = "https://github.com/OpenCover/opencover#putting-opencover-into-archive-mode")]
#pragma warning restore CA1041
public class OpenCoverVerbosityMappingAttribute : VerbosityMappingAttribute
{
    public OpenCoverVerbosityMappingAttribute()
        : base(typeof(OpenCoverVerbosity))
    {
        Quiet = nameof(OpenCoverVerbosity.Off);
        Minimal = nameof(OpenCoverVerbosity.Warn);
        Normal = nameof(OpenCoverVerbosity.Info);
        Verbose = nameof(OpenCoverVerbosity.Verbose);
    }
}

partial class OpenCoverSettingsExtensions
{
    public static OpenCoverSettings SetTargetSettings(this OpenCoverSettings toolSettings, ToolOptions targetSettings)
    {
        return toolSettings
            .SetTargetPath(targetSettings.ProcessToolPath)
            .SetTargetArguments(targetSettings.GetArguments().JoinSpace())
            .SetTargetDirectory(targetSettings.ProcessWorkingDirectory);
    }

    public static OpenCoverSettings ResetTargetSettings(this OpenCoverSettings toolSettings)
    {
        return toolSettings
            .ResetTargetPath()
            .ResetTargetArguments()
            .ResetTargetDirectory();
    }
}
