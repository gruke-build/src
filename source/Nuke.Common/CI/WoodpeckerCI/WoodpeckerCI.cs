// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.WoodpeckerCI;

/// <summary>
/// Interface according to the <a href="https://woodpecker-ci.org/docs/usage/environment">official website</a>.
/// </summary>
[PublicAPI]
[CI]
[ExcludeFromCodeCoverage]
// ReSharper disable InconsistentNaming
public class WoodpeckerCI : Host, IBuildServer, IEnvironment<WoodpeckerCI>
{
    public static string EnvironmentVariablePrefix => "CI";

    public new static WoodpeckerCI Instance => Host.Instance as WoodpeckerCI;

    [UsedImplicitly]
    internal static bool IsRunningWoodpeckerCI => IEnvironment<WoodpeckerCI>.Get("SYSTEM_NAME") == "woodpecker";

    internal WoodpeckerCI()
    {
    }

    string IBuildServer.Commit => CommitSha;
    string IBuildServer.Branch => CommitRef;

    #region Repository

    /// <summary>
    /// Repository full name
    /// </summary>
    /// <example>
    /// <c>&lt;owner&gt;/&lt;name&gt;</c>
    /// </example>
    public string Repository => IEnvironment<WoodpeckerCI>.Get("REPO");

    public string RepositoryOwner => IEnvironment<WoodpeckerCI>.Get("REPO_OWNER");
    public string RepositoryName => IEnvironment<WoodpeckerCI>.Get("REPO_NAME");

    /// <summary>
    /// The unique ID number the repository has on the git forge
    /// </summary>
    [CanBeNull] public long? RepositoryRemoteId => IEnvironment<WoodpeckerCI>.Has("REPO_REMOTE_ID")
        ? IEnvironment<WoodpeckerCI>.Get<long>("REPO_REMOTE_ID")
        : null;

    public string RepositoryUrl => IEnvironment<WoodpeckerCI>.Get("REPO_URL");
    public string RepositoryCloneUrl => IEnvironment<WoodpeckerCI>.Get("REPO_CLONE_URL");
    public string RepositoryCloneSshUrl => IEnvironment<WoodpeckerCI>.Get("REPO_CLONE_SSH_URL");
    public string RepositoryDefaultBranch => IEnvironment<WoodpeckerCI>.Get("REPO_DEFAULT_BRANCH");
    public bool RepositoryIsPrivate => IEnvironment<WoodpeckerCI>.Get<bool>("REPO_PRIVATE");
    public bool RepositoryHasTrustedNetworkAccess => IEnvironment<WoodpeckerCI>.Get<bool>("REPO_TRUSTED_NETWORK");
    public bool RepositoryHasTrustedVolumesAccess => IEnvironment<WoodpeckerCI>.Get<bool>("REPO_TRUSTED_VOLUMES");
    public bool RepositoryHasTrustedSecurityAccess => IEnvironment<WoodpeckerCI>.Get<bool>("REPO_TRUSTED_SECURITY");

    #endregion

    #region Current Commit

    public string CommitSha => IEnvironment<WoodpeckerCI>.Get("COMMIT_SHA");
    public string CommitRef => IEnvironment<WoodpeckerCI>.Get("COMMIT_REF");
    [CanBeNull] public string CommitRefSpec => IEnvironment<WoodpeckerCI>.Get("COMMIT_REFSPEC");

    /// <summary>
    /// Commit branch (equals target branch for pull requests)
    /// </summary>
    public string CommitBranch => IEnvironment<WoodpeckerCI>.Get("COMMIT_BRANCH");

    /// <summary>
    /// Commit source branch (set only for pull request events)
    /// </summary>
    [CanBeNull] public string CommitSourceBranch => IEnvironment<WoodpeckerCI>.Get("COMMIT_SOURCE_BRANCH");

    /// <summary>
    /// Commit target branch (set only for pull request events)
    /// </summary>
    [CanBeNull] public string CommitTargetBranch => IEnvironment<WoodpeckerCI>.Get("COMMIT_TARGET_BRANCH");

    /// <summary>
    /// Commit tag name (null if <see cref="PipelineEvent"/> is not <see cref="WoodpeckerCIEventType.Tag"/>)
    /// </summary>
    [CanBeNull] public string CommitTag => IEnvironment<WoodpeckerCI>.GetOrNullIfEmpty("COMMIT_TAG");

