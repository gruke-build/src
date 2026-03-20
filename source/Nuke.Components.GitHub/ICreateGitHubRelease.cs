// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.ChangeLog;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.Tools.GitHub;
using Nuke.Common.Utilities;
using Octokit;

namespace Nuke.Components;

[PublicAPI]
[ParameterPrefix(GitHubRelease)]
public interface ICreateGitHubRelease : IHazGitRepository, IHazChangelog
{
    public const string GitHubRelease = nameof(GitHubRelease);

    [Parameter("GitHub API token with appropriate API access")] [Secret] string GitHubToken => TryGetValue(() => GitHubToken) ?? GitHubActions.Instance?.Token;

    [Parameter("Owner of the GitHub repository. Only required if the current repo is not cloned from GitHub, or you want to use a different repository for releases.")] public string GitHubOwner => TryGetValue(() => GitHubOwner);
    [Parameter("Name of the GitHub repository. Only required if the current repo is not cloned from GitHub, or you want to use a different repository for releases.")] public string GitHubRepoName => TryGetValue(() => GitHubRepoName);

    string Name { get; }
    bool Prerelease => false;
    bool Draft => false;

    IEnumerable<AbsolutePath> AssetFiles { get; }

    Target CreateGitHubRelease => _ => _
        .Requires(() => GitHubToken)
        .When(!GitRepository.IsGitHubRepository, _ => _
            .Requires(() => GitHubOwner)
            .Requires(() => GitHubRepoName)
        )
        .Executes(async () =>
        {
            async Task<Release> GetOrCreateRelease(string owner, string repoName)
            {
                try
                {
                    return await GitHubTasks.GitHubClient.Repository.Release.Create(
                        owner,
                        repoName,
                        new NewRelease(Name)
                        {
                            Name = Name,
                            Prerelease = Prerelease,
                            Draft = Draft,
                            Body = ChangelogTasks.ExtractChangelogSectionNotes(ChangelogFile).JoinNewLine()
                        });
                }
                catch
                {
                    return await GitHubTasks.GitHubClient.Repository.Release.Get(owner, repoName, Name);
                }
            }

            async Task CreateReleaseOnRepository(string githubRepoOwner, string githubRepoName)
            {
                GitHubTasks.GitHubClient.Credentials = new Credentials(GitHubToken.NotNull());

                var release = await GetOrCreateRelease(githubRepoOwner, githubRepoName);

                var uploadTasks = AssetFiles.Select(async x =>
                {
                    await using var assetFile = File.OpenRead(x);
                    var asset = new ReleaseAssetUpload
                                {
                                    FileName = x.Name,
                                    ContentType = "application/octet-stream",
                                    RawData = assetFile
                                };
                    await GitHubTasks.GitHubClient.Repository.Release.UploadAsset(release, asset);
                }).ToArray();

                Task.WaitAll(uploadTasks);
            }

            if (GitRepository.IsGitHubRepository)
                await CreateReleaseOnRepository(GitRepository.GitHub.Owner, GitRepository.GitHub.Name);
            else
                await CreateReleaseOnRepository(GitHubOwner, GitHubRepoName);
        });
}
