// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.CI.ForgejoActions.Configuration;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace Nuke.Common.CI.ForgejoActions;

/// <summary>
/// Interface according to the <a href="https://forgejo.org/docs/next/user/actions/reference/#workflow-syntax">official website</a>.
/// </summary>
[PublicAPI]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ForgejoActionsAttribute : ConfigurationAttributeBase
{
    private readonly string _name;
    private readonly string[] _runners;
    private ForgejoActionsSubmodules? _submodules;
    private bool? _lfs;
    private uint? _fetchDepth;
    private bool? _progress;
    private string _filter;

    public ForgejoActionsAttribute(
        string name,
        string runner,
        params string[] runners)
    {
        _name = name.Replace(oldChar: ' ', newChar: '_');
        _runners = runners.Prepend(runner).ToArray();
    }

    public override string IdPostfix => _name;
    public override Type HostType => typeof(ForgejoActions);
    public override AbsolutePath ConfigurationFile => Build.RootDirectory / ".forgejo" / "workflows" / $"{_name}.yml";
    public override IEnumerable<AbsolutePath> GeneratedFiles => [ConfigurationFile];

    public override IEnumerable<string> RelevantTargetNames => InvokedTargets;
    public override IEnumerable<string> IrrelevantTargetNames => [];

    public ForgejoActionsTrigger[] On { get; set; } = [];
    public string[] OnPushBranches { get; set; } = [];
    public string[] OnPushBranchesIgnore { get; set; } = [];
    public string[] OnPushTags { get; set; } = [];
    public string[] OnPushTagsIgnore { get; set; } = [];
    public string[] OnPushIncludePaths { get; set; } = [];
    public string[] OnPushExcludePaths { get; set; } = [];
    public string[] OnPullRequestBranches { get; set; } = [];
    public string[] OnPullRequestTags { get; set; } = [];
    public string[] OnPullRequestIncludePaths { get; set; } = [];
    public string[] OnPullRequestExcludePaths { get; set; } = [];
    public string[] OnWorkflowDispatchOptionalInputs { get; set; } = [];
    public string[] OnWorkflowDispatchRequiredInputs { get; set; } = [];
    public string OnCronSchedule { get; set; }

    public string[] ImportSecrets { get; set; } = [];

    public string[] CacheIncludePatterns { get; set; } = [".nuke/temp", "~/.nuget/packages"];
    public string[] CacheExcludePatterns { get; set; } = [];
    public string[] CacheKeyFiles { get; set; } = ["**/global.json", "**/*.csproj", "**/Directory.Packages.props"];

    public bool PublishArtifacts { get; set; } = true;
    public string PublishCondition { get; set; }

    public int TimeoutMinutes { get; set; }

    public string EnvironmentName { get; set; }
    public string EnvironmentUrl { get; set; }

    public string ConcurrencyGroup { get; set; }
    public bool ConcurrencyCancelInProgress { get; set; }

    public string JobConcurrencyGroup { get; set; }
    public bool JobConcurrencyCancelInProgress { get; set; }

    public string[] InvokedTargets { get; set; } = [];

    public ForgejoActionsSubmodules Submodules
    {
        set => _submodules = value;
        get => throw new NotSupportedException();
    }

    public bool Lfs
    {
        set => _lfs = value;
        get => throw new NotSupportedException();
    }

    public uint FetchDepth
    {
        set => _fetchDepth = value;
        get => throw new NotSupportedException();
    }

    public bool Progress
    {
        set => _progress = value;
        get => throw new NotSupportedException();
    }

    public string Filter
    {
        set => _filter = value;
        get => throw new NotSupportedException();
    }

    public override CustomFileWriter CreateWriter(StreamWriter streamWriter)
    {
        return new CustomFileWriter(streamWriter, indentationFactor: 2, commentPrefix: "#");
    }

    public override ConfigurationEntity GetConfiguration(IReadOnlyCollection<ExecutableTarget> relevantTargets)
    {
        var configuration = new ForgejoActionsConfiguration
                            {
                                Name = _name,
                                ShortTriggers = On,
                                DetailedTriggers = GetTriggers().ToArray(),
                                ConcurrencyGroup = ConcurrencyGroup,
                                ConcurrencyCancelInProgress = ConcurrencyCancelInProgress,
                                Jobs = _runners.Select(x => GetJobs(x, relevantTargets)).ToArray()
                            };

        Assert.True(configuration.ShortTriggers.Length == 0 || configuration.DetailedTriggers.Length == 0,
            $"Workflows can only define either shorthand '{nameof(On)}' or '{nameof(On)}*' triggers");
        Assert.True(configuration.ShortTriggers.Length > 0 || configuration.DetailedTriggers.Length > 0,
            $"Workflows must define either shorthand '{nameof(On)}' or '{nameof(On)}*' triggers");

        return configuration;
    }

    protected virtual ForgejoActionsJob GetJobs(string runner, IReadOnlyCollection<ExecutableTarget> relevantTargets)
    {
        return new ForgejoActionsJob
               {
                   Name = $"Run: {InvokedTargets.JoinComma().Truncate(200)} ({runner.Replace(".", "_")})".SingleQuote(),
                   EnvironmentName = EnvironmentName,
                   EnvironmentUrl = EnvironmentUrl,
                   Steps = GetSteps(runner, relevantTargets).ToArray(),
                   RunsOn = runner,
                   TimeoutMinutes = TimeoutMinutes,
                   ConcurrencyGroup = JobConcurrencyGroup,
                   ConcurrencyCancelInProgress = JobConcurrencyCancelInProgress
               };
    }

    private IEnumerable<ForgejoActionsStep> GetSteps(string image, IReadOnlyCollection<ExecutableTarget> relevantTargets)
    {
        yield return new ForgejoActionsCheckoutStep
                     {
                         Submodules = _submodules,
                         Lfs = _lfs,
                         FetchDepth = _fetchDepth,
                         Progress = _progress,
                         Filter = _filter
                     };

        if (CacheKeyFiles.Any())
        {
            yield return new ForgejoActionsCacheStep
                         {
                             IncludePatterns = CacheIncludePatterns,
                             ExcludePatterns = CacheExcludePatterns,
                             KeyFiles = CacheKeyFiles
                         };
        }

        yield return new ForgejoActionsRunStep
                     {
                         BuildCmdPath = BuildCmdPath,
                         InvokedTargets = InvokedTargets,
                         Imports = GetImports().ToDictionary(x => x.Key, x => x.Value)
                     };

        if (PublishArtifacts)
        {
            var artifacts = relevantTargets
                .SelectMany(x => x.ArtifactProducts)
                .Select(x => (AbsolutePath)x)
                // TODO: https://github.com/actions/upload-artifact/issues/11
                .Select(x => x.DescendantsAndSelf(y => y.Parent).FirstOrDefault(y => !y.ToString().ContainsOrdinalIgnoreCase("*")))
                .Distinct().ToList();

            foreach (var artifact in artifacts)
            {
                yield return new ForgejoActionsArtifactStep
                             {
                                 Name = artifact.ToString().TrimStart(artifact.Parent.ToString()).TrimStart('/', '\\'),
                                 Path = Build.RootDirectory.GetUnixRelativePathTo(artifact),
                                 Condition = PublishCondition
                             };
            }
        }
    }

    protected virtual IEnumerable<(string Key, string Value)> GetImports()
    {
        foreach (var input in OnWorkflowDispatchOptionalInputs.Concat(OnWorkflowDispatchRequiredInputs))
            yield return (input, $"${{{{ forgejo.event.inputs.{input} }}}}");

        static string GetSecretValue(string secret)
            => $"${{{{ secrets.{secret.SplitCamelHumpsWithKnownWords().JoinUnderscore().ToUpperInvariant()} }}}}";

        foreach (var secret in ImportSecrets)
            yield return (secret, GetSecretValue(secret));
    }

    protected virtual IEnumerable<ForgejoActionsDetailedTrigger> GetTriggers()
    {
        if (OnPushBranches.Length > 0 ||
            OnPushBranchesIgnore.Length > 0 ||
            OnPushTags.Length > 0 ||
            OnPushTagsIgnore.Length > 0 ||
            OnPushIncludePaths.Length > 0 ||
            OnPushExcludePaths.Length > 0)
        {
            Assert.True(
                OnPushBranches.Length == 0 && OnPushTags.Length == 0 || OnPushBranchesIgnore.Length == 0 && OnPushTagsIgnore.Length == 0,
                $"Cannot use {nameof(OnPushBranches)}/{nameof(OnPushTags)} and {nameof(OnPushBranchesIgnore)}/{nameof(OnPushTagsIgnore)} in combination");

            yield return new ForgejoActionsVcsTrigger
                         {
                             Kind = ForgejoActionsTrigger.Push,
                             Branches = OnPushBranches,
                             BranchesIgnore = OnPushBranchesIgnore,
                             Tags = OnPushTags,
                             TagsIgnore = OnPushTagsIgnore,
                             IncludePaths = OnPushIncludePaths,
                             ExcludePaths = OnPushExcludePaths
                         };
        }

        if (OnPullRequestBranches.Length > 0 ||
            OnPullRequestTags.Length > 0 ||
            OnPullRequestIncludePaths.Length > 0 ||
            OnPullRequestExcludePaths.Length > 0)
        {
            yield return new ForgejoActionsVcsTrigger
                         {
                             Kind = ForgejoActionsTrigger.PullRequest,
                             Branches = OnPullRequestBranches,
                             BranchesIgnore = [],
                             Tags = OnPullRequestTags,
                             TagsIgnore = [],
                             IncludePaths = OnPullRequestIncludePaths,
                             ExcludePaths = OnPullRequestExcludePaths
                         };
        }

        if (OnWorkflowDispatchOptionalInputs.Length > 0 ||
            OnWorkflowDispatchRequiredInputs.Length > 0)
        {
            yield return new ForgejoActionsWorkflowDispatchTrigger
                         {
                             OptionalInputs = OnWorkflowDispatchOptionalInputs,
                             RequiredInputs = OnWorkflowDispatchRequiredInputs
                         };
        }

        if (OnCronSchedule != null)
            yield return new ForgejoActionsScheduledTrigger { Cron = OnCronSchedule };
    }
}