    /// <summary>
    /// Commit pull request number (set only for pull request events)
    /// </summary>
    [CanBeNull] public long? CommitPullRequest => IEnvironment<WoodpeckerCI>.Get<long?>("COMMIT_PULL_REQUEST");

    /// <summary>
    /// Labels assigned to pull request (set only for pull request events)
    /// </summary>
    [CanBeNull] public string CommitPullRequestLabels => IEnvironment<WoodpeckerCI>.Get("COMMIT_PULL_REQUEST_LABELS");

    /// <summary>
    /// Milestone assigned to pull request (set only if <see cref="PipelineEvent"/>
    /// is <see cref="WoodpeckerCIEventType.PullRequest"/> or <see cref="WoodpeckerCIEventType.PullRequestClosed"/>)
    /// </summary>
    [CanBeNull] public string CommitPullRequestMilestone => IEnvironment<WoodpeckerCI>.Get("COMMIT_PULL_REQUEST_MILESTONE");

    public string CommitMessage => IEnvironment<WoodpeckerCI>.Get("COMMIT_MESSAGE");
    public string CommitAuthor => IEnvironment<WoodpeckerCI>.Get("COMMIT_AUTHOR");
    public string CommitAuthorEmail => IEnvironment<WoodpeckerCI>.Get("COMMIT_AUTHOR_EMAIL");

    /// <summary>
    /// Release is a pre-release (null if event is not <see cref="WoodpeckerCIEventType.Release"/>)
    /// </summary>
    [CanBeNull] public bool? CommitPrerelease => IEnvironment<WoodpeckerCI>.Get<bool?>("COMMIT_PRERELEASE");

    #endregion

    #region Current pipeline

    public long PipelineNumber => IEnvironment<WoodpeckerCI>.Get<long>("PIPELINE_NUMBER");
    public long PipelineParentNumber => IEnvironment<WoodpeckerCI>.Get<long>("PIPELINE_PARENT");

    /// <summary>
    /// Pipeline event (see <a href="https://woodpecker-ci.org/docs/usage/workflow-syntax#event">event</a>)
    /// </summary>
    public WoodpeckerCIEventType PipelineEvent => IEnvironment<WoodpeckerCI>.Get<WoodpeckerCIEventType>("PIPELINE_EVENT");

    /// <summary>
    /// Exact reason why <see cref="WoodpeckerCIEventType.PullRequestMetadata"/> event was sent.
    /// <br/>
    /// It is forge instance specific and can change.
    /// <example>
    /// <c>label_updated</c>, <c>milestoned</c>, <c>demilestoned</c>, <c>assigned</c>, <c>edited</c>, ...
    /// </example>
    /// </summary>
    [CanBeNull] public string PipelineEventReason => IEnvironment<WoodpeckerCI>.Get("PIPELINE_EVENT_REASON");

    /// <summary>
    /// Link to the web UI for the pipeline
    /// </summary>
    public string PipelineUrl => IEnvironment<WoodpeckerCI>.Get("PIPELINE_URL");

    /// <summary>
    /// Link to the forge's web UI for the commit(s) or tag that triggered the pipeline
    /// </summary>
    public string PipelineForgeUrl => IEnvironment<WoodpeckerCI>.Get("PIPELINE_FORGE_URL");

    /// <summary>
    /// Pipeline deploy target for <see cref="WoodpeckerCIEventType.Deployment"/> events
    /// </summary>
    [CanBeNull] public string PipelineDeployTarget => IEnvironment<WoodpeckerCI>.Get("PIPELINE_DEPLOY_TARGET");

    /// <summary>
    /// Pipeline deploy task for <see cref="WoodpeckerCIEventType.Deployment"/> events
    /// </summary>
    [CanBeNull] public string PipelineDeployTask => IEnvironment<WoodpeckerCI>.Get("PIPELINE_DEPLOY_TASK");

    public DateTime PipelineCreatedAt => DateTime.FromUnixTimestamp(IEnvironment<WoodpeckerCI>.Get<long>("PIPELINE_CREATED"));
    public DateTime PipelineStartedAt => DateTime.FromUnixTimestamp(IEnvironment<WoodpeckerCI>.Get<long>("PIPELINE_STARTED"));

