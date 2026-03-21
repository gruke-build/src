// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.Tools.Forgejo;
using Nuke.Common.Tools.GitHub;
using Nuke.Common.Tools.GitLab;
using Nuke.Common.Utilities;

namespace Nuke.Common.Git;

[PublicAPI]
public static class GitRepositoryExtensions
{
    extension(GitRepository repo)
    {
        #region GitHub

        public bool IsGitHubRepository => repo?.Endpoint?.EqualsOrdinalIgnoreCase("github.com") ?? false;
        public GitHubRepository GitHub => new(repo);

        #endregion

        #region Forgejo

        /// <summary>
        /// Determines if the current <see cref="GitRepository"/> is a repository hosted on the Forgejo server specified in <see cref="ForgejoHost.Default"/>.
        /// Use <see cref="IsRepositoryOnForgejoHost"/> to check against a different server, or change the value of <see cref="ForgejoHost.Default"/> to modify the operation of this property.
        /// </summary>
        public bool IsForgejoRepository => repo?.Endpoint?.EqualsOrdinalIgnoreCase(ForgejoHost.Default) ?? false;

        /// <summary>
        /// Create a <see cref="ForgejoRepository"/> instance, basing the Forgejo server off the current repository.
        /// To create a <see cref="ForgejoRepository"/> from a custom Forgejo server, see <see cref="GitRepositoryExtensions.Forgejo(GitRepository, ForgejoHost)"/>.
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
        /// Use <see cref="get_IsForgejoRepository"/> to check against the default server specified by <see cref="ForgejoHost.Default"/>.
        /// </summary>
        /// <param name="host">The Forgejo host. See static methods on <see cref="ForgejoHost"/>.</param>
        public bool IsRepositoryOnForgejoHost(ForgejoHost host)
        {
            return repo?.Endpoint?.EqualsOrdinalIgnoreCase(host) ?? false;
        }

        #endregion

        #region GitLab

        /// <summary>
        /// Determines if the current <see cref="GitRepository"/> is a repository hosted on the GitLab server specified in <see cref="GitLabHost.Default"/>.
        /// Use <see cref="IsRepositoryOnGitLabHost"/> to check against a different server, or change the value of <see cref="GitLabHost.Default"/> to modify the operation of this property.
        /// </summary>
        public bool IsGitLabRepository => repo?.Endpoint?.EqualsOrdinalIgnoreCase(GitLabHost.Default) ?? false;

        /// <summary>
        /// Create a <see cref="GitLabRepository"/> instance, basing the GitLab server off the current repository.
        /// To create a <see cref="GitLabRepository"/> from a custom GitLab server, see <see cref="GitRepositoryExtensions.GitLab(GitRepository, GitLabHost)"/>.
        /// </summary>
        public GitLabRepository GitLab()
        {
            return repo.GitLab(GitLabHost.FromRepository(repo));
        }

        /// <summary>
        /// Create a <see cref="GitLabRepository"/> instance, basing the GitLab server off an arbitrary hostname.
        /// </summary>
        public GitLabRepository GitLab(GitLabHost host)
        {
            return new GitLabRepository(repo, host);
        }

        /// <summary>
        /// Determines if the current <see cref="GitRepository"/> is a repository hosted on the GitLab server specified by <paramref name="host"/>.
        /// Use <see cref="get_IsGitLabRepository"/> to check against the default server specified by <see cref="GitLabHost.Default"/>.
        /// </summary>
        /// <param name="host">The GitLab host. See static methods on <see cref="GitLabHost"/>.</param>
        public bool IsRepositoryOnGitLabHost(GitLabHost host)
        {
            return repo?.Endpoint?.EqualsOrdinalIgnoreCase(host) ?? false;
        }

        #endregion

        #region Branch helpers

        public bool IsOnMainOrMasterBranch => repo.IsOnMainBranch || repo.IsOnMasterBranch;

        public bool IsOnMasterBranch => repo.Branch?.EqualsOrdinalIgnoreCase("master") ?? false;

        public bool IsOnMainBranch => repo.Branch?.EqualsOrdinalIgnoreCase("main") ?? false;

        public bool IsOnDevelopBranch =>
            (repo.Branch?.EqualsOrdinalIgnoreCase("dev") ?? false) ||
            (repo.Branch?.EqualsOrdinalIgnoreCase("develop") ?? false) ||
            (repo.Branch?.EqualsOrdinalIgnoreCase("development") ?? false);

        public bool IsOnFeatureBranch =>
            (repo.Branch?.StartsWithOrdinalIgnoreCase("feature/") ?? false) ||
            (repo.Branch?.StartsWithOrdinalIgnoreCase("features/") ?? false);

        public bool IsOnBugfixBranch => repo.Branch?.StartsWithOrdinalIgnoreCase("feature/fix-") ?? false;

        public bool IsOnReleaseBranch
            => (repo.Branch?.StartsWithOrdinalIgnoreCase("release/") ?? false) ||
               (repo.Branch?.StartsWithOrdinalIgnoreCase("releases/") ?? false);

        public bool IsOnHotfixBranch
            => (repo.Branch?.StartsWithOrdinalIgnoreCase("hotfix/") ?? false) ||
               (repo.Branch?.StartsWithOrdinalIgnoreCase("hotfixes/") ?? false);

        #endregion
    }
}
