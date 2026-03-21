// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Nuke.Common.CI;
using Nuke.Common.IO;
using Nuke.Common.Utilities;
using Serilog;

namespace Nuke.Common.Git;

public enum GitProtocol
{
    Https,
    Ssh
}

[PublicAPI]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public partial record GitRepository
{
    private const string FallbackRemoteName = "origin";

    public static GitRepository FromUrl(string url, string branch = null)
    {
        var (protocol, endpoint, identifier) = GetRemoteConnectionFromUrl(url);
        return new GitRepository(
            protocol,
            endpoint,
            identifier,
            branch,
            localDirectory: null,
            head: null,
            commit: null,
            tags: null,
            remoteName: null,
            remoteBranch: null);
    }

    /// <summary>
    /// Obtains information from a local git repository.
    /// </summary>
    public static GitRepository FromLocalDirectory(AbsolutePath directory)
    {
        var rootDirectory = directory.FindParentOrSelf(x => x.ContainsDirectory(".git")).NotNull($"No parent Git directory for '{directory}'");
        var gitDirectory = rootDirectory / ".git";

        var head = GetHead(gitDirectory);
        var branch = (GetBranchFromCI() ?? GetHeadIfAttached(head))?.TrimStart("refs/heads/").TrimStart("origin/");
        var commit = GetCommitFromCI() ?? GetCommitFromHead(gitDirectory, head);
        var tags = GetTagsFromCommit(gitDirectory, commit);
        var (remoteName, remoteBranch) = GetRemoteNameAndBranch(gitDirectory, branch);
        var (protocol, endpoint, identifier) = GetRemoteConnectionFromConfig(gitDirectory, remoteName ?? FallbackRemoteName);

        return new GitRepository(
            protocol,
            endpoint,
            identifier,
            branch,
            rootDirectory,
            head,
            commit,
            tags,
            remoteName,
            remoteBranch);
    }

    private static (string Name, string Branch) GetRemoteNameAndBranch(AbsolutePath gitDirectory, [CanBeNull] string branch)
    {
        if (branch == null)
            return (null, null);

        var configFile = gitDirectory / "config";
        var configFileContent = configFile.ReadAllLines();
        var data = new Dictionary<string, string>();
        var rawData = configFileContent
            .Select(x => x.Trim())
            .SkipWhile(x => x != $"[branch {branch.DoubleQuote()}]")
            .Skip(1)
            .TakeWhile(x => !x.StartsWith("["))
            .Select(x => x.Split('='));

        foreach (var partPair in rawData)
        {
            var key = partPair.ElementAt(0).Trim();
            var keyData = partPair.ElementAt(1).Trim();
            if (data.TryGetValue(key, out var existingData))
            {
                Log.Warning("Duplicate branch configuration found for '{branch}'; key: {key}, value: {value}", 
                    branch, key, existingData);
                Log.Warning("Overriding existing value '{oldValue}' with new value '{newValue}'", existingData, keyData);
                Log.Warning("To remove this warning, remove the mentioned duplicate entries from your config at '{configFile}'.", configFile.ToString());
            }

            data[key] = keyData;
        }

        return data.TryGetValue("remote", out var remote) && data.TryGetValue("merge", out var merge)
            ? (remote, merge.TrimStart("refs/heads/"))
            : (null, null);
    }

    internal static string GetHeadIfAttached(string head)
    {
        return head.StartsWith("refs/heads/") ? head : null;
    }

    internal static string GetCommitFromHead(AbsolutePath gitDirectory, string head)
    {
        if (!head.StartsWith("refs/heads/"))
            return head;

        var headRefFile = gitDirectory / head;
        if (headRefFile.Exists())
            return headRefFile.ReadAllLines().First();

        var commit = GetPackedRefs(gitDirectory)
            .Where(x => x.Reference == head)
            .Select(x => x.Commit)
            .FirstOrDefault();

        commit.NotNull("Could not find commit information");

        return commit;
    }

    private static string GetHead(AbsolutePath gitDirectory)
    {
        var headFile = gitDirectory / "HEAD";
        Assert.FileExists(headFile);
        return headFile.ReadAllText().TrimStart("ref: ").Trim();
    }

    [CanBeNull]
    internal static string GetBranchFromCI()
    {
        return (Host.Instance as IBuildServer)?.Branch;
    }

    [CanBeNull]
    internal static string GetCommitFromCI()
    {
        return (Host.Instance as IBuildServer)?.Commit;
    }

    private static IReadOnlyCollection<string> GetTagsFromCommit(AbsolutePath gitDirectory, string commit)
    {
        if (commit == null)
            return [];

        var packedTags = GetPackedRefs(gitDirectory)
            .Where(x => x.Commit == commit && x.Reference.StartsWithOrdinalIgnoreCase("refs/tags"))
            .Select(x => x.Reference.TrimStart("refs/tags/"));

        var tagsDirectory = gitDirectory / "refs" / "tags";
        var localTags = tagsDirectory
            .GlobFiles("**/*")
            .Where(x => x.ReadAllText().Trim() == commit)
            .Select(x => tagsDirectory.GetUnixRelativePathTo(x).ToString());

        return localTags.Concat(packedTags).ToList();
    }

    private static IEnumerable<(string Commit, string Reference)> GetPackedRefs(AbsolutePath gitDirectory)
    {
        var packedRefsFile = gitDirectory / "packed-refs";
        if (!packedRefsFile.Exists())
            return [];

        return packedRefsFile.ReadAllLines()
            .Where(x => !x.StartsWith("#") && !x.StartsWith("^"))
            .Select(x => x.Split(' '))
            .Select(x => (Commit: x[0], Reference: x[1]));
    }

    private static (GitProtocol Protocol, string Endpoint, string Identifier) GetRemoteConnectionFromUrl(string url)
    {
        var match = GitRemoteRegex.Match(url.NotNull().Trim());

        Assert.True(match.Success, $"Url '{url}' could not be parsed.");
        var protocol = match.Groups["protocol"].Value.EqualsOrdinalIgnoreCase(nameof(GitProtocol.Https))
            ? GitProtocol.Https
            : GitProtocol.Ssh;
        return (protocol, match.Groups["endpoint"].Value, match.Groups["identifier"].Value);
    }

    private static (GitProtocol? Protocol, string Endpoint, string Identifier) GetRemoteConnectionFromConfig(
        AbsolutePath gitDirectory,
        string remote)
    {
        var configFile = gitDirectory / "config";
        var configFileContent = configFile.ReadAllLines();
        var url = configFileContent
            .Select(x => x.Trim())
            .SkipWhile(x => x != $"[remote {remote.DoubleQuote()}]")
            .Skip(1)
            .TakeWhile(x => !x.StartsWith("["))
            .SingleOrDefault(x => x.StartsWithOrdinalIgnoreCase("url = "))
            ?.Split('=').ElementAt(1)
            .Trim();

        if (url == null)
            return (null, null, null);

        return GetRemoteConnectionFromUrl(url);
    }

    public GitRepository(
        GitProtocol? protocol,
        string endpoint,
        string identifier,
        string branch,
        AbsolutePath localDirectory,
        string head,
        string commit,
        IReadOnlyCollection<string> tags,
        string remoteName,
        string remoteBranch)
    {
        Protocol = protocol;
        Endpoint = endpoint;
        Identifier = identifier;
        Branch = branch;
        LocalDirectory = localDirectory;
        Head = head;
        Commit = commit;
        Tags = tags;
        RemoteName = remoteName;
        RemoteBranch = remoteBranch;
    }

    /// <summary>Default protocol for the repository.</summary>
    public GitProtocol? Protocol { get; private set; }

    /// <summary>Endpoint for the repository. For instance <em>github.com</em>.</summary>
    public string Endpoint { get; private set; }

    /// <summary>Identifier of the repository.</summary>
    public string Identifier { get; private set; }

    /// <summary>
    ///     Creates a copy of the current <see cref="GitRepository"/> with the specified parameters set as the respective properties on the copy.
    /// </summary>
    /// <param name="identifier"><see cref="Identifier"/></param>
    /// <param name="endpoint"><see cref="Endpoint"/></param>
    public GitRepository ModifyCopy(string identifier = null, string endpoint = null)
    {
        return this with
               {
                   Identifier = identifier ?? Identifier,
                   Endpoint = endpoint ?? Endpoint
               };
    }

    /// <summary>Local path from which the repository was parsed.</summary>
    [CanBeNull]
    public AbsolutePath LocalDirectory { get; private set; }

    /// <summary>Current head; <c>null</c> if parsed from URL.</summary>
    [CanBeNull]
    public string Head { get; private set; }

    /// <summary>Current commit; <c>null</c> if parsed from URL.</summary>
    [CanBeNull]
    public string Commit { get; }

    /// <summary>List of tags; <c>null</c> if parsed from URL.</summary>
    public IReadOnlyCollection<string> Tags { get; }

    /// <summary>Name of the remote.</summary>
    [CanBeNull]
    public string RemoteName { get; }

    /// <summary>Name of the remote branch.</summary>
    [CanBeNull]
    public string RemoteBranch { get; }

    /// <summary>Current branch; <c>null</c> if head is detached.</summary>
    [CanBeNull]
    public string Branch { get; private set; }

    /// <summary>Url in the form of <c>https://endpoint/identifier.git</c></summary>
    [CanBeNull]
    public string HttpsUrl => Endpoint != null ? $"https://{Endpoint}/{Identifier}.git" : null;

    /// <summary>Url in the form of <c>git@endpoint:identifier.git</c></summary>
    [CanBeNull]
    public string SshUrl => Endpoint != null ? $"git@{Endpoint}:{Identifier}.git" : null;

    public GitRepository SetBranch(string branch)
    {
        return new GitRepository(
            Protocol,
            Endpoint,
            Identifier,
            branch,
            LocalDirectory,
            Head,
            Commit,
            Tags,
            RemoteName,
            RemoteBranch);
    }

    [ContractAnnotation("path: null => null; path: notnull => notnull")]
    internal string GetRelativePath([CanBeNull] string path)
    {
        if (path == null)
            return null;

        if (!Path.IsPathRooted(path))
            return path;

        var localDirectory = LocalDirectory.NotNull();
        Assert.True(localDirectory.Contains(path), $"Path {path.SingleQuote()} must be descendant of {localDirectory:s}");
        return localDirectory.GetRelativePathTo(path);
    }

    public override string ToString()
    {
        return (Protocol == GitProtocol.Https ? HttpsUrl : SshUrl).TrimEnd(".git");
    }

    public static readonly Regex GitRemoteRegex = GitRemotePattern();

    [GeneratedRegex(@"^(?'protocol'\w+)?(\:\/\/)?(?>(?'user'.*)@)?(?'endpoint'[^\/:]+)(?>\:(?'port'\d+))?[\/:](?'identifier'.*?)\/?(?>\.git)?$")]
    private static partial Regex GitRemotePattern();
}
