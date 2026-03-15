// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Build.CICD;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.AppVeyor;
// [PublicAPI]
// [Headers("Accept: application/json")]
// public interface IAppVeyorRestClient
// {
//     [Post("/api/build/messages")]
//     Task WriteMessage(AppVeyorMessageCategory category, string message, string details = "");
// }
//

[PublicAPI]
public enum AppVeyorMessageCategory
{
    Information,
    Warning,
    Error
}

/// <summary>
/// Interface according to the <a href="https://www.appveyor.com/docs/environment-variables/">official website</a>.
/// </summary>
[PublicAPI]
[CI]
[ExcludeFromCodeCoverage]
public partial class AppVeyor : Host, IBuildServer, IEnvironment<AppVeyor>
{
    public static string EnvironmentVariablePrefix => "APPVEYOR";

    public new static AppVeyor Instance => Host.Instance as AppVeyor;

    private static int s_messageLimit = 500;

    [UsedImplicitly]
    internal static bool IsRunningAppVeyor => IEnvironment<AppVeyor>.Has();

    private readonly Lazy<Tool> _cli = Lazy.Create(() => IsRunningAppVeyor ? ToolResolver.GetEnvironmentOrPathTool("appveyor") : null);
    private int _messageCount;

    internal AppVeyor()
    {
    }

    string IBuildServer.Branch => RepositoryBranch;
    string IBuildServer.Commit => RepositoryCommitSha;

    public Tool Cli => _cli.Value;

    public string Url => IEnvironment<AppVeyor>.Get("URL");
    public string ApiUrl => IEnvironment<AppVeyor>.Get("API_URL");
    public string AccountName => IEnvironment<AppVeyor>.Get("ACCOUNT_NAME");
    public long ProjectId => IEnvironment<AppVeyor>.Get<long>("PROJECT_ID");
    public string ProjectName => IEnvironment<AppVeyor>.Get("PROJECT_NAME");
    public string ProjectSlug => IEnvironment<AppVeyor>.Get("PROJECT_SLUG");
    public string BuildFolder => IEnvironment<AppVeyor>.Get("BUILD_FOLDER");
    public long BuildId => IEnvironment<AppVeyor>.Get<long>("BUILD_ID");
    public long BuildNumber => IEnvironment<AppVeyor>.Get<long>("BUILD_NUMBER");
    public string BuildVersion => IEnvironment<AppVeyor>.Get("BUILD_VERSION");
    public string BuildWorkerImage => IEnvironment<AppVeyor>.Get("BUILD_WORKER_IMAGE");
    [CanBeNull] public long? PullRequestNumber => IEnvironment<AppVeyor>.Get<long?>("PULL_REQUEST_NUMBER");
    [CanBeNull] public string PullRequestTitle => IEnvironment<AppVeyor>.Get("PULL_REQUEST_TITLE");
    [CanBeNull] public string PullRequestHeadRepositoryName => IEnvironment<AppVeyor>.Get("PULL_REQUEST_HEAD_REPO_NAME");
    [CanBeNull] public string PullRequestHeadRepositoryBranch => IEnvironment<AppVeyor>.Get("PULL_REQUEST_HEAD_REPO_BRANCH");
    [CanBeNull] public string PullRequestHeadCommit => IEnvironment<AppVeyor>.Get("PULL_REQUEST_HEAD_COMMIT");
    public string JobId => IEnvironment<AppVeyor>.Get("JOB_ID");
    [CanBeNull] public string JobName => IEnvironment<AppVeyor>.Get("JOB_NAME");
    public long JobNumber => IEnvironment<AppVeyor>.Get<long>("JOB_NUMBER");
    public string RepositoryProvider => IEnvironment<AppVeyor>.Get("REPO_PROVIDER");
    public string RepositoryScm => IEnvironment<AppVeyor>.Get("REPO_SCM");
    public string RepositoryName => IEnvironment<AppVeyor>.Get("REPO_NAME");
    public string RepositoryBranch => IEnvironment<AppVeyor>.Get("REPO_BRANCH");
    public bool RepositoryTag => IEnvironment<AppVeyor>.Get<bool>("REPO_TAG");
    [CanBeNull] public string RepositoryTagName => IEnvironment<AppVeyor>.Get("REPO_TAG_NAME");
    public string RepositoryCommitSha => IEnvironment<AppVeyor>.Get("REPO_COMMIT");
    public string RepositoryCommitAuthor => IEnvironment<AppVeyor>.Get("REPO_COMMIT_AUTHOR");
    public string RepositoryCommitAuthorEmail => IEnvironment<AppVeyor>.Get("REPO_COMMIT_AUTHOR_EMAIL");
    public DateTime RepositoryCommitTimestamp => IEnvironment<AppVeyor>.Get<DateTime>("REPO_COMMIT_TIMESTAMP");
    public string RepositoryCommitMessage => IEnvironment<AppVeyor>.Get("REPO_COMMIT_MESSAGE");
    [CanBeNull] public string RepositoryCommitMessageExtended => IEnvironment<AppVeyor>.Get("REPO_COMMIT_MESSAGE_EXTENDED");
    public bool ScheduledBuild => IEnvironment<AppVeyor>.Get<bool>("SCHEDULED_BUILD");
    public bool ForcedBuild => IEnvironment<AppVeyor>.Get<bool>("FORCED_BUILD");
    public bool Rebuild => IEnvironment<AppVeyor>.Get<bool>("RE_BUILD");
    [CanBeNull] public string Platform => EnvironmentInfo.GetVariable("PLATFORM");
    [CanBeNull] public string Configuration => EnvironmentInfo.GetVariable("CONFIGURATION");

    public void UpdateBuildVersion(string version)
    {
        Cli?.Invoke($"UpdateBuild -Version {version.DoubleQuote()}");
        EnvironmentInfo.SetVariable("APPVEYOR_BUILD_VERSION", version);
    }

    public void PushArtifact(string path, string name = null)
    {
        name ??= Path.GetFileName(path);
        Cli?.Invoke($"PushArtifact {path} -FileName {name}");
    }

    public void WriteInformation(string message, string details = null)
    {
        WriteMessage(AppVeyorMessageCategory.Information, message, details);
    }

    public void WriteWarning(string message, string details = null)
    {
        WriteMessage(AppVeyorMessageCategory.Warning, message, details);
    }

    public void WriteError(string message, string details = null)
    {
        WriteMessage(AppVeyorMessageCategory.Error, message, details);
    }

    private void WriteMessage(AppVeyorMessageCategory category, string message, string details)
    {
        if (_messageCount == s_messageLimit)
        {
            Theme.WriteWarning(
                $"AppVeyor has a default limit of {s_messageLimit} messages. " +
                "If you're getting an exception from 'appveyor.exe' after this message, " +
                "contact https://appveyor.com/support to resolve this issue for your account.");
        }

        _messageCount++;
        Cli?.Invoke($"AddMessage {message.DoubleQuote()} -Category {category} -Details {details.DoubleQuote()}",
            logInvocation: false,
            logOutput: false);
    }
}
