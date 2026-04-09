// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

// Generated from https://github.com/gruke-build/src/blob/master/source/Nuke.Common/Tools/DotnetPackaging/DotnetPackaging.json

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

namespace Nuke.Common.Tools.DotnetPackaging;

/// <summary><p>DotnetPackaging is able to package your application into various formats, including Deb and AppImage.</p><p>For more details, visit the <a href="https://github.com/superjmn/dotnetpackaging">official website</a>.</p></summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
[NuGetTool(Id = PackageId, Executable = PackageExecutable)]
public partial class DotnetPackagingTasks : ToolTasks, IRequireNuGetPackage
{
    public static string DotnetPackagingPath { get => new DotnetPackagingTasks().GetToolPathInternal(); set => new DotnetPackagingTasks().SetToolPath(value); }
    public const string PackageId = "DotnetPackaging.Console";
    public const string PackageExecutable = "DotnetPackaging.Console.dll";
    /// <summary><p>DotnetPackaging is able to package your application into various formats, including Deb and AppImage.</p><p>For more details, visit the <a href="https://github.com/superjmn/dotnetpackaging">official website</a>.</p></summary>
    public static IReadOnlyCollection<Output> DotnetPackaging(ArgumentStringHandler arguments, string workingDirectory = null, IReadOnlyDictionary<string, string> environmentVariables = null, int? timeout = null, bool? logOutput = null, bool? logInvocation = null, Action<OutputType, string> logger = null, Func<IProcess, object> exitHandler = null) => new DotnetPackagingTasks().Run(arguments, workingDirectory, environmentVariables, timeout, logOutput, logInvocation, logger, exitHandler);
    /// <summary><p>Creates a Debian package from the specified directory.</p><p>For more details, visit the <a href="https://github.com/superjmn/dotnetpackaging">official website</a>.</p></summary>
    /// <remarks><p>This is a <a href="https://nuke.greemdev.net/release/common/cli-tools/#fluent-api">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p><ul><li><c>--directory</c> via <see cref="DotnetPackagingDebSettings.Directory"/></li><li><c>--metadata</c> via <see cref="DotnetPackagingDebSettings.Metadata"/></li><li><c>--output</c> via <see cref="DotnetPackagingDebSettings.Output"/></li></ul></remarks>
    public static IReadOnlyCollection<Output> DotnetPackagingDeb(DotnetPackagingDebSettings options = null) => new DotnetPackagingTasks().Run<DotnetPackagingDebSettings>(options);
    /// <inheritdoc cref="DotnetPackagingTasks.DotnetPackagingDeb(Nuke.Common.Tools.DotnetPackaging.DotnetPackagingDebSettings)"/>
    public static IReadOnlyCollection<Output> DotnetPackagingDeb(Configure<DotnetPackagingDebSettings> configurator) => new DotnetPackagingTasks().Run<DotnetPackagingDebSettings>(configurator.Invoke(new DotnetPackagingDebSettings()));
    /// <inheritdoc cref="DotnetPackagingTasks.DotnetPackagingDeb(Nuke.Common.Tools.DotnetPackaging.DotnetPackagingDebSettings)"/>
    public static IEnumerable<(DotnetPackagingDebSettings Settings, IReadOnlyCollection<Output> Output)> DotnetPackagingDeb(CombinatorialConfigure<DotnetPackagingDebSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false) => configurator.Invoke(DotnetPackagingDeb, degreeOfParallelism, completeOnFailure);
    /// <summary><p>Creates an AppImage package.</p><p>For more details, visit the <a href="https://github.com/superjmn/dotnetpackaging">official website</a>.</p></summary>
    /// <remarks><p>This is a <a href="https://nuke.greemdev.net/release/common/cli-tools/#fluent-api">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p><ul><li><c>--additional-categories</c> via <see cref="DotnetPackagingAppImageSettings.AdditionalCategories"/></li><li><c>--appId</c> via <see cref="DotnetPackagingAppImageSettings.AppId"/></li><li><c>--application-name</c> via <see cref="DotnetPackagingAppImageSettings.ApplicationName"/></li><li><c>--directory</c> via <see cref="DotnetPackagingAppImageSettings.Directory"/></li><li><c>--homepage</c> via <see cref="DotnetPackagingAppImageSettings.Homepage"/></li><li><c>--icon</c> via <see cref="DotnetPackagingAppImageSettings.Icon"/></li><li><c>--license</c> via <see cref="DotnetPackagingAppImageSettings.License"/></li><li><c>--main-category</c> via <see cref="DotnetPackagingAppImageSettings.MainCategory"/></li><li><c>--output</c> via <see cref="DotnetPackagingAppImageSettings.Output"/></li><li><c>--screenshot-urls</c> via <see cref="DotnetPackagingAppImageSettings.ScreenshotUrls"/></li><li><c>--summary</c> via <see cref="DotnetPackagingAppImageSettings.Summary"/></li><li><c>--version</c> via <see cref="DotnetPackagingAppImageSettings.Version"/></li></ul></remarks>
    public static IReadOnlyCollection<Output> DotnetPackagingAppImage(DotnetPackagingAppImageSettings options = null) => new DotnetPackagingTasks().Run<DotnetPackagingAppImageSettings>(options);
    /// <inheritdoc cref="DotnetPackagingTasks.DotnetPackagingAppImage(Nuke.Common.Tools.DotnetPackaging.DotnetPackagingAppImageSettings)"/>
    public static IReadOnlyCollection<Output> DotnetPackagingAppImage(Configure<DotnetPackagingAppImageSettings> configurator) => new DotnetPackagingTasks().Run<DotnetPackagingAppImageSettings>(configurator.Invoke(new DotnetPackagingAppImageSettings()));
    /// <inheritdoc cref="DotnetPackagingTasks.DotnetPackagingAppImage(Nuke.Common.Tools.DotnetPackaging.DotnetPackagingAppImageSettings)"/>
    public static IEnumerable<(DotnetPackagingAppImageSettings Settings, IReadOnlyCollection<Output> Output)> DotnetPackagingAppImage(CombinatorialConfigure<DotnetPackagingAppImageSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false) => configurator.Invoke(DotnetPackagingAppImage, degreeOfParallelism, completeOnFailure);
}
#region DotnetPackagingDebSettings
/// <inheritdoc cref="DotnetPackagingTasks.DotnetPackagingDeb(Nuke.Common.Tools.DotnetPackaging.DotnetPackagingDebSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Command(Type = typeof(DotnetPackagingTasks), Command = nameof(DotnetPackagingTasks.DotnetPackagingDeb), Arguments = "deb")]
public partial class DotnetPackagingDebSettings : ToolOptions
{
    /// <summary>The input directory from which to create the package.</summary>
    [Argument(Format = "--directory={value}")] public string Directory => Get<string>(() => Directory);
    /// <summary>The metadata file to include in the package.</summary>
    [Argument(Format = "--metadata={value}")] public string Metadata => Get<string>(() => Metadata);
    /// <summary>The output DEB file to create.</summary>
    [Argument(Format = "--output={value}")] public string Output => Get<string>(() => Output);
}
#endregion
#region DotnetPackagingAppImageSettings
/// <inheritdoc cref="DotnetPackagingTasks.DotnetPackagingAppImage(Nuke.Common.Tools.DotnetPackaging.DotnetPackagingAppImageSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Command(Type = typeof(DotnetPackagingTasks), Command = nameof(DotnetPackagingTasks.DotnetPackagingAppImage), Arguments = "appimage")]
public partial class DotnetPackagingAppImageSettings : ToolOptions
{
    /// <summary>The input directory from which to create the AppImage.</summary>
    [Argument(Format = "--directory={value}")] public string Directory => Get<string>(() => Directory);
    /// <summary>The output AppImage file to create.</summary>
    [Argument(Format = "--output={value}")] public string Output => Get<string>(() => Output);
    /// <summary>The name of the application for the AppImage.</summary>
    [Argument(Format = "--application-name={value}")] public string ApplicationName => Get<string>(() => ApplicationName);
    /// <summary>Main category of the application.</summary>
    [Argument(Format = "--main-category {value}")] public DotnetPackagingMainCategory MainCategory => Get<DotnetPackagingMainCategory>(() => MainCategory);
    /// <summary>Additional categories for the application.</summary>
    [Argument(Format = "--additional-categories {value}")] public IReadOnlyList<DotnetPackagingAdditionalCategory> AdditionalCategories => Get<List<DotnetPackagingAdditionalCategory>>(() => AdditionalCategories);
    /// <summary>The icon path for the application. When not provided, the tool looks up for an image called <c>AppImage.png</c>.</summary>
    [Argument(Format = "--icon {value}")] public string Icon => Get<string>(() => Icon);
    /// <summary>Home page of the application.</summary>
    [Argument(Format = "--homepage {value}")] public string Homepage => Get<string>(() => Homepage);
    /// <summary>License of the application.</summary>
    [Argument(Format = "--license {value}")] public string License => Get<string>(() => License);
    /// <summary>Version of the application.</summary>
    [Argument(Format = "--version {value}")] public string Version => Get<string>(() => Version);
    /// <summary>URLs of screenshots of the application.</summary>
    [Argument(Format = "--screenshot-urls {value}")] public IReadOnlyList<string> ScreenshotUrls => Get<List<string>>(() => ScreenshotUrls);
    /// <summary>Short description of the application.</summary>
    [Argument(Format = "--summary {value}")] public string Summary => Get<string>(() => Summary);
    /// <summary>Application ID, usually a reverse DNS name like <c>com.SomeCompany.SomeApplication</c>.</summary>
    [Argument(Format = "--appId {value}")] public string AppId => Get<string>(() => AppId);
}
#endregion
#region DotnetPackagingDebSettingsExtensions
/// <inheritdoc cref="DotnetPackagingTasks.DotnetPackagingDeb(Nuke.Common.Tools.DotnetPackaging.DotnetPackagingDebSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class DotnetPackagingDebSettingsExtensions
{
    #region Directory
    /// <inheritdoc cref="DotnetPackagingDebSettings.Directory"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingDebSettings), Property = nameof(DotnetPackagingDebSettings.Directory))]
    public static T SetDirectory<T>(this T o, string v) where T : DotnetPackagingDebSettings => o.Modify(b => b.Set(() => o.Directory, v));
    /// <inheritdoc cref="DotnetPackagingDebSettings.Directory"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingDebSettings), Property = nameof(DotnetPackagingDebSettings.Directory))]
    public static T ResetDirectory<T>(this T o) where T : DotnetPackagingDebSettings => o.Modify(b => b.Remove(() => o.Directory));
    #endregion
    #region Metadata
    /// <inheritdoc cref="DotnetPackagingDebSettings.Metadata"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingDebSettings), Property = nameof(DotnetPackagingDebSettings.Metadata))]
    public static T SetMetadata<T>(this T o, string v) where T : DotnetPackagingDebSettings => o.Modify(b => b.Set(() => o.Metadata, v));
    /// <inheritdoc cref="DotnetPackagingDebSettings.Metadata"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingDebSettings), Property = nameof(DotnetPackagingDebSettings.Metadata))]
    public static T ResetMetadata<T>(this T o) where T : DotnetPackagingDebSettings => o.Modify(b => b.Remove(() => o.Metadata));
    #endregion
    #region Output
    /// <inheritdoc cref="DotnetPackagingDebSettings.Output"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingDebSettings), Property = nameof(DotnetPackagingDebSettings.Output))]
    public static T SetOutput<T>(this T o, string v) where T : DotnetPackagingDebSettings => o.Modify(b => b.Set(() => o.Output, v));
    /// <inheritdoc cref="DotnetPackagingDebSettings.Output"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingDebSettings), Property = nameof(DotnetPackagingDebSettings.Output))]
    public static T ResetOutput<T>(this T o) where T : DotnetPackagingDebSettings => o.Modify(b => b.Remove(() => o.Output));
    #endregion
}
#endregion
#region DotnetPackagingAppImageSettingsExtensions
/// <inheritdoc cref="DotnetPackagingTasks.DotnetPackagingAppImage(Nuke.Common.Tools.DotnetPackaging.DotnetPackagingAppImageSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class DotnetPackagingAppImageSettingsExtensions
{
    #region Directory
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.Directory"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.Directory))]
    public static T SetDirectory<T>(this T o, string v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Set(() => o.Directory, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.Directory"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.Directory))]
    public static T ResetDirectory<T>(this T o) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Remove(() => o.Directory));
    #endregion
    #region Output
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.Output"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.Output))]
    public static T SetOutput<T>(this T o, string v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Set(() => o.Output, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.Output"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.Output))]
    public static T ResetOutput<T>(this T o) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Remove(() => o.Output));
    #endregion
    #region ApplicationName
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.ApplicationName"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.ApplicationName))]
    public static T SetApplicationName<T>(this T o, string v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Set(() => o.ApplicationName, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.ApplicationName"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.ApplicationName))]
    public static T ResetApplicationName<T>(this T o) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Remove(() => o.ApplicationName));
    #endregion
    #region MainCategory
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.MainCategory"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.MainCategory))]
    public static T SetMainCategory<T>(this T o, DotnetPackagingMainCategory v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Set(() => o.MainCategory, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.MainCategory"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.MainCategory))]
    public static T ResetMainCategory<T>(this T o) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Remove(() => o.MainCategory));
    #endregion
    #region AdditionalCategories
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.AdditionalCategories"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.AdditionalCategories))]
    public static T SetAdditionalCategories<T>(this T o, params DotnetPackagingAdditionalCategory[] v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Set(() => o.AdditionalCategories, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.AdditionalCategories"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.AdditionalCategories))]
    public static T SetAdditionalCategories<T>(this T o, IEnumerable<DotnetPackagingAdditionalCategory> v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Set(() => o.AdditionalCategories, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.AdditionalCategories"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.AdditionalCategories))]
    public static T AddAdditionalCategories<T>(this T o, params DotnetPackagingAdditionalCategory[] v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.AddCollection(() => o.AdditionalCategories, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.AdditionalCategories"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.AdditionalCategories))]
    public static T AddAdditionalCategories<T>(this T o, IEnumerable<DotnetPackagingAdditionalCategory> v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.AddCollection(() => o.AdditionalCategories, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.AdditionalCategories"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.AdditionalCategories))]
    public static T RemoveAdditionalCategories<T>(this T o, params DotnetPackagingAdditionalCategory[] v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.RemoveCollection(() => o.AdditionalCategories, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.AdditionalCategories"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.AdditionalCategories))]
    public static T RemoveAdditionalCategories<T>(this T o, IEnumerable<DotnetPackagingAdditionalCategory> v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.RemoveCollection(() => o.AdditionalCategories, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.AdditionalCategories"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.AdditionalCategories))]
    public static T ClearAdditionalCategories<T>(this T o) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.ClearCollection(() => o.AdditionalCategories));
    #endregion
    #region Icon
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.Icon"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.Icon))]
    public static T SetIcon<T>(this T o, string v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Set(() => o.Icon, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.Icon"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.Icon))]
    public static T ResetIcon<T>(this T o) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Remove(() => o.Icon));
    #endregion
    #region Homepage
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.Homepage"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.Homepage))]
    public static T SetHomepage<T>(this T o, string v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Set(() => o.Homepage, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.Homepage"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.Homepage))]
    public static T ResetHomepage<T>(this T o) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Remove(() => o.Homepage));
    #endregion
    #region License
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.License"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.License))]
    public static T SetLicense<T>(this T o, string v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Set(() => o.License, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.License"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.License))]
    public static T ResetLicense<T>(this T o) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Remove(() => o.License));
    #endregion
    #region Version
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.Version"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.Version))]
    public static T SetVersion<T>(this T o, string v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Set(() => o.Version, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.Version"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.Version))]
    public static T ResetVersion<T>(this T o) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Remove(() => o.Version));
    #endregion
    #region ScreenshotUrls
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.ScreenshotUrls"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.ScreenshotUrls))]
    public static T SetScreenshotUrls<T>(this T o, params string[] v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Set(() => o.ScreenshotUrls, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.ScreenshotUrls"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.ScreenshotUrls))]
    public static T SetScreenshotUrls<T>(this T o, IEnumerable<string> v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Set(() => o.ScreenshotUrls, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.ScreenshotUrls"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.ScreenshotUrls))]
    public static T AddScreenshotUrls<T>(this T o, params string[] v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.AddCollection(() => o.ScreenshotUrls, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.ScreenshotUrls"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.ScreenshotUrls))]
    public static T AddScreenshotUrls<T>(this T o, IEnumerable<string> v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.AddCollection(() => o.ScreenshotUrls, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.ScreenshotUrls"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.ScreenshotUrls))]
    public static T RemoveScreenshotUrls<T>(this T o, params string[] v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.RemoveCollection(() => o.ScreenshotUrls, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.ScreenshotUrls"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.ScreenshotUrls))]
    public static T RemoveScreenshotUrls<T>(this T o, IEnumerable<string> v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.RemoveCollection(() => o.ScreenshotUrls, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.ScreenshotUrls"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.ScreenshotUrls))]
    public static T ClearScreenshotUrls<T>(this T o) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.ClearCollection(() => o.ScreenshotUrls));
    #endregion
    #region Summary
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.Summary"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.Summary))]
    public static T SetSummary<T>(this T o, string v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Set(() => o.Summary, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.Summary"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.Summary))]
    public static T ResetSummary<T>(this T o) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Remove(() => o.Summary));
    #endregion
    #region AppId
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.AppId"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.AppId))]
    public static T SetAppId<T>(this T o, string v) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Set(() => o.AppId, v));
    /// <inheritdoc cref="DotnetPackagingAppImageSettings.AppId"/>
    [Pure] [Builder(Type = typeof(DotnetPackagingAppImageSettings), Property = nameof(DotnetPackagingAppImageSettings.AppId))]
    public static T ResetAppId<T>(this T o) where T : DotnetPackagingAppImageSettings => o.Modify(b => b.Remove(() => o.AppId));
    #endregion
}
#endregion
#region DotnetPackagingMainCategory
/// <summary>Used within <see cref="DotnetPackagingTasks"/>.</summary>
[PublicAPI]
[Serializable]
[ExcludeFromCodeCoverage]
[TypeConverter(typeof(TypeConverter<DotnetPackagingMainCategory>))]
public partial class DotnetPackagingMainCategory : Enumeration
{
    public static readonly DotnetPackagingMainCategory AudioVideo = (DotnetPackagingMainCategory) "AudioVideo";
    public static readonly DotnetPackagingMainCategory Audio = (DotnetPackagingMainCategory) "Audio";
    public static readonly DotnetPackagingMainCategory Video = (DotnetPackagingMainCategory) "Video";
    public static readonly DotnetPackagingMainCategory Development = (DotnetPackagingMainCategory) "Development";
    public static readonly DotnetPackagingMainCategory Education = (DotnetPackagingMainCategory) "Education";
    public static readonly DotnetPackagingMainCategory Game = (DotnetPackagingMainCategory) "Game";
    public static readonly DotnetPackagingMainCategory Graphics = (DotnetPackagingMainCategory) "Graphics";
    public static readonly DotnetPackagingMainCategory Network = (DotnetPackagingMainCategory) "Network";
    public static readonly DotnetPackagingMainCategory Office = (DotnetPackagingMainCategory) "Office";
    public static readonly DotnetPackagingMainCategory Settings = (DotnetPackagingMainCategory) "Settings";
    public static readonly DotnetPackagingMainCategory Utility = (DotnetPackagingMainCategory) "Utility";
    public static implicit operator DotnetPackagingMainCategory(string value)
    {
        return new DotnetPackagingMainCategory { Value = value };
    }
}
#endregion
#region DotnetPackagingAdditionalCategory
/// <summary>Used within <see cref="DotnetPackagingTasks"/>.</summary>
[PublicAPI]
[Serializable]
[ExcludeFromCodeCoverage]
[TypeConverter(typeof(TypeConverter<DotnetPackagingAdditionalCategory>))]
public partial class DotnetPackagingAdditionalCategory : Enumeration
{
    public static readonly DotnetPackagingAdditionalCategory Building = (DotnetPackagingAdditionalCategory) "Building";
    public static readonly DotnetPackagingAdditionalCategory Debugger = (DotnetPackagingAdditionalCategory) "Debugger";
    public static readonly DotnetPackagingAdditionalCategory IDE = (DotnetPackagingAdditionalCategory) "IDE";
    public static readonly DotnetPackagingAdditionalCategory GUIDesigner = (DotnetPackagingAdditionalCategory) "GUIDesigner";
    public static readonly DotnetPackagingAdditionalCategory Profiling = (DotnetPackagingAdditionalCategory) "Profiling";
    public static readonly DotnetPackagingAdditionalCategory RevisionControl = (DotnetPackagingAdditionalCategory) "RevisionControl";
    public static readonly DotnetPackagingAdditionalCategory Translation = (DotnetPackagingAdditionalCategory) "Translation";
    public static readonly DotnetPackagingAdditionalCategory Calendar = (DotnetPackagingAdditionalCategory) "Calendar";
    public static readonly DotnetPackagingAdditionalCategory ContactManagement = (DotnetPackagingAdditionalCategory) "ContactManagement";
    public static readonly DotnetPackagingAdditionalCategory Database = (DotnetPackagingAdditionalCategory) "Database";
    public static readonly DotnetPackagingAdditionalCategory Dictionary = (DotnetPackagingAdditionalCategory) "Dictionary";
    public static readonly DotnetPackagingAdditionalCategory Chart = (DotnetPackagingAdditionalCategory) "Chart";
    public static readonly DotnetPackagingAdditionalCategory Email = (DotnetPackagingAdditionalCategory) "Email";
    public static readonly DotnetPackagingAdditionalCategory Finance = (DotnetPackagingAdditionalCategory) "Finance";
    public static readonly DotnetPackagingAdditionalCategory FlowChart = (DotnetPackagingAdditionalCategory) "FlowChart";
    public static readonly DotnetPackagingAdditionalCategory PDA = (DotnetPackagingAdditionalCategory) "PDA";
    public static readonly DotnetPackagingAdditionalCategory ProjectManagement = (DotnetPackagingAdditionalCategory) "ProjectManagement";
    public static readonly DotnetPackagingAdditionalCategory Presentation = (DotnetPackagingAdditionalCategory) "Presentation";
    public static readonly DotnetPackagingAdditionalCategory Spreadsheet = (DotnetPackagingAdditionalCategory) "Spreadsheet";
    public static readonly DotnetPackagingAdditionalCategory WordProcessor = (DotnetPackagingAdditionalCategory) "WordProcessor";
    public static readonly DotnetPackagingAdditionalCategory TwoDGraphics = (DotnetPackagingAdditionalCategory) "TwoDGraphics";
    public static readonly DotnetPackagingAdditionalCategory VectorGraphics = (DotnetPackagingAdditionalCategory) "VectorGraphics";
    public static readonly DotnetPackagingAdditionalCategory RasterGraphics = (DotnetPackagingAdditionalCategory) "RasterGraphics";
    public static readonly DotnetPackagingAdditionalCategory ThreeDGraphics = (DotnetPackagingAdditionalCategory) "ThreeDGraphics";
    public static readonly DotnetPackagingAdditionalCategory Scanning = (DotnetPackagingAdditionalCategory) "Scanning";
    public static readonly DotnetPackagingAdditionalCategory OCR = (DotnetPackagingAdditionalCategory) "OCR";
    public static readonly DotnetPackagingAdditionalCategory Photography = (DotnetPackagingAdditionalCategory) "Photography";
    public static readonly DotnetPackagingAdditionalCategory Publishing = (DotnetPackagingAdditionalCategory) "Publishing";
    public static readonly DotnetPackagingAdditionalCategory Viewer = (DotnetPackagingAdditionalCategory) "Viewer";
    public static readonly DotnetPackagingAdditionalCategory TextTools = (DotnetPackagingAdditionalCategory) "TextTools";
    public static readonly DotnetPackagingAdditionalCategory DesktopSettings = (DotnetPackagingAdditionalCategory) "DesktopSettings";
    public static readonly DotnetPackagingAdditionalCategory HardwareSettings = (DotnetPackagingAdditionalCategory) "HardwareSettings";
    public static readonly DotnetPackagingAdditionalCategory Printing = (DotnetPackagingAdditionalCategory) "Printing";
    public static readonly DotnetPackagingAdditionalCategory PackageManager = (DotnetPackagingAdditionalCategory) "PackageManager";
    public static readonly DotnetPackagingAdditionalCategory Dialup = (DotnetPackagingAdditionalCategory) "Dialup";
    public static readonly DotnetPackagingAdditionalCategory InstantMessaging = (DotnetPackagingAdditionalCategory) "InstantMessaging";
    public static readonly DotnetPackagingAdditionalCategory Chat = (DotnetPackagingAdditionalCategory) "Chat";
    public static readonly DotnetPackagingAdditionalCategory IRCClient = (DotnetPackagingAdditionalCategory) "IRCClient";
    public static readonly DotnetPackagingAdditionalCategory FileTransfer = (DotnetPackagingAdditionalCategory) "FileTransfer";
    public static readonly DotnetPackagingAdditionalCategory HamRadio = (DotnetPackagingAdditionalCategory) "HamRadio";
    public static readonly DotnetPackagingAdditionalCategory News = (DotnetPackagingAdditionalCategory) "News";
    public static readonly DotnetPackagingAdditionalCategory P2P = (DotnetPackagingAdditionalCategory) "P2P";
    public static readonly DotnetPackagingAdditionalCategory RemoteAccess = (DotnetPackagingAdditionalCategory) "RemoteAccess";
    public static readonly DotnetPackagingAdditionalCategory Telephony = (DotnetPackagingAdditionalCategory) "Telephony";
    public static readonly DotnetPackagingAdditionalCategory TelephonyTools = (DotnetPackagingAdditionalCategory) "TelephonyTools";
    public static readonly DotnetPackagingAdditionalCategory VideoConference = (DotnetPackagingAdditionalCategory) "VideoConference";
    public static readonly DotnetPackagingAdditionalCategory WebBrowser = (DotnetPackagingAdditionalCategory) "WebBrowser";
    public static readonly DotnetPackagingAdditionalCategory WebDevelopment = (DotnetPackagingAdditionalCategory) "WebDevelopment";
    public static readonly DotnetPackagingAdditionalCategory Midi = (DotnetPackagingAdditionalCategory) "Midi";
    public static readonly DotnetPackagingAdditionalCategory Mixer = (DotnetPackagingAdditionalCategory) "Mixer";
    public static readonly DotnetPackagingAdditionalCategory Sequencer = (DotnetPackagingAdditionalCategory) "Sequencer";
    public static readonly DotnetPackagingAdditionalCategory Tuner = (DotnetPackagingAdditionalCategory) "Tuner";
    public static readonly DotnetPackagingAdditionalCategory TV = (DotnetPackagingAdditionalCategory) "TV";
    public static readonly DotnetPackagingAdditionalCategory AudioVideoEditing = (DotnetPackagingAdditionalCategory) "AudioVideoEditing";
    public static readonly DotnetPackagingAdditionalCategory Player = (DotnetPackagingAdditionalCategory) "Player";
    public static readonly DotnetPackagingAdditionalCategory Recorder = (DotnetPackagingAdditionalCategory) "Recorder";
    public static readonly DotnetPackagingAdditionalCategory DiscBurning = (DotnetPackagingAdditionalCategory) "DiscBurning";
    public static readonly DotnetPackagingAdditionalCategory ActionGame = (DotnetPackagingAdditionalCategory) "ActionGame";
    public static readonly DotnetPackagingAdditionalCategory AdventureGame = (DotnetPackagingAdditionalCategory) "AdventureGame";
    public static readonly DotnetPackagingAdditionalCategory ArcadeGame = (DotnetPackagingAdditionalCategory) "ArcadeGame";
    public static readonly DotnetPackagingAdditionalCategory BoardGame = (DotnetPackagingAdditionalCategory) "BoardGame";
    public static readonly DotnetPackagingAdditionalCategory BlocksGame = (DotnetPackagingAdditionalCategory) "BlocksGame";
    public static readonly DotnetPackagingAdditionalCategory CardGame = (DotnetPackagingAdditionalCategory) "CardGame";
    public static readonly DotnetPackagingAdditionalCategory KidsGame = (DotnetPackagingAdditionalCategory) "KidsGame";
    public static readonly DotnetPackagingAdditionalCategory LogicGame = (DotnetPackagingAdditionalCategory) "LogicGame";
    public static readonly DotnetPackagingAdditionalCategory RolePlaying = (DotnetPackagingAdditionalCategory) "RolePlaying";
    public static readonly DotnetPackagingAdditionalCategory Simulation = (DotnetPackagingAdditionalCategory) "Simulation";
    public static readonly DotnetPackagingAdditionalCategory SportsGame = (DotnetPackagingAdditionalCategory) "SportsGame";
    public static readonly DotnetPackagingAdditionalCategory StrategyGame = (DotnetPackagingAdditionalCategory) "StrategyGame";
    public static readonly DotnetPackagingAdditionalCategory Art = (DotnetPackagingAdditionalCategory) "Art";
    public static readonly DotnetPackagingAdditionalCategory Construction = (DotnetPackagingAdditionalCategory) "Construction";
    public static readonly DotnetPackagingAdditionalCategory Music = (DotnetPackagingAdditionalCategory) "Music";
    public static readonly DotnetPackagingAdditionalCategory Languages = (DotnetPackagingAdditionalCategory) "Languages";
    public static readonly DotnetPackagingAdditionalCategory Science = (DotnetPackagingAdditionalCategory) "Science";
    public static readonly DotnetPackagingAdditionalCategory ArtificialIntelligence = (DotnetPackagingAdditionalCategory) "ArtificialIntelligence";
    public static readonly DotnetPackagingAdditionalCategory Astronomy = (DotnetPackagingAdditionalCategory) "Astronomy";
    public static readonly DotnetPackagingAdditionalCategory Biology = (DotnetPackagingAdditionalCategory) "Biology";
    public static readonly DotnetPackagingAdditionalCategory Chemistry = (DotnetPackagingAdditionalCategory) "Chemistry";
    public static readonly DotnetPackagingAdditionalCategory ComputerScience = (DotnetPackagingAdditionalCategory) "ComputerScience";
    public static readonly DotnetPackagingAdditionalCategory DataVisualization = (DotnetPackagingAdditionalCategory) "DataVisualization";
    public static readonly DotnetPackagingAdditionalCategory Economy = (DotnetPackagingAdditionalCategory) "Economy";
    public static readonly DotnetPackagingAdditionalCategory Electricity = (DotnetPackagingAdditionalCategory) "Electricity";
    public static readonly DotnetPackagingAdditionalCategory Geography = (DotnetPackagingAdditionalCategory) "Geography";
    public static readonly DotnetPackagingAdditionalCategory Geology = (DotnetPackagingAdditionalCategory) "Geology";
    public static readonly DotnetPackagingAdditionalCategory Geoscience = (DotnetPackagingAdditionalCategory) "Geoscience";
    public static readonly DotnetPackagingAdditionalCategory History = (DotnetPackagingAdditionalCategory) "History";
    public static readonly DotnetPackagingAdditionalCategory ImageProcessing = (DotnetPackagingAdditionalCategory) "ImageProcessing";
    public static readonly DotnetPackagingAdditionalCategory Literature = (DotnetPackagingAdditionalCategory) "Literature";
    public static readonly DotnetPackagingAdditionalCategory Math = (DotnetPackagingAdditionalCategory) "Math";
    public static readonly DotnetPackagingAdditionalCategory NumericalAnalysis = (DotnetPackagingAdditionalCategory) "NumericalAnalysis";
    public static readonly DotnetPackagingAdditionalCategory MedicalSoftware = (DotnetPackagingAdditionalCategory) "MedicalSoftware";
    public static readonly DotnetPackagingAdditionalCategory Physics = (DotnetPackagingAdditionalCategory) "Physics";
    public static readonly DotnetPackagingAdditionalCategory Robotics = (DotnetPackagingAdditionalCategory) "Robotics";
    public static readonly DotnetPackagingAdditionalCategory Sports = (DotnetPackagingAdditionalCategory) "Sports";
    public static readonly DotnetPackagingAdditionalCategory ParallelComputing = (DotnetPackagingAdditionalCategory) "ParallelComputing";
    public static readonly DotnetPackagingAdditionalCategory Amusement = (DotnetPackagingAdditionalCategory) "Amusement";
    public static readonly DotnetPackagingAdditionalCategory Archiving = (DotnetPackagingAdditionalCategory) "Archiving";
    public static readonly DotnetPackagingAdditionalCategory Compression = (DotnetPackagingAdditionalCategory) "Compression";
    public static readonly DotnetPackagingAdditionalCategory Electronics = (DotnetPackagingAdditionalCategory) "Electronics";
    public static readonly DotnetPackagingAdditionalCategory Emulator = (DotnetPackagingAdditionalCategory) "Emulator";
    public static readonly DotnetPackagingAdditionalCategory Engineering = (DotnetPackagingAdditionalCategory) "Engineering";
    public static readonly DotnetPackagingAdditionalCategory FileTools = (DotnetPackagingAdditionalCategory) "FileTools";
    public static readonly DotnetPackagingAdditionalCategory FileManager = (DotnetPackagingAdditionalCategory) "FileManager";
    public static readonly DotnetPackagingAdditionalCategory TerminalEmulator = (DotnetPackagingAdditionalCategory) "TerminalEmulator";
    public static readonly DotnetPackagingAdditionalCategory Filesystem = (DotnetPackagingAdditionalCategory) "Filesystem";
    public static readonly DotnetPackagingAdditionalCategory Monitor = (DotnetPackagingAdditionalCategory) "Monitor";
    public static readonly DotnetPackagingAdditionalCategory Security = (DotnetPackagingAdditionalCategory) "Security";
    public static readonly DotnetPackagingAdditionalCategory Accessibility = (DotnetPackagingAdditionalCategory) "Accessibility";
    public static readonly DotnetPackagingAdditionalCategory Calculator = (DotnetPackagingAdditionalCategory) "Calculator";
    public static readonly DotnetPackagingAdditionalCategory Clock = (DotnetPackagingAdditionalCategory) "Clock";
    public static readonly DotnetPackagingAdditionalCategory TextEditor = (DotnetPackagingAdditionalCategory) "TextEditor";
    public static readonly DotnetPackagingAdditionalCategory Documentation = (DotnetPackagingAdditionalCategory) "Documentation";
    public static readonly DotnetPackagingAdditionalCategory Core = (DotnetPackagingAdditionalCategory) "Core";
    public static readonly DotnetPackagingAdditionalCategory KDE = (DotnetPackagingAdditionalCategory) "KDE";
    public static readonly DotnetPackagingAdditionalCategory GNOME = (DotnetPackagingAdditionalCategory) "GNOME";
    public static readonly DotnetPackagingAdditionalCategory GTK = (DotnetPackagingAdditionalCategory) "GTK";
    public static readonly DotnetPackagingAdditionalCategory Qt = (DotnetPackagingAdditionalCategory) "Qt";
    public static readonly DotnetPackagingAdditionalCategory Motif = (DotnetPackagingAdditionalCategory) "Motif";
    public static readonly DotnetPackagingAdditionalCategory Java = (DotnetPackagingAdditionalCategory) "Java";
    public static readonly DotnetPackagingAdditionalCategory ConsoleOnly = (DotnetPackagingAdditionalCategory) "ConsoleOnly";
    public static implicit operator DotnetPackagingAdditionalCategory(string value)
    {
        return new DotnetPackagingAdditionalCategory { Value = value };
    }
}
#endregion
