// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Nuke.Common.CI;
using Nuke.Common.CI.AppVeyor;
using Nuke.Common.CI.AzurePipelines;
using Nuke.Common.CI.ForgejoActions;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.CI.TeamCity;
using Nuke.Common.CI.WoodpeckerCI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using VerifyXunit;
using Xunit;

namespace Nuke.Common.Tests.CI;

public class ConfigurationGenerationTest
{
    private static readonly TestBuild s_testBuild = new();

    [Theory]
    [MemberData(nameof(GetAttributes))]
    public Task Test(string testName, ITestConfigurationGenerator attribute)
    {
        var relevantTargets = ExecutableTargetFactory
            .CreateAll(s_testBuild, x => x.Compile);

        var stream = new MemoryStream();
        attribute.Stream = new StreamWriter(stream, leaveOpen: true);
        attribute.Generate(relevantTargets);

        stream.Seek(offset: 0, SeekOrigin.Begin);
        var reader = new StreamReader(stream);
        var str = reader.ReadToEnd();

        return Verifier.Verify(str)
            .UseParameters(testName, attribute.GetType().BaseType.NotNull().Name);
    }

    public static IEnumerable<object[]> GetAttributes()
    {
        return TestBuild.GetAttributes(s_testBuild).Select(x => new object[] { x.TestName, x.Generator });
    }

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [AppVeyorSecret("GitHubToken", "encrypted-yaml")]
    [TeamCityToken("GitHubToken", "74928d76-46e8-45cc-ad22-6438915ac070")]
    public class TestBuild : NukeBuild
    {
        public static IEnumerable<(string TestName, IConfigurationGenerator Generator)> GetAttributes(TestBuild testBuild)
        {
            yield return
            (
                null,
                new TestTeamCityAttribute
                {
                    Build = testBuild,
                    Description = "description",
                    Version = "1.3.3.7",
                    NonEntryTargets = [nameof(Clean)],
                    VcsTriggeredTargets = [nameof(Test), nameof(Pack)],
                    ManuallyTriggeredTargets = [nameof(Publish)],
                    NightlyTriggeredTargets = [nameof(Publish)],
                    NightlyTriggerBranchFilters = ["nightly_branch_filter"],
                    VcsTriggerBranchFilters = ["vcs_branch_filter"],
                    ImportSecrets = ["GitHubToken", "ManualToken"]
                }
            );

            yield return
            (
                null,
                new TestAzurePipelinesAttribute(
                    AzurePipelinesImage.Ubuntu2204,
                    AzurePipelinesImage.Windows2025)
                {
                    Build = testBuild,
                    NonEntryTargets = [nameof(Clean)],
                    InvokedTargets = [nameof(Test)],
                    ExcludedTargets = [nameof(Pack)],
                    EnableAccessToken = true,
                    ImportVariableGroups = ["variable-group-1"],
                    ImportSecrets = [nameof(ApiKey)],
                    TriggerBatch = true,
                    TriggerBranchesInclude = ["included_branch"],
                    TriggerBranchesExclude = ["excluded_branch"],
                    TriggerPathsInclude = ["included_path"],
                    TriggerPathsExclude = ["excluded_path"],
                    TriggerTagsInclude = ["included_tags"],
                    TriggerTagsExclude = ["excluded_tags"],
                    Submodules = true,
                    LargeFileStorage = false,
                    Clean = true,
                    FetchDepth = 1
                }
            );

            yield return
            (
                null,
                new TestAppVeyorAttribute(
                    AppVeyorImage.UbuntuLatest,
                    AppVeyorImage.VisualStudio2022)
                {
                    Build = testBuild,
                    InvokedTargets = [nameof(Test)],
                    BranchesOnly = ["only_branch"],
                    BranchesExcept = ["except_branch"],
                    SkipTags = true,
                    SkipBranchesWithPullRequest = true,
                    Submodules = true,
                    Secrets = ["GitHubToken"]
                }
            );

            yield return
            (
                "simple-triggers",
                new TestGitHubActionsAttribute(
                    GitHubActionsImage.MacOsLatest,
                    GitHubActionsImage.UbuntuLatest,
                    GitHubActionsImage.WindowsLatest)
                {
                    Build = testBuild,
                    On = [GitHubActionsTrigger.Push, GitHubActionsTrigger.PullRequest],
                    InvokedTargets = [nameof(Test)],
                    ImportSecrets = [nameof(ApiKey)],
                    EnableGitHubToken = true,
                    WritePermissions = [GitHubActionsPermissions.Contents],
                    ReadPermissions = [GitHubActionsPermissions.Actions]
                }
            );

            yield return
            (
                "simple-triggers",
                new TestForgejoActionsAttribute(
                    CodebergRunners.Tiny,
                    CodebergRunners.SmallLazy)
                {
                    Build = testBuild,
                    On = [ForgejoActionsTrigger.Push, ForgejoActionsTrigger.PullRequest],
                    InvokedTargets = [nameof(Test)],
                    ImportSecrets = [nameof(ApiKey)],
                    ConcurrencyCancelInProgress = true
                }
            );

            yield return
            (
                "detailed-triggers",
                new TestGitHubActionsAttribute(
                    GitHubActionsImage.MacOsLatest,
                    GitHubActionsImage.UbuntuLatest,
                    GitHubActionsImage.WindowsLatest)
                {
                    Build = testBuild,
                    InvokedTargets = [nameof(Test)],
                    OnCronSchedule = "* 0 * * *",
                    OnPushBranches = ["push_branch"],
                    OnPushTags = ["push_tag/*"],
                    OnPushIncludePaths = ["push_include_path"],
                    OnPushExcludePaths = ["push_exclude_path"],
                    OnPullRequestBranches = ["pull_request_branch"],
                    OnPullRequestTags = ["pull_request_tag"],
                    OnPullRequestIncludePaths = ["pull_request_include_path"],
                    OnPullRequestExcludePaths = ["pull_request_exclude_path/**"],
                    OnWorkflowDispatchOptionalInputs = ["OptionalInput"],
                    OnWorkflowDispatchRequiredInputs = ["RequiredInput"],
                    PublishCondition = "success() || failure()",
                    Submodules = GitHubActionsSubmodules.Recursive,
                    Lfs = true,
                    FetchDepth = 2,
                    Progress = false,
                    Filter = "tree:0",
                    TimeoutMinutes = 30,
                    ConcurrencyCancelInProgress = true,
                    JobConcurrencyCancelInProgress = true,
                    JobConcurrencyGroup = "custom-job-group",
                    EnvironmentName = "environment-name",
                    EnvironmentUrl = "environment-url"
                }
            );

            yield return
            (
                "detailed-triggers",
                new TestForgejoActionsAttribute(
                    CodebergRunners.Tiny,
                    "my-windows-runner-that-isnt-real")
                {
                    Build = testBuild,
                    InvokedTargets = [nameof(Test)],
                    OnCronSchedule = "* 0 * * *",
                    OnPushBranches = ["push_branch"],
                    OnPushTags = ["push_tag/*"],
                    OnPushIncludePaths = ["push_include_path"],
                    OnPushExcludePaths = ["push_exclude_path"],
                    OnPullRequestBranches = ["pull_request_branch"],
                    OnPullRequestTags = ["pull_request_tag"],
                    OnPullRequestIncludePaths = ["pull_request_include_path"],
                    OnPullRequestExcludePaths = ["pull_request_exclude_path/**"],
                    OnWorkflowDispatchOptionalInputs = ["OptionalInput"],
                    OnWorkflowDispatchRequiredInputs = ["RequiredInput"],
                    PublishCondition = "success() || failure()",
                    Submodules = ForgejoActionsSubmodules.Recursive,
                    Lfs = true,
                    FetchDepth = 2,
                    Progress = false,
                    Filter = "tree:0",
                    TimeoutMinutes = 30,
                    ConcurrencyCancelInProgress = true,
                    JobConcurrencyCancelInProgress = true,
                    JobConcurrencyGroup = "custom-job-group",
                    EnvironmentName = "environment-name",
                    EnvironmentUrl = "environment-url"
                }
            );

            yield return
            (
                null,
                new TestSpaceAutomationAttribute("Name", "mcr.microsoft.com/dotnet/sdk:5.0")
                {
                    Build = testBuild,
                    InvokedTargets = [nameof(Test)],
                    VolumeSize = "10.gb",
                    ResourcesCpu = "1.cpu",
                    ResourcesMemory = "2000.mb",
                    OnPush = true,
                    OnPushBranchIncludes = ["refs/heads/include"],
                    OnPushBranchExcludes = ["refs/heads/exclude"],
                    OnPushBranchRegexIncludes = [@"\binclude\b"],
                    OnPushBranchRegexExcludes = [@"\bexclude\b"],
                    OnPushPathIncludes = ["include-path"],
                    OnPushPathExcludes = ["exclude-path"],
                    OnCronSchedule = "* 0 * * *",
                    ImportSecrets = ["GitHubToken"],
                    TimeoutInMinutes = 15
                }
            );

            yield return (
                null,
                new TestWoodpeckerCIAttribute
                {
                    OnlyOnBranches = [default, "feature/*"],
                    Triggers = [WoodpeckerCIEvent.Push, WoodpeckerCIEvent.PullRequest],
                    InvokedTargets = [nameof(Test), nameof(Publish)],
                    MinimalFetch = false
                }
            );

            yield return
            (
                null,
                new TestGitLabCIAttribute
                {
                    Build = testBuild,
                    InvokedTargets = [nameof(Publish)],
                    UploadProducedArtifacts = true,
                    ExcludedArtifacts = ["output/packages/*.nupkg"],
                    OnlyOnPushesToBranches = [default]
                }
            );
        }

