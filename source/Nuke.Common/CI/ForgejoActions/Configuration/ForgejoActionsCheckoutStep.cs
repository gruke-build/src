// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.ForgejoActions.Configuration;

[PublicAPI]
public class ForgejoActionsCheckoutStep : ForgejoActionsStep
{
    public ForgejoActionsSubmodules? Submodules { get; set; }
    public bool? Lfs { get; set; }
    public uint? FetchDepth { get; set; }
    public bool? Progress { get; set; }
    public string Filter { get; set; }

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine("- uses: https://data.forgejo.org/actions/checkout@v6");

        if (Submodules.HasValue || Lfs.HasValue || FetchDepth.HasValue || Progress.HasValue || !Filter.IsNullOrWhiteSpace())
        {
            using (writer.Indent())
            {
                writer.WriteLine("with:");
                using (writer.Indent())
                {
                    if (Submodules.HasValue)
                        writer.WriteLine($"submodules: {Submodules.ToString()!.ToLowerInvariant()}");
                    if(Lfs.HasValue)
                        writer.WriteLine($"lfs: {Lfs.ToString().ToLowerInvariant()}");
                    if (FetchDepth.HasValue)
                        writer.WriteLine($"fetch-depth: {FetchDepth}");
                    if (Progress.HasValue)
                        writer.WriteLine($"progress: {Progress.ToString().ToLowerInvariant()}");
                    if (!Filter.IsNullOrWhiteSpace())
                        writer.WriteLine($"filter: {Filter}");
                }
            }
        }
    }
}
