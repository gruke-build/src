// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.IO;
using System.Linq;
using Nuke.Common.CI;

namespace Nuke.Common.Tests.CI;

public interface ITestConfigurationGenerator : IConfigurationGenerator
{
    StreamWriter Stream { set; }
}
