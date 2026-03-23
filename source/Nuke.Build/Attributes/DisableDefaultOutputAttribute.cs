// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;

namespace Nuke.Common;

[PublicAPI]
[AttributeUsage(AttributeTargets.Class)]
public class DisableDefaultOutputAttribute : Attribute
{
    public DisableDefaultOutputAttribute(params DefaultOutput[] disabledOutputs)
    {
        DisabledOutputs = disabledOutputs.Length > 0
            ? disabledOutputs
            : Enum.GetValues(typeof(DefaultOutput)).Cast<DefaultOutput>().ToArray();
    }

    public DefaultOutput[] DisabledOutputs { get; }

    public virtual bool IsApplicable(INukeBuild build)
    {
        return true;
    }
}

[PublicAPI]
[AttributeUsage(AttributeTargets.Class)]
public class DisableDefaultOutputAttribute<T> : DisableDefaultOutputAttribute
    where T : Host
{
    public DisableDefaultOutputAttribute(params DefaultOutput[] disabledOutputs)
        : base(disabledOutputs)
    {
    }

    public override bool IsApplicable(INukeBuild build)
    {
        return build.Host is T;
    }
}


public enum DefaultOutput
{
    Logo,
    TargetHeader,
    TargetCollapse,
    ErrorsAndWarnings,
    TargetOutcome,
    BuildOutcome,
    Timestamps,
    Notifications
}
