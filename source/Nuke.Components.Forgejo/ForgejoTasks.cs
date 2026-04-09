// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Nuke.Common.CI.ForgejoActions;
using Nuke.Components.Forgejo;
using Nuke.Components.Forgejo.Models;
using Nuke.Common.Utilities;
using Nuke.Components;
using static Nuke.Common.IO.PathConstruction;

namespace Nuke.Common.Tools.Forgejo;

[PublicAPI]
public static class ForgejoTasks
{
    public static ForgejoApiClient ApiClient { get; private set; } = new(new HttpClientRequestAdapter(CreateAuthProvider()));

    public static void Reauthenticate(ICreateForgejoRelease build)
    {
        ApiClient = new(new HttpClientRequestAdapter(CreateAuthProvider(build))
                        {
                            BaseUrl = $"{(build.AccessOverHttps ? "https" : "http")}://{build.Forgejo.Host ?? build.ForgejoHostName}/api/v1"
                        });
    }

    #region Auth

    private struct ConstantTokenProvider(string token, string[] allowedHosts = null) : IAccessTokenProvider
    {
        public Task<string> GetAuthorizationTokenAsync(
            Uri uri,
            Dictionary<string, object> additionalAuthenticationContext = null,
            CancellationToken cancellationToken = new())
        {
            return Task.FromResult(token);
        }

        public AllowedHostsValidator AllowedHostsValidator => new(allowedHosts);
    }

    private struct TokenProvider : IAccessTokenProvider
    {
        public Task<string> GetAuthorizationTokenAsync(
            Uri uri,
            Dictionary<string, object> additionalAuthenticationContext = null,
            CancellationToken cancellationToken = new())
        {
            if (ForgejoActions.Instance?.Token is { } token)
                return Task.FromResult(token);

            return Task.FromResult<string>(null);
        }

        public AllowedHostsValidator AllowedHostsValidator => new();
    }

    private static IAuthenticationProvider CreateAuthProvider(ICreateForgejoRelease build = null)
    {
        if (build is not null)
        {
            return new BaseBearerTokenAuthenticationProvider(
                new ConstantTokenProvider(build.ForgejoToken, [build.ForgejoHostName])
            );
        }

        if (ForgejoActions.Instance != null)
            return new BaseBearerTokenAuthenticationProvider(new TokenProvider());

        return new AnonymousAuthenticationProvider();
    }

    #endregion

    public static async Task<IEnumerable<(string DownloadUrl, string RelativePath)>> GetDownloadUrls(
        this ForgejoRepository repository,
        string directory = null,
        string branch = null)
    {
        Assert.True(!HasPathRoot(directory) || repository.Assertion().LocalDirectory != null);

        var relativeDirectory = HasPathRoot(directory)
            ? repository.Git.LocalDirectory.GetRelativePathTo(directory)
            : directory;
        relativeDirectory = (relativeDirectory + "/").TrimStart("/");

        branch ??= await repository.GetDefaultBranch();
        var treeResponse = await ApiClient.Repos[repository.Owner][repository.Name].Git.Trees[branch].GetAsync();
        if (treeResponse?.Tree is null)
            return [];

        return treeResponse.Tree
            .Where(x => x.Path.StartsWithOrdinalIgnoreCase(relativeDirectory))
            .Select(x => (repository.GetDownloadUrl(x.Path, branch), x.Path.TrimStart(relativeDirectory)));
    }

    public static async Task<string> GetDefaultBranch(this ForgejoRepository repository)
    {
        _ = repository.Assertion();
        var repo = await ApiClient.Repos[repository.Owner][repository.Name].GetAsync();
        return repo?.DefaultBranch;
    }

    public static async Task<string> GetLatestRelease(this ForgejoRepository repository, bool includePrerelease = false, bool trimPrefix = false)
    {
        _ = repository.Assertion();
        var release = await ApiClient.Repos[repository.Owner][repository.Name].Releases.Latest.GetAsync();
        return release?.TagName?.TrimStart(trimPrefix ? "v" : string.Empty);
    }

    [ItemCanBeNull]
    public static async Task<Milestone> GetMilestone(this ForgejoRepository repository, string name)
    {
        _ = repository.Assertion();

        var milestones = await ApiClient.Repos[repository.Owner][repository.Name].Milestones
            .GetAsync();

        return milestones?.FirstOrDefault(x => x.Title == name);
    }

    public static async Task<IReadOnlyList<Issue>> GetMilestoneIssues(this ForgejoRepository repository, string name)
    {
        _ = repository.Assertion();

        var milestone = await repository.GetMilestone(name).NotNull();

        return await ApiClient.Repos[repository.Owner][repository.Name].Issues
            .GetAsync(rc => rc.QueryParameters.Milestones = $"{milestone.Id}");
    }

    public static async Task TryCreateMilestone(this ForgejoRepository repository, string title)
    {
        _ = repository.Assertion();

        try
        {
            await repository.CreateMilestone(title);
        }
        catch
        {
            // ignored
        }
    }

    public static async Task<Milestone> CreateMilestone(this ForgejoRepository repository, string title)
    {
        _ = repository.Assertion();

        return await ApiClient.Repos[repository.Owner][repository.Name].Milestones
            .PostAsync(new CreateMilestoneOption
                       {
                           Title = title
                       });
    }

    public static async Task CloseMilestone(this ForgejoRepository repository, string title, bool enableIssueChecks = true)
    {
        _ = repository.Assertion();
        var milestone = (await repository.GetMilestone(title)).NotNull("milestone != null");

        if (enableIssueChecks)
        {
            Assert.True(milestone.OpenIssues == 0);
            Assert.True(milestone.ClosedIssues != 0);
        }

        Assert.True(milestone.Id != null);

        await ApiClient.Repos[repository.Owner][repository.Name].Milestones[milestone.Id.Value]
            .PatchAsync(new EditMilestoneOption
                        {
                            State = "closed"
                        });
    }
}
