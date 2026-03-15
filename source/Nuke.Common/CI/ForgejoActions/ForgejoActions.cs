// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nuke.Build.CICD;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.ForgejoActions;

/// <summary>
/// Interface according to the <a href="https://forgejo.org/docs/next/user/actions/reference/#env-1">official website</a>.
/// </summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
public class ForgejoActions : Host, IBuildServer, IEnvironment<ForgejoActions>
{
    public static string EnvironmentVariablePrefix => "FORGEJO";

    [UsedImplicitly]
    internal static bool IsRunningForgejoActions => IEnvironment<ForgejoActions>.Has("ACTIONS");

    public new static ForgejoActions Instance => Host.Instance as ForgejoActions;

    private readonly Lazy<JObject> _eventContext;

    internal ForgejoActions()
    {
        _eventContext = Lazy.Create(() =>
            JsonConvert.DeserializeObject<JObject>(File.ReadAllText(EventPath))
        );
    }

    string IBuildServer.Branch => Ref;
    string IBuildServer.Commit => Sha;

    ///<summary>The numerical id of the current step.</summary>
    public long Action => IEnvironment<ForgejoActions>.Get<long>("ACTION");

    /// <summary>
    /// When evaluated while running a <c>composite</c> action (i.e. <c>using: "composite"</c>), the path where an action files are located.
    /// </summary>
    public string ActionPath => IEnvironment<ForgejoActions>.Get("ACTION_PATH");

    /// <summary>
    /// For a step executing an action, this is the owner and repository name of the action (e.g. <c>actions/checkout</c>).
    /// </summary>
    public string ActionRepository => IEnvironment<ForgejoActions>.Get("ACTION_REPOSITORY");

    /// <summary>
    /// The name of the user that triggered the <c>workflow</c>.
    /// </summary>
    public string Actor => IEnvironment<ForgejoActions>.Get("ACTOR");

    /// <summary>
    /// The API endpoint of the Forgejo instance running the workflow (e.g. <c>https://code.forgejo.org/api/v1</c>).
    /// </summary>
    public string ApiUrl => IEnvironment<ForgejoActions>.Get("API_URL");

    /// <summary>
    /// The name of the base branch of the pull request (e.g. main).
    /// Only defined when a workflow runs because of a <c>pull_request</c> or <c>pull_request_target</c> event.
    /// </summary>
    [CanBeNull] public string BaseRef => IEnvironment<ForgejoActions>.Get("BASE_REF");

    /// <summary>
    /// Always set to true.
    /// </summary>
    public bool Ci => EnvironmentInfo.GetVariable<bool>("CI");

    /// <summary>
    /// The name of the base branch of the pull request (e.g. my-feature).
    /// Only defined when a workflow runs because of a <c>pull_request</c> or <c>pull_request_target</c> event.
    /// </summary>
    [CanBeNull] public string HeadRef => IEnvironment<ForgejoActions>.Get("HEAD_REF");

    /// <summary>
    /// The path on the runner to the file that sets variables from workflow commands. This file is unique to the current step and changes for each step in a job.
    /// </summary>
    public string Env => IEnvironment<ForgejoActions>.Get("ENV");

    /// <summary>
    /// The name of the event that triggered the workflow (e.g. <c>push</c>).
    /// </summary>
    public string EventName => IEnvironment<ForgejoActions>.Get("EVENT_NAME");

    ///<summary>The path to the file on the Forgejo runner that contains the full event webhook payload.</summary>
    public string EventPath => IEnvironment<ForgejoActions>.Get("EVENT_PATH");

    /// <summary>
    /// The <c>job_id</c> of the current job.
    /// </summary>
    public string Job => IEnvironment<ForgejoActions>.Get("JOB");

    /// <summary>
    /// The path on the runner to the file that sets the current step’s outputs. This file is unique to the current step.
    /// </summary>
    public string Output => IEnvironment<ForgejoActions>.Get("OUTPUT");

    /// <summary>
    ///	The path on the runner to the file that sets the PATH environment variable. This file is unique to the current step.
    /// </summary>
    public string Path => IEnvironment<ForgejoActions>.Get("PATH");

    ///<summary>
    /// The fully formed git reference (i.e. starting with <c>refs/</c>) associated with the event that triggered the workflow, if any (e.g. <c>refs/heads/main</c>).
    /// </summary>
    public string Ref => IEnvironment<ForgejoActions>.Get("REF");

    /// <summary>
    /// The short git reference name of the branch or tag associated with the workflow, if any (e.g. <c>main</c>).
    /// </summary>
    public string RefName => IEnvironment<ForgejoActions>.Get("REF_NAME");

    /// <summary>
    /// The owner and repository name (e.g. <c>forgejo/docs</c>).
    /// </summary>
    public string Repository => IEnvironment<ForgejoActions>.Get("REPOSITORY");

    /// <summary>
    /// The repository owner’s name (e.g. <c>forgejo</c>)
    /// </summary>
    public string RepositoryOwner => IEnvironment<ForgejoActions>.Get("REPOSITORY_OWNER");

    /// <summary>
    ///	Attempt number for this run, beginning at 1 and incrementing when the job is re-run.
    /// </summary>
    public long RunAttempt => IEnvironment<ForgejoActions>.Get<long>("RUN_ATTEMPT");

    /// <summary>
    /// A unique id for the current workflow run in the repository of the workflow.
    /// </summary>
    public long RunNumber => IEnvironment<ForgejoActions>.Get<long>("RUN_NUMBER");

    /// <summary>
    /// A unique id for the current workflow run in the Forgejo instance.
    /// </summary>
    public long RunId => IEnvironment<ForgejoActions>.Get<long>("RUN_ID");

    /// <summary>
    /// The URL of the Forgejo instance running the workflow (e.g. https://code.forgejo.org)
    /// </summary>
    public string ServerUrl => IEnvironment<ForgejoActions>.Get("SERVER_URL");

    ///<summary>The commit SHA that triggered the workflow. The value of this commit SHA depends on the event that triggered the workflow. For example, <c>ffac537e6cbbf934b08745a378932722df287a53</c>.</summary>
    public string Sha => IEnvironment<ForgejoActions>.Get("SHA");

    /// <summary>
    /// The unique authentication token automatically created for duration of the workflow.
    /// </summary>
    public string Token => IEnvironment<ForgejoActions>.Get("TOKEN");

    /// <summary>
    /// The default working directory on the runner for steps, and the default location of the repository when using the checkout action.
    /// </summary>
    public string Workspace => IEnvironment<ForgejoActions>.Get("WORKSPACE");

    /// <summary>
    /// Ref path to the workflow, for example, <c>owner/example-respository/.forgejo/workflows/test-workflow.yaml@refs/heads/main</c>
    /// </summary>
    public string WorkflowRef => IEnvironment<ForgejoActions>.Get("WORKFLOW_REF");
}
