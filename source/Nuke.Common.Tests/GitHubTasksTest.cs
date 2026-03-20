// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using FluentAssertions;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.Tools.GitHub;
using Xunit;

namespace Nuke.Common.Tests;

public class GitHubTasksTest
{
    private static AbsolutePath RootDirectory => Constants.TryGetRootDirectoryFrom(EnvironmentInfo.WorkingDirectory).NotNull();

    [Fact]
    public void GitHubRepositoryFromLocalDirectoryTest()
    {
        var repository = GitRepository.FromLocalDirectory(RootDirectory).NotNull();
        if (!repository.IsGitHubRepository)
            return;

        var rawUrl = $"https://raw.githubusercontent.com/{repository.Identifier}/{repository.Branch}";
        var blobUrl = $"https://github.com/{repository.Identifier}/blob/{repository.Branch}";
        var treeUrl = $"https://github.com/{repository.Identifier}/tree/{repository.Branch}";

        repository.GitHub.GetDownloadUrl(RootDirectory / "LICENSE").Should().Be($"{rawUrl}/LICENSE");

        repository.GitHub.GetBrowseUrl("LICENSE").Should().Be($"{blobUrl}/LICENSE");
        repository.GitHub.GetBrowseUrl("source").Should().Be($"{treeUrl}/source");

        repository.GitHub.GetBrowseUrl(RootDirectory / "LICENSE").Should().Be($"{blobUrl}/LICENSE");
        repository.GitHub.GetBrowseUrl(RootDirectory / "source").Should().Be($"{treeUrl}/source");
        repository.GitHub.GetBrowseUrl(RootDirectory / "source" / "Directory.Build.props").Should().Be($"{blobUrl}/source/Directory.Build.props");

        repository.GitHub.GetBrowseUrl("directory", itemType: GitHubItemType.Directory).Should().Be($"{treeUrl}/directory");
        repository.GitHub.GetBrowseUrl("dir/file", itemType: GitHubItemType.File).Should().Be($"{blobUrl}/dir/file");

        repository.GitHub.GetBrowseUrl(branch: repository.Branch).Should().Be(treeUrl);
    }

    [Fact]
    public void GitHubRepositoryFromUrlTest()
    {
        var repository = GitRepository.FromUrl("https://github.com/gruke-build/src", "dev");

        repository.GitHub.GetBrowseUrl("LICENSE", itemType: GitHubItemType.File).Should().Be($"{repository}/blob/dev/LICENSE");
        repository.GitHub.GetBrowseUrl("source", itemType: GitHubItemType.Directory).Should().Be($"{repository}/tree/dev/source");
    }
}
