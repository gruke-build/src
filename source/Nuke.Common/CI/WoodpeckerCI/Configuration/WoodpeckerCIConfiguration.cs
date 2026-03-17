// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace Nuke.Common.CI.WoodpeckerCI.Configuration;

[PublicAPI]
public class WoodpeckerCIConfiguration : ConfigurationEntity
{
    public IEnumerable<string> InvokedTargets { get; set; }

    public string[] OnlyOnBranches { get; set; }

    public WoodpeckerCIEvent[] Triggers { get; set; }

    public WoodpeckerCIStep[] Steps { get; set; }

    public override void Write(CustomFileWriter writer)
    {
        // naive implementation: this can be extended in the future
        // https://woodpecker-ci.org/docs/usage/workflow-syntax
        
        Assert.NotEmpty(Triggers, "Cannot create a Woodpecker workflow with no triggers.");

        writer.WriteLine("when:");
        using (writer.Indent())
        {
            if (OnlyOnBranches.Length > 0)
            {
                OnlyOnBranches.Replace(oldValue: null, "${CI_REPO_DEFAULT_BRANCH}");
                writer.WriteLine(OnlyOnBranches.Length is 1
                    ? $"branch: {OnlyOnBranches[0].SingleQuoteIfNeeded()}"
                    : $"branch: [{OnlyOnBranches.JoinComma()}]"
                );
            }

            writer.WriteLine(Triggers.Length is 1
                ? $"event: {Triggers[0].GetValue()}"
                : $"event: [{Triggers.Select(x => x.GetValue()).JoinComma()}]");
        }

        writer.WriteLine();
        writer.WriteLine("steps:");
        using (writer.Indent())
        {
            Steps.ForEach(x => x.Write(writer));
        }
    }
}
