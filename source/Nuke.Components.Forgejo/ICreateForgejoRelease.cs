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
using Nuke.Common.CI.ForgejoActions;
using Nuke.Common.IO;
using Nuke.Common.Tools.Forgejo;
using Nuke.Common.Utilities;
using Nuke.Common.Git;
using Nuke.Components.Forgejo.Models;
using Serilog;

namespace Nuke.Components;

[PublicAPI]
[ParameterPrefix(ForgejoRelease)]
public interface ICreateForgejoRelease : IHazGitRepository, IHazChangelog
{
    public const string ForgejoRelease = nameof(ForgejoRelease);

    [Parameter("Forgejo API token with appropriate API access")]
    [Secret] string ForgejoToken => TryGetValue(() => ForgejoToken) ?? ForgejoActions.Instance?.Token;

    [Parameter(
        "Owner of the Forgejo repository. Only required if the current repo is not cloned from Forgejo, or you want to use a different repository for releases.")]
    public string ForgejoOwner => TryGetValue(() => ForgejoOwner);

    [Parameter(
        "Name of the Forgejo repository. Only required if the current repo is not cloned from Forgejo, or you want to use a different repository for releases.")]
    public string ForgejoRepoName => TryGetValue(() => ForgejoRepoName);

    [Parameter(
        "Host of the Forgejo server. Only required if the current repo is not cloned from Forgejo, or you want to use a different server for releases.")]
    public string ForgejoHostName => TryGetValue(() => ForgejoHostName);

    internal ForgejoRepository Forgejo => ForgejoHostName != null
        ? GitRepository.Forgejo(ForgejoHostName)
        : GitRepository.Forgejo();

    string Name { get; }
    bool Prerelease => false;
    bool Draft => false;

    IEnumerable<AbsolutePath> AssetFiles { get; }

    Target CreateForgejoRelease => _ => _
        .Requires(() => ForgejoToken)
        .When(!Forgejo.IsForgejoRepository, _ => _
            .Requires(() => ForgejoOwner)
            .Requires(() => ForgejoRepoName)
        )
        .Executes(async () =>
        {
            ForgejoTasks.Reauthenticate(this);

            if (Forgejo.IsForgejoRepository && (ForgejoOwner is null || ForgejoRepoName is null))
                await createReleaseOnRepository(Forgejo.Owner, Forgejo.Name);
            else
                await createReleaseOnRepository(ForgejoOwner, ForgejoRepoName);

            return;

            async Task createReleaseOnRepository(string repoOwner, string repoName)
            {
                var release = await getOrCreateRelease(repoOwner, repoName);
                if (release.Id is not { } releaseId)
                {
                    Assert.Fail("Release object doesn't have a numeric ID.");
                    return;
                }

                foreach (var assetFile in AssetFiles)
                {
                    await using var data = File.OpenRead(assetFile);
                    await ForgejoTasks.ApiClient.Repos[repoOwner][repoName]
                        .Releases[releaseId].Assets
                        .PostAsync(data,
                            rc =>
                            {
                                rc.QueryParameters.Name = assetFile.Name;
                            });

                    Log.Information("Uploaded '{path}' to {host}.", assetFile, Forgejo.Host);
                }
            }

            async Task<Release> getOrCreateRelease(string owner, string repoName)
            {
                try
                {
                    return await ForgejoTasks.ApiClient.Repos[owner][repoName].Releases.PostAsync(
                        new CreateReleaseOption
                        {
                            Name = Name,
                            TagName = Name,
                            Prerelease = Prerelease,
                            Draft = Draft,
                            Body = ChangelogTasks.ExtractChangelogSectionNotes(ChangelogFile).JoinNewLine()
                        });
                }
                catch (Exception e)
                {
                    Log.Error(e, string.Empty);
                    var releases = await ForgejoTasks.ApiClient.Repos[owner][repoName].Releases
                        .GetAsync(rc => rc.QueryParameters.Q = Name);

                    return releases?.FirstOrDefault(x => x.Name == Name).NotNull();
                }
            }
        });
}
