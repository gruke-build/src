// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Nuke.Common.Git;
using Nuke.Common.Utilities;

namespace Nuke.Common.Tools.GitHub;

public readonly ref struct ForgejoRepository(GitRepository repo, ForgejoHost host)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GitRepository Assertion()
    {
        Assert.True(repo.IsRepositoryOnForgejoHost(host));
        return repo;
    }

    public string Owner
    {
        get
        {
            return Assertion().Identifier.Split('/')[0];
        }
    }

    public string Name
    {
        get
        {
            return Assertion().Identifier.Split('/')[1];
        }
    }

    public string GetCompareCommitsUrl(string startCommitSha, string endCommitSha)
    {
        return $"https://{host}/{Assertion().Identifier}/compare/{startCommitSha}^...{endCommitSha}";
    }

    public string GetCompareTagToHeadUrl(string tag)
    {
        return $"https://{host}/{Assertion().Identifier}/compare/{tag}...HEAD";
    }

    public string GetCompareTagsUrl(string startTag, string endTag)
    {
        return $"https://{host}/{Assertion().Identifier}/compare/{startTag}...{endTag}";
    }

    public string GetCommitUrl(string commitSha)
    {
        return $"https://{host}/{Assertion().Identifier}/commit/{commitSha}";
    }

    /// <summary>Url in the form of <c>https://{host}/{identifier}/raw/branch/{branch}/{file}</c>.</summary>
    public string GetDownloadUrl(string file, string branch = null)
    {
        _ = Assertion();

        branch ??= repo.Branch.NotNull("repo.Branch != null");
        var relativePath = repo.GetRepositoryRelativePath(file);
        return $"https://{host}/{repo.Identifier}/raw/branch/{branch}/{relativePath}";
    }

    /// <summary>
    /// Url in the form of <c>https://{host}/{identifier}/src/branch/{branch}/{path}</c>.
    /// </summary>
    public string GetBrowseUrl(
        string path = null,
        string branch = null)
    {
        _ = Assertion();

        branch ??= repo.Branch.NotNull("repo.Branch != null");
        var relativePath = repo.GetRepositoryRelativePath(path);

        return $"https://{host}/{repo.Identifier}/src/branch/{branch}/{relativePath}".TrimEnd("/");
    }
}

public readonly struct ForgejoHost(string host)
{
    public string Host => host;

    public static string Default { get; set; } = "codeberg.org";

    public static ForgejoHost FromRepository(GitRepository repo)
    {
        return new ForgejoHost(repo.Endpoint);
    }

    /// <param name="hostname">If <paramref name="hostname"/> is null, <see cref="Default"/> is used instead.</param>
    public static ForgejoHost FromHostname([CanBeNull] string hostname = null)
    {
        return new ForgejoHost(hostname ?? Default);
    }

    public static implicit operator ForgejoHost(string hostname)
    {
        return new ForgejoHost(hostname);
    }

    public static implicit operator string(ForgejoHost host)
    {
        return host.ToString();
    }

    public override string ToString()
    {
        return Host.TrimEnd('/').TrimStart("https://").TrimStart("http://");
    }
}

public partial class GitRepositoryExtensions
{
    extension(GitRepository repo)
    {
        /// <summary>
        /// Determines if the current <see cref="GitRepository"/> is a repository hosted on the Forgejo server specified in <see cref="ForgejoHost.Default"/>.
        /// Use <see cref="IsRepositoryOnForgejoHost"/> to check against a different server, or change the value of <see cref="ForgejoHost.Default"/> to modify the operation of this property.
        /// </summary>
        public bool IsForgejoRepository => repo?.Endpoint?.EqualsOrdinalIgnoreCase(ForgejoHost.Default) ?? false;

        /// <summary>
        /// Create a <see cref="ForgejoRepository"/> instance, basing the Forgejo server off the current repository.
        /// To create a <see cref="ForgejoRepository"/> from a custom Forgejo server, see <see cref="GitRepositoryExtensions.Forgejo(Nuke.Common.Git.GitRepository,Nuke.Common.Tools.GitHub.ForgejoHost)"/>.
        /// </summary>
        public ForgejoRepository Forgejo()
        {
            return repo.Forgejo(ForgejoHost.FromRepository(repo));
        }

        /// <summary>
        /// Create a <see cref="ForgejoRepository"/> instance, basing the Forgejo server off an arbitrary hostname.
        /// </summary>
        public ForgejoRepository Forgejo(ForgejoHost host)
        {
            return new ForgejoRepository(repo, host);
        }

        /// <summary>
        /// Determines if the current <see cref="GitRepository"/> is a repository hosted on the Forgejo server specified by <paramref name="host"/>.
        /// Use <see cref="IsForgejoRepository"/> to check against the default server specified by <see cref="ForgejoHost.Default"/>.
        /// </summary>
        /// <param name="host">The Forgejo host. See static methods on <see cref="ForgejoHost"/>.</param>
        public bool IsRepositoryOnForgejoHost(ForgejoHost host)
        {
            return repo?.Endpoint?.EqualsOrdinalIgnoreCase(host) ?? false;
        }
    }
}
