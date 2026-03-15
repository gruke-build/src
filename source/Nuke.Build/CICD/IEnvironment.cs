// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using JetBrains.Annotations;
using Nuke.Common;

namespace Nuke.Build.CICD;

/// <summary>
/// Represents a CI/CD build environment.
/// </summary>
/// <typeparam name="TEnvironment">
/// The environment type.<br/><br/>
/// If implementing this class, this should be the implementation type.
/// <br/>
/// If using the static members of this class, this should be the build server host you want to use the prefix for.
/// </typeparam>
public interface IEnvironment<TEnvironment>
    where TEnvironment : IEnvironment<TEnvironment>
{
    public static abstract string EnvironmentVariablePrefix { get; }

    public static T Get<[CanBeNull] T>(string subName = null)
        => EnvironmentInfo.GetVariable<T>(subName is null
            ? TEnvironment.EnvironmentVariablePrefix
            : $"{TEnvironment.EnvironmentVariablePrefix}_{subName}"
        );

    public static string Get(string subName = null)
        => EnvironmentInfo.GetVariable(subName is null
            ? TEnvironment.EnvironmentVariablePrefix
            : $"{TEnvironment.EnvironmentVariablePrefix}_{subName}"
        );

    public static bool Has(string subName = null, bool allowEmpty = true)
        => EnvironmentInfo.HasVariable(subName is null
                ? TEnvironment.EnvironmentVariablePrefix
                : $"{TEnvironment.EnvironmentVariablePrefix}_{subName}",
            allowEmpty
        );

    public static void Set(string subName, string value)
        => EnvironmentInfo.SetVariable($"{TEnvironment.EnvironmentVariablePrefix}_{subName}", value);
}
