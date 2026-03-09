// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using JetBrains.Annotations;
using Nuke.CodeGeneration.Model;
using Nuke.CodeGeneration.Writers;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

// ReSharper disable UnusedMethodReturnValue.Local

namespace Nuke.CodeGeneration.Generators;

public static class ToolGenerator
{
    public static void Run(Tool tool, StreamWriter streamWriter)
    {
        using var writer = new ToolWriter(tool, streamWriter);
        writer
            .WriteLine(s_projectLicenseHeader.Value)
            .WriteLine(string.Empty)
            .WriteLineIfTrue(tool.SourceFile != null, $"// Generated from {tool.SourceFile}")
            .WriteLine(string.Empty)
            .ForEach(GetNamespaceImports(tool), x => writer.WriteLine($"using {x};"))
            .WriteLine(string.Empty)
            .WriteLineIfTrue(tool.Namespace != null, $"namespace {tool.Namespace};")
            .WriteLine(string.Empty)
            .WriteAll();
    }

    internal static AbsolutePath GlobalNukeDirectory => EnvironmentInfo.SpecialFolder(SpecialFolders.UserProfile) / ".nuke";
    private static AbsolutePath RootDirectory => TryGetRootDirectoryFrom(EnvironmentInfo.WorkingDirectory).NotNull();

    private static AbsolutePath NukeDotSettings => RootDirectory / "Gruke.sln.DotSettings";

    [CanBeNull]
    internal static AbsolutePath TryGetRootDirectoryFrom(AbsolutePath startDirectory, bool includeLegacy = true)
    {
        var rootDirectory = new DirectoryInfo(startDirectory)
            .DescendantsAndSelf(x => x.Parent)
            .FirstOrDefault(x => x.GetDirectories(".nuke").Length != 0 ||
                                 includeLegacy && x.GetFiles(".nuke").Length != 0)
            ?.FullName;
        return rootDirectory != GlobalNukeDirectory.Parent 
            ? (AbsolutePath) rootDirectory 
            : null;
    }

    private static readonly Lazy<string> s_projectLicenseHeader = new(() =>
    {
        var doc = XDocument.Load(NukeDotSettings);
        var resourceDict = doc.Elements().First().Descendants();
        var fileHeader = resourceDict.First(x =>
            // what
            x.Attribute("{http://schemas.microsoft.com/winfx/2006/xaml}Key")
                ?.Value == "/Default/CodeStyle/FileHeader/FileHeaderText/@EntryValue");
        return fileHeader.Value.Replace("${CurrentDate.Year}", DateTime.Now.Year.ToString())
            .SplitLineBreaks()
            .Select(x => $"// {x}")
            .JoinNewLine();
    });

    private static ToolWriter WriteAll(this ToolWriter w)
    {
        return w
            .WriteAlias()
            .WriteDataClasses()
            .WriteEnumerations();
    }

    private static ToolWriter WriteAlias(this ToolWriter writer)
    {
        TaskGenerator.Run(writer.Tool, writer);
        return writer;
    }

    private static ToolWriter WriteDataClasses(this ToolWriter writer)
    {
        var dataClasses = writer.Tool.Tasks.Select(x => x.SettingsClass).Concat(writer.Tool.DataClasses).ToList();
        dataClasses.ForEach(x => DataClassGenerator.Run(x, writer));
        dataClasses.Where(x => x.ExtensionMethods).ForEach(x => DataClassExtensionGenerator.Run(x, writer));
        return writer;
    }

    private static ToolWriter WriteEnumerations(this ToolWriter writer)
    {
        writer.Tool.Enumerations.ForEach(x => EnumerationGenerator.Run(x, writer));
        return writer;
    }

    private static IEnumerable<string> GetNamespaceImports(Tool tool)
    {
        return new[]
               {
                   "JetBrains.Annotations",
                   "Newtonsoft.Json",
                   "Nuke.Common",
                   "Nuke.Common.Tooling",
                   "Nuke.Common.Tools",
                   "Nuke.Common.Utilities.Collections",
                   "System",
                   "System.Collections.Generic",
                   "System.Collections.ObjectModel",
                   "System.ComponentModel",
                   "System.Diagnostics.CodeAnalysis",
                   "System.IO",
                   "System.Linq",
                   "System.Text"
               }
            .Concat(tool.Imports ?? new List<string>())
            .OrderBy(x => x);
    }
}
