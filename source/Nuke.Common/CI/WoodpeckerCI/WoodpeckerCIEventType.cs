// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.ComponentModel;
using JetBrains.Annotations;
using Nuke.Common.Tooling;

namespace Nuke.Common.CI.WoodpeckerCI;

/// <summary>
///     Enumeration according to the <a href="https://woodpecker-ci.org/docs/usage/workflow-syntax#event">documentation</a>.
/// </summary>
[PublicAPI]
public enum WoodpeckerCIEvent
{
    /// <summary>
    /// Triggered when a commit is pushed to a branch.
    /// </summary>
    [EnumValue("push")] Push,
    /// <summary>
    /// Triggered when a pull request is opened or a new commit is pushed to it.
    /// </summary>
    [EnumValue("pull_request")] PullRequest,
    /// <summary>
    /// Triggered when a pull request is closed or merged.
    /// </summary>
    [EnumValue("pull_request_closed")] PullRequestClosed,
    /// <summary>
    /// Triggered when a pull request metadata has changed (e.g. title, body, label, milestone, ...).
    /// </summary>
    [EnumValue("pull_request_metadata")] PullRequestMetadata,
    /// <summary>
    /// Triggered when a tag is pushed.
    /// </summary>
    [EnumValue("tag")] Tag,
    /// <summary>
    /// Triggered when a release, pre-release or draft is created.
    /// (You can apply further filters using <a href="https://woodpecker-ci.org/docs/usage/workflow-syntax#evaluate">evaluate</a>
    /// with <a href="https://woodpecker-ci.org/docs/usage/environment#built-in-environment-variables">environment variables</a>.)
    /// </summary>
    [EnumValue("release")] Release,
    /// <summary>
    /// Triggered when a deployment is created in the repository. (This event can be triggered from Woodpecker directly. GitHub also supports webhook triggers.)
    /// </summary>
    [EnumValue("deployment")] Deployment,
    /// <summary>
    /// Triggered when a cron job is executed.
    /// </summary>
    [EnumValue("cron")] Cron,
    /// <summary>
    /// Triggered when a user manually triggers a pipeline.
    /// </summary>
    [EnumValue("manual")] Manual
}

/// <summary>
///     Enumeration according to the <a href="https://woodpecker-ci.org/docs/usage/workflow-syntax#event">documentation</a>.
/// </summary>
[TypeConverter(typeof(TypeConverter<WoodpeckerCIEventType>))]
[PublicAPI]
public class WoodpeckerCIEventType : Enumeration
{
    /// <summary>
    /// Triggered when a commit is pushed to a branch.
    /// </summary>
    public static readonly WoodpeckerCIEventType Push = new(WoodpeckerCIEvent.Push);
    /// <summary>
    /// Triggered when a pull request is opened or a new commit is pushed to it.
    /// </summary>
    public static readonly WoodpeckerCIEventType PullRequest = new(WoodpeckerCIEvent.PullRequest);
    /// <summary>
    /// Triggered when a pull request is closed or merged.
    /// </summary>
    public static readonly WoodpeckerCIEventType PullRequestClosed = new(WoodpeckerCIEvent.PullRequestClosed);
    /// <summary>
    /// Triggered when a pull request metadata has changed (e.g. title, body, label, milestone, ...).
    /// </summary>
    public static readonly WoodpeckerCIEventType PullRequestMetadata = new(WoodpeckerCIEvent.PullRequestMetadata);
    /// <summary>
    /// Triggered when a tag is pushed.
    /// </summary>
    public static readonly WoodpeckerCIEventType Tag = new(WoodpeckerCIEvent.Tag);
    /// <summary>
    /// Triggered when a release, pre-release or draft is created.
    /// (You can apply further filters using <a href="https://woodpecker-ci.org/docs/usage/workflow-syntax#evaluate">evaluate</a>
    /// with <a href="https://woodpecker-ci.org/docs/usage/environment#built-in-environment-variables">environment variables</a>.)
    /// </summary>
    public static readonly WoodpeckerCIEventType Release = new(WoodpeckerCIEvent.Release);
    /// <summary>
    /// Triggered when a deployment is created in the repository. (This event can be triggered from Woodpecker directly. GitHub also supports webhook triggers.)
    /// </summary>
    public static readonly WoodpeckerCIEventType Deployment = new(WoodpeckerCIEvent.Deployment);
    /// <summary>
    /// Triggered when a cron job is executed.
    /// </summary>
    public static readonly WoodpeckerCIEventType Cron = new(WoodpeckerCIEvent.Cron);
    /// <summary>
    /// Triggered when a user manually triggers a pipeline.
    /// </summary>
    public static readonly WoodpeckerCIEventType Manual = new(WoodpeckerCIEvent.Manual);

    private WoodpeckerCIEventType(WoodpeckerCIEvent ciEvent)
    {
        Value = ciEvent.GetValue();
    }

    public static implicit operator string(WoodpeckerCIEventType eventType)
    {
        return eventType.Value;
    }
}
