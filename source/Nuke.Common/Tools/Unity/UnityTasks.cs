// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.IO;
using Nuke.Common.IO;

namespace Nuke.Common.Tools.Unity;

public partial class UnityTasks
{
    /// <summary>
    /// Creates a new <see cref="UnityPackageBuilder"/>, runs the provided configuration lambda on it, and calls <see cref="UnityPackageBuilder.Build"/> on the result.
    /// </summary>
    /// <returns>The resulting <c>.unitypackage</c> file.</returns>
    /// <exception cref="DirectoryNotFoundException"><see cref="UnityPackageBuilder.SourceDirectory"/> does not exist as a directory.</exception>
    /// <exception cref="InvalidOperationException"><see cref="UnityPackageBuilder.SourceDirectory"/> does not contain any Unity assets.</exception>
    /// <exception cref="InvalidDataException">The Unity package's contents are invalid.</exception>
    public static AbsolutePath CreatePackage(Func<UnityPackageBuilder, UnityPackageBuilder> configurator)
    {
        return configurator(new UnityPackageBuilder()).Build();
    }
}
