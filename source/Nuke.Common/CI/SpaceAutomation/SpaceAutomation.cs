// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;

namespace Nuke.Common.CI.SpaceAutomation;

/// <summary>
/// Interface according to the <a href="https://www.jetbrains.com/help/space/automation.html">official website</a>.
/// </summary>
[PublicAPI]
[CI]
[ExcludeFromCodeCoverage]
public partial class SpaceAutomation : Host, IBuildServer, IEnvironment<SpaceAutomation>
{
    public static string EnvironmentVariablePrefix => "JB_SPACE";

    public new static SpaceAutomation Instance => Host.Instance as SpaceAutomation;

    [UsedImplicitly]
    internal static bool IsRunningSpaceAutomation => IEnvironment<SpaceAutomation>.Has("PROJECT_KEY");

    string IBuildServer.Branch => GitBranch;
    string IBuildServer.Commit => GitRevision;

    public string ProjectKey => IEnvironment<SpaceAutomation>.Get("PROJECT_KEY");
    public string ProjectId => IEnvironment<SpaceAutomation>.Get("PROJECT_ID");
    public string ApiUrl => IEnvironment<SpaceAutomation>.Get("API_URL");
    public string ClientId => IEnvironment<SpaceAutomation>.Get("CLIENT_ID");
    public string ClientSecret => IEnvironment<SpaceAutomation>.Get("CLIENT_SECRET");
    public string ClientToken => IEnvironment<SpaceAutomation>.Get("CLIENT_TOKEN");
    public string ExecutionNumber => IEnvironment<SpaceAutomation>.Get("EXECUTION_NUMBER");
    public string ExecutionId => IEnvironment<SpaceAutomation>.Get("EXECUTION_ID");
    public string ExecutionUrl => IEnvironment<SpaceAutomation>.Get("EXECUTION_URL");
    public string RepositoryName => IEnvironment<SpaceAutomation>.Get("GIT_REPOSITORY_NAME");
    public string GitRevision => IEnvironment<SpaceAutomation>.Get("GIT_REVISION");
    public string GitBranch => IEnvironment<SpaceAutomation>.Get("GIT_BRANCH");
    public string StepDataPath => IEnvironment<SpaceAutomation>.Get("STEP_DATA_PATH");
    public string WorkDirPath => IEnvironment<SpaceAutomation>.Get("WORK_DIR_PATH");
    public string FileSharePath => IEnvironment<SpaceAutomation>.Get("FILE_SHARE_PATH");
}
