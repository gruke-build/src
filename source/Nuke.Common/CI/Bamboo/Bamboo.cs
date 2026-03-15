// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Nuke.Common.CI.Bamboo;

/// <summary>
/// Interface according to the <a href="https://confluence.atlassian.com/bamboo/bamboo-variables-289277087.html">official website</a>.
/// </summary>
[PublicAPI]
[CI]
[ExcludeFromCodeCoverage]
public class Bamboo : Host, IBuildServer, IEnvironment<Bamboo>
{
    public static string EnvironmentVariablePrefix => "bamboo";

    public new static Bamboo Instance => Host.Instance as Bamboo;

    [UsedImplicitly]
    internal static bool IsRunningBamboo => EnvironmentInfo.HasVariable("bamboo_planKey");

    internal Bamboo()
    {
    }

    string IBuildServer.Branch => null;
    string IBuildServer.Commit => null;

    public long AgentId => IEnvironment<Bamboo>.Get<long>("agentId");
    public string AgentWorkingDirectory => IEnvironment<Bamboo>.Get("agentWorkingDirectory");
    public string AgentHome => EnvironmentInfo.GetVariable("BAMBOO_AGENT_HOME");
    public string BuildKey => IEnvironment<Bamboo>.Get("buildKey");
    public long BuildNumber => IEnvironment<Bamboo>.Get<long>("buildNumber");
    public string BuildPlanName => IEnvironment<Bamboo>.Get("buildPlanName");
    public string BuildResultsKey => IEnvironment<Bamboo>.Get("buildResultKey");
    public string BuildResultsUrl => IEnvironment<Bamboo>.Get("buildResultsUrl");
    public DateTime BuildTimeStamp => IEnvironment<Bamboo>.Get<DateTime>("buildTimeStamp");
    public string BuildWorkingDirectory => IEnvironment<Bamboo>.Get("build_working_directory");
    public bool BuildFailed => IEnvironment<Bamboo>.Get<bool>("buildFailed");
    public string PlanKey => IEnvironment<Bamboo>.Get("planKey");
    public string ShortPlanKey => IEnvironment<Bamboo>.Get("shortPlanKey");
    public string PlanName => IEnvironment<Bamboo>.Get("planName");
    public string ShortPlanName => IEnvironment<Bamboo>.Get("shortPlanName");
    public string PlanStorageTag => IEnvironment<Bamboo>.Get("plan_storageTag");
    public string PlanResultsUrl => IEnvironment<Bamboo>.Get("resultsUrl");
    public string ShortJobKey => IEnvironment<Bamboo>.Get("shortJobKey");
    public string ShortJobName => IEnvironment<Bamboo>.Get("shortJobName");
}
