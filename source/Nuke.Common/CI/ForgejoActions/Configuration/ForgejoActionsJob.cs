// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace Nuke.Common.CI.ForgejoActions.Configuration;

[PublicAPI]
public class ForgejoActionsJob : ConfigurationEntity
{
    public string Name { get; set; }
    public string RunsOn { get; set; }
    public int TimeoutMinutes { get; set; }
    public string ConcurrencyGroup { get; set; }
    public string EnvironmentName { get; set; }
    public string EnvironmentUrl { get; set; }
    public bool ConcurrencyCancelInProgress { get; set; }
    public ForgejoActionsStep[] Steps { get; set; }

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine($"{Name}:");

        using (writer.Indent())
        {
            writer.WriteLine($"name: {Name}");
            writer.WriteLine($"runs-on: {RunsOn}");
            // https://codeberg.org/actions/meta#available-runners
            switch (RunsOn)
            {
                case CodebergRunners.Tiny or CodebergRunners.TinyLazy:
                    writer.WriteLine("timeout-minutes: 2");
                    break;
                case CodebergRunners.Small or CodebergRunners.SmallLazy:
                    writer.WriteLine("timeout-minutes: 5");
                    break;
                case CodebergRunners.Medium or CodebergRunners.MediumLazy:
                    writer.WriteLine("timeout-minutes: 10");
                    break;
                default:
                    if (TimeoutMinutes > 0)
                    {
                        writer.WriteLine($"timeout-minutes: {TimeoutMinutes}");
                    }
                    break;
            }

            if (!ConcurrencyGroup.IsNullOrWhiteSpace() || ConcurrencyCancelInProgress)
            {
                writer.WriteLine("concurrency:");
                using (writer.Indent())
                {
                    var group = ConcurrencyGroup;
                    if (group.IsNullOrWhiteSpace())
                    {
                        // create a default value that only cancels in-progress runs of the same workflow
                        // we don't fall back to forgejo.ref which would disable multiple runs in main/master which is usually what is wanted
                        group = "${{ forgejo.workflow }} @ ${{ forgejo.event.pull_request.head.label || forgejo.head_ref || forgejo.run_id }}";
                    }

                    writer.WriteLine($"group: {group}");
                    if (ConcurrencyCancelInProgress)
                    {
                        writer.WriteLine("cancel-in-progress: true");
                    }
                }
            }

            if (!EnvironmentName.IsNullOrWhiteSpace())
            {
                if (EnvironmentUrl.IsNullOrWhiteSpace())
                {
                    writer.WriteLine($"environment: {EnvironmentName}");
                }
                else
                {
                    writer.WriteLine("environment:");
                    using (writer.Indent())
                    {
                        writer.WriteLine($"name: {EnvironmentName}");
                        writer.WriteLine($"url: {EnvironmentUrl}");
                    }
                }
            }

            writer.WriteLine("steps:");
            using (writer.Indent())
            {
                Steps.ForEach(x => x.Write(writer));
            }
        }
    }
}
