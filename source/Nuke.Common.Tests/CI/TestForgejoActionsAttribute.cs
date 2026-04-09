// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.IO;
using Nuke.Common.CI.ForgejoActions;

namespace Nuke.Common.Tests.CI;

public class TestForgejoActionsAttribute : ForgejoActionsAttribute, ITestConfigurationGenerator
{
    public TestForgejoActionsAttribute(string runner, params string[] runners)
        : base("test", runner, runners)
    {
    }

    public StreamWriter Stream { get; set; }

    protected override StreamWriter CreateStream()
    {
        return Stream;
    }
}
