// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Build.CICD;

namespace Nuke.Common.CI.Bitbucket;

/// <summary>
/// Interface according to the <a href="https://support.atlassian.com/bitbucket-cloud/docs/variables-and-secrets/">official website</a>.
/// </summary>
[PublicAPI]
[CI]
[ExcludeFromCodeCoverage]
public class Bitbucket : Host, IBuildServer, IEnvironment<Bitbucket>
{
    public static string EnvironmentVariablePrefix => "BITBUCKET";

    public new static Bitbucket Instance => Host.Instance as Bitbucket;

    [UsedImplicitly]
    internal static bool IsRunningBitbucket => IEnvironment<Bitbucket>.Has("PIPELINE_UUID");

    internal Bitbucket()
    {
    }

    string IBuildServer.Branch => Branch;
    string IBuildServer.Commit => Commit;

    /// <summary>
    /// The unique identifier for a build. It increments with each build and can be used to create unique artifact names.
    /// </summary>
    public long BuildNumber => IEnvironment<Bitbucket>.Get<long>("BUILD_NUMBER");

    /// <summary>
    /// The absolute path of the directory that the repository is cloned into within the Docker container.
    /// </summary>
    public string CloneDirectory => IEnvironment<Bitbucket>.Get("CLONE_DIR");

    /// <summary>
    /// The commit hash of a commit that kicked off the build.
    /// </summary>
    public string Commit => IEnvironment<Bitbucket>.Get("COMMIT");

    /// <summary>
    /// The name of the workspace in which the repository lives.
    /// </summary>
    public string Workspace => IEnvironment<Bitbucket>.Get("WORKSPACE");

    /// <summary>
    /// The URL-friendly version of a repository name.
    /// </summary>
    public string RepositorySlug => IEnvironment<Bitbucket>.Get("REPO_SLUG");

    /// <summary>
    /// The UUID of the repository.
    /// </summary>
    public string RepositoryUuid => IEnvironment<Bitbucket>.Get("REPO_UUID");

    /// <summary>
    /// The full name of the repository (everything that comes after http://bitbucket.org/).
    /// </summary>
    public string RepositoryFullName => IEnvironment<Bitbucket>.Get("REPO_FULL_NAME");

    /// <summary>
    /// The source branch. This value is only available on branches. Not available for builds against tags, or custom pipelines.
    /// </summary>
    public string Branch => IEnvironment<Bitbucket>.Get("BRANCH");

    /// <summary>
    /// The tag of a commit that kicked off the build. This value is only available on tags. Not available for builds against branches.
    /// </summary>
    public string Tag => IEnvironment<Bitbucket>.Get("TAG");

    /// <summary>
    /// For use with Mercurial projects.
    /// </summary>
    public string Bookmark => IEnvironment<Bitbucket>.Get("BOOKMARK");

    /// <summary>
    /// Zero-based index of the current step in the group, for example: 0, 1, 2, … Not available outside a parallel step.
    /// </summary>
    public long ParallelStep => IEnvironment<Bitbucket>.Get<long>("PARALLEL_STEP");

    /// <summary>
    /// Total number of steps in the group, for example: 5. Not available outside a parallel step.
    /// </summary>
    public long ParallelStepCount => IEnvironment<Bitbucket>.Get<long>("PARALLEL_STEP_COUNT");

    /// <summary>
    /// The pull request ID. Only available on a pull request triggered build.
    /// </summary>
    public long PullRequestId => IEnvironment<Bitbucket>.Get<long>("PR_ID");

    /// <summary>
    /// The pull request destination branch (used in combination with BITBUCKET_BRANCH). Only available on a pull request triggered build.
    /// </summary>
    public string PullRequestDestinationBranch => IEnvironment<Bitbucket>.Get("PR_DESTINATION_BRANCH");

    /// <summary>
    /// The URL for the origin, for example: http://bitbucket.org/&lt;account&gt;/&lt;repo&gt;
    /// </summary>
    public string GitHttpOrigin => IEnvironment<Bitbucket>.Get("GIT_HTTP_ORIGIN");

    /// <summary>
    /// Your SSH origin, for example: git@bitbucket.org:/&lt;account&gt;/&lt;repo&gt;.git
    /// </summary>
    public string GitSshOrigin => IEnvironment<Bitbucket>.Get("GIT_SSH_ORIGIN");

    /// <summary>
    /// The exit code of a step, can be used in after-script sections. Values can be 0 (success) or 1 (failed)
    /// </summary>
    public string ExitCode => IEnvironment<Bitbucket>.Get("EXIT_CODE");

    /// <summary>
    /// The UUID of the step.
    /// </summary>
    public string StepUuid => IEnvironment<Bitbucket>.Get("STEP_UUID");

    /// <summary>
    ///  The UUID of the pipeline.
    /// </summary>
    public string PipelineUuid => IEnvironment<Bitbucket>.Get("PIPELINE_UUID");

    /// <summary>
    /// The URL friendly version of the environment name.
    /// </summary>
    public string DeploymentEnvironment => IEnvironment<Bitbucket>.Get("DEPLOYMENT_ENVIRONMENT");

    /// <summary>
    /// The UUID of the environment to access environments via the REST API.
    /// </summary>
    public string DeploymentEnvironmentUuid => IEnvironment<Bitbucket>.Get("DEPLOYMENT_ENVIRONMENT_UUID");

    /// <summary>
    /// The key of the project the current pipeline belongs to.
    /// </summary>
    public string ProjectKey => IEnvironment<Bitbucket>.Get("PROJECT_KEY");

    /// <summary>
    /// The UUID of the project the current pipeline belongs to.
    /// </summary>
    public string ProjectUuid => IEnvironment<Bitbucket>.Get("PROJECT_UUID");

    /// <summary>
    /// The person who kicked off the build ( by doing a push, merge etc), and for scheduled builds, the uuid of the pipelines user.
    /// </summary>
    public string StepTriggererUuid => IEnvironment<Bitbucket>.Get("STEP_TRIGGERER_UUID");

    /// <summary>
    /// The 'ID Token' generated by the Bitbucket OIDC provider that identifies the step. This token can be used to access resource servers, such as AWS and GCP without using credentials.
    /// </summary>
    public string StepOidcToken => IEnvironment<Bitbucket>.Get("STEP_OIDC_TOKEN");
}
