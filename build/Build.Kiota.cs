// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using Nuke.Common;
using Nuke.Common.Tools.Kiota;

public partial class Build
{
    [Parameter(description: "Clean Kiota output before running the generator.")] readonly bool CleanOutput;

    Target Kiota => _ => _
        .After(GenerateTools)
        .Executes(() =>
        {
            KiotaTasks.KiotaGenerate(s => s
                .SetOpenApiDescription("https://codeberg.org/swagger.v1.json")
                .SetTargetLanguage(KiotaLanguage.csharp)
                .SetLogLevel(KiotaLogLevel.error)
                .EnableExcludeBackwardsCompatible()
                .AddExcludePaths("/admin/**")
                .SetClassName("ForgejoApiClient")
                .SetNamespaceName("Nuke.Common.Components.Forgejo")
                .SetCleanOutput(CleanOutput)
                .SetOutputPath(Solution.Nuke_Components_Forgejo.Directory / "generated")
            );
        });
}
