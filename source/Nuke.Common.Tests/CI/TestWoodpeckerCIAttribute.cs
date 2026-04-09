// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.IO;
using Nuke.Common.CI.WoodpeckerCI;

namespace Nuke.Common.Tests.CI;

public class TestWoodpeckerCIAttribute : WoodpeckerCIAttribute, ITestConfigurationGenerator
{
    public TestWoodpeckerCIAttribute()
        : base("test")
    { }

    public StreamWriter Stream { get; set; }

    protected override StreamWriter CreateStream()
    {
        return Stream;
    }
}
