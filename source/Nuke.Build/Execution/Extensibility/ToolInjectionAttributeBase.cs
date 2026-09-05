// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using System.Reflection;
using Nuke.Common.ValueInjection;

namespace Nuke.Common.Tooling;

public abstract class ToolInjectionAttributeBase : ValueInjectionAttributeBase
{
    public override bool SuppressWarnings => true;

    public abstract ToolRequirement GetRequirement(MemberInfo member);
}
