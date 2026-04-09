// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.Utilities;

namespace Nuke.Common.Tools.GitLab;

public readonly struct GitLabRepository(GitRepository repo, GitLabHost host)
{
    public string Host => host;

    public bool IsGitLabRepository
        => repo?.IsRepositoryOnGitLabHost(host) ?? false;

    internal GitRepository Git => repo;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GitRepository Assertion()
    {
        //Assert.True(repo.IsRepositoryOnGitLabHost(host));
        return repo;
    }

    public string Owner => Assertion().Identifier.Split('/')[0];

    /// <summary>
    /// GitLab allows for Subgroups under a Group, and Subgroups in those Subgroups.
    /// To account for this, all subgroups and the actual project slug are in this value;
    /// the project slug being the last value delimited by a / or the whole value if there are no slashes.
    /// The top-most group is in <see cref="Owner"/>.
    /// <br/><br/>
    /// If you are using <see cref="Owner"/> + <see cref="SubPath"/> or <see cref="GitRepository.Identifier"/> in HTTP requests to GitLab, ensure you URL encode them first.
    /// <see cref="ApiIdentifier"/> has been provided for this reason, as a convenience.
    /// </summary>
    public string SubPath => Assertion().Identifier.Split('/').Skip(1).JoinSlash();

    /// <summary>
    /// URL-encoded <see cref="GitRepository.Identifier"/>.
    /// </summary>
    public string ApiIdentifier => WebUtility.UrlEncode(Assertion().Identifier);

    public string GetCompareUrl(string startCommitShaOrTag, string endCommitShaOrTagOrBranch, bool useHttps = true)
    {
        return $"{(useHttps ? "https" : "http")}://{host}/{Assertion().Identifier}/-/compare/{startCommitShaOrTag}...{endCommitShaOrTagOrBranch}";
    }

    public string GetCommitUrl(string commitSha, bool useHttps = true)
    {
        return $"{(useHttps ? "https" : "http")}://{host}/{Assertion().Identifier}/-/commit/{commitSha}";
    }

    /// <summary>Url in the form of <c>https://{host}/{identifier}/raw/branch/{branch}/{file}</c>.</summary>
    public string GetDownloadUrl(string file, string branch = null, bool useHttps = true)
    {
        _ = Assertion();

        branch ??= repo.Branch.NotNull("repo.Branch != null");
        var relativePath = repo.GetRelativePath(file);
        return $"{(useHttps ? "https" : "http")}://{host}/{repo.Identifier}/-/raw/{branch}/{relativePath}";
    }

    /// <summary>
    /// Url in the form of <c>https://{host}/{identifier}/-/tree/{branch}/directory</c> or
    /// <c>https://{host}/{identifier}/-/blob/{branch}/file</c> depending on the item type.
    /// </summary>
    public string GetBrowseUrl(
        string path = null,
        string branch = null,
        GitLabItemType itemType = GitLabItemType.Automatic, 
        bool useHttps = true)
    {
        branch ??= Assertion().Branch.NotNull();
        var relativePath = repo.GetRelativePath(path);
        var method = GetMethod(relativePath, itemType, repo);
        Assert.True(path == null || method != null, "Could not determine item type");

        return $"{(useHttps ? "https" : "http")}://{host}/{repo.Identifier}/-/{method}/{branch}/{relativePath}".TrimEnd("/");
    }

    [CanBeNull]
    private static string GetMethod([CanBeNull] string relativePath, GitLabItemType itemType, GitRepository repository)
    {
        var absolutePath = repository.LocalDirectory != null && relativePath != null
            ? PathConstruction.NormalizePath(Path.Combine(repository.LocalDirectory, relativePath))
            : null;

        if (itemType == GitLabItemType.Directory || Directory.Exists(absolutePath) || relativePath == null)
            return "tree";

        if (itemType == GitLabItemType.File || File.Exists(absolutePath))
            return "blob";

        return null;
    }
}

public enum GitLabItemType
{
    Automatic,
    File,
    Directory
}

public readonly struct GitLabHost(string host)
{
    public string Host => host;

    public static string Default { get; set; } = "gitlab.com";

    public static GitLabHost FromRepository(GitRepository repo)
    {
        return new GitLabHost(repo.Endpoint);
    }

    /// <param name="hostname">If <paramref name="hostname"/> is null, <see cref="Default"/> is used instead.</param>
    public static GitLabHost FromHostname([CanBeNull] string hostname = null)
    {
        return new GitLabHost(hostname ?? Default);
    }

    public static implicit operator GitLabHost(string hostname)
    {
        return new GitLabHost(hostname);
    }

    public static implicit operator string(GitLabHost host)
    {
        return host.ToString();
    }

    public override string ToString()
    {
        return Host.TrimEnd('/').TrimStart("https://").TrimStart("http://");
    }
}
