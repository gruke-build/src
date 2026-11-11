// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nuke.Common.CI.WoodpeckerCI;
using Nuke.Common.CI.WoodpeckerCI.Configuration;
using Nuke.Common.Utilities;

namespace Nuke.Common.Tests.CI;

public class TestWoodpeckerCIAttribute : WoodpeckerCIAttribute, ITestConfigurationGenerator
{
    public TestWoodpeckerCIAttribute()
        : base("test")
    { }

    public required string DockerImage { get; init; }

    public override IEnumerable<WoodpeckerCIStep> GetSteps()
    {
        yield return new WoodpeckerCIRunStep
                     {
                         InvokedTargets = InvokedTargets,
                         DockerImage = DockerImage,
                         Name = $"Run: {InvokedTargets.JoinCommaSpace()}",
                         ImportedSecrets = GetImports().ToDictionary(x => x.Key, x => x.Value)
                     };
    }

    public StreamWriter Stream { get; set; }

    protected override StreamWriter CreateStream()
    {
        return Stream;
    }
}
