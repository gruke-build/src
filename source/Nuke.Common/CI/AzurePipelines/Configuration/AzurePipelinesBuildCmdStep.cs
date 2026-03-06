// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace Nuke.Common.CI.AzurePipelines.Configuration;

[PublicAPI]
public class AzurePipelinesCmdStep : AzurePipelinesStep
{
    public virtual string DisplayName { get; set; }
    public virtual string Command { get; set; }
    [CanBeNull] public virtual string Arguments { get; set; }
    public virtual Dictionary<string, string> Imports { get; set; } = new();

    public override void Write(CustomFileWriter writer)
    {
        using (writer.WriteBlock("- task: CmdLine@2"))
        {
            writer.WriteLine("displayName: " + DisplayName.SingleQuoteIfNeeded());

            using (writer.WriteBlock("inputs:"))
            {
                writer.WriteLine($"script: {$"{Command}{(Arguments != null ? $" {Arguments}" : string.Empty)}".SingleQuote()}");
            }

            if (Imports.Count > 0)
            {
                using (writer.WriteBlock("env:"))
                {
                    Imports.ForEach(x => writer.WriteLine($"{x.Key}: {x.Value}"));
                }
            }
        }
    }
}

[PublicAPI]
public class AzurePipelinesBuildCmdStep : AzurePipelinesCmdStep
{
    public string[] InvokedTargets { get; set; }
    public string BuildCmdPath { get; set; }
    public int? PartitionSize { get; set; }

    public override string Arguments
    {
        get
        {
            var arguments = $"{InvokedTargets.JoinSpace()} --skip";
            if (PartitionSize != null)
                arguments += $" --partition $(System.JobPositionInPhase)/{PartitionSize}";

            return arguments;
        }
        set => throw new NotSupportedException("get-only override");
    }

    public override string DisplayName
    {
        get => $"Run: {InvokedTargets.JoinCommaSpace()}";
        set => throw new NotSupportedException("get-only override");
    }

    public override string Command
    {
        get => $"./{BuildCmdPath}";
        set => throw new NotSupportedException("get-only override");
    }

    public override Dictionary<string, string> Imports { get; set; }
}
