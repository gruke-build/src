// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;
using Serilog;
using static Nuke.Common.Constants;

namespace Nuke.Common;

public abstract partial class NukeBuild
{
    static NukeBuild()
    {
        RootDirectory = GetRootDirectory();
        TemporaryDirectory = GetTemporaryDirectory(RootDirectory).CreateDirectory();

        BuildAssemblyFile = GetBuildAssemblyFile();
        BuildAssemblyDirectory = BuildAssemblyFile?.Parent;

        BuildProjectFile = GetBuildProjectFile(BuildAssemblyDirectory);
        BuildProjectDirectory = BuildProjectFile?.Parent;

        Verbosity = ParameterService.GetParameter<Verbosity?>(() => Verbosity) ?? Verbosity.Normal;
        Host = ParameterService.GetParameter(() => Host) ?? Host.Default;
        LoadedLocalProfiles = ParameterService.GetParameter(() => LoadedLocalProfiles) ?? [];
    }

    /// <summary>
    /// Gets the full path to the root directory.
    /// </summary>
    [Parameter("Root directory during build execution.", Name = RootDirectoryParameterName)]
    public static AbsolutePath RootDirectory { get; }

    /// <summary>
    /// Gets the full path to the temporary directory <c>/.nuke/temp</c>.
    /// </summary>
    public static AbsolutePath TemporaryDirectory { get; }

    /// <summary>
    /// Gets the full path to the build assembly file.
    /// </summary>
    [CanBeNull]
    public static AbsolutePath BuildAssemblyFile { get; }

    /// <summary>
    /// Gets the full path to the build assembly directory.
    /// </summary>
    [CanBeNull]
    public static AbsolutePath BuildAssemblyDirectory { get; }

    /// <summary>
    /// Gets the full path to the build project directory, or <c>null</c>
    /// </summary>
    [CanBeNull]
    public static AbsolutePath BuildProjectDirectory { get; }

    /// <summary>
    /// Gets the full path to the build project file, or <c>null</c>
    /// </summary>
    [CanBeNull]
    public static AbsolutePath BuildProjectFile { get; }

    /// <summary>
    /// Gets the logging verbosity during build execution. Default is <see cref="Nuke.Common.Verbosity.Normal"/>.
    /// </summary>
    [Parameter("Logging verbosity during build execution. Default is 'Normal'.")]
    public static Verbosity Verbosity
    {
        get => (Verbosity) Logging.Level;
        set => Logging.Level = (LogLevel) value;
    }

    /// <summary>
    /// Gets the host for execution. Default is <em>automatic</em>.
    /// </summary>
    [Parameter("Host for execution. Default is 'automatic'.")]
    public static Host Host { get; set; }

    [Parameter("Defines the profiles to load.", Name = LoadedLocalProfilesParameterName)]
    public static string[] LoadedLocalProfiles { get; }

    public static bool IsLocalBuild => !IsServerBuild;
    public static bool IsServerBuild => Host is IBuildServer;

    private static AbsolutePath GetRootDirectory()
    {
        var parameterValue = ParameterService.GetParameter(() => RootDirectory);
        if (parameterValue != null)
            return parameterValue;

        if (ParameterService.GetParameter<bool>(() => RootDirectory))
            return EnvironmentInfo.WorkingDirectory;

        return TryGetRootDirectoryFrom(EnvironmentInfo.WorkingDirectory)
            .NotNull(new[]
                     {
                         $"Could not locate '{NukeDirectoryName}' directory/file while walking up from '{EnvironmentInfo.WorkingDirectory}'.",
                         "Either create a directory/file to mark the root directory, or add '--root [path]' to the invocation."
                     }.JoinNewLine());
    }

    [CanBeNull]
    private static AbsolutePath GetBuildAssemblyFile()
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly == null || entryAssembly.GetTypes().All(x => !x.IsSubclassOf(typeof(NukeBuild))))
        {
            var assemblyName = entryAssembly?.GetName().Name;
            Assert.True(assemblyName == null ||
                        assemblyName.StartsWith("ReSharperTestRunner") ||
                        assemblyName == "testhost",
                $"Assembly name was {assemblyName.SingleQuote()}");
            return null;
        }

        var assemblyLocation = entryAssembly.Location;
        var invokedLocation = Environment.GetCommandLineArgs().First();

        // .NET 11 upgrade: previous versions directly compared to the 2 above strings.
        // This code has worked as-is for little over 4 years, until the release of .NET 11 in November 2026.
        // It seems a change was made somewhere in the runtime to store the first program argument (aka its executable name) as an app host path, instead of a .dll.
        // Simply comparing the paths without their extensions should be fine enough to reasonably assume that the 
        // invoked binary is an app host for the assembly at the reported location.

        var assemblyLocationWithoutExtension = GetFilePathWithoutExtension(assemblyLocation).ToString();
        var invokedLocationWithoutExtension = GetFilePathWithoutExtension(invokedLocation).ToString();

        Console.WriteLine($"Assembly location: {assemblyLocationWithoutExtension}");
        Console.WriteLine($"Invoked location: {invokedLocationWithoutExtension}");

        Assert.True(assemblyLocation == string.Empty || assemblyLocationWithoutExtension == invokedLocationWithoutExtension);

        return assemblyLocation != string.Empty ? assemblyLocation : invokedLocation;

        static ReadOnlySpan<char> GetFilePathWithoutExtension(ReadOnlySpan<char> path)
        {
            var lastPeriod = path.LastIndexOf('.');
            return lastPeriod < 0 ?
                path : // No extension was found
                path[..lastPeriod];
        }
    }

    [CanBeNull]
    private static AbsolutePath GetBuildProjectFile([CanBeNull] AbsolutePath buildAssemblyDirectory)
    {
        if (buildAssemblyDirectory == null)
            return null;

        return new DirectoryInfo(buildAssemblyDirectory)
            .DescendantsAndSelf(x => x.Parent)
            .Select(x => x.GetFiles("*.csproj", SearchOption.TopDirectoryOnly)
                .SingleOrDefaultOrError($"Found multiple project files in '{x}'."))
            .FirstOrDefault(x => x != null)
            ?.FullName;
    }
}
