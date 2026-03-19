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

public static class GitRepositoryExtensions
{
    extension(GitRepository repo)
    {
        public bool IsGitHubRepository => repo?.Endpoint?.EqualsOrdinalIgnoreCase("github.com") ?? false;

        public string GitHubOwner
        {
            get
            {
                Assert.True(repo.IsGitHubRepository);
                return repo.Identifier.Split('/')[0];
            }
        }
        
        public string GitHubName
        {
            get
            {
                Assert.True(repo.IsGitHubRepository);
                return repo.Identifier.Split('/')[1];
            }
        }
    }

    public static string GetGitHubCompareCommitsUrl(this GitRepository repository, string startCommitSha, string endCommitSha)
    {
        Assert.True(repository.IsGitHubRepository);
        return $"https://github.com/{repository.Identifier}/compare/{endCommitSha}^...{startCommitSha}";
    }

    public static string GetGitHubCompareTagToHeadUrl(this GitRepository repository, string tag)
    {
        Assert.True(repository.IsGitHubRepository);
        return $"https://github.com/{repository.Identifier}/compare/{tag}...HEAD";
    }

    public static string GetGitHubCompareTagsUrl(this GitRepository repository, string startTag, string endTag)
    {
        Assert.True(repository.IsGitHubRepository);
        return $"https://github.com/{repository.Identifier}/compare/{endTag}...{startTag}";
    }

    public static string GetGitHubCommitUrl(this GitRepository repository, string commitSha)
    {
        Assert.True(repository.IsGitHubRepository);
        return $"https://github.com/{repository.Identifier}/commit/{commitSha}";
    }

    /// <summary>Url in the form of <c>https://raw.githubusercontent.com/{identifier}/{branch}/{file}</c>.</summary>
    public static string GetGitHubDownloadUrl(this GitRepository repository, string file, string branch = null)
    {
        Assert.True(repository.IsGitHubRepository);

        branch ??= repository.Branch.NotNull("repository.Branch != null");
        var relativePath = GetRepositoryRelativePath(file, repository);
        return $"https://raw.githubusercontent.com/{repository.Identifier}/{branch}/{relativePath}";
    }

    /// <summary>
    /// Url in the form of <c>https://github.com/{identifier}/tree/{branch}/directory</c> or
    /// <c>https://github.com/{identifier}/blob/{branch}/file</c> depending on the item type.
    /// </summary>
    public static string GetGitHubBrowseUrl(
        this GitRepository repository,
        string path = null,
        string branch = null,
        GitHubItemType itemType = GitHubItemType.Automatic)
    {
        Assert.True(repository.IsGitHubRepository);

        branch ??= repository.Branch.NotNull();
        var relativePath = GetRepositoryRelativePath(path, repository);
        var method = GetMethod(relativePath, itemType, repository);
        Assert.True(path == null || method != null, "Could not determine item type");

        return $"https://github.com/{repository.Identifier}/{method}/{branch}/{relativePath}".TrimEnd("/");
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

    [ContractAnnotation("path: null => null; path: notnull => notnull")]
    private static string GetRepositoryRelativePath([CanBeNull] string path, GitRepository repository)
    {
        if (path == null)
            return null;

        if (!Path.IsPathRooted(path))
            return path;

        var localDirectory = repository.LocalDirectory.NotNull();
        Assert.True(IsDescendantPath(localDirectory, path), $"Path {path.SingleQuote()} must be descendant of {localDirectory:s}");
        return GetRelativePath(localDirectory, path).Replace(oldChar: '\\', newChar: '/');
    }
}
