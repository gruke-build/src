// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NGitLab.Models;
using Nuke.Common;
using Nuke.Common.ChangeLog;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.Tools.GitLab;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;
using Nuke.Common.Utilities.Net;
using Nuke.Components.GitLab;
using Serilog;

namespace Nuke.Components;

[PublicAPI]
[ParameterPrefix(GitLabRelease)]
public interface ICreateGitLabRelease : IHazGitRepository, IHazChangelog
{
    public const string GitLabRelease = nameof(GitLabRelease);

    [Parameter("GitLab API token with appropriate API access")]
    [Secret] string GitLabToken => TryGetValue(() => GitLabToken) ?? Common.CI.GitLab.GitLab.Instance?.JobToken;

    [Parameter(
        "Path the GitLab repository. Only required if the current repo is not cloned from GitLab, or you want to use a different repository for releases.")]
    public string GitLabProjectPath => TryGetValue(() => GitLabProjectPath);

    [Parameter(
        "Host of the GitLab server. Only required if the current repo is not cloned from GitLab, or you want to use a different server for releases.")]
    public string GitLabHostName => TryGetValue(() => GitLabHostName);

    internal GitLabRepository GitLab => GitLabHostName != null
        ? GitRepository.GitLab(GitLabHostName)
        : GitRepository.GitLab();

    string Name { get; }
    string PackageName { get; }
    string Version { get; }

    bool AccessOverHttps => true;

    IEnumerable<AbsolutePath> AssetFiles { get; }

    Target CreateGitLabRelease => _ => _
        .When(Host is not Common.CI.GitLab.GitLab, _ => _
            .Requires(() => GitLabToken)
        )
        .When(!GitLab.IsGitLabRepository, _ => _
            .Requires(() => GitLabProjectPath)
        )
        .Executes(async () =>
        {
            string apiUrl;
            IHttpClientProxy http;
            if (Host is Common.CI.GitLab.GitLab gl && GitLabProjectPath is null)
            {
                http = gl.CreateHttpClient();
                apiUrl = gl.ApiV4Url.EnsureEnding('/');
            }
            else
            {
                http = GitLabApi.CreateHttpClient(
                    GitLabHostName ?? GitLabHost.Default,
                    AccessOverHttps,
                    GitLabToken.NotNull("GitLab API token is required when not running on GitLab CI, or targeting another project.")
                );
                apiUrl = (GitLabHostName ?? GitLabHost.Default).EnsureEnding("/api/v4/").EnsureStarting(AccessOverHttps ? "https://" : "http://");
            }

            GitLabTasks.Reauthenticate(this);

            await uploadAllArtifacts();

            if (GitLab.IsGitLabRepository && GitLabProjectPath is null)
                await createOrUpdateRelease(GitRepository.Identifier);
            else
                await createOrUpdateRelease(GitLabProjectPath);

            return;

            async Task uploadAllArtifacts()
            {
                IReadOnlyList<string> uploadErrors;
                if (Host is Common.CI.GitLab.GitLab gl && GitLabProjectPath is null)
                {
                    uploadErrors = await gl.UploadGenericPackagesAsync(http, AssetFiles, PackageName, Version);
                }
                else
                {
                    uploadErrors = await GitLabApi.UploadGenericPackagesToSpecificProjectAsync(
                        http,
                        AssetFiles,
                        GitLabProjectPath,
                        PackageName,
                        Version
                    );
                }

                if (uploadErrors!.Count is not 0)
                {
                    Log.Error("Errors occurred when uploading packages:");
                    uploadErrors.ForEach(new Action<string>(Log.Error));
                }
            }

            async Task createOrUpdateRelease(string projectPath)
            {
                if (await GitLabApi.FindMatchingPackageAsync(http, projectPath, PackageName, Version) is not { } package)
                {
                    Log.Error("Could not create a release, because a generic package matching the version could not be found.");
                    return;
                }

                var packageFiles = await package.GetPackageFiles(http, projectPath)
                    .GetAllAsync(onNonSuccess: _ => Log.Error("Target project has the package registry disabled."));

                if (packageFiles is null)
                {
                    Log.Error("Could not create a release because the request to get all package files for package matching name "
                              + "{PackageName}, version {PackageVersion} on project {ProjectPath} failed.",
                        PackageName, Version, projectPath);
                    return;
                }

                var project = await GitLabTasks.ApiClient.Projects.GetByNamespacedPathAsync(projectPath);

                var releaseLinks = packageFiles
                    .Select(x => new ReleaseLink
                                 {
                                     Name = x.Name,
                                     LinkType = ReleaseLinkType.Package,
                                     Url = $"{apiUrl}projects/{project.Id}/packages/generic/{PackageName}/{Version}/{x.Name}"
                                 });

                await GitLabApi.UpdateReleaseAsync(
                    projectPath,
                    Name,
                    ChangelogTasks.ExtractChangelogSectionNotes(ChangelogFile).JoinNewLine(),
                    Version,
                    @ref: null,
                    releaseLinks: releaseLinks.ToArray(),
                    gr: GitRepository
                );
            }
        });
}
