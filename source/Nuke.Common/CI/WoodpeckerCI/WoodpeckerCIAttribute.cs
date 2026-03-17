// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.CI.WoodpeckerCI.Configuration;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.WoodpeckerCI;

[PublicAPI]
[AttributeUsage(AttributeTargets.Class)]
public class WoodpeckerCIAttribute : ConfigurationAttributeBase
{
    private readonly string _name;

    public string[] InvokedTargets { get; set; } = [];

    public string[] OnlyOnBranches { get; set; }

    public WoodpeckerCIEvent[] Triggers { get; set; }

    public WoodpeckerCIAttribute(
        string name)
    {
        _name = name.Replace(oldChar: ' ', newChar: '_');
    }

    public override string IdPostfix => _name;
    public override Type HostType => typeof(WoodpeckerCI);
    public override IEnumerable<AbsolutePath> GeneratedFiles => [ ConfigurationFile ];
    public override IEnumerable<string> RelevantTargetNames => InvokedTargets;
    public override IEnumerable<string> IrrelevantTargetNames => [];
    public override CustomFileWriter CreateWriter(StreamWriter streamWriter)
    {
        return new CustomFileWriter(streamWriter, indentationFactor: 2, commentPrefix: "#");
    }

    public override ConfigurationEntity GetConfiguration(IReadOnlyCollection<ExecutableTarget> relevantTargets)
    {
        return new WoodpeckerCIConfiguration
               {
                   InvokedTargets = InvokedTargets,
                   OnlyOnBranches = OnlyOnBranches,
                   Triggers = Triggers,
                   Steps = GetSteps().ToArray()
               };
    }

    public IEnumerable<WoodpeckerCIStep> GetSteps()
    {
        yield return new WoodpeckerCIRunStep
                     {
                         InvokedTargets = InvokedTargets,
                         Name = $"Run: {InvokedTargets.JoinCommaAnd()}"
                     };
    }

    public override AbsolutePath ConfigurationFile => Build.RootDirectory / ".woodpecker" / $"{_name}.yml";
}
