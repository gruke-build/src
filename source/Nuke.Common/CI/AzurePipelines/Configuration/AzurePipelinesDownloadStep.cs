// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.AzurePipelines.Configuration;

[PublicAPI]
public class AzurePipelinesDownloadStep : AzurePipelinesStep
{
    public string ArtifactName { get; set; }
    public string DownloadPath { get; set; }

    public override void Write(CustomFileWriter writer)
    {
        using (writer.WriteBlock("- task: DownloadBuildArtifacts@0"))
        {
            // writer.WriteLine("displayName: Download Artifacts");
            using (writer.WriteBlock("inputs:"))
            {
                writer.WriteLine($"artifactName: {ArtifactName}");
                writer.WriteLine($"downloadPath: {DownloadPath.SingleQuote()}");
            }
        }
    }
}
