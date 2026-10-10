// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using Nuke.Common.IO;
using Nuke.Common.Tooling;

namespace Nuke.Common.Tools.Gradle;

public partial class GradleTasks
{
    protected override string GetToolPath(ToolOptions options = null)
    {
        var gradleWrapper = OperatingSystem.IsWindows() ? "gradlew.bat" : "gradlew";

        var workingDir = options?.ProcessWorkingDirectory;
        var searchPath = workingDir != null ? AbsolutePath.Create(workingDir) : NukeBuild.RootDirectory;

        AbsolutePath wrapperPath;
        if ((wrapperPath = searchPath / gradleWrapper).FileExists())
            return wrapperPath;

        return ToolPathResolver.GetPathExecutable("gradle");
    }
}
