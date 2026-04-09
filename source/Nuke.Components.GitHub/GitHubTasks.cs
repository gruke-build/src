// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Nuke.Common.Git;
using Nuke.Common.Utilities;
using Octokit;
using static Nuke.Common.IO.PathConstruction;

namespace Nuke.Common.Tools.GitHub;

[PublicAPI]
public static class GitHubTasks
{
    public static GitHubClient GitHubClient = new(new ProductHeaderValue(nameof(NukeBuild)));

    static GitHubTasks()
    {
        if (EnvironmentInfo.GetVariable("GITHUB_TOKEN") is { } token)
            GitHubClient.Credentials = new Credentials(token);
    }

    public static async Task<IEnumerable<(string DownloadUrl, string RelativePath)>> GetGitHubDownloadUrls(
        this GitRepository repository,
        string directory = null,
        string branch = null)
    {
        Assert.True(repository.IsGitHubRepository);
        Assert.True(!HasPathRoot(directory) || repository.LocalDirectory != null);

        var relativeDirectory = HasPathRoot(directory)
            ? GetRelativePath(repository.LocalDirectory, directory)
            : directory;
        relativeDirectory = (relativeDirectory + "/").TrimStart("/");

        branch ??= await repository.GetDefaultBranch();
        var treeResponse = await GitHubClient.Git.Tree.GetRecursive(
            repository.GitHub.Owner,
            repository.GitHub.Name,
            branch);

        return treeResponse.Tree
            .Where(x => x.Type == TreeType.Blob)
            .Where(x => x.Path.StartsWithOrdinalIgnoreCase(relativeDirectory))
            .Select(x => (repository.GitHub.GetDownloadUrl(x.Path, branch), x.Path.TrimStart(relativeDirectory)));
    }

    public static async Task<string> GetDefaultBranch(this GitRepository repository)
    {
        Assert.True(repository.IsGitHubRepository);

        var repo = await GitHubClient.Repository.Get(repository.GitHub.Owner, repository.GitHub.Name);
        return repo.DefaultBranch;
    }

    public static async Task<string> GetLatestRelease(this GitRepository repository, bool includePrerelease = false, bool trimPrefix = false)
    {
        Assert.True(repository.IsGitHubRepository);
        var releases = await GitHubClient.Repository.Release.GetAll(repository.GitHub.Owner, repository.GitHub.Name);
        return releases.First(x => !x.Prerelease || includePrerelease).TagName.TrimStart(trimPrefix ? "v" : string.Empty);
    }

    [ItemCanBeNull]
    public static async Task<Milestone> GetGitHubMilestone(this GitRepository repository, string name)
    {
        Assert.True(repository.IsGitHubRepository);
        var milestones = await GitHubClient.Issue.Milestone.GetAllForRepository(
            repository.GitHub.Owner,
            repository.GitHub.Name,
            new MilestoneRequest { State = ItemStateFilter.All });
        return milestones.FirstOrDefault(x => x.Title == name);
    }

    public static async Task<IReadOnlyList<Issue>> GetGitHubMilestoneIssues(this GitRepository repository, string name)
    {
        Assert.True(repository.IsGitHubRepository);
        var milestone = await repository.GetGitHubMilestone(name).NotNull();
        return await GitHubClient.Issue.GetAllForRepository(
            repository.GitHub.Owner,
            repository.GitHub.Name,
            new RepositoryIssueRequest { State = ItemStateFilter.All, Milestone = milestone.Number.ToString() });
    }

    public static async Task TryCreateGitHubMilestone(this GitRepository repository, string title)
    {
        try
        {
            await repository.CreateGitHubMilestone(title);
        }
        catch
        {
            // ignored
        }
    }

    public static async Task CreateGitHubMilestone(this GitRepository repository, string title)
    {
        Assert.True(repository.IsGitHubRepository);
        await GitHubClient.Issue.Milestone.Create(
            repository.GitHub.Owner,
            repository.GitHub.Name,
            new NewMilestone(title));
    }

    public static async Task CloseGitHubMilestone(this GitRepository repository, string title, bool enableIssueChecks = true)
    {
        Assert.True(repository.IsGitHubRepository);
        var milestone = (await repository.GetGitHubMilestone(title)).NotNull("milestone != null");

        if (enableIssueChecks)
        {
            Assert.True(milestone.OpenIssues == 0);
            Assert.True(milestone.ClosedIssues != 0);
        }

        await GitHubClient.Issue.Milestone.Update(
            repository.GitHub.Owner,
            repository.GitHub.Name,
            milestone.Number,
            new MilestoneUpdate { State = ItemState.Closed });
    }
}
