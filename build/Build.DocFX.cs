// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.Tools.DocFX;
using Nuke.Common.Tools.Git;
using Nuke.Common.Git;
using Nuke.Common.Utilities;
using Nuke.Components;
using Nuke.Utilities.Text.Json;
using Serilog;

public partial class Build
{
    AbsolutePath GeneratedDocsDirectory => DocsDirectory / "generated";
    AbsolutePath ApiMetaDirectory => DocsDirectory / "apimeta";
    AbsolutePath ApiDirectory => DocsDirectory / "api";

    AbsolutePath DocsDirectory => RootDirectory / "docs";

    AbsolutePath DocFxConfiguration => DocsDirectory / "docfx.json";
    AbsolutePath ChangelogOutput => DocsDirectory / "changelog.md";

    AbsolutePath ApiIndexMd => DocsDirectory / "api" / "index.md";
    AbsolutePath IndexMd => DocsDirectory / "index.md";

    Target DocFx => _ => _
        .Requires(() => From<IHazDebuggableGitVersion>().Versioning)
        .When(Host is Terminal, _ => _
            .Requires(() => GitTasks.GitHasCleanWorkingCopy()))
        .Executes(() =>
        {
            ApiMetaDirectory.CleanDirectory();
            GeneratedDocsDirectory.CleanDirectory();
            ApiDirectory.CreateOrCleanDirectory();

            ApiIndexMd.WriteAllText("# GRUKE API Documentation", eofLineBreak: true);
            IndexMd.WriteAllLines((RootDirectory / "README.md")
                .ReadAllLines()
                .Skip(1)
                .Prepend(string.Empty)
                .Prepend("# Home")
            );

            var original = DocFxConfiguration.ExistingFile()?.ReadAllLines()
                           ?? throw new InvalidOperationException($"Could not read DocFX config at '{DocFxConfiguration}'.");

            var changelogLines = File.ReadAllLines(From<IHazChangelog>().ChangelogFile)
                .Skip(5)
                .Prepend("# Changelog");

            ChangelogOutput.WriteAllLines(changelogLines, platformFamily: PlatformFamily.Linux);

            var hotswap = HotswapDocfxConfigContents(original);
            try
            {
                DocFXTasks.DocFX($"{DocFxConfiguration}");
            }
            finally
            {
                ChangelogOutput.DeleteFile();
                ApiDirectory.DeleteDirectory();
                IndexMd.DeleteFile();
                ApiMetaDirectory.DeleteDirectory();
                hotswap.Dispose();
                Log.Information("Generated DocFX documentation can be found at '{Path}'.", GeneratedDocsDirectory);
            }
        });

    private static string HtmlSpanWrapFooter(string currentFooter, string secondaryContent)
    {
        var sb = new StringBuilder("<span>");
        sb.Append(currentFooter);
        sb.Append("<br/>"); // line break
        sb.Append(secondaryContent);
        sb.Append("</span>");
        return sb.ToString();
    }

    private static string HtmlHyperlink(string visibleText, string url) => $"<a href=\"{url}\">{visibleText}</a>";

    private IDisposable HotswapDocfxConfigContents(string[] jsonLines) =>
        DelegateDisposable.CreateBracket(
            () =>
            {
                var modified = JObject.ReadFrom(new JsonTextReader(new StringReader(jsonLines.JoinNewLine())));
                var appTitle = modified.GetNested<string>("build.globalMetadata._appTitle");
                var appFooter = modified.GetNested<string>("build.globalMetadata._appFooter");
                modified.SetNested("build.globalMetadata._appTitle",
                    $"{appTitle} {From<IHazDebuggableGitVersion>().Versioning.MajorMinorPatch}");
                modified.SetNested("build.globalMetadata._appFooter", HtmlSpanWrapFooter(
                        appFooter,
                        $"{From<IHazDebuggableGitVersion>().Versioning.FullSemVer} @ " +
                        HtmlHyperlink(GitRepository.Commit![..7], GitRepository.GitHub.GetCommitUrl(GitRepository.Commit))
                    )
                );

                DocFxConfiguration.WriteJson(modified);
            },
            () =>
            {
                DocFxConfiguration.WriteAllLines(jsonLines);
            }
        );
}
