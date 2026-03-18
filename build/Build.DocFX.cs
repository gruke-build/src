// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.DocFX;
using Nuke.Common.Tools.Git;
using Nuke.Components;

public partial class Build
{
    AbsolutePath DocFxConfiguration => RootDirectory / "docs" / "docfx.json";
    AbsolutePath ChangelogOutput => RootDirectory / "docs" / "changelog.md";

    Target DocFx => _ => _
        .Requires(() => GitTasks.GitHasCleanWorkingCopy())
        .Executes(() =>
        {
            AbsolutePath.Create(From<IHazChangelog>().ChangelogFile)
                .Copy(ChangelogOutput, ExistsPolicy.FileOverwrite, createDirectories: true);
            try
            {
                DocFXTasks.DocFX($"{DocFxConfiguration}");
            }
            finally
            {
                ChangelogOutput.DeleteFile();
            }
        });
}
