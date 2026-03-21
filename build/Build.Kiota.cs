// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.Kiota;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;
using Nuke.Common.Utilities.Net;

public partial class Build
{
    [Parameter(description: "Clean Kiota output before running the generator.")] readonly bool CleanOutput;

    AbsolutePath OpenApiFile => RootDirectory / ".nuke" / "forgejo-openapi-spec.json";

    Target Kiota => _ => _
        .After(GenerateTools)
        .Executes(async () =>
        {
            await SetupOpenApiDescription();

            KiotaTasks.KiotaGenerate(s => s
                .SetOpenApiDescription(OpenApiFile)
                .SetTargetLanguage(KiotaLanguage.csharp)
                .SetLogLevel(KiotaLogLevel.error)
                .EnableExcludeBackwardsCompatible()
                .AddExcludePaths("/admin/**")
                .SetClassName("ForgejoApiClient")
                .SetNamespaceName("Nuke.Common.Components.Forgejo")
                .SetCleanOutput(CleanOutput)
                .SetOutputPath(Solution.Nuke_Components_Forgejo.Directory / "generated")
            );

            // generation from local file creates a broken https:///api/v1 base URL 
            var apiClientContent = (Solution.Nuke_Components_Forgejo.Directory / "generated" / "ForgejoApiClient.cs").ReadAllLines();
            apiClientContent.Replace("                RequestAdapter.BaseUrl = \"https:///api/v1\";",
                "                RequestAdapter.BaseUrl = \"https://codeberg.org/api/v1\";");
            (Solution.Nuke_Components_Forgejo.Directory / "generated" / "ForgejoApiClient.cs").WriteAllLines(apiClientContent);
        });

    /// <summary>
    /// https://github.com/microsoft/kiota/issues/6451
    /// </summary>
    private async Task SetupOpenApiDescription()
    {
        var spec = await (await HttpClientProxy.Shared
                .CreateRequest(HttpMethod.Get, "https://codeberg.org/swagger.v1.json")
                .GetResponseAsync()
            ).GetBodyAsJson();

        spec["paths"]?
            .Values()
            .ForEach(token =>
            {
                if (token["post"] is { } postSpec)
                {
                    // ReSharper disable once ConstantConditionalAccessQualifier
                    var consumes = postSpec["consumes"]?.Values<string>()?.ToArray();
                    if (consumes is not null && consumes.Contains("multipart/form-data"))
                    {
                        postSpec["consumes"] = new JArray(consumes
                            .Where(x => x != "multipart/form-data")
                            .ToArray<object>()
                        );
                    }
                }
            });

        OpenApiFile.WriteJson(spec);
    }
}
