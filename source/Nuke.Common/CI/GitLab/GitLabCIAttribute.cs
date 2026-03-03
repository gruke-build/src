// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.CI.GitLab.Configuration;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.GitLab;

public class GitLabCIAttribute : ConfigurationAttributeBase
{
    public bool Docker { get; set; } = true;
    
    public bool RecurseSubmodules { get; set; }

    [CanBeNull] public string DockerImage { get; set; }

    public string[] InvokedTargets { get; set; } = [];

    public bool UploadProducedArtifacts { get; set; }

    public override Type HostType => typeof(GitLab);
    public override IEnumerable<AbsolutePath> GeneratedFiles => [ ConfigurationFile ];
    public override IEnumerable<string> RelevantTargetNames => InvokedTargets;
    public override IEnumerable<string> IrrelevantTargetNames => [];
    public override CustomFileWriter CreateWriter(StreamWriter streamWriter)
    {
        return new CustomFileWriter(streamWriter, indentationFactor: 2, commentPrefix: "#");
    }

    public override ConfigurationEntity GetConfiguration(IReadOnlyCollection<ExecutableTarget> relevantTargets)
    {
        return new GitLabCIConfiguration
               {
                   UseDocker = Docker,
                   DockerImage = DockerImage,
                   UploadArtifacts = UploadProducedArtifacts,
                   InvokedTargets = InvokedTargets,
                   Artifacts = GetArtifacts(relevantTargets).ToArray(),
                   Variables = GetVariables()
               };
    }

    private IDictionary<string, object> GetVariables()
    {
        var dict = new Dictionary<string, object>();
        dict.Add("GIT_DEPTH", value: 0);
        if (RecurseSubmodules)
        {
            dict.Add("GIT_SUBMODULE_STRATEGY", "recursive");
        }

        return dict;
    }

    private IEnumerable<string> GetArtifacts(IReadOnlyCollection<ExecutableTarget> relevantTargets)
    {
        return relevantTargets
            .Select(x => x.ArtifactProducts)
            .SelectMany(x => x)
            .Select(x => Build.RootDirectory.GetUnixRelativePathTo(x).ToString());
    }

    public override AbsolutePath ConfigurationFile => Build.RootDirectory / ".gitlab-ci.yml";
}
