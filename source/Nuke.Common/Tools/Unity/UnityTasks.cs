// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using Nuke.Common.IO;

namespace Nuke.Common.Tools.Unity;

public partial class UnityTasks
{
    /// <summary>
    /// Creates a new <see cref="UnityPackageBuilder"/>, runs the provided configuration lambda on it, and calls <see cref="UnityPackageBuilder.Build"/> on the result.
    /// </summary>
    /// <returns>The resulting <c>.unitypackage</c> file.</returns>
    public static AbsolutePath CreatePackage(Func<UnityPackageBuilder, UnityPackageBuilder> configurator)
    {
        return configurator(new UnityPackageBuilder()).Build();
    }
}
