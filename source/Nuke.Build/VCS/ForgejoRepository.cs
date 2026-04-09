// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Nuke.Common.Git;
using Nuke.Common.Utilities;

namespace Nuke.Common.Tools.Forgejo;

public readonly struct ForgejoRepository(GitRepository repo, ForgejoHost host)
{
    public string Host => host;

    public bool IsForgejoRepository
        => repo?.IsRepositoryOnForgejoHost(host) ?? false;

    internal GitRepository Git => repo;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GitRepository Assertion()
    {
        //Assert.True(repo.IsRepositoryOnForgejoHost(host));
        return repo;
    }

    public string Owner => Assertion().Identifier.Split('/')[0];

    public string Name => Assertion().Identifier.Split('/')[1];

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
        var relativePath = repo.GetRelativePath(file);
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
        var relativePath = repo.GetRelativePath(path);

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
