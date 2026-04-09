// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.WoodpeckerCI.Configuration;

public static class WoodpeckerCICustomWriterExtensions
{
    public static IDisposable WriteYamlBlock(this CustomFileWriter cfw, string text)
    {
        return cfw.WriteBlock($"{text.NotNull("text != null")}:");
    }
}
