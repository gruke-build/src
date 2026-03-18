// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.Collections.Generic;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.WoodpeckerCI.Configuration;

public class WoodpeckerCIRunStep : WoodpeckerCIStep
{
    public override required string Name { get; set; }

    public override string DockerImage { get; set; } = DotNetDockerImages.Current;

    public IEnumerable<string> InvokedTargets { get; set; }

    public override void Write(CustomFileWriter writer)
    {
        base.Write(writer);

        using (writer.Indent())
        {
            using (writer.WriteYamlBlock("commands"))
            {
                writer.WriteLine($"- './build.sh {InvokedTargets.JoinSpace()}'");
            }
        }
    }
}
