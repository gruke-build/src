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
using Nuke.Utilities.Text.Json;
using Serilog;

public partial class Build
{
    AbsolutePath DocsDirectory => RootDirectory / "docs";

    AbsolutePath DocFxConfiguration => DocsDirectory / "docfx.json";
    AbsolutePath ChangelogOutput => DocsDirectory / "changelog.md";

    AbsolutePath ApiIndexMd => DocsDirectory / "api" / "index.md";
    AbsolutePath IndexMd => DocsDirectory / "index.md";

    Target DocFx => _ => _
        .Requires(() => From<IHazGitVersion>().Versioning)
        .Requires(() => GitTasks.GitHasCleanWorkingCopy())
        .Executes(() =>
        {
            (DocsDirectory / "apimeta").CleanDirectory();
            (DocsDirectory / "generated").CleanDirectory();
            (DocsDirectory / "api").CreateOrCleanDirectory();

            ApiIndexMd.WriteAllText("# GRUKE API Documentation", eofLineBreak: true);
            IndexMd.WriteAllLines((RootDirectory / "README.md")
                .ReadAllLines()
                .Prepend(string.Empty).Prepend("# Home"));

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
                (DocsDirectory / "api").DeleteDirectory();
                IndexMd.DeleteFile();
                (DocsDirectory / "apimeta").DeleteDirectory();
                hotswap.Dispose();
                Log.Information("Generated DocFX documentation can be found at '{Path}'.", DocsDirectory / "generated");
            }
        });

    private IDisposable HotswapDocfxConfigContents(string[] jsonLines) =>
        DelegateDisposable.CreateBracket(
            () =>
            {
                var modified = JObject.ReadFrom(new JsonTextReader(new StringReader(jsonLines.JoinNewLine())));
                var appName = modified.GetNested<string>("build.globalMetadata._appName");
                var appTitle = modified.GetNested<string>("build.globalMetadata._appTitle");
                modified.SetNested("build.globalMetadata._appName", $"{appName} {From<IHazGitVersion>().Versioning.MajorMinorPatch}");
                modified.SetNested("build.globalMetadata._appTitle", $"{appTitle} {From<IHazGitVersion>().Versioning.MajorMinorPatch}");

                DocFxConfiguration.WriteJson(modified);
            },
            () =>
            {
                DocFxConfiguration.WriteAllLines(jsonLines);
            }
        );
}
