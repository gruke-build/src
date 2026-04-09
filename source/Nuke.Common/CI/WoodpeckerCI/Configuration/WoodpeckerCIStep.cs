// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.Collections.Generic;
using JetBrains.Annotations;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.WoodpeckerCI.Configuration;

[PublicAPI]
public abstract class WoodpeckerCIStep : ConfigurationEntity
{
    public abstract string Name { get; set; }

    public abstract string DockerImage { get; set; }

    public virtual Dictionary<string, object> EnvironmentVariables { get; set; } = new();//unused for now
    public virtual Dictionary<string, string> ImportedSecrets { get; set; } = new();

    public override void Write(CustomFileWriter writer)
    {
        using (writer.WriteBlock($"- name: {Name.SingleQuoteIfNeeded()}"))
        {
            writer.WriteLine($"image: {DockerImage.NotNull("DockerImage != null")}");
            if (EnvironmentVariables.Count > 0 || ImportedSecrets.Count > 0)
            {
                using (writer.WriteYamlBlock("environment"))
                {
                    foreach (var (secretName, secretKey) in ImportedSecrets)
                    {
                        using (writer.WriteYamlBlock(secretName))
                        {
                            writer.WriteLine($"from_secret: {secretKey}");
                        }
                    }

                    foreach (var (varName, varValue) in EnvironmentVariables)
                    {
                        writer.WriteLine(varValue is bool
                            ? $"{varName}: {varValue.ToString()!.ToLower()}"
                            : $"{varName}: {varValue.NotNull().ToString().SingleQuoteIfNeeded()}");
                    }
                }
            }
        }
    }
}
