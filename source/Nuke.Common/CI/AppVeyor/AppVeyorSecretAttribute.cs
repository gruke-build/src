// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;

namespace Nuke.Common.CI.AppVeyor;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AppVeyorSecretAttribute : Attribute
{
    public AppVeyorSecretAttribute(string parameter, string value)
    {
        Parameter = parameter;
        Value = value;
    }

    public string Parameter { get; }
    public string Value { get; }
}