    /// <summary>
    /// A list of the changed files (empty if event is not <see cref="WoodpeckerCIEventType.Push"/> or <see cref="WoodpeckerCIEventType.PullRequest"/>).
    /// <br/>
    /// It is null if more than 500 files are touched.
    /// </summary>
    [CanBeNull] public IReadOnlyList<string> PipelineFiles =>
        IEnvironment<WoodpeckerCI>.Has("PIPELINE_FILES")
            ? System.Text.Json.JsonSerializer.Deserialize<IReadOnlyList<string>>(
                IEnvironment<WoodpeckerCI>.Get("PIPELINE_FILES")
            )
            : null;

    public string PipelineAuthor => IEnvironment<WoodpeckerCI>.Get("PIPELINE_AUTHOR");

    /// <summary>
    /// Pipeline author avatar URL from the git forge
    /// </summary>
    public string PipelineAuthorAvatar => IEnvironment<WoodpeckerCI>.Get("PIPELINE_AVATAR");

    #endregion

    #region Current workflow/step

    public string WorkflowName => IEnvironment<WoodpeckerCI>.Get("WORKFLOW_NAME");
    [NoValueCheck] public string StepName => IEnvironment<WoodpeckerCI>.Get("STEP_NAME");
    public long StepNumber => IEnvironment<WoodpeckerCI>.Get<long>("STEP_NUMBER");
    public DateTime StepStartedAt => DateTime.FromUnixTimestamp(IEnvironment<WoodpeckerCI>.Get<long>("STEP_STARTED"));

    /// <summary>
    /// Link to step in web UI
    /// </summary>
    public string StepUrl => IEnvironment<WoodpeckerCI>.Get("STEP_URL");

    #endregion

    #region Previous Commit

    public string PreviousCommitSha => IEnvironment<WoodpeckerCI>.Get("PREV_COMMIT_SHA");
    public string PreviousCommitRef => IEnvironment<WoodpeckerCI>.Get("PREV_COMMIT_REF");
    [CanBeNull] public string PreviousCommitRefSpec => IEnvironment<WoodpeckerCI>.Get("PREV_COMMIT_REFSPEC");
    public string PreviousCommitBranch => IEnvironment<WoodpeckerCI>.Get("PREV_COMMIT_BRANCH");

    /// <summary>
    /// Previous commit source branch (set only for pull request events)
    /// </summary>
    [CanBeNull] public string PreviousCommitSourceBranch => IEnvironment<WoodpeckerCI>.Get("PREV_COMMIT_SOURCE_BRANCH");

    /// <summary>
    /// Previous commit target branch (set only for pull request events)
    /// </summary>
    [CanBeNull] public string PreviousCommitTargetBranch => IEnvironment<WoodpeckerCI>.Get("PREV_COMMIT_TARGET_BRANCH");

    /// <summary>
    /// Previous commit link in forge UI
    /// </summary>
    public string PreviousCommitUrl => IEnvironment<WoodpeckerCI>.Get("PREV_COMMIT_URL");

    public string PreviousCommitMessage => IEnvironment<WoodpeckerCI>.Get("PREV_COMMIT_MESSAGE");
    public string PreviousCommitAuthor => IEnvironment<WoodpeckerCI>.Get("PREV_COMMIT_AUTHOR");
    public string PreviousCommitAuthorEmail => IEnvironment<WoodpeckerCI>.Get("PREV_COMMIT_AUTHOR_EMAIL");

    #endregion

    #region Previous pipeline

    public long PreviousPipelineNumber => IEnvironment<WoodpeckerCI>.Get<long>("PREV_PIPELINE_NUMBER");
    public long PreviousPipelineParentNumber => IEnvironment<WoodpeckerCI>.Get<long>("PREV_PIPELINE_PARENT");

    /// <summary>
    /// Previous pipeline event (see <a href="https://woodpecker-ci.org/docs/usage/workflow-syntax#event">event</a>)
    /// </summary>
    public WoodpeckerCIEventType PreviousPipelineEvent => IEnvironment<WoodpeckerCI>.Get<WoodpeckerCIEventType>("PREV_PIPELINE_EVENT");

