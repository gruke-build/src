// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using JetBrains.Annotations;

namespace Nuke.Common.CI;

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
    /// <summary>
    /// Common prefix for environment variables in this CI/CD environment. This is utilized by static methods in <see cref="IEnvironment{TEnvironment}"/>.
    /// </summary>
    public static abstract string EnvironmentVariablePrefix { get; }

    /// <summary>
    /// Get the value of '<see cref="EnvironmentVariablePrefix"/>_&lt;<paramref name="subName"/>&gt;', parsed as the specified type <typeparamref name="T"/>.
    /// <br/>
    /// If <paramref name="subName"/> is null, just the value of <see cref="EnvironmentVariablePrefix"/> is retrieved.
    /// </summary>
    /// <param name="subName">The variable name after the <see cref="EnvironmentVariablePrefix"/>.</param>
    /// <typeparam name="T">The expected type that the environment variable is parseable to.</typeparam>
    public static T Get<[CanBeNull] T>(string subName = null)
    {
        return EnvironmentInfo.GetVariable<T>(subName is null
            ? TEnvironment.EnvironmentVariablePrefix
            : $"{TEnvironment.EnvironmentVariablePrefix}_{subName}"
        );
    }

    /// <summary>
    /// Get the value of '<see cref="EnvironmentVariablePrefix"/>_&lt;<paramref name="subName"/>&gt;'.
    /// <br/>
    /// If <paramref name="subName"/> is null, just the value of <see cref="EnvironmentVariablePrefix"/> is retrieved.
    /// </summary>
    /// <param name="subName">The variable name after the <see cref="EnvironmentVariablePrefix"/>.</param>
    public static string Get(string subName = null)
    {
        return EnvironmentInfo.GetVariable(subName is null
            ? TEnvironment.EnvironmentVariablePrefix
            : $"{TEnvironment.EnvironmentVariablePrefix}_{subName}"
        );
    }

    /// <summary>
    /// Get the value of '<see cref="EnvironmentVariablePrefix"/>_&lt;<paramref name="subName"/>&gt;', returning <see langword="null"/> if it's an empty value.
    /// <br/>
    /// If <paramref name="subName"/> is null, just the value of <see cref="EnvironmentVariablePrefix"/> is retrieved.
    /// </summary>
    /// <param name="subName">The variable name after the <see cref="EnvironmentVariablePrefix"/>.</param>
    public static string GetOrNullIfEmpty(string subName = null)
    {
        return Get(subName) is {} value
            ? !string.IsNullOrEmpty(value)
                ? value
                : null
            : null;
    }

    /// <summary>
    /// Checks if an environment variable matching the name '<see cref="EnvironmentVariablePrefix"/>_&lt;<paramref name="subName"/>&gt;' exists.
    /// If <paramref name="subName"/> is null, just the value of <see cref="EnvironmentVariablePrefix"/> is checked for existence.
    /// </summary>
    /// <param name="subName">The variable name after the <see cref="EnvironmentVariablePrefix"/>.</param>
    /// <param name="allowEmpty">If a set, but empty, variable should be treated as non-existent.</param>
    public static bool Has(string subName = null, bool allowEmpty = true)
    {
        return EnvironmentInfo.HasVariable(subName is null
                ? TEnvironment.EnvironmentVariablePrefix
                : $"{TEnvironment.EnvironmentVariablePrefix}_{subName}",
            allowEmpty
        );
    }

    /// <summary>
    /// Sets an environment variable matching the name '<see cref="EnvironmentVariablePrefix"/>_&lt;<paramref name="subName"/>&gt;' to the specified value.
    /// </summary>
    /// <param name="subName">The variable name after the <see cref="EnvironmentVariablePrefix"/>.</param>
    /// <param name="value">The desired value.</param>
    public static void Set(string subName, string value)
    {
        EnvironmentInfo.SetVariable($"{TEnvironment.EnvironmentVariablePrefix}_{subName}", value);
    }
}
