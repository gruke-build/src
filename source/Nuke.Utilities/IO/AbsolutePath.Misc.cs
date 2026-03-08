// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;

namespace Nuke.Common.IO;

partial class AbsolutePathExtensions
{
    public static bool IsDotDirectory(this AbsolutePath path)
    {
        return path.DirectoryExists() && path.Name.StartsWith(".");
    }
}
