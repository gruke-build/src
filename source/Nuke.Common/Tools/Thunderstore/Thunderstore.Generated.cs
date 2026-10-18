// Copyright 2018-2025 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

// Generated from https://github.com/gruke-build/src/blob/master/source/Nuke.Common/Tools/Thunderstore/Thunderstore.json

using JetBrains.Annotations;
using Newtonsoft.Json;
using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools;
using Nuke.Common.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace Nuke.Common.Tools.Thunderstore;

/// <summary><p>Thunderstore CLI is a command line tool for building and uploading mod packages to Thunderstore mod database, and installing mods via the command line.</p><p>For more details, visit the <a href="https://github.com/thunderstore-io/thunderstore-cli">official website</a>.</p></summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
[NuGetTool(Id = PackageId, Executable = PackageExecutable)]
public partial class ThunderstoreTasks : ToolTasks, IRequireNuGetPackage
{
    public static string ThunderstorePath { get => new ThunderstoreTasks().GetToolPathInternal(); set => new ThunderstoreTasks().SetToolPath(value); }
    public const string PackageId = "tcli";
    public const string PackageExecutable = "tcli.exe|tcli|tcli.dll";
    /// <summary><p>Thunderstore CLI is a command line tool for building and uploading mod packages to Thunderstore mod database, and installing mods via the command line.</p><p>For more details, visit the <a href="https://github.com/thunderstore-io/thunderstore-cli">official website</a>.</p></summary>
    public static IReadOnlyCollection<Output> Thunderstore(ArgumentStringHandler arguments, string workingDirectory = null, IReadOnlyDictionary<string, string> environmentVariables = null, int? timeout = null, bool? logOutput = null, bool? logInvocation = null, Action<OutputType, string> logger = null, Func<IProcess, object> exitHandler = null) => new ThunderstoreTasks().Run(arguments, workingDirectory, environmentVariables, timeout, logOutput, logInvocation, logger, exitHandler);
    /// <summary><p>Build a package.</p><p>For more details, visit the <a href="https://github.com/thunderstore-io/thunderstore-cli">official website</a>.</p></summary>
    /// <remarks><p>This is a <a href="https://nuke.greemdev.net/release/common/cli-tools/#fluent-api">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p><ul><li><c>--config-path</c> via <see cref="ThunderstoreBuildSettings.ConfigPath"/></li><li><c>--package-name</c> via <see cref="ThunderstoreBuildSettings.PackageName"/></li><li><c>--package-namespace</c> via <see cref="ThunderstoreBuildSettings.PackageNamespace"/></li><li><c>--package-version</c> via <see cref="ThunderstoreBuildSettings.PackageVersion"/></li><li><c>--repository</c> via <see cref="ThunderstoreBuildSettings.Repository"/></li><li><c>--tcli-directory</c> via <see cref="ThunderstoreBuildSettings.DataDirectory"/></li></ul></remarks>
    public static IReadOnlyCollection<Output> ThunderstoreBuild(ThunderstoreBuildSettings options = null) => new ThunderstoreTasks().Run<ThunderstoreBuildSettings>(options);
    /// <inheritdoc cref="ThunderstoreTasks.ThunderstoreBuild(Nuke.Common.Tools.Thunderstore.ThunderstoreBuildSettings)"/>
    public static IReadOnlyCollection<Output> ThunderstoreBuild(Configure<ThunderstoreBuildSettings> configurator) => new ThunderstoreTasks().Run<ThunderstoreBuildSettings>(configurator.Invoke(new ThunderstoreBuildSettings()));
    /// <inheritdoc cref="ThunderstoreTasks.ThunderstoreBuild(Nuke.Common.Tools.Thunderstore.ThunderstoreBuildSettings)"/>
    public static IEnumerable<(ThunderstoreBuildSettings Settings, IReadOnlyCollection<Output> Output)> ThunderstoreBuild(CombinatorialConfigure<ThunderstoreBuildSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false) => configurator.Invoke(ThunderstoreBuild, degreeOfParallelism, completeOnFailure);
    /// <summary><p>Build & publish a package.</p><p>For more details, visit the <a href="https://github.com/thunderstore-io/thunderstore-cli">official website</a>.</p></summary>
    /// <remarks><p>This is a <a href="https://nuke.greemdev.net/release/common/cli-tools/#fluent-api">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p><ul><li><c>--config-path</c> via <see cref="ThunderstorePublishSettings.ConfigPath"/></li><li><c>--package-name</c> via <see cref="ThunderstorePublishSettings.PackageName"/></li><li><c>--package-namespace</c> via <see cref="ThunderstorePublishSettings.PackageNamespace"/></li><li><c>--package-version</c> via <see cref="ThunderstorePublishSettings.PackageVersion"/></li><li><c>--repository</c> via <see cref="ThunderstorePublishSettings.Repository"/></li><li><c>--tcli-directory</c> via <see cref="ThunderstorePublishSettings.DataDirectory"/></li><li><c>--token</c> via <see cref="ThunderstorePublishSettings.Token"/></li></ul></remarks>
    public static IReadOnlyCollection<Output> ThunderstorePublish(ThunderstorePublishSettings options = null) => new ThunderstoreTasks().Run<ThunderstorePublishSettings>(options);
    /// <inheritdoc cref="ThunderstoreTasks.ThunderstorePublish(Nuke.Common.Tools.Thunderstore.ThunderstorePublishSettings)"/>
    public static IReadOnlyCollection<Output> ThunderstorePublish(Configure<ThunderstorePublishSettings> configurator) => new ThunderstoreTasks().Run<ThunderstorePublishSettings>(configurator.Invoke(new ThunderstorePublishSettings()));
    /// <inheritdoc cref="ThunderstoreTasks.ThunderstorePublish(Nuke.Common.Tools.Thunderstore.ThunderstorePublishSettings)"/>
    public static IEnumerable<(ThunderstorePublishSettings Settings, IReadOnlyCollection<Output> Output)> ThunderstorePublish(CombinatorialConfigure<ThunderstorePublishSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false) => configurator.Invoke(ThunderstorePublish, degreeOfParallelism, completeOnFailure);
    /// <summary><p>Publish an existing package.</p><p>For more details, visit the <a href="https://github.com/thunderstore-io/thunderstore-cli">official website</a>.</p></summary>
    /// <remarks><p>This is a <a href="https://nuke.greemdev.net/release/common/cli-tools/#fluent-api">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p><ul><li><c>--config-path</c> via <see cref="ThunderstorePublishFileSettings.ConfigPath"/></li><li><c>--file</c> via <see cref="ThunderstorePublishFileSettings.File"/></li><li><c>--repository</c> via <see cref="ThunderstorePublishFileSettings.Repository"/></li><li><c>--tcli-directory</c> via <see cref="ThunderstorePublishFileSettings.DataDirectory"/></li><li><c>--token</c> via <see cref="ThunderstorePublishFileSettings.Token"/></li></ul></remarks>
    public static IReadOnlyCollection<Output> ThunderstorePublishFile(ThunderstorePublishFileSettings options = null) => new ThunderstoreTasks().Run<ThunderstorePublishFileSettings>(options);
    /// <inheritdoc cref="ThunderstoreTasks.ThunderstorePublishFile(Nuke.Common.Tools.Thunderstore.ThunderstorePublishFileSettings)"/>
    public static IReadOnlyCollection<Output> ThunderstorePublishFile(Configure<ThunderstorePublishFileSettings> configurator) => new ThunderstoreTasks().Run<ThunderstorePublishFileSettings>(configurator.Invoke(new ThunderstorePublishFileSettings()));
    /// <inheritdoc cref="ThunderstoreTasks.ThunderstorePublishFile(Nuke.Common.Tools.Thunderstore.ThunderstorePublishFileSettings)"/>
    public static IEnumerable<(ThunderstorePublishFileSettings Settings, IReadOnlyCollection<Output> Output)> ThunderstorePublishFile(CombinatorialConfigure<ThunderstorePublishFileSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false) => configurator.Invoke(ThunderstorePublishFile, degreeOfParallelism, completeOnFailure);
}
#region ThunderstoreBuildSettings
/// <inheritdoc cref="ThunderstoreTasks.ThunderstoreBuild(Nuke.Common.Tools.Thunderstore.ThunderstoreBuildSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Command(Type = typeof(ThunderstoreTasks), Command = nameof(ThunderstoreTasks.ThunderstoreBuild), Arguments = "build")]
public partial class ThunderstoreBuildSettings : ToolOptions
{
    /// <summary>Directory where TCLI keeps its data, %APPDATA%/ThunderstoreCLI on Windows and ~/.config/ThunderstoreCLI on Linux</summary>
    [Argument(Format = "--tcli-directory {value}")] public string DataDirectory => Get<string>(() => DataDirectory);
    /// <summary>URL of the default repository</summary>
    [Argument(Format = "--repository {value}")] public string Repository => Get<string>(() => Repository);
    /// <summary>Path for the project configuration file</summary>
    [Argument(Format = "--config-path {value}")] public string ConfigPath => Get<string>(() => ConfigPath);
    /// <summary>Name for the package</summary>
    [Argument(Format = "--package-name {value}")] public string PackageName => Get<string>(() => PackageName);
    /// <summary>Namespace for the package</summary>
    [Argument(Format = "--package-namespace {value}")] public string PackageNamespace => Get<string>(() => PackageNamespace);
    /// <summary>Version for the package</summary>
    [Argument(Format = "--package-version {value}")] public string PackageVersion => Get<string>(() => PackageVersion);
}
#endregion
#region ThunderstorePublishSettings
/// <inheritdoc cref="ThunderstoreTasks.ThunderstorePublish(Nuke.Common.Tools.Thunderstore.ThunderstorePublishSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Command(Type = typeof(ThunderstoreTasks), Command = nameof(ThunderstoreTasks.ThunderstorePublish), Arguments = "publish")]
public partial class ThunderstorePublishSettings : ToolOptions
{
    /// <summary>Directory where TCLI keeps its data, %APPDATA%/ThunderstoreCLI on Windows and ~/.config/ThunderstoreCLI on Linux</summary>
    [Argument(Format = "--tcli-directory {value}")] public string DataDirectory => Get<string>(() => DataDirectory);
    /// <summary>URL of the default repository</summary>
    [Argument(Format = "--repository {value}")] public string Repository => Get<string>(() => Repository);
    /// <summary>Path for the project configuration file</summary>
    [Argument(Format = "--config-path {value}")] public string ConfigPath => Get<string>(() => ConfigPath);
    /// <summary>Name for the package</summary>
    [Argument(Format = "--package-name {value}")] public string PackageName => Get<string>(() => PackageName);
    /// <summary>Namespace for the package</summary>
    [Argument(Format = "--package-namespace {value}")] public string PackageNamespace => Get<string>(() => PackageNamespace);
    /// <summary>Version for the package</summary>
    [Argument(Format = "--package-version {value}")] public string PackageVersion => Get<string>(() => PackageVersion);
    /// <summary>Authentication token to use for publishing</summary>
    [Argument(Format = "--token {value}")] public string Token => Get<string>(() => Token);
}
#endregion
#region ThunderstorePublishFileSettings
/// <inheritdoc cref="ThunderstoreTasks.ThunderstorePublishFile(Nuke.Common.Tools.Thunderstore.ThunderstorePublishFileSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Command(Type = typeof(ThunderstoreTasks), Command = nameof(ThunderstoreTasks.ThunderstorePublishFile), Arguments = "publish")]
public partial class ThunderstorePublishFileSettings : ToolOptions
{
    /// <summary>If provided, use defined package instead of building.</summary>
    [Argument(Format = "--file {value}")] public string File => Get<string>(() => File);
    /// <summary>Directory where TCLI keeps its data, %APPDATA%/ThunderstoreCLI on Windows and ~/.config/ThunderstoreCLI on Linux</summary>
    [Argument(Format = "--tcli-directory {value}")] public string DataDirectory => Get<string>(() => DataDirectory);
    /// <summary>URL of the default repository</summary>
    [Argument(Format = "--repository {value}")] public string Repository => Get<string>(() => Repository);
    /// <summary>Path for the project configuration file</summary>
    [Argument(Format = "--config-path {value}")] public string ConfigPath => Get<string>(() => ConfigPath);
    /// <summary>Authentication token to use for publishing</summary>
    [Argument(Format = "--token {value}")] public string Token => Get<string>(() => Token);
}
#endregion
#region ThunderstoreBuildSettingsExtensions
/// <inheritdoc cref="ThunderstoreTasks.ThunderstoreBuild(Nuke.Common.Tools.Thunderstore.ThunderstoreBuildSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class ThunderstoreBuildSettingsExtensions
{
    #region DataDirectory
    /// <inheritdoc cref="ThunderstoreBuildSettings.DataDirectory"/>
    [Pure] [Builder(Type = typeof(ThunderstoreBuildSettings), Property = nameof(ThunderstoreBuildSettings.DataDirectory))]
    public static T SetDataDirectory<T>(this T o, string v) where T : ThunderstoreBuildSettings => o.Modify(b => b.Set(() => o.DataDirectory, v));
    /// <inheritdoc cref="ThunderstoreBuildSettings.DataDirectory"/>
    [Pure] [Builder(Type = typeof(ThunderstoreBuildSettings), Property = nameof(ThunderstoreBuildSettings.DataDirectory))]
    public static T ResetDataDirectory<T>(this T o) where T : ThunderstoreBuildSettings => o.Modify(b => b.Remove(() => o.DataDirectory));
    #endregion
    #region Repository
    /// <inheritdoc cref="ThunderstoreBuildSettings.Repository"/>
    [Pure] [Builder(Type = typeof(ThunderstoreBuildSettings), Property = nameof(ThunderstoreBuildSettings.Repository))]
    public static T SetRepository<T>(this T o, string v) where T : ThunderstoreBuildSettings => o.Modify(b => b.Set(() => o.Repository, v));
    /// <inheritdoc cref="ThunderstoreBuildSettings.Repository"/>
    [Pure] [Builder(Type = typeof(ThunderstoreBuildSettings), Property = nameof(ThunderstoreBuildSettings.Repository))]
    public static T ResetRepository<T>(this T o) where T : ThunderstoreBuildSettings => o.Modify(b => b.Remove(() => o.Repository));
    #endregion
    #region ConfigPath
    /// <inheritdoc cref="ThunderstoreBuildSettings.ConfigPath"/>
    [Pure] [Builder(Type = typeof(ThunderstoreBuildSettings), Property = nameof(ThunderstoreBuildSettings.ConfigPath))]
    public static T SetConfigPath<T>(this T o, string v) where T : ThunderstoreBuildSettings => o.Modify(b => b.Set(() => o.ConfigPath, v));
    /// <inheritdoc cref="ThunderstoreBuildSettings.ConfigPath"/>
    [Pure] [Builder(Type = typeof(ThunderstoreBuildSettings), Property = nameof(ThunderstoreBuildSettings.ConfigPath))]
    public static T ResetConfigPath<T>(this T o) where T : ThunderstoreBuildSettings => o.Modify(b => b.Remove(() => o.ConfigPath));
    #endregion
    #region PackageName
    /// <inheritdoc cref="ThunderstoreBuildSettings.PackageName"/>
    [Pure] [Builder(Type = typeof(ThunderstoreBuildSettings), Property = nameof(ThunderstoreBuildSettings.PackageName))]
    public static T SetPackageName<T>(this T o, string v) where T : ThunderstoreBuildSettings => o.Modify(b => b.Set(() => o.PackageName, v));
    /// <inheritdoc cref="ThunderstoreBuildSettings.PackageName"/>
    [Pure] [Builder(Type = typeof(ThunderstoreBuildSettings), Property = nameof(ThunderstoreBuildSettings.PackageName))]
    public static T ResetPackageName<T>(this T o) where T : ThunderstoreBuildSettings => o.Modify(b => b.Remove(() => o.PackageName));
    #endregion
    #region PackageNamespace
    /// <inheritdoc cref="ThunderstoreBuildSettings.PackageNamespace"/>
    [Pure] [Builder(Type = typeof(ThunderstoreBuildSettings), Property = nameof(ThunderstoreBuildSettings.PackageNamespace))]
    public static T SetPackageNamespace<T>(this T o, string v) where T : ThunderstoreBuildSettings => o.Modify(b => b.Set(() => o.PackageNamespace, v));
    /// <inheritdoc cref="ThunderstoreBuildSettings.PackageNamespace"/>
    [Pure] [Builder(Type = typeof(ThunderstoreBuildSettings), Property = nameof(ThunderstoreBuildSettings.PackageNamespace))]
    public static T ResetPackageNamespace<T>(this T o) where T : ThunderstoreBuildSettings => o.Modify(b => b.Remove(() => o.PackageNamespace));
    #endregion
    #region PackageVersion
    /// <inheritdoc cref="ThunderstoreBuildSettings.PackageVersion"/>
    [Pure] [Builder(Type = typeof(ThunderstoreBuildSettings), Property = nameof(ThunderstoreBuildSettings.PackageVersion))]
    public static T SetPackageVersion<T>(this T o, string v) where T : ThunderstoreBuildSettings => o.Modify(b => b.Set(() => o.PackageVersion, v));
    /// <inheritdoc cref="ThunderstoreBuildSettings.PackageVersion"/>
    [Pure] [Builder(Type = typeof(ThunderstoreBuildSettings), Property = nameof(ThunderstoreBuildSettings.PackageVersion))]
    public static T ResetPackageVersion<T>(this T o) where T : ThunderstoreBuildSettings => o.Modify(b => b.Remove(() => o.PackageVersion));
    #endregion
}
#endregion
#region ThunderstorePublishSettingsExtensions
/// <inheritdoc cref="ThunderstoreTasks.ThunderstorePublish(Nuke.Common.Tools.Thunderstore.ThunderstorePublishSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class ThunderstorePublishSettingsExtensions
{
    #region DataDirectory
    /// <inheritdoc cref="ThunderstorePublishSettings.DataDirectory"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishSettings), Property = nameof(ThunderstorePublishSettings.DataDirectory))]
    public static T SetDataDirectory<T>(this T o, string v) where T : ThunderstorePublishSettings => o.Modify(b => b.Set(() => o.DataDirectory, v));
    /// <inheritdoc cref="ThunderstorePublishSettings.DataDirectory"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishSettings), Property = nameof(ThunderstorePublishSettings.DataDirectory))]
    public static T ResetDataDirectory<T>(this T o) where T : ThunderstorePublishSettings => o.Modify(b => b.Remove(() => o.DataDirectory));
    #endregion
    #region Repository
    /// <inheritdoc cref="ThunderstorePublishSettings.Repository"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishSettings), Property = nameof(ThunderstorePublishSettings.Repository))]
    public static T SetRepository<T>(this T o, string v) where T : ThunderstorePublishSettings => o.Modify(b => b.Set(() => o.Repository, v));
    /// <inheritdoc cref="ThunderstorePublishSettings.Repository"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishSettings), Property = nameof(ThunderstorePublishSettings.Repository))]
    public static T ResetRepository<T>(this T o) where T : ThunderstorePublishSettings => o.Modify(b => b.Remove(() => o.Repository));
    #endregion
    #region ConfigPath
    /// <inheritdoc cref="ThunderstorePublishSettings.ConfigPath"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishSettings), Property = nameof(ThunderstorePublishSettings.ConfigPath))]
    public static T SetConfigPath<T>(this T o, string v) where T : ThunderstorePublishSettings => o.Modify(b => b.Set(() => o.ConfigPath, v));
    /// <inheritdoc cref="ThunderstorePublishSettings.ConfigPath"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishSettings), Property = nameof(ThunderstorePublishSettings.ConfigPath))]
    public static T ResetConfigPath<T>(this T o) where T : ThunderstorePublishSettings => o.Modify(b => b.Remove(() => o.ConfigPath));
    #endregion
    #region PackageName
    /// <inheritdoc cref="ThunderstorePublishSettings.PackageName"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishSettings), Property = nameof(ThunderstorePublishSettings.PackageName))]
    public static T SetPackageName<T>(this T o, string v) where T : ThunderstorePublishSettings => o.Modify(b => b.Set(() => o.PackageName, v));
    /// <inheritdoc cref="ThunderstorePublishSettings.PackageName"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishSettings), Property = nameof(ThunderstorePublishSettings.PackageName))]
    public static T ResetPackageName<T>(this T o) where T : ThunderstorePublishSettings => o.Modify(b => b.Remove(() => o.PackageName));
    #endregion
    #region PackageNamespace
    /// <inheritdoc cref="ThunderstorePublishSettings.PackageNamespace"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishSettings), Property = nameof(ThunderstorePublishSettings.PackageNamespace))]
    public static T SetPackageNamespace<T>(this T o, string v) where T : ThunderstorePublishSettings => o.Modify(b => b.Set(() => o.PackageNamespace, v));
    /// <inheritdoc cref="ThunderstorePublishSettings.PackageNamespace"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishSettings), Property = nameof(ThunderstorePublishSettings.PackageNamespace))]
    public static T ResetPackageNamespace<T>(this T o) where T : ThunderstorePublishSettings => o.Modify(b => b.Remove(() => o.PackageNamespace));
    #endregion
    #region PackageVersion
    /// <inheritdoc cref="ThunderstorePublishSettings.PackageVersion"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishSettings), Property = nameof(ThunderstorePublishSettings.PackageVersion))]
    public static T SetPackageVersion<T>(this T o, string v) where T : ThunderstorePublishSettings => o.Modify(b => b.Set(() => o.PackageVersion, v));
    /// <inheritdoc cref="ThunderstorePublishSettings.PackageVersion"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishSettings), Property = nameof(ThunderstorePublishSettings.PackageVersion))]
    public static T ResetPackageVersion<T>(this T o) where T : ThunderstorePublishSettings => o.Modify(b => b.Remove(() => o.PackageVersion));
    #endregion
    #region Token
    /// <inheritdoc cref="ThunderstorePublishSettings.Token"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishSettings), Property = nameof(ThunderstorePublishSettings.Token))]
    public static T SetToken<T>(this T o, string v) where T : ThunderstorePublishSettings => o.Modify(b => b.Set(() => o.Token, v));
    /// <inheritdoc cref="ThunderstorePublishSettings.Token"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishSettings), Property = nameof(ThunderstorePublishSettings.Token))]
    public static T ResetToken<T>(this T o) where T : ThunderstorePublishSettings => o.Modify(b => b.Remove(() => o.Token));
    #endregion
}
#endregion
#region ThunderstorePublishFileSettingsExtensions
/// <inheritdoc cref="ThunderstoreTasks.ThunderstorePublishFile(Nuke.Common.Tools.Thunderstore.ThunderstorePublishFileSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class ThunderstorePublishFileSettingsExtensions
{
    #region File
    /// <inheritdoc cref="ThunderstorePublishFileSettings.File"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishFileSettings), Property = nameof(ThunderstorePublishFileSettings.File))]
    public static T SetFile<T>(this T o, string v) where T : ThunderstorePublishFileSettings => o.Modify(b => b.Set(() => o.File, v));
    /// <inheritdoc cref="ThunderstorePublishFileSettings.File"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishFileSettings), Property = nameof(ThunderstorePublishFileSettings.File))]
    public static T ResetFile<T>(this T o) where T : ThunderstorePublishFileSettings => o.Modify(b => b.Remove(() => o.File));
    #endregion
    #region DataDirectory
    /// <inheritdoc cref="ThunderstorePublishFileSettings.DataDirectory"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishFileSettings), Property = nameof(ThunderstorePublishFileSettings.DataDirectory))]
    public static T SetDataDirectory<T>(this T o, string v) where T : ThunderstorePublishFileSettings => o.Modify(b => b.Set(() => o.DataDirectory, v));
    /// <inheritdoc cref="ThunderstorePublishFileSettings.DataDirectory"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishFileSettings), Property = nameof(ThunderstorePublishFileSettings.DataDirectory))]
    public static T ResetDataDirectory<T>(this T o) where T : ThunderstorePublishFileSettings => o.Modify(b => b.Remove(() => o.DataDirectory));
    #endregion
    #region Repository
    /// <inheritdoc cref="ThunderstorePublishFileSettings.Repository"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishFileSettings), Property = nameof(ThunderstorePublishFileSettings.Repository))]
    public static T SetRepository<T>(this T o, string v) where T : ThunderstorePublishFileSettings => o.Modify(b => b.Set(() => o.Repository, v));
    /// <inheritdoc cref="ThunderstorePublishFileSettings.Repository"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishFileSettings), Property = nameof(ThunderstorePublishFileSettings.Repository))]
    public static T ResetRepository<T>(this T o) where T : ThunderstorePublishFileSettings => o.Modify(b => b.Remove(() => o.Repository));
    #endregion
    #region ConfigPath
    /// <inheritdoc cref="ThunderstorePublishFileSettings.ConfigPath"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishFileSettings), Property = nameof(ThunderstorePublishFileSettings.ConfigPath))]
    public static T SetConfigPath<T>(this T o, string v) where T : ThunderstorePublishFileSettings => o.Modify(b => b.Set(() => o.ConfigPath, v));
    /// <inheritdoc cref="ThunderstorePublishFileSettings.ConfigPath"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishFileSettings), Property = nameof(ThunderstorePublishFileSettings.ConfigPath))]
    public static T ResetConfigPath<T>(this T o) where T : ThunderstorePublishFileSettings => o.Modify(b => b.Remove(() => o.ConfigPath));
    #endregion
    #region Token
    /// <inheritdoc cref="ThunderstorePublishFileSettings.Token"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishFileSettings), Property = nameof(ThunderstorePublishFileSettings.Token))]
    public static T SetToken<T>(this T o, string v) where T : ThunderstorePublishFileSettings => o.Modify(b => b.Set(() => o.Token, v));
    /// <inheritdoc cref="ThunderstorePublishFileSettings.Token"/>
    [Pure] [Builder(Type = typeof(ThunderstorePublishFileSettings), Property = nameof(ThunderstorePublishFileSettings.Token))]
    public static T ResetToken<T>(this T o) where T : ThunderstorePublishFileSettings => o.Modify(b => b.Remove(() => o.Token));
    #endregion
}
#endregion
