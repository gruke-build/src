// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.GitLab.Configuration;

[PublicAPI]
public class GitLabCIConfiguration : ConfigurationEntity
{
    public bool UseDocker { get; set; }
    [CanBeNull] public string DockerImage { get; set; }

    public IDictionary<string, object> Variables { get; set; }

    public IEnumerable<string> InvokedTargets { get; set; }

    public bool UploadArtifacts { get; set; }
    public string[] Artifacts { get; set; }

    public string[] ExcludedArtifacts { get; set; }

    public string[] OnlyOnPushesToBranches { get; set; }

    public override void Write(CustomFileWriter writer)
    {
        if (UseDocker && DockerImage == null && DotNetDockerImages.LookupCurrent(out var dockerImage))
        {
            DockerImage = dockerImage;
        }

        if (UseDocker)
            writer.WriteLine($"image: {DockerImage}");

        if (Variables.Count > 0)
        {
            writer.WriteLine();
            writer.WriteLine("variables:");
            using (writer.Indent())
            {
                foreach (var (varName, varValue) in Variables)
                {
                    writer.WriteLine($"{varName}: {varValue}");
                }
            }
        }

        // naive implementation: this can be extended in the future

        writer.WriteLine();
        writer.WriteLine("stages:");
        using (writer.Indent())
        {
            writer.WriteLine("- build");
        }

        writer.WriteLine();

        var title = $"Run: {InvokedTargets.JoinComma()}".Truncate(255);

        writer.WriteLine($"{title.SingleQuote()}:");
        using (writer.Indent())
        {
            writer.WriteLine("stage: build");
            writer.WriteLine("script:");
            using (writer.Indent())
            {
                writer.WriteLine($"- './build.sh {InvokedTargets.JoinSpace()}'");
            }

            if (UploadArtifacts && Artifacts.Length > 0)
            {
                writer.WriteLine("artifacts:");
                using (writer.Indent())
                {
                    writer.WriteLine("paths:");
                    using (writer.Indent())
                    {
                        foreach (var artifactPath in Artifacts.Except(ExcludedArtifacts))
                        {
                            writer.WriteLine($"- {artifactPath.SingleQuoteIfNeeded()}");
                        }
                    }
                }
            }

            if (OnlyOnPushesToBranches.Length > 0)
            {
                writer.WriteLine("rules:");
                using (writer.Indent())
                {
                    var longCondition = OnlyOnPushesToBranches.Select(FormatCondition).Join(" || ");

                    writer.WriteLine($"- if: {longCondition}");

                    static string FormatCondition(string branchName) =>
                        branchName is null
                            ? "$CI_COMMIT_BRANCH == $CI_DEFAULT_BRANCH" 
                            : $"$CI_COMMIT_BRANCH == {branchName.DoubleQuote()}";
                }
            }
        }
    }
}
