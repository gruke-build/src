// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.GitLab.Configuration;

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
        if (UseDocker && DockerImage == null &&
            s_sdkVersionToDockerImageVersions.TryGetValue(Environment.Version.Major, out var dockerImage))
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

    private static readonly Dictionary<int, string> s_sdkVersionToDockerImageVersions = new();

    static GitLabCIConfiguration()
    {
        s_sdkVersionToDockerImageVersions.Add(key: 10, "mcr.microsoft.com/dotnet/sdk:10.0.103");
        s_sdkVersionToDockerImageVersions.Add(key: 9, "mcr.microsoft.com/dotnet/sdk:9.0.311");
        s_sdkVersionToDockerImageVersions.Add(key: 8, "mcr.microsoft.com/dotnet/sdk:8.0.418");
        s_sdkVersionToDockerImageVersions.Add(key: 7, "mcr.microsoft.com/dotnet/sdk:7.0.410");
        s_sdkVersionToDockerImageVersions.Add(key: 6, "mcr.microsoft.com/dotnet/sdk:6.0.428-1");
        s_sdkVersionToDockerImageVersions.Add(key: 5, "mcr.microsoft.com/dotnet/sdk:5.0.408");
    }
}
