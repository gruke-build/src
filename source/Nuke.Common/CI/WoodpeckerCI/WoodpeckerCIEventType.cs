// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.ComponentModel;
using Nuke.Common.Tooling;

namespace Nuke.Common.CI.WoodpeckerCI;

/// <summary>
///     Enumeration according to the <a href="https://woodpecker-ci.org/docs/usage/workflow-syntax#event">documentation</a>.
/// </summary>
[TypeConverter(typeof(TypeConverter<WoodpeckerCIEventType>))]
public class WoodpeckerCIEventType : Enumeration
{
    /// <summary>
    /// Triggered when a commit is pushed to a branch.
    /// </summary>
    public static WoodpeckerCIEventType Push = new("push");
    /// <summary>
    /// Triggered when a pull request is opened or a new commit is pushed to it.
    /// </summary>
    public static WoodpeckerCIEventType PullRequest = new("pull_request");
    /// <summary>
    /// Triggered when a pull request is closed or merged.
    /// </summary>
    public static WoodpeckerCIEventType PullRequestClosed = new("pull_request_closed");
    /// <summary>
    /// Triggered when a pull request metadata has changed (e.g. title, body, label, milestone, ...).
    /// </summary>
    public static WoodpeckerCIEventType PullRequestMetadata = new("pull_request_metadata");
    /// <summary>
    /// Triggered when a tag is pushed.
    /// </summary>
    public static WoodpeckerCIEventType Tag = new("tag");
    /// <summary>
    /// Triggered when a release, pre-release or draft is created.
    /// (You can apply further filters using <a href="https://woodpecker-ci.org/docs/usage/workflow-syntax#evaluate">evaluate</a>
    /// with <a href="https://woodpecker-ci.org/docs/usage/environment#built-in-environment-variables">environment variables</a>.)
    /// </summary>
    public static WoodpeckerCIEventType Release = new("release");
    /// <summary>
    /// Triggered when a deployment is created in the repository. (This event can be triggered from Woodpecker directly. GitHub also supports webhook triggers.)
    /// </summary>
    public static WoodpeckerCIEventType Deployment = new("deployment");
    /// <summary>
    /// Triggered when a cron job is executed.
    /// </summary>
    public static WoodpeckerCIEventType Cron = new("cron");
    /// <summary>
    /// Triggered when a user manually triggers a pipeline.
    /// </summary>
    public static WoodpeckerCIEventType Manual = new("manual");

    private WoodpeckerCIEventType(string value)
    {
        Value = value;
    }

    public static implicit operator string(WoodpeckerCIEventType eventType)
    {
        return eventType.Value;
    }
}
