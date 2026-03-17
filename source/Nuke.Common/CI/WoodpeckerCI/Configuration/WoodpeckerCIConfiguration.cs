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
    public bool MinimalFetch { get; set; }

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
                    ? $"branch: {OnlyOnBranches[0].SingleQuoteIfNeeded(' ', '*', '/')}"
                    : $"branch: [ {OnlyOnBranches.Select(x => x.SingleQuoteIfNeeded(' ', '*', '/')).JoinCommaSpace()} ]"
                );
            }

            writer.WriteLine(Triggers.Length is 1
                ? $"event: {Triggers[0].GetValue()}"
                : $"event: [ {Triggers.Select(x => x.GetValue()).JoinCommaSpace()} ]");
        }

        if (!MinimalFetch)
        {
            writer.WriteLine("clone:");
            using (writer.Indent())
            {
                writer.WriteLine("- name: 'Checkout Code'");
                using (writer.Indent())
                {
                    writer.WriteLine("image: woodpeckerci/plugin-git");
                    writer.WriteLine("settings:");
                    using (writer.Indent())
                    {
                        writer.WriteLine("depth: 0");
                        writer.WriteLine("partial: false");
                        writer.WriteLine("tags: true");
                    }
                }
            }
        }

        writer.WriteLine();
        writer.WriteLine("steps:");
        using (writer.Indent())
        {
            Steps.ForEach(x => x.Write(writer));
        }
    }
}
