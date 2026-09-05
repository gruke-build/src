// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

// Generated from https://github.com/gruke-build/src/blob/master/source/Nuke.Common/Tools/Gradle/Gradle.json

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

namespace Nuke.Common.Tools.Gradle;

/// <summary><p>Gradle is the open source build system of choice for Kotlin, Java, and Android developers; offering an extensive range of DSLs to configure the build process, as well as plugins to augment various functionalities and provide new ones.</p><p>For more details, visit the <a href="https://docs.gradle.org/">official website</a>.</p></summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
public partial class GradleTasks : ToolTasks
{
    public static string GradlePath { get => new GradleTasks().GetToolPathInternal(); set => new GradleTasks().SetToolPath(value); }
    /// <summary><p>Gradle is the open source build system of choice for Kotlin, Java, and Android developers; offering an extensive range of DSLs to configure the build process, as well as plugins to augment various functionalities and provide new ones.</p><p>For more details, visit the <a href="https://docs.gradle.org/">official website</a>.</p></summary>
    [Obsolete("The untyped generic task is unable to resolve the gradle wrapper from a working directory you provide it. The typed API can do this, however.")]
    public static IReadOnlyCollection<Output> Gradle(ArgumentStringHandler arguments, string workingDirectory = null, IReadOnlyDictionary<string, string> environmentVariables = null, int? timeout = null, bool? logOutput = null, bool? logInvocation = null, Action<OutputType, string> logger = null, Func<IProcess, object> exitHandler = null) => new GradleTasks().Run(arguments, workingDirectory, environmentVariables, timeout, logOutput, logInvocation, logger, exitHandler);
    /// <summary><p>Invoke any Gradle task, by name. Capable of using the Gradle Wrapper in the working directory.</p><p>For more details, visit the <a href="https://docs.gradle.org/">official website</a>.</p></summary>
    /// <remarks><p>This is a <a href="https://nuke.greemdev.net/release/common/cli-tools/#fluent-api">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p><ul><li><c>&lt;task&gt;</c> via <see cref="GradleSettings.Task"/></li><li><c>--build-cache</c> via <see cref="GradleSettings.BuildCache"/></li><li><c>--configuration-cache</c> via <see cref="GradleSettings.ConfigurationCache"/></li><li><c>--configure-on-demand</c> via <see cref="GradleSettings.ConfigureOnDemand"/></li><li><c>--console</c> via <see cref="GradleSettings.Console"/></li><li><c>--console-unicode</c> via <see cref="GradleSettings.ConsoleUnicode"/></li><li><c>--continue</c> via <see cref="GradleSettings.ContinueOnTaskFailure"/></li><li><c>--daemon</c> via <see cref="GradleSettings.Daemon"/></li><li><c>--debug</c> via <see cref="GradleSettings.DebugLogging"/></li><li><c>--dry-run</c> via <see cref="GradleSettings.DryRun"/></li><li><c>--exclude-task</c> via <see cref="GradleSettings.ExcludedTasks"/></li><li><c>--foreground</c> via <see cref="GradleSettings.ForegroundDaemon"/></li><li><c>--full-stacktrace</c> via <see cref="GradleSettings.FullStacktrace"/></li><li><c>--gradle-user-home</c> via <see cref="GradleSettings.GradleUserHome"/></li><li><c>--include-build</c> via <see cref="GradleSettings.IncludeBuild"/></li><li><c>--info</c> via <see cref="GradleSettings.InfoLogging"/></li><li><c>--init-script</c> via <see cref="GradleSettings.InitScript"/></li><li><c>--isolated-projects</c> via <see cref="GradleSettings.IsolatedProjects"/></li><li><c>--max-workers</c> via <see cref="GradleSettings.MaxWorkers"/></li><li><c>--no-build-cache</c> via <see cref="GradleSettings.NoBuildCache"/></li><li><c>--no-configuration-cache</c> via <see cref="GradleSettings.NoConfigurationCache"/></li><li><c>--no-configure-on-demand</c> via <see cref="GradleSettings.NoConfigureOnDemand"/></li><li><c>--no-continue</c> via <see cref="GradleSettings.NoContinueOnTaskFailure"/></li><li><c>--no-daemon</c> via <see cref="GradleSettings.NoDaemon"/></li><li><c>--no-isolated-projects</c> via <see cref="GradleSettings.NoIsolatedProjects"/></li><li><c>--no-parallel</c> via <see cref="GradleSettings.NoParallel"/></li><li><c>--no-rebuild</c> via <see cref="GradleSettings.NoRebuildDependencies"/></li><li><c>--no-watch-fs</c> via <see cref="GradleSettings.NoWatchFileSystem"/></li><li><c>--non-interactive</c> via <see cref="GradleSettings.NonInteractive"/></li><li><c>--offline</c> via <see cref="GradleSettings.Offline"/></li><li><c>--parallel</c> via <see cref="GradleSettings.Parallel"/></li><li><c>--priority</c> via <see cref="GradleSettings.Priority"/></li><li><c>--project-cache-dir</c> via <see cref="GradleSettings.ProjectCacheDir"/></li><li><c>--project-dir</c> via <see cref="GradleSettings.ProjectDir"/></li><li><c>--project-prop</c> via <see cref="GradleSettings.ProjectProperties"/></li><li><c>--quiet</c> via <see cref="GradleSettings.MinimalLogging"/></li><li><c>--refresh-dependencies</c> via <see cref="GradleSettings.RefreshDependencies"/></li><li><c>--rerun-tasks</c> via <see cref="GradleSettings.RerunTasks"/></li><li><c>--stacktrace</c> via <see cref="GradleSettings.Stacktrace"/></li><li><c>--stop</c> via <see cref="GradleSettings.StopDaemon"/></li><li><c>--system-prop</c> via <see cref="GradleSettings.SystemProperties"/></li><li><c>--warn</c> via <see cref="GradleSettings.WarnLogging"/></li><li><c>--watch-fs</c> via <see cref="GradleSettings.WatchFileSystem"/></li></ul></remarks>
    public static IReadOnlyCollection<Output> Gradle(GradleSettings options = null) => new GradleTasks().Run<GradleSettings>(options);
    /// <inheritdoc cref="GradleTasks.Gradle(Nuke.Common.Tools.Gradle.GradleSettings)"/>
    public static IReadOnlyCollection<Output> Gradle(Configure<GradleSettings> configurator) => new GradleTasks().Run<GradleSettings>(configurator.Invoke(new GradleSettings()));
    /// <inheritdoc cref="GradleTasks.Gradle(Nuke.Common.Tools.Gradle.GradleSettings)"/>
    public static IEnumerable<(GradleSettings Settings, IReadOnlyCollection<Output> Output)> Gradle(CombinatorialConfigure<GradleSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false) => configurator.Invoke(Gradle, degreeOfParallelism, completeOnFailure);
}
#region GradleSettings
/// <inheritdoc cref="GradleTasks.Gradle(Nuke.Common.Tools.Gradle.GradleSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Command(Type = typeof(GradleTasks), Command = nameof(GradleTasks.Gradle))]
public partial class GradleSettings : ToolOptions
{
    /// <summary>The fully formed Gradle task string or strings to invoke.</summary>
    [Argument(Format = "{value}", Position = 1)] public string Task => Get<string>(() => Task);
    /// <summary>Logs errors only.</summary>
    [Argument(Format = "--quiet")] public bool? MinimalLogging => Get<bool?>(() => MinimalLogging);
    /// <summary>Sets log level to debug. Includes the normal stacktrace.</summary>
    [Argument(Format = "--debug")] public bool? DebugLogging => Get<bool?>(() => DebugLogging);
    /// <summary>Sets the log level to info.</summary>
    [Argument(Format = "--info")] public bool? InfoLogging => Get<bool?>(() => InfoLogging);
    /// <summary>Sets the log level to warn.</summary>
    [Argument(Format = "--warn")] public bool? WarnLogging => Get<bool?>(() => WarnLogging);
    /// <summary>Prints the stacktrace for all exceptions.</summary>
    [Argument(Format = "--stacktrace")] public bool? Stacktrace => Get<bool?>(() => Stacktrace);
    /// <summary>Prints the full (very verbose) stacktrace for all exceptions.</summary>
    [Argument(Format = "--full-stacktrace")] public bool? FullStacktrace => Get<bool?>(() => FullStacktrace);
    /// <summary>Specifies which type of console output to generate. Default value is <c>auto</c>.</summary>
    [Argument(Format = "--console {value}")] public GradleConsoleOutput Console => Get<GradleConsoleOutput>(() => Console);
    /// <summary>Specifies which character types are allowed in the console output. Default value is <c>auto</c>.</summary>
    [Argument(Format = "--console-unicode {value}")] public GradleConsoleUnicode ConsoleUnicode => Get<GradleConsoleUnicode>(() => ConsoleUnicode);
    /// <summary>Do not do interactive prompting. [<a href="https://docs.gradle.org/current/javadoc/org/gradle/api/Incubating.html">incubating</a>]</summary>
    [Argument(Format = "--non-interactive")] public bool? NonInteractive => Get<bool?>(() => NonInteractive);
    /// <summary>Run the build as a <a href="https://docs.gradle.org/current/userguide/composite_builds.html#composite_builds">composite</a>, including the specified build.</summary>
    [Argument(Format = "--include-build {value}")] public string IncludeBuild => Get<string>(() => IncludeBuild);
    /// <summary>Specifies an <a href="https://docs.gradle.org/current/userguide/init_scripts.html#init_scripts">initialization script</a>.</summary>
    [Argument(Format = "--init-script {value}")] public string InitScript => Get<string>(() => InitScript);
    /// <summary>Specifies the Gradle user home directory. Default is <c>~/.gradle</c>.</summary>
    [Argument(Format = "--gradle-user-home {value}")] public string GradleUserHome => Get<string>(() => GradleUserHome);
    /// <summary>Specifies the start directory for Gradle. Default is the current directory.</summary>
    [Argument(Format = "--project-dir {value}")] public string ProjectDir => Get<string>(() => ProjectDir);
    /// <summary> Specifies the project-specific cache directory. Default is <c>.gradle</c> in the root project directory.</summary>
    [Argument(Format = "--project-cache-dir {value}")] public string ProjectCacheDir => Get<string>(() => ProjectCacheDir);
    /// <summary>Runs the build without accessing network resources.</summary>
    [Argument(Format = "--offline")] public bool? Offline => Get<bool?>(() => Offline);
    /// <summary>Refreshes the state of dependencies.</summary>
    [Argument(Format = "--refresh-dependencies")] public bool? RefreshDependencies => Get<bool?>(() => RefreshDependencies);
    /// <summary>The property values to pass to the Gradle invocation, for access within the build script.</summary>
    [Argument(Format = "--project-prop {key}={value}")] public IReadOnlyDictionary<string, string> ProjectProperties => Get<Dictionary<string, string>>(() => ProjectProperties);
    /// <summary>The JVM system properties to invoke Gradle with.</summary>
    [Argument(Format = "--system-prop {key}={value}")] public IReadOnlyDictionary<string, string> SystemProperties => Get<Dictionary<string, string>>(() => SystemProperties);
    /// <summary>Continues task execution after a task failure.</summary>
    [Argument(Format = "--continue")] public bool? ContinueOnTaskFailure => Get<bool?>(() => ContinueOnTaskFailure);
    /// <summary>Stops task execution after a task failure.</summary>
    [Argument(Format = "--no-continue")] public bool? NoContinueOnTaskFailure => Get<bool?>(() => NoContinueOnTaskFailure);
    /// <summary>Runs the build with all task actions disabled. Use this to show the chain of tasks that would have executed.</summary>
    [Argument(Format = "--dry-run")] public bool? DryRun => Get<bool?>(() => DryRun);
    /// <summary>Disables rebuilding of project dependencies.</summary>
    [Argument(Format = "--no-rebuild")] public bool? NoRebuildDependencies => Get<bool?>(() => NoRebuildDependencies);
    /// <summary>Ignores previously cached task results.</summary>
    [Argument(Format = "--rerun-tasks")] public bool? RerunTasks => Get<bool?>(() => RerunTasks);
    /// <summary>Specifies a task to exclude from execution.</summary>
    [Argument(Format = "--exclude-task={value}")] public IReadOnlyList<string> ExcludedTasks => Get<List<string>>(() => ExcludedTasks);
    /// <summary>Configures the maximum number of concurrent workers Gradle is allowed to use.</summary>
    [Argument(Format = "--max-workers={value}")] public int? MaxWorkers => Get<int?>(() => MaxWorkers);
    /// <summary>Specifies the scheduling priority for the Gradle daemon and all processes launched by it. Default value is <c>normal</c>.</summary>
    [Argument(Format = "--priority={value}")] public GradlePriority Priority => Get<GradlePriority>(() => Priority);
    /// <summary>Enables the Gradle build cache. Gradle will try to reuse outputs from previous builds.</summary>
    [Argument(Format = "--build-cache")] public bool? BuildCache => Get<bool?>(() => BuildCache);
    /// <summary>Disables the Gradle build cache.</summary>
    [Argument(Format = "--no-build-cache")] public bool? NoBuildCache => Get<bool?>(() => NoBuildCache);
    /// <summary>Enables the configuration cache. Gradle will try to reuse the build configuration from previous builds.</summary>
    [Argument(Format = "--configuration-cache")] public bool? ConfigurationCache => Get<bool?>(() => ConfigurationCache);
    /// <summary>Disables the Gradle configuration cache.</summary>
    [Argument(Format = "--no-configuration-cache")] public bool? NoConfigurationCache => Get<bool?>(() => NoConfigurationCache);
    /// <summary>Configures necessary projects only. Gradle will attempt to reduce configuration time for large multi-project builds. [<a href="https://docs.gradle.org/current/javadoc/org/gradle/api/Incubating.html">incubating</a>]</summary>
    [Argument(Format = "--configure-on-demand")] public bool? ConfigureOnDemand => Get<bool?>(() => ConfigureOnDemand);
    /// <summary>Disables the use of configuration on demand. [<a href="https://docs.gradle.org/current/javadoc/org/gradle/api/Incubating.html">incubating</a>]</summary>
    [Argument(Format = "--no-configure-on-demand")] public bool? NoConfigureOnDemand => Get<bool?>(() => NoConfigureOnDemand);
    /// <summary>Enables Isolated Projects. Projects are configured in parallel. Implies <c>--configuration-cache</c>. [<a href="https://docs.gradle.org/current/javadoc/org/gradle/api/Incubating.html">incubating</a>]</summary>
    [Argument(Format = "--isolated-projects")] public bool? IsolatedProjects => Get<bool?>(() => IsolatedProjects);
    /// <summary>Disables Isolated Projects. [<a href="https://docs.gradle.org/current/javadoc/org/gradle/api/Incubating.html">incubating</a>]</summary>
    [Argument(Format = "--no-isolated-projects")] public bool? NoIsolatedProjects => Get<bool?>(() => NoIsolatedProjects);
    /// <summary>Builds projects in parallel. Gradle will attempt to determine the optimal number of executor threads to use.</summary>
    [Argument(Format = "--parallel")] public bool? Parallel => Get<bool?>(() => Parallel);
    /// <summary>Disables parallel project execution.</summary>
    [Argument(Format = "--no-parallel")] public bool? NoParallel => Get<bool?>(() => NoParallel);
    /// <summary>Enables file system watching. Reuses file system data for subsequent builds.</summary>
    [Argument(Format = "--watch-fs")] public bool? WatchFileSystem => Get<bool?>(() => WatchFileSystem);
    /// <summary>Disables file system watching.</summary>
    [Argument(Format = "--no-watch-fs")] public bool? NoWatchFileSystem => Get<bool?>(() => NoWatchFileSystem);
    /// <summary>Uses the Gradle daemon to run the build. Starts the daemon if it is not running.</summary>
    [Argument(Format = "--daemon")] public bool? Daemon => Get<bool?>(() => Daemon);
    /// <summary>Runs the build without the Gradle daemon. Useful occasionally if you have configured Gradle to always run with the daemon by default.</summary>
    [Argument(Format = "--no-daemon")] public bool? NoDaemon => Get<bool?>(() => NoDaemon);
    /// <summary>Stops the Gradle daemon if it is running.</summary>
    [Argument(Format = "--stop")] public bool? StopDaemon => Get<bool?>(() => StopDaemon);
    /// <summary>Starts the Gradle daemon in the foreground.</summary>
    [Argument(Format = "--foreground")] public bool? ForegroundDaemon => Get<bool?>(() => ForegroundDaemon);
}
#endregion
#region GradleSettingsExtensions
/// <inheritdoc cref="GradleTasks.Gradle(Nuke.Common.Tools.Gradle.GradleSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class GradleSettingsExtensions
{
    #region Task
    /// <inheritdoc cref="GradleSettings.Task"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.Task))]
    public static T SetTask<T>(this T o, string v) where T : GradleSettings => o.Modify(b => b.Set(() => o.Task, v));
    /// <inheritdoc cref="GradleSettings.Task"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.Task))]
    public static T ResetTask<T>(this T o) where T : GradleSettings => o.Modify(b => b.Remove(() => o.Task));
    #endregion
    #region MinimalLogging
    /// <inheritdoc cref="GradleSettings.MinimalLogging"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.MinimalLogging))]
    public static T MinimalLogging<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.MinimalLogging, true));
    #endregion
    #region DebugLogging
    /// <inheritdoc cref="GradleSettings.DebugLogging"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.DebugLogging))]
    public static T DebugLogging<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.DebugLogging, true));
    #endregion
    #region InfoLogging
    /// <inheritdoc cref="GradleSettings.InfoLogging"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.InfoLogging))]
    public static T InfoLogging<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.InfoLogging, true));
    #endregion
    #region WarnLogging
    /// <inheritdoc cref="GradleSettings.WarnLogging"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.WarnLogging))]
    public static T WarnLogging<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.WarnLogging, true));
    #endregion
    #region Stacktrace
    /// <inheritdoc cref="GradleSettings.Stacktrace"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.Stacktrace))]
    public static T ShowStacktrace<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.Stacktrace, true));
    #endregion
    #region FullStacktrace
    /// <inheritdoc cref="GradleSettings.FullStacktrace"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.FullStacktrace))]
    public static T ShowFullStacktrace<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.FullStacktrace, true));
    #endregion
    #region Console
    /// <inheritdoc cref="GradleSettings.Console"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.Console))]
    public static T SetConsole<T>(this T o, GradleConsoleOutput v) where T : GradleSettings => o.Modify(b => b.Set(() => o.Console, v));
    /// <inheritdoc cref="GradleSettings.Console"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.Console))]
    public static T ResetConsole<T>(this T o) where T : GradleSettings => o.Modify(b => b.Remove(() => o.Console));
    #endregion
    #region ConsoleUnicode
    /// <inheritdoc cref="GradleSettings.ConsoleUnicode"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ConsoleUnicode))]
    public static T SetConsoleUnicode<T>(this T o, GradleConsoleUnicode v) where T : GradleSettings => o.Modify(b => b.Set(() => o.ConsoleUnicode, v));
    /// <inheritdoc cref="GradleSettings.ConsoleUnicode"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ConsoleUnicode))]
    public static T ResetConsoleUnicode<T>(this T o) where T : GradleSettings => o.Modify(b => b.Remove(() => o.ConsoleUnicode));
    #endregion
    #region NonInteractive
    /// <inheritdoc cref="GradleSettings.NonInteractive"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.NonInteractive))]
    public static T NonInteractive<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.NonInteractive, true));
    #endregion
    #region IncludeBuild
    /// <inheritdoc cref="GradleSettings.IncludeBuild"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.IncludeBuild))]
    public static T SetIncludeBuild<T>(this T o, string v) where T : GradleSettings => o.Modify(b => b.Set(() => o.IncludeBuild, v));
    /// <inheritdoc cref="GradleSettings.IncludeBuild"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.IncludeBuild))]
    public static T ResetIncludeBuild<T>(this T o) where T : GradleSettings => o.Modify(b => b.Remove(() => o.IncludeBuild));
    #endregion
    #region InitScript
    /// <inheritdoc cref="GradleSettings.InitScript"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.InitScript))]
    public static T SetInitScript<T>(this T o, string v) where T : GradleSettings => o.Modify(b => b.Set(() => o.InitScript, v));
    /// <inheritdoc cref="GradleSettings.InitScript"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.InitScript))]
    public static T ResetInitScript<T>(this T o) where T : GradleSettings => o.Modify(b => b.Remove(() => o.InitScript));
    #endregion
    #region GradleUserHome
    /// <inheritdoc cref="GradleSettings.GradleUserHome"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.GradleUserHome))]
    public static T SetGradleUserHome<T>(this T o, string v) where T : GradleSettings => o.Modify(b => b.Set(() => o.GradleUserHome, v));
    /// <inheritdoc cref="GradleSettings.GradleUserHome"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.GradleUserHome))]
    public static T ResetGradleUserHome<T>(this T o) where T : GradleSettings => o.Modify(b => b.Remove(() => o.GradleUserHome));
    #endregion
    #region ProjectDir
    /// <inheritdoc cref="GradleSettings.ProjectDir"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ProjectDir))]
    public static T SetProjectDir<T>(this T o, string v) where T : GradleSettings => o.Modify(b => b.Set(() => o.ProjectDir, v));
    /// <inheritdoc cref="GradleSettings.ProjectDir"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ProjectDir))]
    public static T ResetProjectDir<T>(this T o) where T : GradleSettings => o.Modify(b => b.Remove(() => o.ProjectDir));
    #endregion
    #region ProjectCacheDir
    /// <inheritdoc cref="GradleSettings.ProjectCacheDir"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ProjectCacheDir))]
    public static T SetProjectCacheDir<T>(this T o, string v) where T : GradleSettings => o.Modify(b => b.Set(() => o.ProjectCacheDir, v));
    /// <inheritdoc cref="GradleSettings.ProjectCacheDir"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ProjectCacheDir))]
    public static T ResetProjectCacheDir<T>(this T o) where T : GradleSettings => o.Modify(b => b.Remove(() => o.ProjectCacheDir));
    #endregion
    #region Offline
    /// <inheritdoc cref="GradleSettings.Offline"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.Offline))]
    public static T Offline<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.Offline, true));
    #endregion
    #region RefreshDependencies
    /// <inheritdoc cref="GradleSettings.RefreshDependencies"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.RefreshDependencies))]
    public static T RefreshDependencies<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.RefreshDependencies, true));
    #endregion
    #region ProjectProperties
    /// <inheritdoc cref="GradleSettings.ProjectProperties"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ProjectProperties))]
    public static T SetProjectProperties<T>(this T o, IDictionary<string, string> v) where T : GradleSettings => o.Modify(b => b.Set(() => o.ProjectProperties, v.ToDictionary(x => x.Key, x => x.Value, StringComparer.OrdinalIgnoreCase)));
    /// <inheritdoc cref="GradleSettings.ProjectProperties"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ProjectProperties))]
    public static T SetProjectProperty<T>(this T o, string k, string v) where T : GradleSettings => o.Modify(b => b.SetDictionary(() => o.ProjectProperties, k, v));
    /// <inheritdoc cref="GradleSettings.ProjectProperties"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ProjectProperties))]
    public static T AddProjectProperty<T>(this T o, string k, string v) where T : GradleSettings => o.Modify(b => b.AddDictionary(() => o.ProjectProperties, k, v));
    /// <inheritdoc cref="GradleSettings.ProjectProperties"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ProjectProperties))]
    public static T RemoveProjectProperty<T>(this T o, string k) where T : GradleSettings => o.Modify(b => b.RemoveDictionary(() => o.ProjectProperties, k));
    /// <inheritdoc cref="GradleSettings.ProjectProperties"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ProjectProperties))]
    public static T ClearProjectProperties<T>(this T o) where T : GradleSettings => o.Modify(b => b.ClearDictionary(() => o.ProjectProperties));
    #endregion
    #region SystemProperties
    /// <inheritdoc cref="GradleSettings.SystemProperties"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.SystemProperties))]
    public static T SetSystemProperties<T>(this T o, IDictionary<string, string> v) where T : GradleSettings => o.Modify(b => b.Set(() => o.SystemProperties, v.ToDictionary(x => x.Key, x => x.Value, StringComparer.OrdinalIgnoreCase)));
    /// <inheritdoc cref="GradleSettings.SystemProperties"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.SystemProperties))]
    public static T SetSystemProperty<T>(this T o, string k, string v) where T : GradleSettings => o.Modify(b => b.SetDictionary(() => o.SystemProperties, k, v));
    /// <inheritdoc cref="GradleSettings.SystemProperties"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.SystemProperties))]
    public static T AddSystemProperty<T>(this T o, string k, string v) where T : GradleSettings => o.Modify(b => b.AddDictionary(() => o.SystemProperties, k, v));
    /// <inheritdoc cref="GradleSettings.SystemProperties"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.SystemProperties))]
    public static T RemoveSystemProperty<T>(this T o, string k) where T : GradleSettings => o.Modify(b => b.RemoveDictionary(() => o.SystemProperties, k));
    /// <inheritdoc cref="GradleSettings.SystemProperties"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.SystemProperties))]
    public static T ClearSystemProperties<T>(this T o) where T : GradleSettings => o.Modify(b => b.ClearDictionary(() => o.SystemProperties));
    #endregion
    #region ContinueOnTaskFailure
    /// <inheritdoc cref="GradleSettings.ContinueOnTaskFailure"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ContinueOnTaskFailure))]
    public static T ContinueOnTaskFailure<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.ContinueOnTaskFailure, true));
    #endregion
    #region NoContinueOnTaskFailure
    /// <inheritdoc cref="GradleSettings.NoContinueOnTaskFailure"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.NoContinueOnTaskFailure))]
    public static T NoContinueOnTaskFailure<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.NoContinueOnTaskFailure, true));
    #endregion
    #region DryRun
    /// <inheritdoc cref="GradleSettings.DryRun"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.DryRun))]
    public static T DryRun<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.DryRun, true));
    #endregion
    #region NoRebuildDependencies
    /// <inheritdoc cref="GradleSettings.NoRebuildDependencies"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.NoRebuildDependencies))]
    public static T NoRebuildDependencies<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.NoRebuildDependencies, true));
    #endregion
    #region RerunTasks
    /// <inheritdoc cref="GradleSettings.RerunTasks"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.RerunTasks))]
    public static T RerunTasks<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.RerunTasks, true));
    #endregion
    #region ExcludedTasks
    /// <inheritdoc cref="GradleSettings.ExcludedTasks"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ExcludedTasks))]
    public static T SetExcludedTasks<T>(this T o, params string[] v) where T : GradleSettings => o.Modify(b => b.Set(() => o.ExcludedTasks, v));
    /// <inheritdoc cref="GradleSettings.ExcludedTasks"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ExcludedTasks))]
    public static T SetExcludedTasks<T>(this T o, IEnumerable<string> v) where T : GradleSettings => o.Modify(b => b.Set(() => o.ExcludedTasks, v));
    /// <inheritdoc cref="GradleSettings.ExcludedTasks"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ExcludedTasks))]
    public static T AddExcludedTasks<T>(this T o, params string[] v) where T : GradleSettings => o.Modify(b => b.AddCollection(() => o.ExcludedTasks, v));
    /// <inheritdoc cref="GradleSettings.ExcludedTasks"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ExcludedTasks))]
    public static T AddExcludedTasks<T>(this T o, IEnumerable<string> v) where T : GradleSettings => o.Modify(b => b.AddCollection(() => o.ExcludedTasks, v));
    /// <inheritdoc cref="GradleSettings.ExcludedTasks"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ExcludedTasks))]
    public static T RemoveExcludedTasks<T>(this T o, params string[] v) where T : GradleSettings => o.Modify(b => b.RemoveCollection(() => o.ExcludedTasks, v));
    /// <inheritdoc cref="GradleSettings.ExcludedTasks"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ExcludedTasks))]
    public static T RemoveExcludedTasks<T>(this T o, IEnumerable<string> v) where T : GradleSettings => o.Modify(b => b.RemoveCollection(() => o.ExcludedTasks, v));
    /// <inheritdoc cref="GradleSettings.ExcludedTasks"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ExcludedTasks))]
    public static T ClearExcludedTasks<T>(this T o) where T : GradleSettings => o.Modify(b => b.ClearCollection(() => o.ExcludedTasks));
    #endregion
    #region MaxWorkers
    /// <inheritdoc cref="GradleSettings.MaxWorkers"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.MaxWorkers))]
    public static T SetMaxWorkers<T>(this T o, int? v) where T : GradleSettings => o.Modify(b => b.Set(() => o.MaxWorkers, v));
    /// <inheritdoc cref="GradleSettings.MaxWorkers"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.MaxWorkers))]
    public static T ResetMaxWorkers<T>(this T o) where T : GradleSettings => o.Modify(b => b.Remove(() => o.MaxWorkers));
    #endregion
    #region Priority
    /// <inheritdoc cref="GradleSettings.Priority"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.Priority))]
    public static T SetPriority<T>(this T o, GradlePriority v) where T : GradleSettings => o.Modify(b => b.Set(() => o.Priority, v));
    /// <inheritdoc cref="GradleSettings.Priority"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.Priority))]
    public static T ResetPriority<T>(this T o) where T : GradleSettings => o.Modify(b => b.Remove(() => o.Priority));
    #endregion
    #region BuildCache
    /// <inheritdoc cref="GradleSettings.BuildCache"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.BuildCache))]
    public static T UseBuildCache<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.BuildCache, true));
    #endregion
    #region NoBuildCache
    /// <inheritdoc cref="GradleSettings.NoBuildCache"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.NoBuildCache))]
    public static T NoBuildCache<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.NoBuildCache, true));
    #endregion
    #region ConfigurationCache
    /// <inheritdoc cref="GradleSettings.ConfigurationCache"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ConfigurationCache))]
    public static T UseConfigurationCache<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.ConfigurationCache, true));
    #endregion
    #region NoConfigurationCache
    /// <inheritdoc cref="GradleSettings.NoConfigurationCache"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.NoConfigurationCache))]
    public static T NoConfigurationCache<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.NoConfigurationCache, true));
    #endregion
    #region ConfigureOnDemand
    /// <inheritdoc cref="GradleSettings.ConfigureOnDemand"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ConfigureOnDemand))]
    public static T UseConfigureOnDemand<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.ConfigureOnDemand, true));
    #endregion
    #region NoConfigureOnDemand
    /// <inheritdoc cref="GradleSettings.NoConfigureOnDemand"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.NoConfigureOnDemand))]
    public static T NoConfigureOnDemand<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.NoConfigureOnDemand, true));
    #endregion
    #region IsolatedProjects
    /// <inheritdoc cref="GradleSettings.IsolatedProjects"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.IsolatedProjects))]
    public static T UseIsolatedProjects<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.IsolatedProjects, true));
    #endregion
    #region NoIsolatedProjects
    /// <inheritdoc cref="GradleSettings.NoIsolatedProjects"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.NoIsolatedProjects))]
    public static T NoIsolatedProjects<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.NoIsolatedProjects, true));
    #endregion
    #region Parallel
    /// <inheritdoc cref="GradleSettings.Parallel"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.Parallel))]
    public static T UseParallel<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.Parallel, true));
    #endregion
    #region NoParallel
    /// <inheritdoc cref="GradleSettings.NoParallel"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.NoParallel))]
    public static T NoParallel<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.NoParallel, true));
    #endregion
    #region WatchFileSystem
    /// <inheritdoc cref="GradleSettings.WatchFileSystem"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.WatchFileSystem))]
    public static T UseWatchFileSystem<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.WatchFileSystem, true));
    #endregion
    #region NoWatchFileSystem
    /// <inheritdoc cref="GradleSettings.NoWatchFileSystem"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.NoWatchFileSystem))]
    public static T NoWatchFileSystem<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.NoWatchFileSystem, true));
    #endregion
    #region Daemon
    /// <inheritdoc cref="GradleSettings.Daemon"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.Daemon))]
    public static T UseDaemon<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.Daemon, true));
    #endregion
    #region NoDaemon
    /// <inheritdoc cref="GradleSettings.NoDaemon"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.NoDaemon))]
    public static T NoDaemon<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.NoDaemon, true));
    #endregion
    #region StopDaemon
    /// <inheritdoc cref="GradleSettings.StopDaemon"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.StopDaemon))]
    public static T StopDaemon<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.StopDaemon, true));
    #endregion
    #region ForegroundDaemon
    /// <inheritdoc cref="GradleSettings.ForegroundDaemon"/>
    [Pure] [Builder(Type = typeof(GradleSettings), Property = nameof(GradleSettings.ForegroundDaemon))]
    public static T ForegroundDaemon<T>(this T o) where T : GradleSettings => o.Modify(b => b.Set(() => o.ForegroundDaemon, true));
    #endregion
}
#endregion
#region GradlePriority
/// <summary>Used within <see cref="GradleTasks"/>.</summary>
[PublicAPI]
[Serializable]
[ExcludeFromCodeCoverage]
[TypeConverter(typeof(TypeConverter<GradlePriority>))]
public partial class GradlePriority : Enumeration
{
    public static readonly GradlePriority normal = (GradlePriority) "normal";
    public static readonly GradlePriority low = (GradlePriority) "low";
    public static implicit operator GradlePriority(string value)
    {
        return new GradlePriority { Value = value };
    }
}
#endregion
#region GradleConsoleOutput
/// <summary>Used within <see cref="GradleTasks"/>.</summary>
[PublicAPI]
[Serializable]
[ExcludeFromCodeCoverage]
[TypeConverter(typeof(TypeConverter<GradleConsoleOutput>))]
public partial class GradleConsoleOutput : Enumeration
{
    public static readonly GradleConsoleOutput auto = (GradleConsoleOutput) "auto";
    public static readonly GradleConsoleOutput plain = (GradleConsoleOutput) "plain";
    public static readonly GradleConsoleOutput colored = (GradleConsoleOutput) "colored";
    public static readonly GradleConsoleOutput rich = (GradleConsoleOutput) "rich";
    public static readonly GradleConsoleOutput verbose = (GradleConsoleOutput) "verbose";
    public static implicit operator GradleConsoleOutput(string value)
    {
        return new GradleConsoleOutput { Value = value };
    }
}
#endregion
#region GradleConsoleUnicode
/// <summary>Used within <see cref="GradleTasks"/>.</summary>
[PublicAPI]
[Serializable]
[ExcludeFromCodeCoverage]
[TypeConverter(typeof(TypeConverter<GradleConsoleUnicode>))]
public partial class GradleConsoleUnicode : Enumeration
{
    public static readonly GradleConsoleUnicode auto = (GradleConsoleUnicode) "auto";
    public static readonly GradleConsoleUnicode enable = (GradleConsoleUnicode) "enable";
    public static readonly GradleConsoleUnicode disable = (GradleConsoleUnicode) "disable";
    public static implicit operator GradleConsoleUnicode(string value)
    {
        return new GradleConsoleUnicode { Value = value };
    }
}
#endregion
