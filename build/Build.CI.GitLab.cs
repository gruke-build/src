// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.Linq;
using NuGet.Packaging;
using Nuke.Common;
using Nuke.Common.CI.GitLab;
using Nuke.Components;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.ControlFlow;

[GitLabCI(
    InvokedTargets = [nameof(IPublish.Publish)],
    UploadProducedArtifacts = true,
    ExcludedArtifacts = [ "output/packages/*.nupkg" ],
    OnlyOnPushesToBranches = [ default ]
)]
partial class Build
{
    Target DeleteGitLabCIPackages => _ => _
        .OnlyWhenStatic(() => Host is GitLab)
        .DependentFor<IPublish>()
        .After<IPack>()
        .Executes(() =>
        {
            void DeletePackage(string id, string version)
                => DotNet(
                    $"nuget delete {id} {version} "
                    + $"--source {GitLab.Instance.GetNuGetSourceUrlForCurrentProject()} "
                    + $"--api-key {GitLab.Instance.JobToken} --non-interactive",
                    logOutput: false);

            var packageIds = NuGetPackageFiles.Select(x => new PackageArchiveReader(x).NuspecReader.GetId());
            foreach (var packageId in packageIds)
                SuppressErrors(() => DeletePackage(packageId, DefaultDeploymentVersion), logWarning: false);
        });
}
