// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace Nuke.Common.CI.ForgejoActions.Configuration;

[PublicAPI]
public class ForgejoActionsConfiguration : ConfigurationEntity
{
    public string Name { get; set; }

    public ForgejoActionsTrigger[] ShortTriggers { get; set; }
    public ForgejoActionsDetailedTrigger[] DetailedTriggers { get; set; }
    public string ConcurrencyGroup { get; set; }
    public bool ConcurrencyCancelInProgress { get; set; }
    public ForgejoActionsJob[] Jobs { get; set; }

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine($"name: {Name}");
        writer.WriteLine();

        if (ShortTriggers.Length > 0)
            writer.WriteLine($"on: [{ShortTriggers.Select(x => x.GetValue().ToLowerInvariant()).JoinCommaSpace()}]");
        else
        {
            writer.WriteLine("on:");
            using (writer.Indent())
            {
                DetailedTriggers.ForEach(x => x.Write(writer));
            }
        }

        if (!ConcurrencyGroup.IsNullOrWhiteSpace() || ConcurrencyCancelInProgress)
        {
            writer.WriteLine();
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

        writer.WriteLine();

        writer.WriteLine("jobs:");
        using (writer.Indent())
        {
            Jobs.ForEach(x => x.Write(writer));
        }
    }
}