        public AbsolutePath SourceDirectory => RootDirectory / "src";

        public Target Clean => _ => _
            .Before(Restore);

        [Parameter] public readonly bool IgnoreFailedSources;

        public Target Restore => _ => _
            .Produces(SourceDirectory / "*/obj/**");

        [Parameter("Configuration for compilation")]
        public readonly Configuration Configuration = Configuration.Debug;

        [Parameter] public readonly string[] StringArray = ["first", "second"];
        [Parameter] public readonly int[] IntegerArray = [1, 2];
        [Parameter] public readonly Configuration[] ConfigurationArray = [Configuration.Debug, Configuration.Release];

        public AbsolutePath OutputDirectory => RootDirectory / "output";

        public Target Compile => _ => _
            .DependsOn(Restore)
            .Produces(SourceDirectory / "*/bin/**");

        public AbsolutePath PackageDirectory => OutputDirectory / "packages";

        public Target Pack => _ => _
            .DependsOn(Compile)
            .Consumes(Restore, Compile)
            .Produces(PackageDirectory / "*.nupkg");

        public AbsolutePath TestResultDirectory => OutputDirectory / "test-results";

        public Target Test => _ => _
            .DependsOn(Compile)
            .Produces(TestResultDirectory / "*.trx")
            .Produces(TestResultDirectory / "*.xml")
            .Partition(2);

        public string CoverageReportArchive => OutputDirectory / "coverage-report.zip";

        public Target Coverage => _ => _
            .DependsOn(Test)
            .TriggeredBy(Test)
            .Consumes(Test)
            .Produces(CoverageReportArchive);

        [Parameter("NuGet Api Key")] [Secret] public readonly string ApiKey;

        [Parameter("NuGet Source for Packages")]
        public readonly string Source = "https://api.nuget.org/v3/index.json";

        public Target Publish => _ => _
            .DependsOn(Clean, Test, Pack)
            .Consumes(Pack)
            .Requires(() => ApiKey);

        public Target Announce => _ => _
            .TriggeredBy(Publish)
            .AssuredAfterFailure();
    }

    [TypeConverter(typeof(TypeConverter<Configuration>))]
    public class Configuration : Enumeration
    {
        public static Configuration Debug = new() { Value = nameof(Debug) };
        public static Configuration Release = new() { Value = nameof(Release) };

        public static implicit operator string(Configuration configuration)
        {
            return configuration.Value;
        }
    }
}
