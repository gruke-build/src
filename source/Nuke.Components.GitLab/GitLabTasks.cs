// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using JetBrains.Annotations;
using NGitLab;
using Nuke.Common.Utilities;
using Nuke.Components;

namespace Nuke.Common.Tools.GitLab;

[PublicAPI]
public static class GitLabTasks
{
    public static GitLabClient ApiClient { get; private set; } = new("https://gitlab.com/");

    public static void Reauthenticate(ICreateGitLabRelease build)
    {
        ApiClient = new((build.GitLabHostName ?? GitLabHost.Default).EnsureStarting("https://"), build.GitLabToken ?? CI.GitLab.GitLab.Instance.JobToken);
    }
}