    /// <summary>
    /// Previous exact reason why <see cref="WoodpeckerCIEventType.PullRequestMetadata"/> event was sent.
    /// <br/>
    /// It is forge instance specific and can change.
    /// <example>
    /// <c>label_updated</c>, <c>milestoned</c>, <c>demilestoned</c>, <c>assigned</c>, <c>edited</c>, ...
    /// </example>
    /// </summary>
    [CanBeNull] public string PreviousPipelineEventReason => IEnvironment<WoodpeckerCI>.Get("PREV_PIPELINE_EVENT_REASON");

    /// <summary>
    /// Link to the web UI for the previous pipeline
    /// </summary>
    public string PreviousPipelineUrl => IEnvironment<WoodpeckerCI>.Get("PREV_PIPELINE_URL");

    /// <summary>
    /// Link to the forge's web UI for the commit(s) or tag that triggered the previous pipeline
    /// </summary>
    public string PreviousPipelineForgeUrl => IEnvironment<WoodpeckerCI>.Get("PREV_PIPELINE_FORGE_URL");

    /// <summary>
    /// Previous pipeline deploy target for <see cref="WoodpeckerCIEventType.Deployment"/> events
    /// </summary>
    [CanBeNull] public string PreviousPipelineDeployTarget => IEnvironment<WoodpeckerCI>.Get("PREV_PIPELINE_DEPLOY_TARGET");

    /// <summary>
    /// Previous pipeline deploy task for <see cref="WoodpeckerCIEventType.Deployment"/> events
    /// </summary>
    [CanBeNull] public string PreviousPipelineDeployTask => IEnvironment<WoodpeckerCI>.Get("PREV_PIPELINE_DEPLOY_TASK");

    /// <example>
    /// <c>success</c>, <c>failure</c>
    /// </example>
    public string PreviousPipelineStatus => IEnvironment<WoodpeckerCI>.Get("PREV_PIPELINE_STATUS");

    public DateTime PreviousPipelineCreatedAt => DateTime.FromUnixTimestamp(IEnvironment<WoodpeckerCI>.Get<long>("PREV_PIPELINE_CREATED"));
    public DateTime PreviousPipelineStartedAt => DateTime.FromUnixTimestamp(IEnvironment<WoodpeckerCI>.Get<long>("PREV_PIPELINE_STARTED"));
    public DateTime PreviousPipelineFinishedAt => DateTime.FromUnixTimestamp(IEnvironment<WoodpeckerCI>.Get<long>("PREV_PIPELINE_FINISHED"));

    public string PreviousPipelineAuthor => IEnvironment<WoodpeckerCI>.Get("PREV_PIPELINE_AUTHOR");

    /// <summary>
    /// Previous pipeline author avatar URL from the git forge
    /// </summary>
    public string PreviousPipelineAuthorAvatar => IEnvironment<WoodpeckerCI>.Get("PREV_PIPELINE_AVATAR");

    #endregion

    #region Misc

    /// <summary>
    /// Path that source code gets cloned to for this project
    /// </summary>
    public string Workspace => IEnvironment<WoodpeckerCI>.Get("WORKSPACE");

    /// <summary>
    /// Name of forge
    /// </summary>
    /// <example>
    /// <c>bitbucket</c>, <c>bitbucket_dc</c>, <c>forgejo</c>, <c>gitea</c>, <c>github</c>, <c>gitlab</c>
    /// </example>
    public string ForgeName => IEnvironment<WoodpeckerCI>.Get("FORGE_TYPE");

    /// <summary>
    /// Root URL of configured forge
    /// </summary>
    /// <example><c>https://git.example.com</c></example>
    public string ForgeUrl => IEnvironment<WoodpeckerCI>.Get("FORGE_URL");

    /// <summary>
    /// Name of the CI system
    /// </summary>
    /// <example><c>woodpecker</c></example>
    public string SystemName => IEnvironment<WoodpeckerCI>.Get("SYSTEM_NAME");

    /// <summary>
    /// Link to CI system
    /// </summary>
    public string SystemUrl => IEnvironment<WoodpeckerCI>.Get("SYSTEM_URL");

    /// <summary>
    /// Hostname of CI server
    /// </summary>
    public string SystemHost => IEnvironment<WoodpeckerCI>.Get("SYSTEM_HOST");

    /// <summary>
    /// Version of the server
    /// </summary>
    [NoConvert] public string SystemVersion => IEnvironment<WoodpeckerCI>.Get("SYSTEM_VERSION");

    #endregion
}
