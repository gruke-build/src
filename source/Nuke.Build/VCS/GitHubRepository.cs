// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.IO;
using JetBrains.Annotations;
using Nuke.Common.Git;
using Nuke.Common.Utilities;
using static Nuke.Common.IO.PathConstruction;

namespace Nuke.Common.Tools.GitHub;

public enum GitHubItemType
{
    Automatic,
    File,
    Directory
}

public readonly ref struct GitHubRepository(GitRepository repo)
{
    public bool IsGitHubRepository => repo?.Endpoint?.EqualsOrdinalIgnoreCase("github.com") ?? false;

    public string Owner
    {
        get
        {
            Assert.True(repo.IsGitHubRepository);
            return repo.Identifier.Split('/')[0];
        }
    }

    public string Name
    {
        get
        {
            Assert.True(repo.IsGitHubRepository);
            return repo.Identifier.Split('/')[1];
        }
    }

    public string GetCompareCommitsUrl(string startCommitSha, string endCommitSha)
    {
        Assert.True(repo.IsGitHubRepository);
        return $"https://github.com/{repo.Identifier}/compare/{endCommitSha}^...{startCommitSha}";
    }

    public string GetCompareTagToHeadUrl(string tag)
    {
        Assert.True(repo.IsGitHubRepository);
        return $"https://github.com/{repo.Identifier}/compare/{tag}...HEAD";
    }

    public string GetCompareTagsUrl(string startTag, string endTag)
    {
        Assert.True(repo.IsGitHubRepository);
        return $"https://github.com/{repo.Identifier}/compare/{endTag}...{startTag}";
    }

    public string GetCommitUrl(string commitSha)
    {
        Assert.True(repo.IsGitHubRepository);
        return $"https://github.com/{repo.Identifier}/commit/{commitSha}";
    }

    /// <summary>Url in the form of <c>https://raw.githubusercontent.com/{identifier}/{branch}/{file}</c>.</summary>
    public string GetDownloadUrl(string file, string branch = null)
    {
        Assert.True(repo.IsGitHubRepository);

        branch ??= repo.Branch.NotNull("repo.Branch != null");
        var relativePath = repo.GetRelativePath(file);
        return $"https://raw.githubusercontent.com/{repo.Identifier}/{branch}/{relativePath}";
    }

    /// <summary>
    /// Url in the form of <c>https://github.com/{identifier}/tree/{branch}/directory</c> or
    /// <c>https://github.com/{identifier}/blob/{branch}/file</c> depending on the item type.
    /// </summary>
    public string GetBrowseUrl(
        string path = null,
        string branch = null,
        GitHubItemType itemType = GitHubItemType.Automatic)
    {
        Assert.True(repo.IsGitHubRepository);

        branch ??= repo.Branch.NotNull();
        var relativePath = repo.GetRelativePath(path);
        var method = GetMethod(relativePath, itemType, repo);
        Assert.True(path == null || method != null, "Could not determine item type");

        return $"https://github.com/{repo.Identifier}/{method}/{branch}/{relativePath}".TrimEnd("/");
    }

    [CanBeNull]
    private static string GetMethod([CanBeNull] string relativePath, GitHubItemType itemType, GitRepository repository)
    {
        var absolutePath = repository.LocalDirectory != null && relativePath != null
            ? NormalizePath(Path.Combine(repository.LocalDirectory, relativePath))
            : null;

        if (itemType == GitHubItemType.Directory || Directory.Exists(absolutePath) || relativePath == null)
            return "tree";

        if (itemType == GitHubItemType.File || File.Exists(absolutePath))
            return "blob";

        return null;
    }
}
