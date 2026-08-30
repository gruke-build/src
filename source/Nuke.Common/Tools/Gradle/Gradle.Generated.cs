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
    public static IReadOnlyCollection<Output> Gradle(ArgumentStringHandler arguments, string workingDirectory = null, IReadOnlyDictionary<string, string> environmentVariables = null, int? timeout = null, bool? logOutput = null, bool? logInvocation = null, Action<OutputType, string> logger = null, Func<IProcess, object> exitHandler = null) => new GradleTasks().Run(arguments, workingDirectory, environmentVariables, timeout, logOutput, logInvocation, logger, exitHandler);
    /// <summary><p>Invoke any Gradle task(s), by name. Capable of using the Gradle Wrapper in the working directory.</p><p>For more details, visit the <a href="https://docs.gradle.org/">official website</a>.</p></summary>
    /// <remarks><p>This is a <a href="https://nuke.greemdev.net/release/common/cli-tools/#fluent-api">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p><ul><li><c>&lt;task&gt;</c> via <see cref="GradleInvokeSettings.Task"/></li></ul></remarks>
    public static IReadOnlyCollection<Output> GradleInvoke(GradleInvokeSettings options = null) => new GradleTasks().Run<GradleInvokeSettings>(options);
    /// <inheritdoc cref="GradleTasks.GradleInvoke(Nuke.Common.Tools.Gradle.GradleInvokeSettings)"/>
    public static IReadOnlyCollection<Output> GradleInvoke(Configure<GradleInvokeSettings> configurator) => new GradleTasks().Run<GradleInvokeSettings>(configurator.Invoke(new GradleInvokeSettings()));
    /// <inheritdoc cref="GradleTasks.GradleInvoke(Nuke.Common.Tools.Gradle.GradleInvokeSettings)"/>
    public static IEnumerable<(GradleInvokeSettings Settings, IReadOnlyCollection<Output> Output)> GradleInvoke(CombinatorialConfigure<GradleInvokeSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false) => configurator.Invoke(GradleInvoke, degreeOfParallelism, completeOnFailure);
}
#region GradleInvokeSettings
/// <inheritdoc cref="GradleTasks.GradleInvoke(Nuke.Common.Tools.Gradle.GradleInvokeSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Command(Type = typeof(GradleTasks), Command = nameof(GradleTasks.GradleInvoke))]
public partial class GradleInvokeSettings : ToolOptions
{
    /// <summary></summary>
    [Argument(Format = "{value}", Position = 1)] public string Task => Get<string>(() => Task);
}
#endregion
#region GradleInvokeSettingsExtensions
/// <inheritdoc cref="GradleTasks.GradleInvoke(Nuke.Common.Tools.Gradle.GradleInvokeSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class GradleInvokeSettingsExtensions
{
    #region Task
    /// <inheritdoc cref="GradleInvokeSettings.Task"/>
    [Pure] [Builder(Type = typeof(GradleInvokeSettings), Property = nameof(GradleInvokeSettings.Task))]
    public static T SetTask<T>(this T o, string v) where T : GradleInvokeSettings => o.Modify(b => b.Set(() => o.Task, v));
    /// <inheritdoc cref="GradleInvokeSettings.Task"/>
    [Pure] [Builder(Type = typeof(GradleInvokeSettings), Property = nameof(GradleInvokeSettings.Task))]
    public static T ResetTask<T>(this T o) where T : GradleInvokeSettings => o.Modify(b => b.Remove(() => o.Task));
    #endregion
}
#endregion
