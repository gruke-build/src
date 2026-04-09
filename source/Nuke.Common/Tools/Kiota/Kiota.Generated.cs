// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

// Generated from https://github.com/gruke-build/src/blob/master/source/Nuke.Common/Tools/Kiota/Kiota.json

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

namespace Nuke.Common.Tools.Kiota;

/// <summary><p>Kiota is a command line tool for generating an API client to call any OpenAPI-described API you're interested in. The goal is to eliminate the need to take a dependency on a different API client library for every API that you need to call. Kiota API clients provide a strongly typed experience with all the features you expect from a high quality API SDK, but without having to learn a new library for every HTTP API.</p><p>For more details, visit the <a href="https://learn.microsoft.com/en-gb/openapi/kiota/">official website</a>.</p></summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
[NuGetTool(Id = PackageId, Executable = PackageExecutable)]
public partial class KiotaTasks : ToolTasks, IRequireNuGetPackage
{
    public static string KiotaPath { get => new KiotaTasks().GetToolPathInternal(); set => new KiotaTasks().SetToolPath(value); }
    public const string PackageId = "Microsoft.OpenApi.Kiota";
    public const string PackageExecutable = "kiota.dll";
    /// <summary><p>Kiota is a command line tool for generating an API client to call any OpenAPI-described API you're interested in. The goal is to eliminate the need to take a dependency on a different API client library for every API that you need to call. Kiota API clients provide a strongly typed experience with all the features you expect from a high quality API SDK, but without having to learn a new library for every HTTP API.</p><p>For more details, visit the <a href="https://learn.microsoft.com/en-gb/openapi/kiota/">official website</a>.</p></summary>
    public static IReadOnlyCollection<Output> Kiota(ArgumentStringHandler arguments, string workingDirectory = null, IReadOnlyDictionary<string, string> environmentVariables = null, int? timeout = null, bool? logOutput = null, bool? logInvocation = null, Action<OutputType, string> logger = null, Func<IProcess, object> exitHandler = null) => new KiotaTasks().Run(arguments, workingDirectory, environmentVariables, timeout, logOutput, logInvocation, logger, exitHandler);
    /// <summary><p>Search for APIs and their description from various registries.</p><p>For more details, visit the <a href="https://learn.microsoft.com/en-gb/openapi/kiota/">official website</a>.</p></summary>
    /// <remarks><p>This is a <a href="https://nuke.greemdev.net/release/common/cli-tools/#fluent-api">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p><ul><li><c>&lt;searchTerm&gt;</c> via <see cref="KiotaSearchSettings.SearchTerm"/></li><li><c>--clear-cache</c> via <see cref="KiotaSearchSettings.ClearCache"/></li><li><c>--log-level</c> via <see cref="KiotaSearchSettings.LogLevel"/></li><li><c>--version</c> via <see cref="KiotaSearchSettings.Version"/></li></ul></remarks>
    public static IReadOnlyCollection<Output> KiotaSearch(KiotaSearchSettings options = null) => new KiotaTasks().Run<KiotaSearchSettings>(options);
    /// <inheritdoc cref="KiotaTasks.KiotaSearch(Nuke.Common.Tools.Kiota.KiotaSearchSettings)"/>
    public static IReadOnlyCollection<Output> KiotaSearch(Configure<KiotaSearchSettings> configurator) => new KiotaTasks().Run<KiotaSearchSettings>(configurator.Invoke(new KiotaSearchSettings()));
    /// <inheritdoc cref="KiotaTasks.KiotaSearch(Nuke.Common.Tools.Kiota.KiotaSearchSettings)"/>
    public static IEnumerable<(KiotaSearchSettings Settings, IReadOnlyCollection<Output> Output)> KiotaSearch(CombinatorialConfigure<KiotaSearchSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false) => configurator.Invoke(KiotaSearch, degreeOfParallelism, completeOnFailure);
    /// <summary><p>Download an API description.</p><p>For more details, visit the <a href="https://learn.microsoft.com/en-gb/openapi/kiota/">official website</a>.</p></summary>
    /// <remarks><p>This is a <a href="https://nuke.greemdev.net/release/common/cli-tools/#fluent-api">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p><ul><li><c>&lt;searchTerm&gt;</c> via <see cref="KiotaDownloadSettings.SearchTerm"/></li><li><c>--clean-output</c> via <see cref="KiotaDownloadSettings.CleanOutput"/></li><li><c>--clear-cache</c> via <see cref="KiotaDownloadSettings.ClearCache"/></li><li><c>--disable-ssl-validation</c> via <see cref="KiotaDownloadSettings.DisableSslValidation"/></li><li><c>--log-level</c> via <see cref="KiotaDownloadSettings.LogLevel"/></li><li><c>--output</c> via <see cref="KiotaDownloadSettings.OutputPath"/></li><li><c>--version</c> via <see cref="KiotaDownloadSettings.Version"/></li></ul></remarks>
    public static IReadOnlyCollection<Output> KiotaDownload(KiotaDownloadSettings options = null) => new KiotaTasks().Run<KiotaDownloadSettings>(options);
    /// <inheritdoc cref="KiotaTasks.KiotaDownload(Nuke.Common.Tools.Kiota.KiotaDownloadSettings)"/>
    public static IReadOnlyCollection<Output> KiotaDownload(Configure<KiotaDownloadSettings> configurator) => new KiotaTasks().Run<KiotaDownloadSettings>(configurator.Invoke(new KiotaDownloadSettings()));
    /// <inheritdoc cref="KiotaTasks.KiotaDownload(Nuke.Common.Tools.Kiota.KiotaDownloadSettings)"/>
    public static IEnumerable<(KiotaDownloadSettings Settings, IReadOnlyCollection<Output> Output)> KiotaDownload(CombinatorialConfigure<KiotaDownloadSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false) => configurator.Invoke(KiotaDownload, degreeOfParallelism, completeOnFailure);
    /// <summary><p>Show the API paths tree for an API description.</p><p>For more details, visit the <a href="https://learn.microsoft.com/en-gb/openapi/kiota/">official website</a>.</p></summary>
    /// <remarks><p>This is a <a href="https://nuke.greemdev.net/release/common/cli-tools/#fluent-api">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p><ul><li><c>--clean-output</c> via <see cref="KiotaShowSettings.CleanOutput"/></li><li><c>--clear-cache</c> via <see cref="KiotaShowSettings.ClearCache"/></li><li><c>--disable-ssl-validation</c> via <see cref="KiotaShowSettings.DisableSslValidation"/></li><li><c>--exclude-path</c> via <see cref="KiotaShowSettings.ExcludePaths"/></li><li><c>--include-path</c> via <see cref="KiotaShowSettings.IncludePaths"/></li><li><c>--log-level</c> via <see cref="KiotaShowSettings.LogLevel"/></li><li><c>--max-depth</c> via <see cref="KiotaShowSettings.MaxDepth"/></li><li><c>--openapi</c> via <see cref="KiotaShowSettings.OpenApiDescription"/></li><li><c>--output</c> via <see cref="KiotaShowSettings.OutputPath"/></li><li><c>--search-key</c> via <see cref="KiotaShowSettings.SearchKey"/></li><li><c>--version</c> via <see cref="KiotaShowSettings.Version"/></li></ul></remarks>
    public static IReadOnlyCollection<Output> KiotaShow(KiotaShowSettings options = null) => new KiotaTasks().Run<KiotaShowSettings>(options);
    /// <inheritdoc cref="KiotaTasks.KiotaShow(Nuke.Common.Tools.Kiota.KiotaShowSettings)"/>
    public static IReadOnlyCollection<Output> KiotaShow(Configure<KiotaShowSettings> configurator) => new KiotaTasks().Run<KiotaShowSettings>(configurator.Invoke(new KiotaShowSettings()));
    /// <inheritdoc cref="KiotaTasks.KiotaShow(Nuke.Common.Tools.Kiota.KiotaShowSettings)"/>
    public static IEnumerable<(KiotaShowSettings Settings, IReadOnlyCollection<Output> Output)> KiotaShow(CombinatorialConfigure<KiotaShowSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false) => configurator.Invoke(KiotaShow, degreeOfParallelism, completeOnFailure);
    /// <summary><p>Generate a client for any API from its description.</p><p>For more details, visit the <a href="https://learn.microsoft.com/en-gb/openapi/kiota/">official website</a>.</p></summary>
    /// <remarks><p>This is a <a href="https://nuke.greemdev.net/release/common/cli-tools/#fluent-api">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p><ul><li><c>--additional-data</c> via <see cref="KiotaGenerateSettings.AdditionalData"/></li><li><c>--backing-store</c> via <see cref="KiotaGenerateSettings.BackingStore"/></li><li><c>--class-name</c> via <see cref="KiotaGenerateSettings.ClassName"/></li><li><c>--clean-output</c> via <see cref="KiotaGenerateSettings.CleanOutput"/></li><li><c>--clear-cache</c> via <see cref="KiotaGenerateSettings.ClearCache"/></li><li><c>--deserializer</c> via <see cref="KiotaGenerateSettings.DeserializerClasses"/></li><li><c>--disable-ssl-validation</c> via <see cref="KiotaGenerateSettings.DisableSslValidation"/></li><li><c>--exclude-backward-compatible</c> via <see cref="KiotaGenerateSettings.ExcludeBackwardsCompatible"/></li><li><c>--exclude-path</c> via <see cref="KiotaGenerateSettings.ExcludePaths"/></li><li><c>--include-path</c> via <see cref="KiotaGenerateSettings.IncludePaths"/></li><li><c>--language</c> via <see cref="KiotaGenerateSettings.TargetLanguage"/></li><li><c>--log-level</c> via <see cref="KiotaGenerateSettings.LogLevel"/></li><li><c>--namespace-name</c> via <see cref="KiotaGenerateSettings.NamespaceName"/></li><li><c>--openapi</c> via <see cref="KiotaGenerateSettings.OpenApiDescription"/></li><li><c>--output</c> via <see cref="KiotaGenerateSettings.OutputPath"/></li><li><c>--serializer</c> via <see cref="KiotaGenerateSettings.SerializerClasses"/></li><li><c>--structured-mime-types</c> via <see cref="KiotaGenerateSettings.StructuredMimeTypes"/></li><li><c>--type-access-modifier</c> via <see cref="KiotaGenerateSettings.TypeAccessModifier"/></li><li><c>--version</c> via <see cref="KiotaGenerateSettings.Version"/></li></ul></remarks>
    public static IReadOnlyCollection<Output> KiotaGenerate(KiotaGenerateSettings options = null) => new KiotaTasks().Run<KiotaGenerateSettings>(options);
    /// <inheritdoc cref="KiotaTasks.KiotaGenerate(Nuke.Common.Tools.Kiota.KiotaGenerateSettings)"/>
    public static IReadOnlyCollection<Output> KiotaGenerate(Configure<KiotaGenerateSettings> configurator) => new KiotaTasks().Run<KiotaGenerateSettings>(configurator.Invoke(new KiotaGenerateSettings()));
    /// <inheritdoc cref="KiotaTasks.KiotaGenerate(Nuke.Common.Tools.Kiota.KiotaGenerateSettings)"/>
    public static IEnumerable<(KiotaGenerateSettings Settings, IReadOnlyCollection<Output> Output)> KiotaGenerate(CombinatorialConfigure<KiotaGenerateSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false) => configurator.Invoke(KiotaGenerate, degreeOfParallelism, completeOnFailure);
    /// <summary><p>Update existing clients from previous generations. This command searches for lock files in the output directory and all its subdirectories and triggers generations to refresh the existing clients using settings from the lock files.</p><p>For more details, visit the <a href="https://learn.microsoft.com/en-gb/openapi/kiota/">official website</a>.</p></summary>
    /// <remarks><p>This is a <a href="https://nuke.greemdev.net/release/common/cli-tools/#fluent-api">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p><ul><li><c>--clean-output</c> via <see cref="KiotaUpdateSettings.CleanOutput"/></li><li><c>--clear-cache</c> via <see cref="KiotaUpdateSettings.ClearCache"/></li><li><c>--log-level</c> via <see cref="KiotaUpdateSettings.LogLevel"/></li><li><c>--output</c> via <see cref="KiotaUpdateSettings.OutputPath"/></li></ul></remarks>
    public static IReadOnlyCollection<Output> KiotaUpdate(KiotaUpdateSettings options = null) => new KiotaTasks().Run<KiotaUpdateSettings>(options);
    /// <inheritdoc cref="KiotaTasks.KiotaUpdate(Nuke.Common.Tools.Kiota.KiotaUpdateSettings)"/>
    public static IReadOnlyCollection<Output> KiotaUpdate(Configure<KiotaUpdateSettings> configurator) => new KiotaTasks().Run<KiotaUpdateSettings>(configurator.Invoke(new KiotaUpdateSettings()));
    /// <inheritdoc cref="KiotaTasks.KiotaUpdate(Nuke.Common.Tools.Kiota.KiotaUpdateSettings)"/>
    public static IEnumerable<(KiotaUpdateSettings Settings, IReadOnlyCollection<Output> Output)> KiotaUpdate(CombinatorialConfigure<KiotaUpdateSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false) => configurator.Invoke(KiotaUpdate, degreeOfParallelism, completeOnFailure);
    /// <summary><p>Show languages and runtime dependencies information.</p><p>For more details, visit the <a href="https://learn.microsoft.com/en-gb/openapi/kiota/">official website</a>.</p></summary>
    /// <remarks><p>This is a <a href="https://nuke.greemdev.net/release/common/cli-tools/#fluent-api">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p><ul><li><c>--clear-cache</c> via <see cref="KiotaInfoSettings.ClearCache"/></li><li><c>--dependency-type</c> via <see cref="KiotaInfoSettings.DependencyTypes"/></li><li><c>--json</c> via <see cref="KiotaInfoSettings.Json"/></li><li><c>--language</c> via <see cref="KiotaInfoSettings.TargetLanguage"/></li><li><c>--log-level</c> via <see cref="KiotaInfoSettings.LogLevel"/></li><li><c>--openapi</c> via <see cref="KiotaInfoSettings.OpenApiDescription"/></li><li><c>--search-key</c> via <see cref="KiotaInfoSettings.SearchKey"/></li><li><c>--version</c> via <see cref="KiotaInfoSettings.Version"/></li></ul></remarks>
    public static IReadOnlyCollection<Output> KiotaInfo(KiotaInfoSettings options = null) => new KiotaTasks().Run<KiotaInfoSettings>(options);
    /// <inheritdoc cref="KiotaTasks.KiotaInfo(Nuke.Common.Tools.Kiota.KiotaInfoSettings)"/>
    public static IReadOnlyCollection<Output> KiotaInfo(Configure<KiotaInfoSettings> configurator) => new KiotaTasks().Run<KiotaInfoSettings>(configurator.Invoke(new KiotaInfoSettings()));
    /// <inheritdoc cref="KiotaTasks.KiotaInfo(Nuke.Common.Tools.Kiota.KiotaInfoSettings)"/>
    public static IEnumerable<(KiotaInfoSettings Settings, IReadOnlyCollection<Output> Output)> KiotaInfo(CombinatorialConfigure<KiotaInfoSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false) => configurator.Invoke(KiotaInfo, degreeOfParallelism, completeOnFailure);
    /// <summary><p>Signs in to private API descriptions repositories. Currently only GitHub is supported.</p><p>For more details, visit the <a href="https://learn.microsoft.com/en-gb/openapi/kiota/">official website</a>.</p></summary>
    /// <remarks><p>This is a <a href="https://nuke.greemdev.net/release/common/cli-tools/#fluent-api">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p><ul><li><c>&lt;loginMethod&gt;</c> via <see cref="KiotaLoginSettings.LoginMethod"/></li><li><c>--log-level</c> via <see cref="KiotaLoginSettings.LogLevel"/></li><li><c>--pat</c> via <see cref="KiotaLoginSettings.AccessToken"/></li></ul></remarks>
    public static IReadOnlyCollection<Output> KiotaLogin(KiotaLoginSettings options = null) => new KiotaTasks().Run<KiotaLoginSettings>(options);
    /// <inheritdoc cref="KiotaTasks.KiotaLogin(Nuke.Common.Tools.Kiota.KiotaLoginSettings)"/>
    public static IReadOnlyCollection<Output> KiotaLogin(Configure<KiotaLoginSettings> configurator) => new KiotaTasks().Run<KiotaLoginSettings>(configurator.Invoke(new KiotaLoginSettings()));
    /// <inheritdoc cref="KiotaTasks.KiotaLogin(Nuke.Common.Tools.Kiota.KiotaLoginSettings)"/>
    public static IEnumerable<(KiotaLoginSettings Settings, IReadOnlyCollection<Output> Output)> KiotaLogin(CombinatorialConfigure<KiotaLoginSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false) => configurator.Invoke(KiotaLogin, degreeOfParallelism, completeOnFailure);
    /// <summary><p>Signs out from private API descriptions repositories. Currently only GitHub is supported.</p><p>For more details, visit the <a href="https://learn.microsoft.com/en-gb/openapi/kiota/">official website</a>.</p></summary>
    /// <remarks><p>This is a <a href="https://nuke.greemdev.net/release/common/cli-tools/#fluent-api">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p><ul><li><c>--log-level</c> via <see cref="KiotaLogoutSettings.LogLevel"/></li></ul></remarks>
    public static IReadOnlyCollection<Output> KiotaLogout(KiotaLogoutSettings options = null) => new KiotaTasks().Run<KiotaLogoutSettings>(options);
    /// <inheritdoc cref="KiotaTasks.KiotaLogout(Nuke.Common.Tools.Kiota.KiotaLogoutSettings)"/>
    public static IReadOnlyCollection<Output> KiotaLogout(Configure<KiotaLogoutSettings> configurator) => new KiotaTasks().Run<KiotaLogoutSettings>(configurator.Invoke(new KiotaLogoutSettings()));
    /// <inheritdoc cref="KiotaTasks.KiotaLogout(Nuke.Common.Tools.Kiota.KiotaLogoutSettings)"/>
    public static IEnumerable<(KiotaLogoutSettings Settings, IReadOnlyCollection<Output> Output)> KiotaLogout(CombinatorialConfigure<KiotaLogoutSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false) => configurator.Invoke(KiotaLogout, degreeOfParallelism, completeOnFailure);
}
#region KiotaSearchSettings
/// <inheritdoc cref="KiotaTasks.KiotaSearch(Nuke.Common.Tools.Kiota.KiotaSearchSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Command(Type = typeof(KiotaTasks), Command = nameof(KiotaTasks.KiotaSearch), Arguments = "search")]
public partial class KiotaSearchSettings : ToolOptions
{
    /// <summary></summary>
    [Argument(Format = "{value}", Position = 1)] public string SearchTerm => Get<string>(() => SearchTerm);
    /// <summary>The log level to use when logging events to the main output. Defaults to <c>warning</c>.</summary>
    [Argument(Format = "--log-level {value}")] public KiotaLogLevel LogLevel => Get<KiotaLogLevel>(() => LogLevel);
    /// <summary>Select a specific version of the API description. No default value.</summary>
    [Argument(Format = "--version {value}")] public string Version => Get<string>(() => Version);
    /// <summary>Clears the currently cached file for the command. Defaults to false.<br/><br/>Cached files are stored under '%TEMP%/kiota/cache' and valid for one (1) hour after the initial download. Kiota caches API descriptions during generation and static index files during search.</summary>
    [Argument(Format = "--clear-cache")] public bool? ClearCache => Get<bool?>(() => ClearCache);
}
#endregion
#region KiotaDownloadSettings
/// <inheritdoc cref="KiotaTasks.KiotaDownload(Nuke.Common.Tools.Kiota.KiotaDownloadSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Command(Type = typeof(KiotaTasks), Command = nameof(KiotaTasks.KiotaDownload), Arguments = "download")]
public partial class KiotaDownloadSettings : ToolOptions
{
    /// <summary></summary>
    [Argument(Format = "{value}", Position = 1)] public string SearchTerm => Get<string>(() => SearchTerm);
    /// <summary>The log level to use when logging events to the main output. Defaults to <c>warning</c>.</summary>
    [Argument(Format = "--log-level {value}")] public KiotaLogLevel LogLevel => Get<KiotaLogLevel>(() => LogLevel);
    /// <summary>Select a specific version of the API description. No default value.</summary>
    [Argument(Format = "--version {value}")] public string Version => Get<string>(() => Version);
    /// <summary>Clears the currently cached file for the command. Defaults to false.<br/><br/>Cached files are stored under '%TEMP%/kiota/cache' and valid for one (1) hour after the initial download. Kiota caches API descriptions during generation and static index files during search.</summary>
    [Argument(Format = "--clear-cache")] public bool? ClearCache => Get<bool?>(() => ClearCache);
    /// <summary>Delete the output directory before generating the client. Defaults to false.</summary>
    [Argument(Format = "--clean-output")] public bool? CleanOutput => Get<bool?>(() => CleanOutput);
    /// <summary>The output directory or file path for the generated code files. Defaults to <c>./output</c> for generate and <c>./output/result.json</c> for download.</summary>
    [Argument(Format = "--output {value}")] public string OutputPath => Get<string>(() => OutputPath);
    /// <summary>Select a specific version of the API description. No default value.</summary>
    [Argument(Format = "--disable-ssl-validation")] public bool? DisableSslValidation => Get<bool?>(() => DisableSslValidation);
}
#endregion
#region KiotaShowSettings
/// <inheritdoc cref="KiotaTasks.KiotaShow(Nuke.Common.Tools.Kiota.KiotaShowSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Command(Type = typeof(KiotaTasks), Command = nameof(KiotaTasks.KiotaShow), Arguments = "show")]
public partial class KiotaShowSettings : ToolOptions
{
    /// <summary></summary>
    [Argument(Format = "--max-depth {value}")] public int? MaxDepth => Get<int?>(() => MaxDepth);
    /// <summary>The location of the OpenAPI description in JSON or YAML format to use to generate the SDK. Accepts a URL or a local path.</summary>
    [Argument(Format = "--openapi {value}")] public string OpenApiDescription => Get<string>(() => OpenApiDescription);
    /// <summary>The log level to use when logging events to the main output. Defaults to <c>warning</c>.</summary>
    [Argument(Format = "--log-level {value}")] public KiotaLogLevel LogLevel => Get<KiotaLogLevel>(() => LogLevel);
    /// <summary>Select a specific version of the API description. No default value.</summary>
    [Argument(Format = "--version {value}")] public string Version => Get<string>(() => Version);
    /// <summary>The search key to use to fetch the Open API description. This parameter can be used in combination with the version option. Shouldn't be used in combination with the <c>--openapi</c> option. Default empty.</summary>
    [Argument(Format = "--search-key {value}")] public string SearchKey => Get<string>(() => SearchKey);
    /// <summary>Clears the currently cached file for the command. Defaults to false.<br/><br/>Cached files are stored under '%TEMP%/kiota/cache' and valid for one (1) hour after the initial download. Kiota caches API descriptions during generation and static index files during search.</summary>
    [Argument(Format = "--clear-cache")] public bool? ClearCache => Get<bool?>(() => ClearCache);
    /// <summary>Delete the output directory before generating the client. Defaults to false.</summary>
    [Argument(Format = "--clean-output")] public bool? CleanOutput => Get<bool?>(() => CleanOutput);
    /// <summary>The output directory or file path for the generated code files. Defaults to <c>./output</c> for generate and <c>./output/result.json</c> for download.</summary>
    [Argument(Format = "--output {value}")] public string OutputPath => Get<string>(() => OutputPath);
    /// <summary>Select a specific version of the API description. No default value.</summary>
    [Argument(Format = "--disable-ssl-validation")] public bool? DisableSslValidation => Get<bool?>(() => DisableSslValidation);
    /// <summary>A glob pattern to include paths from generation. Accepts multiple values. If this parameter is absent, everything is included.<br/><br/>You can also filter specific HTTP methods by appending <c>#METHOD</c> to the pattern, replacing <c>METHOD</c> with the HTTP method to filter. For example, <c>**/users/**#GET</c>.</summary>
    [Argument(Format = "--include-path {value}")] public IReadOnlyList<string> IncludePaths => Get<List<string>>(() => IncludePaths);
    /// <summary>A glob pattern to exclude paths from generation. Accepts multiple values. If this parameter is absent, everything is included.<br/><br/>You can also filter specific HTTP methods by appending <c>#METHOD</c> to the pattern, replacing <c>METHOD</c> with the HTTP method to filter. For example, <c>**/users/**#GET</c>.</summary>
    [Argument(Format = "--exclude-path {value}")] public IReadOnlyList<string> ExcludePaths => Get<List<string>>(() => ExcludePaths);
}
#endregion
#region KiotaGenerateSettings
/// <inheritdoc cref="KiotaTasks.KiotaGenerate(Nuke.Common.Tools.Kiota.KiotaGenerateSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Command(Type = typeof(KiotaTasks), Command = nameof(KiotaTasks.KiotaGenerate), Arguments = "generate")]
public partial class KiotaGenerateSettings : ToolOptions
{
    /// <summary>The class name to use for the core client class. Defaults to <c>ApiClient</c>. The provided name MUST be a valid class name for the target language.</summary>
    [Argument(Format = "--class-name {value}")] public string ClassName => Get<string>(() => ClassName);
    /// <summary>The namespace to use for the core client class specified with the <c>--class-name</c> option. Defaults to <c>ApiSdk</c>. The provided name MUST be a valid module or namespace name for the target language.</summary>
    [Argument(Format = "--namespace-name {value}")] public string NamespaceName => Get<string>(() => NamespaceName);
    /// <summary></summary>
    [Argument(Format = "--backing-store")] public bool? BackingStore => Get<bool?>(() => BackingStore);
    /// <summary>The fully qualified class names for deserializers.<br/><br/>These values are used in the client class to initialize the deserialization providers.<br/><br/>Since version 1.11 this parameter also supports a none key to generate a client with no deserialization providers in order to enable better portability. Defaults visible on Microsoft documentation.</summary>
    [Argument(Format = "--deserializer {value}")] public IReadOnlyList<string> DeserializerClasses => Get<List<string>>(() => DeserializerClasses);
    /// <summary>The fully qualified class names for serializers.<br/><br/>These values are used in the client class to initialize the serialization providers.<br/><br/>Since version 1.11 this parameter also supports a none key to generate a client with no serialization providers in order to enable better portability. Defaults visible on Microsoft documentation.</summary>
    [Argument(Format = "--serializer {value}")] public IReadOnlyList<string> SerializerClasses => Get<List<string>>(() => SerializerClasses);
    /// <summary>The MIME types to use for structured data model generation with their preference weight. Any type without a preference has its preference defaulted to 1. Accepts multiple values. The notation style and the preference weight logic follow the convention <a href="https://www.rfc-editor.org/rfc/rfc9110.html#name-accept">defined in RFC9110</a>. Default values: <c>application/json;q=1</c>, <c>application/x-www-form-urlencoded;q=0.2</c>, <c>multipart/form-data;q=0.1</c>, <c>text/plain;q=0.9</c></summary>
    [Argument(Format = "--structured-mime-types {value}")] public IReadOnlyList<string> StructuredMimeTypes => Get<List<string>>(() => StructuredMimeTypes);
    /// <summary>Controls the accessibility level for generated client types.<br/><br/>This can be useful to reduce the scope of visibility in library scenarios where you do not want to expose the generated types to consuming projects.</summary>
    [Argument(Format = "--type-access-modifier {value}")] public KiotaAccessModifier TypeAccessModifier => Get<KiotaAccessModifier>(() => TypeAccessModifier);
    /// <summary>Include the 'AdditionalData' property for generated models. Defaults to <c>true</c>.</summary>
    [Argument(Format = "--additional-data")] public bool? AdditionalData => Get<bool?>(() => AdditionalData);
    /// <summary>Whether to exclude the code generated only for backward compatibility reasons or not. Defaults to false.<br/><br/>To maintain compatibility with applications that depends on generated clients, Kiota emits extra code marked as obsolete. New clients don't need this extra backward compatible code. The code marked as obsolete will be removed in the next major version of Kiota. Use this option to omit emitting the backward compatible code when generating a new client, or when the application using the existing client being refreshed doesn't depend on backward compatible code.</summary>
    [Argument(Format = "--exclude-backward-compatible")] public bool? ExcludeBackwardsCompatible => Get<bool?>(() => ExcludeBackwardsCompatible);
    /// <summary>The location of the OpenAPI description in JSON or YAML format to use to generate the SDK. Accepts a URL or a local path.</summary>
    [Argument(Format = "--openapi {value}")] public string OpenApiDescription => Get<string>(() => OpenApiDescription);
    /// <summary>The target language for the generated code files or for the information.</summary>
    [Argument(Format = "--language {value}")] public KiotaLanguage TargetLanguage => Get<KiotaLanguage>(() => TargetLanguage);
    /// <summary>The log level to use when logging events to the main output. Defaults to <c>warning</c>.</summary>
    [Argument(Format = "--log-level {value}")] public KiotaLogLevel LogLevel => Get<KiotaLogLevel>(() => LogLevel);
    /// <summary>Select a specific version of the API description. No default value.</summary>
    [Argument(Format = "--version {value}")] public string Version => Get<string>(() => Version);
    /// <summary>Clears the currently cached file for the command. Defaults to false.<br/><br/>Cached files are stored under '%TEMP%/kiota/cache' and valid for one (1) hour after the initial download. Kiota caches API descriptions during generation and static index files during search.</summary>
    [Argument(Format = "--clear-cache")] public bool? ClearCache => Get<bool?>(() => ClearCache);
    /// <summary>Delete the output directory before generating the client. Defaults to false.</summary>
    [Argument(Format = "--clean-output")] public bool? CleanOutput => Get<bool?>(() => CleanOutput);
    /// <summary>The output directory or file path for the generated code files. Defaults to <c>./output</c> for generate and <c>./output/result.json</c> for download.</summary>
    [Argument(Format = "--output {value}")] public string OutputPath => Get<string>(() => OutputPath);
    /// <summary>Select a specific version of the API description. No default value.</summary>
    [Argument(Format = "--disable-ssl-validation")] public bool? DisableSslValidation => Get<bool?>(() => DisableSslValidation);
    /// <summary>A glob pattern to include paths from generation. Accepts multiple values. If this parameter is absent, everything is included.<br/><br/>You can also filter specific HTTP methods by appending <c>#METHOD</c> to the pattern, replacing <c>METHOD</c> with the HTTP method to filter. For example, <c>**/users/**#GET</c>.</summary>
    [Argument(Format = "--include-path {value}")] public IReadOnlyList<string> IncludePaths => Get<List<string>>(() => IncludePaths);
    /// <summary>A glob pattern to exclude paths from generation. Accepts multiple values. If this parameter is absent, everything is included.<br/><br/>You can also filter specific HTTP methods by appending <c>#METHOD</c> to the pattern, replacing <c>METHOD</c> with the HTTP method to filter. For example, <c>**/users/**#GET</c>.</summary>
    [Argument(Format = "--exclude-path {value}")] public IReadOnlyList<string> ExcludePaths => Get<List<string>>(() => ExcludePaths);
}
#endregion
#region KiotaUpdateSettings
/// <inheritdoc cref="KiotaTasks.KiotaUpdate(Nuke.Common.Tools.Kiota.KiotaUpdateSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Command(Type = typeof(KiotaTasks), Command = nameof(KiotaTasks.KiotaUpdate), Arguments = "update")]
public partial class KiotaUpdateSettings : ToolOptions
{
    /// <summary>The log level to use when logging events to the main output. Defaults to <c>warning</c>.</summary>
    [Argument(Format = "--log-level {value}")] public KiotaLogLevel LogLevel => Get<KiotaLogLevel>(() => LogLevel);
    /// <summary>Clears the currently cached file for the command. Defaults to false.<br/><br/>Cached files are stored under '%TEMP%/kiota/cache' and valid for one (1) hour after the initial download. Kiota caches API descriptions during generation and static index files during search.</summary>
    [Argument(Format = "--clear-cache")] public bool? ClearCache => Get<bool?>(() => ClearCache);
    /// <summary>Delete the output directory before generating the client. Defaults to false.</summary>
    [Argument(Format = "--clean-output")] public bool? CleanOutput => Get<bool?>(() => CleanOutput);
    /// <summary>The output directory or file path for the generated code files. Defaults to <c>./output</c> for generate and <c>./output/result.json</c> for download.</summary>
    [Argument(Format = "--output {value}")] public string OutputPath => Get<string>(() => OutputPath);
}
#endregion
#region KiotaInfoSettings
/// <inheritdoc cref="KiotaTasks.KiotaInfo(Nuke.Common.Tools.Kiota.KiotaInfoSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Command(Type = typeof(KiotaTasks), Command = nameof(KiotaTasks.KiotaInfo), Arguments = "info")]
public partial class KiotaInfoSettings : ToolOptions
{
    /// <summary>Renders the output in a machine parsable format.</summary>
    [Argument(Format = "--json")] public bool? Json => Get<bool?>(() => Json);
    /// <summary>The type of dependencies to display when used in combination with the language option. Does not impact the json output. Default empty.</summary>
    [Argument(Format = "--dependency-type {value}")] public IReadOnlyList<KiotaDependencyType> DependencyTypes => Get<List<KiotaDependencyType>>(() => DependencyTypes);
    /// <summary>The location of the OpenAPI description in JSON or YAML format to use to generate the SDK. Accepts a URL or a local path.</summary>
    [Argument(Format = "--openapi {value}")] public string OpenApiDescription => Get<string>(() => OpenApiDescription);
    /// <summary>The target language for the generated code files or for the information.</summary>
    [Argument(Format = "--language {value}")] public KiotaLanguage TargetLanguage => Get<KiotaLanguage>(() => TargetLanguage);
    /// <summary>The log level to use when logging events to the main output. Defaults to <c>warning</c>.</summary>
    [Argument(Format = "--log-level {value}")] public KiotaLogLevel LogLevel => Get<KiotaLogLevel>(() => LogLevel);
    /// <summary>Select a specific version of the API description. No default value.</summary>
    [Argument(Format = "--version {value}")] public string Version => Get<string>(() => Version);
    /// <summary>The search key to use to fetch the Open API description. This parameter can be used in combination with the version option. Shouldn't be used in combination with the <c>--openapi</c> option. Default empty.</summary>
    [Argument(Format = "--search-key {value}")] public string SearchKey => Get<string>(() => SearchKey);
    /// <summary>Clears the currently cached file for the command. Defaults to false.<br/><br/>Cached files are stored under '%TEMP%/kiota/cache' and valid for one (1) hour after the initial download. Kiota caches API descriptions during generation and static index files during search.</summary>
    [Argument(Format = "--clear-cache")] public bool? ClearCache => Get<bool?>(() => ClearCache);
}
#endregion
#region KiotaLoginSettings
/// <inheritdoc cref="KiotaTasks.KiotaLogin(Nuke.Common.Tools.Kiota.KiotaLoginSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Command(Type = typeof(KiotaTasks), Command = nameof(KiotaTasks.KiotaLogin), Arguments = "login github")]
public partial class KiotaLoginSettings : ToolOptions
{
    /// <summary></summary>
    [Argument(Format = "{value}", Position = 1)] public KiotaLoginMethod LoginMethod => Get<KiotaLoginMethod>(() => LoginMethod);
    /// <summary>You can use either a classic personal access token (PAT) or a granular PAT. A classic PAT needs the repo permission and to be granted access to the target organizations. A granular PAT needs a read-only permission for the contents scope under the repository category and they need to be consented for all private repositories or selected private repositories.</summary>
    [Argument(Format = "--pat {value}")] public string AccessToken => Get<string>(() => AccessToken);
    /// <summary>The log level to use when logging events to the main output. Defaults to <c>warning</c>.</summary>
    [Argument(Format = "--log-level {value}")] public KiotaLogLevel LogLevel => Get<KiotaLogLevel>(() => LogLevel);
}
#endregion
#region KiotaLogoutSettings
/// <inheritdoc cref="KiotaTasks.KiotaLogout(Nuke.Common.Tools.Kiota.KiotaLogoutSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Command(Type = typeof(KiotaTasks), Command = nameof(KiotaTasks.KiotaLogout), Arguments = "logout github")]
public partial class KiotaLogoutSettings : ToolOptions
{
    /// <summary>The log level to use when logging events to the main output. Defaults to <c>warning</c>.</summary>
    [Argument(Format = "--log-level {value}")] public KiotaLogLevel LogLevel => Get<KiotaLogLevel>(() => LogLevel);
}
#endregion
#region KiotaSearchSettingsExtensions
/// <inheritdoc cref="KiotaTasks.KiotaSearch(Nuke.Common.Tools.Kiota.KiotaSearchSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class KiotaSearchSettingsExtensions
{
    #region SearchTerm
    /// <inheritdoc cref="KiotaSearchSettings.SearchTerm"/>
    [Pure] [Builder(Type = typeof(KiotaSearchSettings), Property = nameof(KiotaSearchSettings.SearchTerm))]
    public static T SetSearchTerm<T>(this T o, string v) where T : KiotaSearchSettings => o.Modify(b => b.Set(() => o.SearchTerm, v));
    /// <inheritdoc cref="KiotaSearchSettings.SearchTerm"/>
    [Pure] [Builder(Type = typeof(KiotaSearchSettings), Property = nameof(KiotaSearchSettings.SearchTerm))]
    public static T ResetSearchTerm<T>(this T o) where T : KiotaSearchSettings => o.Modify(b => b.Remove(() => o.SearchTerm));
    #endregion
    #region LogLevel
    /// <inheritdoc cref="KiotaSearchSettings.LogLevel"/>
    [Pure] [Builder(Type = typeof(KiotaSearchSettings), Property = nameof(KiotaSearchSettings.LogLevel))]
    public static T SetLogLevel<T>(this T o, KiotaLogLevel v) where T : KiotaSearchSettings => o.Modify(b => b.Set(() => o.LogLevel, v));
    /// <inheritdoc cref="KiotaSearchSettings.LogLevel"/>
    [Pure] [Builder(Type = typeof(KiotaSearchSettings), Property = nameof(KiotaSearchSettings.LogLevel))]
    public static T ResetLogLevel<T>(this T o) where T : KiotaSearchSettings => o.Modify(b => b.Remove(() => o.LogLevel));
    #endregion
    #region Version
    /// <inheritdoc cref="KiotaSearchSettings.Version"/>
    [Pure] [Builder(Type = typeof(KiotaSearchSettings), Property = nameof(KiotaSearchSettings.Version))]
    public static T SetVersion<T>(this T o, string v) where T : KiotaSearchSettings => o.Modify(b => b.Set(() => o.Version, v));
    /// <inheritdoc cref="KiotaSearchSettings.Version"/>
    [Pure] [Builder(Type = typeof(KiotaSearchSettings), Property = nameof(KiotaSearchSettings.Version))]
    public static T ResetVersion<T>(this T o) where T : KiotaSearchSettings => o.Modify(b => b.Remove(() => o.Version));
    #endregion
    #region ClearCache
    /// <inheritdoc cref="KiotaSearchSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaSearchSettings), Property = nameof(KiotaSearchSettings.ClearCache))]
    public static T SetClearCache<T>(this T o, bool? v) where T : KiotaSearchSettings => o.Modify(b => b.Set(() => o.ClearCache, v));
    /// <inheritdoc cref="KiotaSearchSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaSearchSettings), Property = nameof(KiotaSearchSettings.ClearCache))]
    public static T ResetClearCache<T>(this T o) where T : KiotaSearchSettings => o.Modify(b => b.Remove(() => o.ClearCache));
    /// <inheritdoc cref="KiotaSearchSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaSearchSettings), Property = nameof(KiotaSearchSettings.ClearCache))]
    public static T EnableClearCache<T>(this T o) where T : KiotaSearchSettings => o.Modify(b => b.Set(() => o.ClearCache, true));
    /// <inheritdoc cref="KiotaSearchSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaSearchSettings), Property = nameof(KiotaSearchSettings.ClearCache))]
    public static T DisableClearCache<T>(this T o) where T : KiotaSearchSettings => o.Modify(b => b.Set(() => o.ClearCache, false));
    /// <inheritdoc cref="KiotaSearchSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaSearchSettings), Property = nameof(KiotaSearchSettings.ClearCache))]
    public static T ToggleClearCache<T>(this T o) where T : KiotaSearchSettings => o.Modify(b => b.Set(() => o.ClearCache, !o.ClearCache));
    #endregion
}
#endregion
#region KiotaDownloadSettingsExtensions
/// <inheritdoc cref="KiotaTasks.KiotaDownload(Nuke.Common.Tools.Kiota.KiotaDownloadSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class KiotaDownloadSettingsExtensions
{
    #region SearchTerm
    /// <inheritdoc cref="KiotaDownloadSettings.SearchTerm"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.SearchTerm))]
    public static T SetSearchTerm<T>(this T o, string v) where T : KiotaDownloadSettings => o.Modify(b => b.Set(() => o.SearchTerm, v));
    /// <inheritdoc cref="KiotaDownloadSettings.SearchTerm"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.SearchTerm))]
    public static T ResetSearchTerm<T>(this T o) where T : KiotaDownloadSettings => o.Modify(b => b.Remove(() => o.SearchTerm));
    #endregion
    #region LogLevel
    /// <inheritdoc cref="KiotaDownloadSettings.LogLevel"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.LogLevel))]
    public static T SetLogLevel<T>(this T o, KiotaLogLevel v) where T : KiotaDownloadSettings => o.Modify(b => b.Set(() => o.LogLevel, v));
    /// <inheritdoc cref="KiotaDownloadSettings.LogLevel"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.LogLevel))]
    public static T ResetLogLevel<T>(this T o) where T : KiotaDownloadSettings => o.Modify(b => b.Remove(() => o.LogLevel));
    #endregion
    #region Version
    /// <inheritdoc cref="KiotaDownloadSettings.Version"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.Version))]
    public static T SetVersion<T>(this T o, string v) where T : KiotaDownloadSettings => o.Modify(b => b.Set(() => o.Version, v));
    /// <inheritdoc cref="KiotaDownloadSettings.Version"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.Version))]
    public static T ResetVersion<T>(this T o) where T : KiotaDownloadSettings => o.Modify(b => b.Remove(() => o.Version));
    #endregion
    #region ClearCache
    /// <inheritdoc cref="KiotaDownloadSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.ClearCache))]
    public static T SetClearCache<T>(this T o, bool? v) where T : KiotaDownloadSettings => o.Modify(b => b.Set(() => o.ClearCache, v));
    /// <inheritdoc cref="KiotaDownloadSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.ClearCache))]
    public static T ResetClearCache<T>(this T o) where T : KiotaDownloadSettings => o.Modify(b => b.Remove(() => o.ClearCache));
    /// <inheritdoc cref="KiotaDownloadSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.ClearCache))]
    public static T EnableClearCache<T>(this T o) where T : KiotaDownloadSettings => o.Modify(b => b.Set(() => o.ClearCache, true));
    /// <inheritdoc cref="KiotaDownloadSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.ClearCache))]
    public static T DisableClearCache<T>(this T o) where T : KiotaDownloadSettings => o.Modify(b => b.Set(() => o.ClearCache, false));
    /// <inheritdoc cref="KiotaDownloadSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.ClearCache))]
    public static T ToggleClearCache<T>(this T o) where T : KiotaDownloadSettings => o.Modify(b => b.Set(() => o.ClearCache, !o.ClearCache));
    #endregion
    #region CleanOutput
    /// <inheritdoc cref="KiotaDownloadSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.CleanOutput))]
    public static T SetCleanOutput<T>(this T o, bool? v) where T : KiotaDownloadSettings => o.Modify(b => b.Set(() => o.CleanOutput, v));
    /// <inheritdoc cref="KiotaDownloadSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.CleanOutput))]
    public static T ResetCleanOutput<T>(this T o) where T : KiotaDownloadSettings => o.Modify(b => b.Remove(() => o.CleanOutput));
    /// <inheritdoc cref="KiotaDownloadSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.CleanOutput))]
    public static T EnableCleanOutput<T>(this T o) where T : KiotaDownloadSettings => o.Modify(b => b.Set(() => o.CleanOutput, true));
    /// <inheritdoc cref="KiotaDownloadSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.CleanOutput))]
    public static T DisableCleanOutput<T>(this T o) where T : KiotaDownloadSettings => o.Modify(b => b.Set(() => o.CleanOutput, false));
    /// <inheritdoc cref="KiotaDownloadSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.CleanOutput))]
    public static T ToggleCleanOutput<T>(this T o) where T : KiotaDownloadSettings => o.Modify(b => b.Set(() => o.CleanOutput, !o.CleanOutput));
    #endregion
    #region OutputPath
    /// <inheritdoc cref="KiotaDownloadSettings.OutputPath"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.OutputPath))]
    public static T SetOutputPath<T>(this T o, string v) where T : KiotaDownloadSettings => o.Modify(b => b.Set(() => o.OutputPath, v));
    /// <inheritdoc cref="KiotaDownloadSettings.OutputPath"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.OutputPath))]
    public static T ResetOutputPath<T>(this T o) where T : KiotaDownloadSettings => o.Modify(b => b.Remove(() => o.OutputPath));
    #endregion
    #region DisableSslValidation
    /// <inheritdoc cref="KiotaDownloadSettings.DisableSslValidation"/>
    [Pure] [Builder(Type = typeof(KiotaDownloadSettings), Property = nameof(KiotaDownloadSettings.DisableSslValidation))]
    public static T DisableSslValidation<T>(this T o) where T : KiotaDownloadSettings => o.Modify(b => b.Set(() => o.DisableSslValidation, true));
    #endregion
}
#endregion
#region KiotaShowSettingsExtensions
/// <inheritdoc cref="KiotaTasks.KiotaShow(Nuke.Common.Tools.Kiota.KiotaShowSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class KiotaShowSettingsExtensions
{
    #region MaxDepth
    /// <inheritdoc cref="KiotaShowSettings.MaxDepth"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.MaxDepth))]
    public static T SetMaxDepth<T>(this T o, int? v) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.MaxDepth, v));
    /// <inheritdoc cref="KiotaShowSettings.MaxDepth"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.MaxDepth))]
    public static T ResetMaxDepth<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.Remove(() => o.MaxDepth));
    #endregion
    #region OpenApiDescription
    /// <inheritdoc cref="KiotaShowSettings.OpenApiDescription"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.OpenApiDescription))]
    public static T SetOpenApiDescription<T>(this T o, string v) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.OpenApiDescription, v));
    /// <inheritdoc cref="KiotaShowSettings.OpenApiDescription"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.OpenApiDescription))]
    public static T ResetOpenApiDescription<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.Remove(() => o.OpenApiDescription));
    #endregion
    #region LogLevel
    /// <inheritdoc cref="KiotaShowSettings.LogLevel"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.LogLevel))]
    public static T SetLogLevel<T>(this T o, KiotaLogLevel v) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.LogLevel, v));
    /// <inheritdoc cref="KiotaShowSettings.LogLevel"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.LogLevel))]
    public static T ResetLogLevel<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.Remove(() => o.LogLevel));
    #endregion
    #region Version
    /// <inheritdoc cref="KiotaShowSettings.Version"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.Version))]
    public static T SetVersion<T>(this T o, string v) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.Version, v));
    /// <inheritdoc cref="KiotaShowSettings.Version"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.Version))]
    public static T ResetVersion<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.Remove(() => o.Version));
    #endregion
    #region SearchKey
    /// <inheritdoc cref="KiotaShowSettings.SearchKey"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.SearchKey))]
    public static T SetSearchKey<T>(this T o, string v) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.SearchKey, v));
    /// <inheritdoc cref="KiotaShowSettings.SearchKey"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.SearchKey))]
    public static T ResetSearchKey<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.Remove(() => o.SearchKey));
    #endregion
    #region ClearCache
    /// <inheritdoc cref="KiotaShowSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.ClearCache))]
    public static T SetClearCache<T>(this T o, bool? v) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.ClearCache, v));
    /// <inheritdoc cref="KiotaShowSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.ClearCache))]
    public static T ResetClearCache<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.Remove(() => o.ClearCache));
    /// <inheritdoc cref="KiotaShowSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.ClearCache))]
    public static T EnableClearCache<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.ClearCache, true));
    /// <inheritdoc cref="KiotaShowSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.ClearCache))]
    public static T DisableClearCache<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.ClearCache, false));
    /// <inheritdoc cref="KiotaShowSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.ClearCache))]
    public static T ToggleClearCache<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.ClearCache, !o.ClearCache));
    #endregion
    #region CleanOutput
    /// <inheritdoc cref="KiotaShowSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.CleanOutput))]
    public static T SetCleanOutput<T>(this T o, bool? v) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.CleanOutput, v));
    /// <inheritdoc cref="KiotaShowSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.CleanOutput))]
    public static T ResetCleanOutput<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.Remove(() => o.CleanOutput));
    /// <inheritdoc cref="KiotaShowSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.CleanOutput))]
    public static T EnableCleanOutput<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.CleanOutput, true));
    /// <inheritdoc cref="KiotaShowSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.CleanOutput))]
    public static T DisableCleanOutput<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.CleanOutput, false));
    /// <inheritdoc cref="KiotaShowSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.CleanOutput))]
    public static T ToggleCleanOutput<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.CleanOutput, !o.CleanOutput));
    #endregion
    #region OutputPath
    /// <inheritdoc cref="KiotaShowSettings.OutputPath"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.OutputPath))]
    public static T SetOutputPath<T>(this T o, string v) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.OutputPath, v));
    /// <inheritdoc cref="KiotaShowSettings.OutputPath"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.OutputPath))]
    public static T ResetOutputPath<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.Remove(() => o.OutputPath));
    #endregion
    #region DisableSslValidation
    /// <inheritdoc cref="KiotaShowSettings.DisableSslValidation"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.DisableSslValidation))]
    public static T DisableSslValidation<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.DisableSslValidation, true));
    #endregion
    #region IncludePaths
    /// <inheritdoc cref="KiotaShowSettings.IncludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.IncludePaths))]
    public static T SetIncludePaths<T>(this T o, params string[] v) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.IncludePaths, v));
    /// <inheritdoc cref="KiotaShowSettings.IncludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.IncludePaths))]
    public static T SetIncludePaths<T>(this T o, IEnumerable<string> v) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.IncludePaths, v));
    /// <inheritdoc cref="KiotaShowSettings.IncludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.IncludePaths))]
    public static T AddIncludePaths<T>(this T o, params string[] v) where T : KiotaShowSettings => o.Modify(b => b.AddCollection(() => o.IncludePaths, v));
    /// <inheritdoc cref="KiotaShowSettings.IncludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.IncludePaths))]
    public static T AddIncludePaths<T>(this T o, IEnumerable<string> v) where T : KiotaShowSettings => o.Modify(b => b.AddCollection(() => o.IncludePaths, v));
    /// <inheritdoc cref="KiotaShowSettings.IncludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.IncludePaths))]
    public static T RemoveIncludePaths<T>(this T o, params string[] v) where T : KiotaShowSettings => o.Modify(b => b.RemoveCollection(() => o.IncludePaths, v));
    /// <inheritdoc cref="KiotaShowSettings.IncludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.IncludePaths))]
    public static T RemoveIncludePaths<T>(this T o, IEnumerable<string> v) where T : KiotaShowSettings => o.Modify(b => b.RemoveCollection(() => o.IncludePaths, v));
    /// <inheritdoc cref="KiotaShowSettings.IncludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.IncludePaths))]
    public static T ClearIncludePaths<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.ClearCollection(() => o.IncludePaths));
    #endregion
    #region ExcludePaths
    /// <inheritdoc cref="KiotaShowSettings.ExcludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.ExcludePaths))]
    public static T SetExcludePaths<T>(this T o, params string[] v) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.ExcludePaths, v));
    /// <inheritdoc cref="KiotaShowSettings.ExcludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.ExcludePaths))]
    public static T SetExcludePaths<T>(this T o, IEnumerable<string> v) where T : KiotaShowSettings => o.Modify(b => b.Set(() => o.ExcludePaths, v));
    /// <inheritdoc cref="KiotaShowSettings.ExcludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.ExcludePaths))]
    public static T AddExcludePaths<T>(this T o, params string[] v) where T : KiotaShowSettings => o.Modify(b => b.AddCollection(() => o.ExcludePaths, v));
    /// <inheritdoc cref="KiotaShowSettings.ExcludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.ExcludePaths))]
    public static T AddExcludePaths<T>(this T o, IEnumerable<string> v) where T : KiotaShowSettings => o.Modify(b => b.AddCollection(() => o.ExcludePaths, v));
    /// <inheritdoc cref="KiotaShowSettings.ExcludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.ExcludePaths))]
    public static T RemoveExcludePaths<T>(this T o, params string[] v) where T : KiotaShowSettings => o.Modify(b => b.RemoveCollection(() => o.ExcludePaths, v));
    /// <inheritdoc cref="KiotaShowSettings.ExcludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.ExcludePaths))]
    public static T RemoveExcludePaths<T>(this T o, IEnumerable<string> v) where T : KiotaShowSettings => o.Modify(b => b.RemoveCollection(() => o.ExcludePaths, v));
    /// <inheritdoc cref="KiotaShowSettings.ExcludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaShowSettings), Property = nameof(KiotaShowSettings.ExcludePaths))]
    public static T ClearExcludePaths<T>(this T o) where T : KiotaShowSettings => o.Modify(b => b.ClearCollection(() => o.ExcludePaths));
    #endregion
}
#endregion
#region KiotaGenerateSettingsExtensions
/// <inheritdoc cref="KiotaTasks.KiotaGenerate(Nuke.Common.Tools.Kiota.KiotaGenerateSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class KiotaGenerateSettingsExtensions
{
    #region ClassName
    /// <inheritdoc cref="KiotaGenerateSettings.ClassName"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ClassName))]
    public static T SetClassName<T>(this T o, string v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.ClassName, v));
    /// <inheritdoc cref="KiotaGenerateSettings.ClassName"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ClassName))]
    public static T ResetClassName<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Remove(() => o.ClassName));
    #endregion
    #region NamespaceName
    /// <inheritdoc cref="KiotaGenerateSettings.NamespaceName"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.NamespaceName))]
    public static T SetNamespaceName<T>(this T o, string v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.NamespaceName, v));
    /// <inheritdoc cref="KiotaGenerateSettings.NamespaceName"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.NamespaceName))]
    public static T ResetNamespaceName<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Remove(() => o.NamespaceName));
    #endregion
    #region BackingStore
    /// <inheritdoc cref="KiotaGenerateSettings.BackingStore"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.BackingStore))]
    public static T SetBackingStore<T>(this T o, bool? v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.BackingStore, v));
    /// <inheritdoc cref="KiotaGenerateSettings.BackingStore"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.BackingStore))]
    public static T ResetBackingStore<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Remove(() => o.BackingStore));
    /// <inheritdoc cref="KiotaGenerateSettings.BackingStore"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.BackingStore))]
    public static T EnableBackingStore<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.BackingStore, true));
    /// <inheritdoc cref="KiotaGenerateSettings.BackingStore"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.BackingStore))]
    public static T DisableBackingStore<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.BackingStore, false));
    /// <inheritdoc cref="KiotaGenerateSettings.BackingStore"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.BackingStore))]
    public static T ToggleBackingStore<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.BackingStore, !o.BackingStore));
    #endregion
    #region DeserializerClasses
    /// <inheritdoc cref="KiotaGenerateSettings.DeserializerClasses"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.DeserializerClasses))]
    public static T SetDeserializerClasses<T>(this T o, params string[] v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.DeserializerClasses, v));
    /// <inheritdoc cref="KiotaGenerateSettings.DeserializerClasses"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.DeserializerClasses))]
    public static T SetDeserializerClasses<T>(this T o, IEnumerable<string> v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.DeserializerClasses, v));
    /// <inheritdoc cref="KiotaGenerateSettings.DeserializerClasses"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.DeserializerClasses))]
    public static T AddDeserializerClasses<T>(this T o, params string[] v) where T : KiotaGenerateSettings => o.Modify(b => b.AddCollection(() => o.DeserializerClasses, v));
    /// <inheritdoc cref="KiotaGenerateSettings.DeserializerClasses"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.DeserializerClasses))]
    public static T AddDeserializerClasses<T>(this T o, IEnumerable<string> v) where T : KiotaGenerateSettings => o.Modify(b => b.AddCollection(() => o.DeserializerClasses, v));
    /// <inheritdoc cref="KiotaGenerateSettings.DeserializerClasses"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.DeserializerClasses))]
    public static T RemoveDeserializerClasses<T>(this T o, params string[] v) where T : KiotaGenerateSettings => o.Modify(b => b.RemoveCollection(() => o.DeserializerClasses, v));
    /// <inheritdoc cref="KiotaGenerateSettings.DeserializerClasses"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.DeserializerClasses))]
    public static T RemoveDeserializerClasses<T>(this T o, IEnumerable<string> v) where T : KiotaGenerateSettings => o.Modify(b => b.RemoveCollection(() => o.DeserializerClasses, v));
    /// <inheritdoc cref="KiotaGenerateSettings.DeserializerClasses"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.DeserializerClasses))]
    public static T ClearDeserializerClasses<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.ClearCollection(() => o.DeserializerClasses));
    #endregion
    #region SerializerClasses
    /// <inheritdoc cref="KiotaGenerateSettings.SerializerClasses"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.SerializerClasses))]
    public static T SetSerializerClasses<T>(this T o, params string[] v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.SerializerClasses, v));
    /// <inheritdoc cref="KiotaGenerateSettings.SerializerClasses"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.SerializerClasses))]
    public static T SetSerializerClasses<T>(this T o, IEnumerable<string> v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.SerializerClasses, v));
    /// <inheritdoc cref="KiotaGenerateSettings.SerializerClasses"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.SerializerClasses))]
    public static T AddSerializerClasses<T>(this T o, params string[] v) where T : KiotaGenerateSettings => o.Modify(b => b.AddCollection(() => o.SerializerClasses, v));
    /// <inheritdoc cref="KiotaGenerateSettings.SerializerClasses"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.SerializerClasses))]
    public static T AddSerializerClasses<T>(this T o, IEnumerable<string> v) where T : KiotaGenerateSettings => o.Modify(b => b.AddCollection(() => o.SerializerClasses, v));
    /// <inheritdoc cref="KiotaGenerateSettings.SerializerClasses"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.SerializerClasses))]
    public static T RemoveSerializerClasses<T>(this T o, params string[] v) where T : KiotaGenerateSettings => o.Modify(b => b.RemoveCollection(() => o.SerializerClasses, v));
    /// <inheritdoc cref="KiotaGenerateSettings.SerializerClasses"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.SerializerClasses))]
    public static T RemoveSerializerClasses<T>(this T o, IEnumerable<string> v) where T : KiotaGenerateSettings => o.Modify(b => b.RemoveCollection(() => o.SerializerClasses, v));
    /// <inheritdoc cref="KiotaGenerateSettings.SerializerClasses"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.SerializerClasses))]
    public static T ClearSerializerClasses<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.ClearCollection(() => o.SerializerClasses));
    #endregion
    #region StructuredMimeTypes
    /// <inheritdoc cref="KiotaGenerateSettings.StructuredMimeTypes"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.StructuredMimeTypes))]
    public static T SetStructuredMimeTypes<T>(this T o, params string[] v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.StructuredMimeTypes, v));
    /// <inheritdoc cref="KiotaGenerateSettings.StructuredMimeTypes"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.StructuredMimeTypes))]
    public static T SetStructuredMimeTypes<T>(this T o, IEnumerable<string> v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.StructuredMimeTypes, v));
    /// <inheritdoc cref="KiotaGenerateSettings.StructuredMimeTypes"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.StructuredMimeTypes))]
    public static T AddStructuredMimeTypes<T>(this T o, params string[] v) where T : KiotaGenerateSettings => o.Modify(b => b.AddCollection(() => o.StructuredMimeTypes, v));
    /// <inheritdoc cref="KiotaGenerateSettings.StructuredMimeTypes"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.StructuredMimeTypes))]
    public static T AddStructuredMimeTypes<T>(this T o, IEnumerable<string> v) where T : KiotaGenerateSettings => o.Modify(b => b.AddCollection(() => o.StructuredMimeTypes, v));
    /// <inheritdoc cref="KiotaGenerateSettings.StructuredMimeTypes"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.StructuredMimeTypes))]
    public static T RemoveStructuredMimeTypes<T>(this T o, params string[] v) where T : KiotaGenerateSettings => o.Modify(b => b.RemoveCollection(() => o.StructuredMimeTypes, v));
    /// <inheritdoc cref="KiotaGenerateSettings.StructuredMimeTypes"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.StructuredMimeTypes))]
    public static T RemoveStructuredMimeTypes<T>(this T o, IEnumerable<string> v) where T : KiotaGenerateSettings => o.Modify(b => b.RemoveCollection(() => o.StructuredMimeTypes, v));
    /// <inheritdoc cref="KiotaGenerateSettings.StructuredMimeTypes"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.StructuredMimeTypes))]
    public static T ClearStructuredMimeTypes<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.ClearCollection(() => o.StructuredMimeTypes));
    #endregion
    #region TypeAccessModifier
    /// <inheritdoc cref="KiotaGenerateSettings.TypeAccessModifier"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.TypeAccessModifier))]
    public static T SetTypeAccessModifier<T>(this T o, KiotaAccessModifier v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.TypeAccessModifier, v));
    /// <inheritdoc cref="KiotaGenerateSettings.TypeAccessModifier"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.TypeAccessModifier))]
    public static T ResetTypeAccessModifier<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Remove(() => o.TypeAccessModifier));
    #endregion
    #region AdditionalData
    /// <inheritdoc cref="KiotaGenerateSettings.AdditionalData"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.AdditionalData))]
    public static T SetAdditionalData<T>(this T o, bool? v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.AdditionalData, v));
    /// <inheritdoc cref="KiotaGenerateSettings.AdditionalData"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.AdditionalData))]
    public static T ResetAdditionalData<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Remove(() => o.AdditionalData));
    /// <inheritdoc cref="KiotaGenerateSettings.AdditionalData"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.AdditionalData))]
    public static T EnableAdditionalData<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.AdditionalData, true));
    /// <inheritdoc cref="KiotaGenerateSettings.AdditionalData"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.AdditionalData))]
    public static T DisableAdditionalData<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.AdditionalData, false));
    /// <inheritdoc cref="KiotaGenerateSettings.AdditionalData"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.AdditionalData))]
    public static T ToggleAdditionalData<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.AdditionalData, !o.AdditionalData));
    #endregion
    #region ExcludeBackwardsCompatible
    /// <inheritdoc cref="KiotaGenerateSettings.ExcludeBackwardsCompatible"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ExcludeBackwardsCompatible))]
    public static T SetExcludeBackwardsCompatible<T>(this T o, bool? v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.ExcludeBackwardsCompatible, v));
    /// <inheritdoc cref="KiotaGenerateSettings.ExcludeBackwardsCompatible"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ExcludeBackwardsCompatible))]
    public static T ResetExcludeBackwardsCompatible<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Remove(() => o.ExcludeBackwardsCompatible));
    /// <inheritdoc cref="KiotaGenerateSettings.ExcludeBackwardsCompatible"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ExcludeBackwardsCompatible))]
    public static T EnableExcludeBackwardsCompatible<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.ExcludeBackwardsCompatible, true));
    /// <inheritdoc cref="KiotaGenerateSettings.ExcludeBackwardsCompatible"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ExcludeBackwardsCompatible))]
    public static T DisableExcludeBackwardsCompatible<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.ExcludeBackwardsCompatible, false));
    /// <inheritdoc cref="KiotaGenerateSettings.ExcludeBackwardsCompatible"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ExcludeBackwardsCompatible))]
    public static T ToggleExcludeBackwardsCompatible<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.ExcludeBackwardsCompatible, !o.ExcludeBackwardsCompatible));
    #endregion
    #region OpenApiDescription
    /// <inheritdoc cref="KiotaGenerateSettings.OpenApiDescription"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.OpenApiDescription))]
    public static T SetOpenApiDescription<T>(this T o, string v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.OpenApiDescription, v));
    /// <inheritdoc cref="KiotaGenerateSettings.OpenApiDescription"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.OpenApiDescription))]
    public static T ResetOpenApiDescription<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Remove(() => o.OpenApiDescription));
    #endregion
    #region TargetLanguage
    /// <inheritdoc cref="KiotaGenerateSettings.TargetLanguage"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.TargetLanguage))]
    public static T SetTargetLanguage<T>(this T o, KiotaLanguage v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.TargetLanguage, v));
    /// <inheritdoc cref="KiotaGenerateSettings.TargetLanguage"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.TargetLanguage))]
    public static T ResetTargetLanguage<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Remove(() => o.TargetLanguage));
    #endregion
    #region LogLevel
    /// <inheritdoc cref="KiotaGenerateSettings.LogLevel"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.LogLevel))]
    public static T SetLogLevel<T>(this T o, KiotaLogLevel v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.LogLevel, v));
    /// <inheritdoc cref="KiotaGenerateSettings.LogLevel"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.LogLevel))]
    public static T ResetLogLevel<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Remove(() => o.LogLevel));
    #endregion
    #region Version
    /// <inheritdoc cref="KiotaGenerateSettings.Version"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.Version))]
    public static T SetVersion<T>(this T o, string v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.Version, v));
    /// <inheritdoc cref="KiotaGenerateSettings.Version"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.Version))]
    public static T ResetVersion<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Remove(() => o.Version));
    #endregion
    #region ClearCache
    /// <inheritdoc cref="KiotaGenerateSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ClearCache))]
    public static T SetClearCache<T>(this T o, bool? v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.ClearCache, v));
    /// <inheritdoc cref="KiotaGenerateSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ClearCache))]
    public static T ResetClearCache<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Remove(() => o.ClearCache));
    /// <inheritdoc cref="KiotaGenerateSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ClearCache))]
    public static T EnableClearCache<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.ClearCache, true));
    /// <inheritdoc cref="KiotaGenerateSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ClearCache))]
    public static T DisableClearCache<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.ClearCache, false));
    /// <inheritdoc cref="KiotaGenerateSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ClearCache))]
    public static T ToggleClearCache<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.ClearCache, !o.ClearCache));
    #endregion
    #region CleanOutput
    /// <inheritdoc cref="KiotaGenerateSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.CleanOutput))]
    public static T SetCleanOutput<T>(this T o, bool? v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.CleanOutput, v));
    /// <inheritdoc cref="KiotaGenerateSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.CleanOutput))]
    public static T ResetCleanOutput<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Remove(() => o.CleanOutput));
    /// <inheritdoc cref="KiotaGenerateSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.CleanOutput))]
    public static T EnableCleanOutput<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.CleanOutput, true));
    /// <inheritdoc cref="KiotaGenerateSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.CleanOutput))]
    public static T DisableCleanOutput<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.CleanOutput, false));
    /// <inheritdoc cref="KiotaGenerateSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.CleanOutput))]
    public static T ToggleCleanOutput<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.CleanOutput, !o.CleanOutput));
    #endregion
    #region OutputPath
    /// <inheritdoc cref="KiotaGenerateSettings.OutputPath"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.OutputPath))]
    public static T SetOutputPath<T>(this T o, string v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.OutputPath, v));
    /// <inheritdoc cref="KiotaGenerateSettings.OutputPath"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.OutputPath))]
    public static T ResetOutputPath<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Remove(() => o.OutputPath));
    #endregion
    #region DisableSslValidation
    /// <inheritdoc cref="KiotaGenerateSettings.DisableSslValidation"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.DisableSslValidation))]
    public static T DisableSslValidation<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.DisableSslValidation, true));
    #endregion
    #region IncludePaths
    /// <inheritdoc cref="KiotaGenerateSettings.IncludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.IncludePaths))]
    public static T SetIncludePaths<T>(this T o, params string[] v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.IncludePaths, v));
    /// <inheritdoc cref="KiotaGenerateSettings.IncludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.IncludePaths))]
    public static T SetIncludePaths<T>(this T o, IEnumerable<string> v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.IncludePaths, v));
    /// <inheritdoc cref="KiotaGenerateSettings.IncludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.IncludePaths))]
    public static T AddIncludePaths<T>(this T o, params string[] v) where T : KiotaGenerateSettings => o.Modify(b => b.AddCollection(() => o.IncludePaths, v));
    /// <inheritdoc cref="KiotaGenerateSettings.IncludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.IncludePaths))]
    public static T AddIncludePaths<T>(this T o, IEnumerable<string> v) where T : KiotaGenerateSettings => o.Modify(b => b.AddCollection(() => o.IncludePaths, v));
    /// <inheritdoc cref="KiotaGenerateSettings.IncludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.IncludePaths))]
    public static T RemoveIncludePaths<T>(this T o, params string[] v) where T : KiotaGenerateSettings => o.Modify(b => b.RemoveCollection(() => o.IncludePaths, v));
    /// <inheritdoc cref="KiotaGenerateSettings.IncludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.IncludePaths))]
    public static T RemoveIncludePaths<T>(this T o, IEnumerable<string> v) where T : KiotaGenerateSettings => o.Modify(b => b.RemoveCollection(() => o.IncludePaths, v));
    /// <inheritdoc cref="KiotaGenerateSettings.IncludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.IncludePaths))]
    public static T ClearIncludePaths<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.ClearCollection(() => o.IncludePaths));
    #endregion
    #region ExcludePaths
    /// <inheritdoc cref="KiotaGenerateSettings.ExcludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ExcludePaths))]
    public static T SetExcludePaths<T>(this T o, params string[] v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.ExcludePaths, v));
    /// <inheritdoc cref="KiotaGenerateSettings.ExcludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ExcludePaths))]
    public static T SetExcludePaths<T>(this T o, IEnumerable<string> v) where T : KiotaGenerateSettings => o.Modify(b => b.Set(() => o.ExcludePaths, v));
    /// <inheritdoc cref="KiotaGenerateSettings.ExcludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ExcludePaths))]
    public static T AddExcludePaths<T>(this T o, params string[] v) where T : KiotaGenerateSettings => o.Modify(b => b.AddCollection(() => o.ExcludePaths, v));
    /// <inheritdoc cref="KiotaGenerateSettings.ExcludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ExcludePaths))]
    public static T AddExcludePaths<T>(this T o, IEnumerable<string> v) where T : KiotaGenerateSettings => o.Modify(b => b.AddCollection(() => o.ExcludePaths, v));
    /// <inheritdoc cref="KiotaGenerateSettings.ExcludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ExcludePaths))]
    public static T RemoveExcludePaths<T>(this T o, params string[] v) where T : KiotaGenerateSettings => o.Modify(b => b.RemoveCollection(() => o.ExcludePaths, v));
    /// <inheritdoc cref="KiotaGenerateSettings.ExcludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ExcludePaths))]
    public static T RemoveExcludePaths<T>(this T o, IEnumerable<string> v) where T : KiotaGenerateSettings => o.Modify(b => b.RemoveCollection(() => o.ExcludePaths, v));
    /// <inheritdoc cref="KiotaGenerateSettings.ExcludePaths"/>
    [Pure] [Builder(Type = typeof(KiotaGenerateSettings), Property = nameof(KiotaGenerateSettings.ExcludePaths))]
    public static T ClearExcludePaths<T>(this T o) where T : KiotaGenerateSettings => o.Modify(b => b.ClearCollection(() => o.ExcludePaths));
    #endregion
}
#endregion
#region KiotaUpdateSettingsExtensions
/// <inheritdoc cref="KiotaTasks.KiotaUpdate(Nuke.Common.Tools.Kiota.KiotaUpdateSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class KiotaUpdateSettingsExtensions
{
    #region LogLevel
    /// <inheritdoc cref="KiotaUpdateSettings.LogLevel"/>
    [Pure] [Builder(Type = typeof(KiotaUpdateSettings), Property = nameof(KiotaUpdateSettings.LogLevel))]
    public static T SetLogLevel<T>(this T o, KiotaLogLevel v) where T : KiotaUpdateSettings => o.Modify(b => b.Set(() => o.LogLevel, v));
    /// <inheritdoc cref="KiotaUpdateSettings.LogLevel"/>
    [Pure] [Builder(Type = typeof(KiotaUpdateSettings), Property = nameof(KiotaUpdateSettings.LogLevel))]
    public static T ResetLogLevel<T>(this T o) where T : KiotaUpdateSettings => o.Modify(b => b.Remove(() => o.LogLevel));
    #endregion
    #region ClearCache
    /// <inheritdoc cref="KiotaUpdateSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaUpdateSettings), Property = nameof(KiotaUpdateSettings.ClearCache))]
    public static T SetClearCache<T>(this T o, bool? v) where T : KiotaUpdateSettings => o.Modify(b => b.Set(() => o.ClearCache, v));
    /// <inheritdoc cref="KiotaUpdateSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaUpdateSettings), Property = nameof(KiotaUpdateSettings.ClearCache))]
    public static T ResetClearCache<T>(this T o) where T : KiotaUpdateSettings => o.Modify(b => b.Remove(() => o.ClearCache));
    /// <inheritdoc cref="KiotaUpdateSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaUpdateSettings), Property = nameof(KiotaUpdateSettings.ClearCache))]
    public static T EnableClearCache<T>(this T o) where T : KiotaUpdateSettings => o.Modify(b => b.Set(() => o.ClearCache, true));
    /// <inheritdoc cref="KiotaUpdateSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaUpdateSettings), Property = nameof(KiotaUpdateSettings.ClearCache))]
    public static T DisableClearCache<T>(this T o) where T : KiotaUpdateSettings => o.Modify(b => b.Set(() => o.ClearCache, false));
    /// <inheritdoc cref="KiotaUpdateSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaUpdateSettings), Property = nameof(KiotaUpdateSettings.ClearCache))]
    public static T ToggleClearCache<T>(this T o) where T : KiotaUpdateSettings => o.Modify(b => b.Set(() => o.ClearCache, !o.ClearCache));
    #endregion
    #region CleanOutput
    /// <inheritdoc cref="KiotaUpdateSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaUpdateSettings), Property = nameof(KiotaUpdateSettings.CleanOutput))]
    public static T SetCleanOutput<T>(this T o, bool? v) where T : KiotaUpdateSettings => o.Modify(b => b.Set(() => o.CleanOutput, v));
    /// <inheritdoc cref="KiotaUpdateSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaUpdateSettings), Property = nameof(KiotaUpdateSettings.CleanOutput))]
    public static T ResetCleanOutput<T>(this T o) where T : KiotaUpdateSettings => o.Modify(b => b.Remove(() => o.CleanOutput));
    /// <inheritdoc cref="KiotaUpdateSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaUpdateSettings), Property = nameof(KiotaUpdateSettings.CleanOutput))]
    public static T EnableCleanOutput<T>(this T o) where T : KiotaUpdateSettings => o.Modify(b => b.Set(() => o.CleanOutput, true));
    /// <inheritdoc cref="KiotaUpdateSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaUpdateSettings), Property = nameof(KiotaUpdateSettings.CleanOutput))]
    public static T DisableCleanOutput<T>(this T o) where T : KiotaUpdateSettings => o.Modify(b => b.Set(() => o.CleanOutput, false));
    /// <inheritdoc cref="KiotaUpdateSettings.CleanOutput"/>
    [Pure] [Builder(Type = typeof(KiotaUpdateSettings), Property = nameof(KiotaUpdateSettings.CleanOutput))]
    public static T ToggleCleanOutput<T>(this T o) where T : KiotaUpdateSettings => o.Modify(b => b.Set(() => o.CleanOutput, !o.CleanOutput));
    #endregion
    #region OutputPath
    /// <inheritdoc cref="KiotaUpdateSettings.OutputPath"/>
    [Pure] [Builder(Type = typeof(KiotaUpdateSettings), Property = nameof(KiotaUpdateSettings.OutputPath))]
    public static T SetOutputPath<T>(this T o, string v) where T : KiotaUpdateSettings => o.Modify(b => b.Set(() => o.OutputPath, v));
    /// <inheritdoc cref="KiotaUpdateSettings.OutputPath"/>
    [Pure] [Builder(Type = typeof(KiotaUpdateSettings), Property = nameof(KiotaUpdateSettings.OutputPath))]
    public static T ResetOutputPath<T>(this T o) where T : KiotaUpdateSettings => o.Modify(b => b.Remove(() => o.OutputPath));
    #endregion
}
#endregion
#region KiotaInfoSettingsExtensions
/// <inheritdoc cref="KiotaTasks.KiotaInfo(Nuke.Common.Tools.Kiota.KiotaInfoSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class KiotaInfoSettingsExtensions
{
    #region Json
    /// <inheritdoc cref="KiotaInfoSettings.Json"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.Json))]
    public static T SetJson<T>(this T o, bool? v) where T : KiotaInfoSettings => o.Modify(b => b.Set(() => o.Json, v));
    /// <inheritdoc cref="KiotaInfoSettings.Json"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.Json))]
    public static T ResetJson<T>(this T o) where T : KiotaInfoSettings => o.Modify(b => b.Remove(() => o.Json));
    /// <inheritdoc cref="KiotaInfoSettings.Json"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.Json))]
    public static T EnableJson<T>(this T o) where T : KiotaInfoSettings => o.Modify(b => b.Set(() => o.Json, true));
    /// <inheritdoc cref="KiotaInfoSettings.Json"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.Json))]
    public static T DisableJson<T>(this T o) where T : KiotaInfoSettings => o.Modify(b => b.Set(() => o.Json, false));
    /// <inheritdoc cref="KiotaInfoSettings.Json"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.Json))]
    public static T ToggleJson<T>(this T o) where T : KiotaInfoSettings => o.Modify(b => b.Set(() => o.Json, !o.Json));
    #endregion
    #region DependencyTypes
    /// <inheritdoc cref="KiotaInfoSettings.DependencyTypes"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.DependencyTypes))]
    public static T SetDependencyTypes<T>(this T o, params KiotaDependencyType[] v) where T : KiotaInfoSettings => o.Modify(b => b.Set(() => o.DependencyTypes, v));
    /// <inheritdoc cref="KiotaInfoSettings.DependencyTypes"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.DependencyTypes))]
    public static T SetDependencyTypes<T>(this T o, IEnumerable<KiotaDependencyType> v) where T : KiotaInfoSettings => o.Modify(b => b.Set(() => o.DependencyTypes, v));
    /// <inheritdoc cref="KiotaInfoSettings.DependencyTypes"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.DependencyTypes))]
    public static T AddDependencyTypes<T>(this T o, params KiotaDependencyType[] v) where T : KiotaInfoSettings => o.Modify(b => b.AddCollection(() => o.DependencyTypes, v));
    /// <inheritdoc cref="KiotaInfoSettings.DependencyTypes"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.DependencyTypes))]
    public static T AddDependencyTypes<T>(this T o, IEnumerable<KiotaDependencyType> v) where T : KiotaInfoSettings => o.Modify(b => b.AddCollection(() => o.DependencyTypes, v));
    /// <inheritdoc cref="KiotaInfoSettings.DependencyTypes"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.DependencyTypes))]
    public static T RemoveDependencyTypes<T>(this T o, params KiotaDependencyType[] v) where T : KiotaInfoSettings => o.Modify(b => b.RemoveCollection(() => o.DependencyTypes, v));
    /// <inheritdoc cref="KiotaInfoSettings.DependencyTypes"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.DependencyTypes))]
    public static T RemoveDependencyTypes<T>(this T o, IEnumerable<KiotaDependencyType> v) where T : KiotaInfoSettings => o.Modify(b => b.RemoveCollection(() => o.DependencyTypes, v));
    /// <inheritdoc cref="KiotaInfoSettings.DependencyTypes"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.DependencyTypes))]
    public static T ClearDependencyTypes<T>(this T o) where T : KiotaInfoSettings => o.Modify(b => b.ClearCollection(() => o.DependencyTypes));
    #endregion
    #region OpenApiDescription
    /// <inheritdoc cref="KiotaInfoSettings.OpenApiDescription"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.OpenApiDescription))]
    public static T SetOpenApiDescription<T>(this T o, string v) where T : KiotaInfoSettings => o.Modify(b => b.Set(() => o.OpenApiDescription, v));
    /// <inheritdoc cref="KiotaInfoSettings.OpenApiDescription"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.OpenApiDescription))]
    public static T ResetOpenApiDescription<T>(this T o) where T : KiotaInfoSettings => o.Modify(b => b.Remove(() => o.OpenApiDescription));
    #endregion
    #region TargetLanguage
    /// <inheritdoc cref="KiotaInfoSettings.TargetLanguage"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.TargetLanguage))]
    public static T SetTargetLanguage<T>(this T o, KiotaLanguage v) where T : KiotaInfoSettings => o.Modify(b => b.Set(() => o.TargetLanguage, v));
    /// <inheritdoc cref="KiotaInfoSettings.TargetLanguage"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.TargetLanguage))]
    public static T ResetTargetLanguage<T>(this T o) where T : KiotaInfoSettings => o.Modify(b => b.Remove(() => o.TargetLanguage));
    #endregion
    #region LogLevel
    /// <inheritdoc cref="KiotaInfoSettings.LogLevel"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.LogLevel))]
    public static T SetLogLevel<T>(this T o, KiotaLogLevel v) where T : KiotaInfoSettings => o.Modify(b => b.Set(() => o.LogLevel, v));
    /// <inheritdoc cref="KiotaInfoSettings.LogLevel"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.LogLevel))]
    public static T ResetLogLevel<T>(this T o) where T : KiotaInfoSettings => o.Modify(b => b.Remove(() => o.LogLevel));
    #endregion
    #region Version
    /// <inheritdoc cref="KiotaInfoSettings.Version"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.Version))]
    public static T SetVersion<T>(this T o, string v) where T : KiotaInfoSettings => o.Modify(b => b.Set(() => o.Version, v));
    /// <inheritdoc cref="KiotaInfoSettings.Version"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.Version))]
    public static T ResetVersion<T>(this T o) where T : KiotaInfoSettings => o.Modify(b => b.Remove(() => o.Version));
    #endregion
    #region SearchKey
    /// <inheritdoc cref="KiotaInfoSettings.SearchKey"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.SearchKey))]
    public static T SetSearchKey<T>(this T o, string v) where T : KiotaInfoSettings => o.Modify(b => b.Set(() => o.SearchKey, v));
    /// <inheritdoc cref="KiotaInfoSettings.SearchKey"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.SearchKey))]
    public static T ResetSearchKey<T>(this T o) where T : KiotaInfoSettings => o.Modify(b => b.Remove(() => o.SearchKey));
    #endregion
    #region ClearCache
    /// <inheritdoc cref="KiotaInfoSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.ClearCache))]
    public static T SetClearCache<T>(this T o, bool? v) where T : KiotaInfoSettings => o.Modify(b => b.Set(() => o.ClearCache, v));
    /// <inheritdoc cref="KiotaInfoSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.ClearCache))]
    public static T ResetClearCache<T>(this T o) where T : KiotaInfoSettings => o.Modify(b => b.Remove(() => o.ClearCache));
    /// <inheritdoc cref="KiotaInfoSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.ClearCache))]
    public static T EnableClearCache<T>(this T o) where T : KiotaInfoSettings => o.Modify(b => b.Set(() => o.ClearCache, true));
    /// <inheritdoc cref="KiotaInfoSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.ClearCache))]
    public static T DisableClearCache<T>(this T o) where T : KiotaInfoSettings => o.Modify(b => b.Set(() => o.ClearCache, false));
    /// <inheritdoc cref="KiotaInfoSettings.ClearCache"/>
    [Pure] [Builder(Type = typeof(KiotaInfoSettings), Property = nameof(KiotaInfoSettings.ClearCache))]
    public static T ToggleClearCache<T>(this T o) where T : KiotaInfoSettings => o.Modify(b => b.Set(() => o.ClearCache, !o.ClearCache));
    #endregion
}
#endregion
#region KiotaLoginSettingsExtensions
/// <inheritdoc cref="KiotaTasks.KiotaLogin(Nuke.Common.Tools.Kiota.KiotaLoginSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class KiotaLoginSettingsExtensions
{
    #region LoginMethod
    /// <inheritdoc cref="KiotaLoginSettings.LoginMethod"/>
    [Pure] [Builder(Type = typeof(KiotaLoginSettings), Property = nameof(KiotaLoginSettings.LoginMethod))]
    public static T SetLoginMethod<T>(this T o, KiotaLoginMethod v) where T : KiotaLoginSettings => o.Modify(b => b.Set(() => o.LoginMethod, v));
    /// <inheritdoc cref="KiotaLoginSettings.LoginMethod"/>
    [Pure] [Builder(Type = typeof(KiotaLoginSettings), Property = nameof(KiotaLoginSettings.LoginMethod))]
    public static T ResetLoginMethod<T>(this T o) where T : KiotaLoginSettings => o.Modify(b => b.Remove(() => o.LoginMethod));
    #endregion
    #region AccessToken
    /// <inheritdoc cref="KiotaLoginSettings.AccessToken"/>
    [Pure] [Builder(Type = typeof(KiotaLoginSettings), Property = nameof(KiotaLoginSettings.AccessToken))]
    public static T SetAccessToken<T>(this T o, string v) where T : KiotaLoginSettings => o.Modify(b => b.Set(() => o.AccessToken, v));
    /// <inheritdoc cref="KiotaLoginSettings.AccessToken"/>
    [Pure] [Builder(Type = typeof(KiotaLoginSettings), Property = nameof(KiotaLoginSettings.AccessToken))]
    public static T ResetAccessToken<T>(this T o) where T : KiotaLoginSettings => o.Modify(b => b.Remove(() => o.AccessToken));
    #endregion
    #region LogLevel
    /// <inheritdoc cref="KiotaLoginSettings.LogLevel"/>
    [Pure] [Builder(Type = typeof(KiotaLoginSettings), Property = nameof(KiotaLoginSettings.LogLevel))]
    public static T SetLogLevel<T>(this T o, KiotaLogLevel v) where T : KiotaLoginSettings => o.Modify(b => b.Set(() => o.LogLevel, v));
    /// <inheritdoc cref="KiotaLoginSettings.LogLevel"/>
    [Pure] [Builder(Type = typeof(KiotaLoginSettings), Property = nameof(KiotaLoginSettings.LogLevel))]
    public static T ResetLogLevel<T>(this T o) where T : KiotaLoginSettings => o.Modify(b => b.Remove(() => o.LogLevel));
    #endregion
}
#endregion
#region KiotaLogoutSettingsExtensions
/// <inheritdoc cref="KiotaTasks.KiotaLogout(Nuke.Common.Tools.Kiota.KiotaLogoutSettings)"/>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class KiotaLogoutSettingsExtensions
{
    #region LogLevel
    /// <inheritdoc cref="KiotaLogoutSettings.LogLevel"/>
    [Pure] [Builder(Type = typeof(KiotaLogoutSettings), Property = nameof(KiotaLogoutSettings.LogLevel))]
    public static T SetLogLevel<T>(this T o, KiotaLogLevel v) where T : KiotaLogoutSettings => o.Modify(b => b.Set(() => o.LogLevel, v));
    /// <inheritdoc cref="KiotaLogoutSettings.LogLevel"/>
    [Pure] [Builder(Type = typeof(KiotaLogoutSettings), Property = nameof(KiotaLogoutSettings.LogLevel))]
    public static T ResetLogLevel<T>(this T o) where T : KiotaLogoutSettings => o.Modify(b => b.Remove(() => o.LogLevel));
    #endregion
}
#endregion
#region KiotaLogLevel
/// <summary>Used within <see cref="KiotaTasks"/>.</summary>
[PublicAPI]
[Serializable]
[ExcludeFromCodeCoverage]
[TypeConverter(typeof(TypeConverter<KiotaLogLevel>))]
public partial class KiotaLogLevel : Enumeration
{
    public static readonly KiotaLogLevel critical = (KiotaLogLevel) "critical";
    public static readonly KiotaLogLevel debug = (KiotaLogLevel) "debug";
    public static readonly KiotaLogLevel error = (KiotaLogLevel) "error";
    public static readonly KiotaLogLevel information = (KiotaLogLevel) "information";
    public static readonly KiotaLogLevel none = (KiotaLogLevel) "none";
    public static readonly KiotaLogLevel trace = (KiotaLogLevel) "trace";
    public static readonly KiotaLogLevel warning = (KiotaLogLevel) "warning";
    public static implicit operator KiotaLogLevel(string value)
    {
        return new KiotaLogLevel { Value = value };
    }
}
#endregion
#region KiotaLanguage
/// <summary>Used within <see cref="KiotaTasks"/>.</summary>
[PublicAPI]
[Serializable]
[ExcludeFromCodeCoverage]
[TypeConverter(typeof(TypeConverter<KiotaLanguage>))]
public partial class KiotaLanguage : Enumeration
{
    public static readonly KiotaLanguage csharp = (KiotaLanguage) "csharp";
    public static readonly KiotaLanguage go = (KiotaLanguage) "go";
    public static readonly KiotaLanguage java = (KiotaLanguage) "java";
    public static readonly KiotaLanguage php = (KiotaLanguage) "php";
    public static readonly KiotaLanguage python = (KiotaLanguage) "python";
    public static readonly KiotaLanguage ruby = (KiotaLanguage) "ruby";
    public static readonly KiotaLanguage shell = (KiotaLanguage) "shell";
    public static readonly KiotaLanguage swift = (KiotaLanguage) "swift";
    public static readonly KiotaLanguage typescript = (KiotaLanguage) "typescript";
    public static implicit operator KiotaLanguage(string value)
    {
        return new KiotaLanguage { Value = value };
    }
}
#endregion
#region KiotaAccessModifier
/// <summary>Used within <see cref="KiotaTasks"/>.</summary>
[PublicAPI]
[Serializable]
[ExcludeFromCodeCoverage]
[TypeConverter(typeof(TypeConverter<KiotaAccessModifier>))]
public partial class KiotaAccessModifier : Enumeration
{
    public static readonly KiotaAccessModifier Public = (KiotaAccessModifier) "Public";
    public static readonly KiotaAccessModifier Internal = (KiotaAccessModifier) "Internal";
    public static readonly KiotaAccessModifier Protected = (KiotaAccessModifier) "Protected";
    public static implicit operator KiotaAccessModifier(string value)
    {
        return new KiotaAccessModifier { Value = value };
    }
}
#endregion
#region KiotaDependencyType
/// <summary>Used within <see cref="KiotaTasks"/>.</summary>
[PublicAPI]
[Serializable]
[ExcludeFromCodeCoverage]
[TypeConverter(typeof(TypeConverter<KiotaDependencyType>))]
public partial class KiotaDependencyType : Enumeration
{
    public static readonly KiotaDependencyType Abstractions = (KiotaDependencyType) "Abstractions";
    public static readonly KiotaDependencyType Additional = (KiotaDependencyType) "Additional";
    public static readonly KiotaDependencyType Authentication = (KiotaDependencyType) "Authentication";
    public static readonly KiotaDependencyType Bundle = (KiotaDependencyType) "Bundle";
    public static readonly KiotaDependencyType HTTP = (KiotaDependencyType) "HTTP";
    public static readonly KiotaDependencyType Serialization = (KiotaDependencyType) "Serialization";
    public static implicit operator KiotaDependencyType(string value)
    {
        return new KiotaDependencyType { Value = value };
    }
}
#endregion
#region KiotaLoginMethod
/// <summary>Used within <see cref="KiotaTasks"/>.</summary>
[PublicAPI]
[Serializable]
[ExcludeFromCodeCoverage]
[TypeConverter(typeof(TypeConverter<KiotaLoginMethod>))]
public partial class KiotaLoginMethod : Enumeration
{
    public static readonly KiotaLoginMethod device = (KiotaLoginMethod) "device";
    public static readonly KiotaLoginMethod pat = (KiotaLoginMethod) "pat";
    public static implicit operator KiotaLoginMethod(string value)
    {
        return new KiotaLoginMethod { Value = value };
    }
}
#endregion
