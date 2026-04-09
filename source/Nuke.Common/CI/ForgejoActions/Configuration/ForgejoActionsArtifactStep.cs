// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.ForgejoActions.Configuration;

public class ForgejoActionsArtifactStep : ForgejoActionsStep
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string Condition { get; set; }

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine("- name: " + $"Publish: {Name}".SingleQuote());
        writer.WriteLine("  uses: https://data.forgejo.org/forgejo/upload-artifact@v5");

        using (writer.Indent())
        {
            if (!Condition.IsNullOrWhiteSpace())
            {
                writer.WriteLine($"if: {Condition}");
            }

            writer.WriteLine("with:");
            using (writer.Indent())
            {
                writer.WriteLine($"name: {Name}");
                writer.WriteLine($"path: {Path}");
            }
        }
    }
}
