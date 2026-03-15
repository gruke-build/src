// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nuke.Build.CICD;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;
using Nuke.Common.Utilities.Net;
using Serilog;

namespace Nuke.Common.CI.GitHubActions;

/// <summary>
/// Interface according to the <a href="https://docs.github.com/en/actions/configuring-and-managing-workflows/using-environment-variables">official website</a>.
/// </summary>
[PublicAPI]
[CI]
[ExcludeFromCodeCoverage]
public partial class GitHubActions : Host, IBuildServer, IEnvironment<GitHubActions>
{
    public static string EnvironmentVariablePrefix => "GITHUB";

    [UsedImplicitly]
    internal static bool IsRunningGitHubActions => IEnvironment<GitHubActions>.Has("ACTIONS");

    public new static GitHubActions Instance => Host.Instance as GitHubActions;

    private readonly Lazy<JObject> _eventContext;

    internal GitHubActions()
    {
        _eventContext = Lazy.Create(() =>
        {
            var content = File.ReadAllText(EventPath);
            return JsonConvert.DeserializeObject<JObject>(content);
        });
    }

    string IBuildServer.Branch => Ref;
    string IBuildServer.Commit => Sha;

    /// <summary>
    /// Always set to <c>true</c>.
    /// </summary>
    public bool Ci => EnvironmentInfo.GetVariable<bool>("CI");

    ///<summary>The path to the GitHub home directory used to store user data. For example, <c>/github/home</c>.</summary>
    public string Home => IEnvironment<GitHubActions>.Get("HOME");

    ///<summary>The name of the workflow.</summary>
    public string Workflow => IEnvironment<GitHubActions>.Get("WORKFLOW");

    ///<summary>The name of the action.</summary>
    public string Action => IEnvironment<GitHubActions>.Get("ACTION");

    ///<summary>The name of the person or app that initiated the workflow. For example, <c>octocat</c>.</summary>
    public string Actor => IEnvironment<GitHubActions>.Get("ACTOR");

    ///<summary>The owner and repository name. For example, <c>octocat/Hello-World</c>.</summary>
    public string Repository => IEnvironment<GitHubActions>.Get("REPOSITORY");

    ///<summary>The owner name. For example, <c>octocat</c>.</summary>
    public string RepositoryOwner => IEnvironment<GitHubActions>.Get("REPOSITORY_OWNER");

    ///<summary>The name of the webhook event that triggered the workflow.</summary>
    public string EventName => IEnvironment<GitHubActions>.Get("EVENT_NAME");

    ///<summary>The path of the file with the complete webhook event payload. For example, <c>/github/workflow/event.json</c>.</summary>
    public string EventPath => IEnvironment<GitHubActions>.Get("EVENT_PATH");

    ///<summary>The GitHub workspace directory path. The workspace directory contains a subdirectory with a copy of your repository if your workflow uses the <c>actions/checkout</c> action. If you don't use the <c>actions/checkout</c> action, the directory will be empty. For example, <c>/home/runner/work/my-repo-name/my-repo-name</c>.</summary>
    public string Workspace => IEnvironment<GitHubActions>.Get("WORKSPACE");

    ///<summary>The commit SHA that triggered the workflow. For example, <c>ffac537e6cbbf934b08745a378932722df287a53</c>.</summary>
    public string Sha => IEnvironment<GitHubActions>.Get("SHA");

    ///<summary>The fully-formed ref of the branch or tag that triggered the workflow run. For workflows triggered by <c>push</c>, this is the branch or tag ref that was pushed. For workflows triggered by <c>pull_request</c>, this is the pull request merge branch. For workflows triggered by <c>release</c>, this is the release tag created. For other triggers, this is the branch or tag ref that triggered the workflow run. This is only set if a branch or tag is available for the event type. The ref given is fully-formed, meaning that for branches the format is <c>refs/heads/&lt;branch_name&gt;</c>, for pull requests it is <c>refs/pull/&lt;pr_number&gt;/merge</c>, and for tags it is <c>refs/tags/&lt;tag_name&gt;</c>. For example, <c>refs/heads/feature-branch-1</c>.</summary>
    public string Ref => IEnvironment<GitHubActions>.Get("REF");

    ///<summary>The short ref name of the branch or tag that triggered the workflow run. This value matches the branch or tag name shown on GitHub. For example, <c>feature-branch-1</c>.</summary>
    public string RefName => IEnvironment<GitHubActions>.Get("REF_NAME");

    ///<summary>Only set for forked repositories. The branch of the head repository.</summary>
    public string HeadRef => IEnvironment<GitHubActions>.Get("HEAD_REF");

    ///<summary>Only set for forked repositories. The branch of the base repository.</summary>
    public string BaseRef => IEnvironment<GitHubActions>.Get("BASE_REF");

    ///<summary>The type of ref that triggered the workflow run. Valid values are <c>branch</c> or <c>tag</c>.</summary>
    public string RefType => IEnvironment<GitHubActions>.Get("REF_TYPE");

    public long RunAttempt => IEnvironment<GitHubActions>.Get<long>("RUN_ATTEMPT");
    public long RunNumber => IEnvironment<GitHubActions>.Get<long>("_RUN_NUMBER");
    public long RunId => IEnvironment<GitHubActions>.Get<long>("RUN_ID");
    public string ServerUrl => IEnvironment<GitHubActions>.Get("SERVER_URL");
    public string Job => IEnvironment<GitHubActions>.Get("JOB");

    // https://github.com/actions/toolkit/tree/master/packages/core/src

    public JObject GitHubEvent => _eventContext.Value;
    public bool IsPullRequest => EventName == "pull_request";
    public int? PullRequestNumber => GitHubEvent.GetPropertyValueOrNull<int>("number");
    [CanBeNull] public string PullRequestAction => GitHubEvent.GetPropertyStringValueOrNull("action");

    public AbsolutePath StepSummaryFile => IEnvironment<GitHubActions>.Get("STEP_SUMMARY");

    public void Group(string group)
    {
        WriteCommand("group", group);
    }

    public void EndGroup(string group)
    {
        WriteCommand("endgroup", group);
    }

    public void WriteDebug(string message)
    {
        WriteCommand("debug", message);
    }

    public void WriteWarning(string message)
    {
        WriteCommand("warning", message);
    }

    public void WriteError(string message)
    {
        WriteCommand("error", message);
    }

    public void WriteCommand(
        string command,
        string message = null,
        Configure<Dictionary<string, object>> dictionaryConfigurator = null)
    {
        var parameters = dictionaryConfigurator.InvokeSafe(new Dictionary<string, object>())
            .Select(x => $"{x.Key}={EscapeProperty(x.Value.ToString())}")
            .JoinCommaSpace();

        Console.WriteLine(parameters.IsNullOrEmpty()
            ? $"::{command}::{EscapeData(message)}"
            : $"::{command} {parameters}::{EscapeData(message)}");
    }

    private string EscapeData([CanBeNull] string data)
    {
        return data?
            .Replace("%", "%25")
            .Replace("\r", "%0D")
            .Replace("\n", "%0A");
    }

    private string EscapeProperty(string value)
    {
        return value
            .Replace("%", "%25")
            .Replace("\r", "%0D")
            .Replace("\n", "%0A")
            .Replace(":", "%3A")
            .Replace(",", "%2C");
    }
}
