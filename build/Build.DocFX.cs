// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.DocFX;
using Nuke.Common.Tools.Git;
using Nuke.Common.Utilities;
using Nuke.Components;

public partial class Build
{
    AbsolutePath DocFxConfiguration => RootDirectory / "docs" / "docfx.json";
    AbsolutePath ChangelogOutput => RootDirectory / "docs" / "changelog.md";

    Target DocFx => _ => _
        .Requires(() => From<IHazGitVersion>().Versioning)
        .Requires(() => GitTasks.GitHasCleanWorkingCopy())
        .Executes(() =>
        {
            (DocFxConfiguration.Parent / "apimeta").CleanDirectory();
            (DocFxConfiguration.Parent / "generated").CleanDirectory();

            var original = DocFxConfiguration.ExistingFile()?.ReadAllLines()
                           ?? throw new InvalidOperationException($"Could not read DocFX config at '{DocFxConfiguration}'.");

            AbsolutePath.Create(From<IHazChangelog>().ChangelogFile)
                .Copy(ChangelogOutput, ExistsPolicy.FileOverwrite, createDirectories: true);

            var hotswap = HotswapDocfxConfigContents(original);
            try
            {
                DocFXTasks.DocFX($"{DocFxConfiguration}");
            }
            finally
            {
                ChangelogOutput.DeleteFile();
                hotswap.Dispose();
            }
        });

    private IDisposable HotswapDocfxConfigContents(string[] jsonLines)
    {
        return DelegateDisposable.CreateBracket(
            () =>
            {
                var modified = JObject.ReadFrom(new JsonTextReader(new StringReader(jsonLines.JoinNewLine())));
                var appName = GetNestedJsonValue<string>(modified, "build", "globalMetadata", "_appName");
                var appTitle = GetNestedJsonValue<string>(modified, "build", "globalMetadata", "_appTitle");
                SetNestedJsonValue(modified, ["build", "globalMetadata", "_appName"],
                    $"{appName} {From<IHazGitVersion>().Versioning.MajorMinorPatch}");
                SetNestedJsonValue(modified, ["build", "globalMetadata", "_appTitle"],
                    $"{appTitle} {From<IHazGitVersion>().Versioning.MajorMinorPatch}");

                DocFxConfiguration.WriteJson(modified);
            },
            () =>
            {
                DocFxConfiguration.WriteAllLines(jsonLines);
            }
        );
    }

    private static void SetNestedJsonValue(JToken jobj, string[] keyNames, JToken value)
    {
        var token = jobj[keyNames[0]].NotNull();

        foreach (var subKey in keyNames.Skip(1).SkipLast(1))
        {
            token = token.NotNull()[subKey];
        }

        token![keyNames.Last()] = value;
    }

    private static T GetNestedJsonValue<T>(JToken jobj, params string[] keyNames)
    {
        var token = jobj[keyNames[0]].NotNull();

        foreach (var subKey in keyNames.Skip(1))
        {
            token = token.NotNull()[subKey];
        }

        return token!.Value<T>();
    }
}
